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

        public static CollisionTemplate? TryCreate(ISymbol collisionType, ISymbol intrinsicsKindMethod, ISymbol collisionCloseMethod, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            if (!InterpretCollisionType(collisionType, typeSymbol, comparer, out var outers, out var inners, out var others, out var tables))
            {
                return default;
            }

            if (!CollectCollisionMethodAndCollisionCloseMethod(intrinsicsKindMethod, collisionCloseMethod, typeSymbol, comparer, outers, inners, others, tables, out var ordinal, out var fma))
            {
                return default;
            }

            return new(typeSymbol, outers, inners, others, tables, ordinal, fma);
        }

        private static bool CollectCollisionMethodAndCollisionCloseMethod(ISymbol collisionMethod, ISymbol collisionCloseMethod, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, TypeStruct[] outers, TypeStruct[] inners, TypeStruct[] others, TypeStruct[] tables, out MethodStruct ordinal, out MethodStruct? fma)
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
            List<ParameterStruct> parameterOuters = new();
            List<ParameterStruct> parameterInners = new();
            List<ParameterStruct> parameterOthers = new();
            List<ParameterStruct> parameterTables = new();
            foreach (var (methodSymbol, attributeData) in executeCandidates)
            {
                var array = attributeData.ConstructorArguments;
                if (array.Length < 1 || array[0].Value is not int kind)
                {
                    continue;
                }

                var intrinsicsKind = (IntrinsicsKind)kind;
                switch (intrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                    case IntrinsicsKind.Fma:
                        break;
                    default:
                        return false;
                }
                
                parameterOuters.Clear();
                parameterInners.Clear();
                parameterOthers.Clear();
                parameterTables.Clear();
                var parameters = methodSymbol.Parameters;
                var parameterIndex = 0;
                for (var typeIndex = 0; typeIndex < outers.Length; ++typeIndex)
                {
                    var typeStruct = outers[typeIndex];
                    foreach (var member in typeStruct.Symbol.GetMembers())
                    {
                        if (member is not IFieldSymbol fieldSymbol || fieldSymbol.IsStatic)
                        {
                            continue;
                        }

                        parameterOuters.Add(new(parameters[parameterIndex++], typeIndex, member.Name));
                    }
                }

                for (var typeIndex = 0; typeIndex < inners.Length; ++typeIndex)
                {
                    var typeStruct = inners[typeIndex];
                    foreach (var member in typeStruct.Symbol.GetMembers())
                    {
                        if (member is not IFieldSymbol fieldSymbol || fieldSymbol.IsStatic)
                        {
                            continue;
                        }

                        parameterInners.Add(new(parameters[parameterIndex++], typeIndex, member.Name));
                    }
                }

                for (var typeIndex = 0; typeIndex < others.Length; ++typeIndex)
                {
                    parameterOthers.Add(new(parameters[parameterIndex++], typeIndex, string.Empty));
                }

                for (var typeIndex = 0; typeIndex < tables.Length; ++typeIndex, parameterIndex += 2)
                {
                    parameterTables.Add(new(parameters[parameterIndex], typeIndex, string.Empty));
                }

                switch (intrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                        isOrdinalInitialized = true;
                        ordinal = new(methodSymbol, parameterOuters.ToArray(), parameterInners.ToArray(), parameterOthers.ToArray(), parameterTables.ToArray(), ordinalCloses.ToArray());
                        break;
                    case IntrinsicsKind.Fma:
                        fma = new(methodSymbol, parameterOuters.ToArray(), parameterInners.ToArray(), parameterOthers.ToArray(), parameterTables.ToArray(), fmaCloses.ToArray());
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
                if (arguments[0].Value is not int kind || arguments[1].Value is not int index || arguments[2].Value is not string name)
                {
                    return;
                }

                var collisionIntrinsicsKind = (IntrinsicsKind)kind;
                switch (collisionIntrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                        ordinalCloses.Add(new(methodSymbol, collisionIntrinsicsKind, index, name));
                        break;
                    case IntrinsicsKind.Fma:
                        fmaCloses.Add(new(methodSymbol, collisionIntrinsicsKind, index, name));
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
            if (length < 6)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[0].Values, arguments[1].Values, arguments[2].Value as string, out outers)
                || !TypeStruct.InterpretCollisionTypeLoopFields(arguments[3].Values, arguments[4].Values, arguments[5].Value as string, out inners))
            {
                return false;
            }

            if (length == 6)
            {
                return true;
            }

            if (length < 9)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[6].Values, arguments[7].Values, arguments[8].Values, out others))
            {
                return false;
            }

            if (length == 9)
            {
                return true;
            }

            if (length < 12)
            {
                return false;
            }

            return TypeStruct.InterpretCollisionTypeLoopFields(arguments[9].Values, arguments[10].Values, arguments[11].Values, out tables);
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
            public readonly int Index;
            public readonly string Name;

            public CloseMethodStruct(IMethodSymbol symbol, IntrinsicsKind kind, int index, string name)
            {
                Kind = kind;
                Symbol = symbol;
                Index = index;
                Name = name;
            }
        }
    }
}
