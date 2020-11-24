using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        public static CollisionTemplate? TryCreate(ISymbol collisionType, ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol collisionParameter, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            TypeStruct[]? outers;
            TypeStruct[]? inners;
            TypeStruct[]? others;
            TypeStruct[]? tables;
            try
            {
                if (!InterpretCollisionType(collisionType, typeSymbol, comparer, out outers, out inners, out others, out tables))
                {
                    return default;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            MethodStruct ordinal;
            MethodStruct? fma;
            try
            {
                if (!CollectCollisionMethodAndCollisionCloseMethod(collisionMethod, collisionCloseMethod, collisionParameter, typeSymbol, comparer, out ordinal, out fma))
                {
                    return default;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return new(typeSymbol, outers, inners, others, tables, ordinal, fma);
        }

        private static bool CollectCollisionMethodAndCollisionCloseMethod(ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol collisionParameter, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, out MethodStruct ordinal, out MethodStruct? fma)
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
                if (array[0].Value is not int kind || array[1].Value is not int outerCount || outerCount == 0)
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

                var collisionIntrinsicsKind = (CollisionIntrinsicsKind)kind;
                switch (collisionIntrinsicsKind)
                {
                    case CollisionIntrinsicsKind.Ordinal:
                    case CollisionIntrinsicsKind.Fma:
                        break;
                    default:
                        return false;
                }

                parameterOuters = new ParameterStruct[outerCount];
                for (var i = 0; i < parameterOuters.Length; i++)
                {
                    var parameter = parameters[i];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterOuters[i]))
                    {
                        return false;
                    }
                }

                parameterInners = new ParameterStruct[innerCount];
                for (var i = 0; i < parameterInners.Length; i++)
                {
                    var parameter = parameters[i + outerCount];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterInners[i]))
                    {
                        return false;
                    }
                }

                parameterOthers = otherCount == 0 ? Array.Empty<ParameterStruct>() : new ParameterStruct[otherCount];
                for (var i = 0; i < parameterOthers.Length; i++)
                {
                    var parameter = parameters[i + outerCount + parameterInners.Length];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterOthers[i]))
                    {
                        return false;
                    }
                }

                parameterTables = tableCount == 0 ? Array.Empty<ParameterStruct>() : new ParameterStruct[tableCount];
                for (var i = 0; i < parameterTables.Length; i++)
                {
                    var parameter = parameters[i + outerCount + parameterInners.Length + parameterOthers.Length];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterTables[i]))
                    {
                        return false;
                    }
                }

                switch (collisionIntrinsicsKind)
                {
                    case CollisionIntrinsicsKind.Ordinal:
                        isOrdinalInitialized = true;
                        ordinal = new MethodStruct(methodSymbol, parameterOuters, parameterInners, parameterOthers, parameterTables, ordinalCloses.ToArray());
                        break;
                    case CollisionIntrinsicsKind.Fma:
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

                var collisionIntrinsicsKind = (CollisionIntrinsicsKind)kind;
                var collisionFieldKind = (CollisionFieldKind)fieldKind;
                switch (collisionIntrinsicsKind)
                {
                    case CollisionIntrinsicsKind.Ordinal:
                        ordinalCloses.Add(new(methodSymbol, collisionIntrinsicsKind, collisionFieldKind, index, name));
                        break;
                    case CollisionIntrinsicsKind.Fma:
                        fmaCloses.Add(new(methodSymbol, collisionIntrinsicsKind, collisionFieldKind, index, name));
                        break;
                    default:
                        return;
                }
            }
        }

        private static bool InterpretCollisionParameter(ISymbol collisionParameter, SymbolEqualityComparer comparer, IParameterSymbol parameter, out ParameterStruct answer)
        {
            var parameterAttribute = parameter.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionParameter));
            if (parameterAttribute is null)
            {
                answer = default;
                return false;
            }

            var arguments = parameterAttribute.ConstructorArguments;
            if (arguments[0].Value is not int fieldIndex || arguments[1].Value is not string fieldName)
            {
                answer = default;
                return false;
            }

            answer = new(parameter, fieldIndex, fieldName);
            return true;
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

            if (!InterpretCollisionTypeLoopFields(arguments[0].Values, arguments[1].Values, out outers)
                || !InterpretCollisionTypeLoopFields(arguments[2].Values, arguments[3].Values, out inners))
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

            if (!InterpretCollisionTypeLoopFields(arguments[4].Values, arguments[5].Values, out others))
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

            return InterpretCollisionTypeLoopFields(arguments[6].Values, arguments[7].Values, out tables);
        }

        private static bool InterpretCollisionTypeLoopFields(ImmutableArray<TypedConstant> types, ImmutableArray<TypedConstant> bools, out TypeStruct[] answer)
        {
            answer = Array.Empty<TypeStruct>();
            if (types.Length == 0 || types.Length != bools.Length)
            {
                return false;
            }

            answer = new TypeStruct[types.Length];
            for (var i = 0; i < answer.Length; i++)
            {
                if (types[i].Value is not ITypeSymbol typeSymbol || bools[i].Value is not bool boolValue)
                {
                    return false;
                }

                answer[i] = new(typeSymbol, boolValue);
            }

            return true;
        }
    }

    public readonly struct TypeStruct
    {
        public readonly ITypeSymbol Symbol;
        public readonly bool IsReadOnly;

        public TypeStruct(ITypeSymbol symbol, bool isReadOnly)
        {
            Symbol = symbol;
            IsReadOnly = isReadOnly;
        }
    }

    public enum CollisionIntrinsicsKind
    {
        Ordinal,
        Fma,
    }

    public enum CollisionFieldKind
    {
        Outer,
        Inner,
        Other,
        Table,
    }

    public readonly struct MethodStruct
    {
        public readonly IMethodSymbol Symbol;
        public readonly ParameterStruct[] OuterLoopParameters;
        public readonly ParameterStruct[] InnerLoopParameters;
        public readonly ParameterStruct[] OtherParameters;
        public readonly ParameterStruct[] TableParameters;

        public readonly CloseMethodStruct[] Closers;

        public MethodStruct(IMethodSymbol symbol, ParameterStruct[] outerLoopParameters, ParameterStruct[] innerLoopParameters, ParameterStruct[] otherParameters, ParameterStruct[] tableParameters, CloseMethodStruct[] closers)
        {
            Symbol = symbol;
            OuterLoopParameters = outerLoopParameters;
            InnerLoopParameters = innerLoopParameters;
            TableParameters = tableParameters;
            OtherParameters = otherParameters;
            Closers = closers;
        }
    }

    public readonly struct CloseMethodStruct
    {
        public readonly IMethodSymbol Symbol;
        public readonly CollisionIntrinsicsKind Kind;
        public readonly CollisionFieldKind FieldKind;
        public readonly int Index;
        public readonly string Name;

        public CloseMethodStruct(IMethodSymbol symbol, CollisionIntrinsicsKind kind, CollisionFieldKind fieldKind, int index, string name)
        {
            Kind = kind;
            Symbol = symbol;
            Index = index;
            Name = name;
            FieldKind = fieldKind;
        }
    }

    public readonly struct ParameterStruct
    {
        public readonly IParameterSymbol Symbol;
        public readonly int Index;
        public readonly string Name;

        public ParameterStruct(IParameterSymbol symbol, int index, string name)
        {
            Symbol = symbol;
            Index = index;
            Name = name;
        }
    }
}
