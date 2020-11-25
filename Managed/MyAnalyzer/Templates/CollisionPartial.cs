using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
    partial class CollisionTemplate
    {
        public readonly INamedTypeSymbol TypeSymbol;
        public readonly TypeStruct[] Outers;
        public readonly TypeStruct[] Inners;
        public readonly TypeStruct[] Others;
        public readonly TypeStruct[] Tables;

        public readonly MethodStruct Ordinal;
        public readonly MethodStruct? Fma;

        public CollisionTemplate(INamedTypeSymbol typeSymbol, TypeStruct[] outers, TypeStruct[] inners, TypeStruct[] others, TypeStruct[] tables, MethodStruct ordinal, MethodStruct? fma)
        {
            TypeSymbol = typeSymbol;
            Outers = outers;
            Inners = inners;
            Others = others;
            Tables = tables;
            Ordinal = ordinal;
            Fma = fma;
        }

        public static CollisionTemplate? TryCreate(ISymbol collisionType, ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol loopParameter, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            if (!InterpretCollisionType(collisionType, typeSymbol, comparer, out var outers, out var inners, out var others, out var tables))
            {
                return default;
            }

            if (!CollectCollisionMethodAndCollisionCloseMethod(collisionMethod, collisionCloseMethod, loopParameter, typeSymbol, comparer, out var ordinal, out var fma))
            {
                return default;
            }

            return new(typeSymbol, outers, inners, others, tables, ordinal, fma);
        }

        private static bool CollectCollisionMethodAndCollisionCloseMethod(ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol loopParameter, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, out MethodStruct ordinal, out MethodStruct? fma)
        {
            ordinal = default;
            List<CloseMethodStruct> ordinalCloses = new();
            List<CloseMethodStruct> fmaCloses = new();
            List<(IMethodSymbol, AttributeData)> executeCandidates = new();

            foreach (var member in typeSymbol.GetMembers())
            {
                CollectCollisionCloseMethod(collisionMethod, collisionCloseMethod, comparer, member, executeCandidates, ordinalCloses, fmaCloses);
            }

            var isOrdinalInitialized = false;
            fma = default;
            ParameterStruct[] parameterOuters;
            ParameterStruct[] parameterInners;
            ParameterStruct[] parameterOthers;
            ParameterStruct[] parameterTables;
            foreach (var (methodSymbol, attributeData) in executeCandidates)
            {
                var array = attributeData.ConstructorArguments;
                if (array.Length < 1 || array[0].Value is not int kind || array.Length < 2 || array[1].Value is not int outerCount || outerCount == 0)
                {
                    continue;
                }

                var parameters = methodSymbol.Parameters;
                var innerCount = parameters.Length - outerCount;
                var otherCount = 0;

                if (array.Length >= 3)
                {
                    if (array[2].Value is not int x)
                    {
                        return false;
                    }

                    innerCount = x;
                    otherCount = parameters.Length - outerCount - innerCount;

                    if (array.Length >= 4)
                    {
                        if (array[3].Value is not int y)
                        {
                            return false;
                        }

                        otherCount = y;
                    }
                }

                var tableCount = parameters.Length - outerCount - innerCount - otherCount;

                var intrinsicsKind = (IntrinsicsKind)kind;
                switch (intrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                    case IntrinsicsKind.Fma:
                        break;
                    default:
                        return false;
                }

                parameterOuters = new ParameterStruct[outerCount];
                for (var i = 0; i < parameterOuters.Length; i++)
                {
                    var parameter = parameters[i];
                    if (!ParameterStruct.InterpretLoopParameter(loopParameter, comparer, parameter, out parameterOuters[i]))
                    {
                        return false;
                    }
                }

                parameterInners = new ParameterStruct[innerCount];
                for (var i = 0; i < parameterInners.Length; i++)
                {
                    var parameter = parameters[i + outerCount];
                    if (!ParameterStruct.InterpretLoopParameter(loopParameter, comparer, parameter, out parameterInners[i]))
                    {
                        return false;
                    }
                }

                parameterOthers = otherCount == 0 ? Array.Empty<ParameterStruct>() : new ParameterStruct[otherCount];
                for (var i = 0; i < parameterOthers.Length; i++)
                {
                    var parameter = parameters[i + outerCount + innerCount];
                    if (!ParameterStruct.InterpretLoopParameter(loopParameter, comparer, parameter, out parameterOthers[i]))
                    {
                        return false;
                    }
                }

                parameterTables = tableCount == 0 ? Array.Empty<ParameterStruct>() : new ParameterStruct[tableCount];
                for (var i = 0; i < parameterTables.Length; i++)
                {
                    var parameter = parameters[i + outerCount + innerCount + otherCount];
                    if (!ParameterStruct.InterpretLoopParameter(loopParameter, comparer, parameter, out parameterTables[i]))
                    {
                        return false;
                    }
                }

                switch (intrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                        isOrdinalInitialized = true;
                        ordinal = new MethodStruct(methodSymbol, parameterOuters, parameterInners, parameterOthers, parameterTables, ordinalCloses.ToArray());
                        break;
                    case IntrinsicsKind.Fma:
                        fma = new MethodStruct(methodSymbol, parameterOuters, parameterInners, parameterOthers, parameterTables, fmaCloses.ToArray());
                        break;
                }
            }

            return isOrdinalInitialized;
        }

        private static void CollectCollisionCloseMethod(ISymbol collisionMethod, ISymbol collisionCloseMethod, SymbolEqualityComparer comparer, ISymbol member, List<(IMethodSymbol, AttributeData)> executeCandidates, List<CloseMethodStruct> ordinalCloses, List<CloseMethodStruct> fmaCloses)
        {
            if (member is not IMethodSymbol { IsStatic: true } methodSymbol)
            {
                return;
            }

            var attributes = methodSymbol.GetAttributes();
            if (attributes.IsDefaultOrEmpty)
            {
                return;
            }

            if (methodSymbol.ReturnsVoid)
            {
                if (methodSymbol.Parameters.Any(x => x.RefKind != RefKind.Ref))
                {
                    return;
                }

                var attr = attributes.SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionMethod));
                if (attr is null)
                {
                    return;
                }

                executeCandidates.Add((methodSymbol, attr));
            }
            else
            {
                if (methodSymbol.Parameters.Any(x => !comparer.Equals(x.Type, methodSymbol.ReturnType)))
                {
                    return;
                }

                var attr = attributes.SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionCloseMethod));
                if (attr is null)
                {
                    return;
                }

                var arguments = attr.ConstructorArguments;
                if (arguments[0].Value is not int kind || arguments[1].Value is not int fieldKind || arguments[2].Value is not int index || arguments[3].Value is not string name)
                {
                    return;
                }

                var collisionIntrinsicsKind = (IntrinsicsKind)kind;
                var collisionFieldKind = (CollisionFieldKind)fieldKind;
                switch (collisionIntrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                        ordinalCloses.Add(new(methodSymbol, collisionIntrinsicsKind, collisionFieldKind, index, name));
                        break;
                    case IntrinsicsKind.Fma:
                        fmaCloses.Add(new(methodSymbol, collisionIntrinsicsKind, collisionFieldKind, index, name));
                        break;
                    default:
                        return;
                }
            }
        }

        private static bool InterpretCollisionType(ISymbol collisionType, ISymbol typeSymbol, SymbolEqualityComparer comparer, out TypeStruct[] outers, out TypeStruct[] inners, out TypeStruct[] others, out TypeStruct[] tables)
        {
            outers = Array.Empty<TypeStruct>();
            inners = Array.Empty<TypeStruct>();
            others = Array.Empty<TypeStruct>();
            tables = Array.Empty<TypeStruct>();
            var typeAttr = typeSymbol.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionType));
            if (typeAttr is null)
            {
                return false;
            }

            var arguments = typeAttr.ConstructorArguments;
            var length = arguments.Length;
            if (length < 4)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[0].Values, arguments[1].Values, out outers)
                || !TypeStruct.InterpretCollisionTypeLoopFields(arguments[2].Values, arguments[3].Values, out inners))
            {
                return false;
            }

            if (length == 4)
            {
                return true;
            }

            if (length < 6)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[4].Values, arguments[5].Values, out others))
            {
                return false;
            }

            if (length == 6)
            {
                return true;
            }

            if (length < 8)
            {
                return false;
            }

            return TypeStruct.InterpretCollisionTypeLoopFields(arguments[6].Values, arguments[7].Values, out tables);
        }

        public readonly struct MethodStruct
        {
            public readonly IMethodSymbol Symbol;
            public readonly ParameterStruct[] Outers;
            public readonly ParameterStruct[] Inners;
            public readonly ParameterStruct[] Others;
            public readonly ParameterStruct[] Tables;

            public readonly CloseMethodStruct[] Closers;

            public MethodStruct(IMethodSymbol symbol, ParameterStruct[] outers, ParameterStruct[] inners, ParameterStruct[] others, ParameterStruct[] tables, CloseMethodStruct[] closers)
            {
                Symbol = symbol;
                Outers = outers;
                Inners = inners;
                Others = others;
                Tables = tables;
                Closers = closers;
            }
        }

        public readonly struct CloseMethodStruct
        {
            public readonly IMethodSymbol Symbol;
            public readonly IntrinsicsKind Kind;
            public readonly CollisionFieldKind FieldKind;
            public readonly int Index;
            public readonly string Name;

            public CloseMethodStruct(IMethodSymbol symbol, IntrinsicsKind kind, CollisionFieldKind fieldKind, int index, string name)
            {
                Kind = kind;
                Symbol = symbol;
                Index = index;
                Name = name;
                FieldKind = fieldKind;
            }
        }
    }
}
