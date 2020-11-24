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
        public readonly TypeStruct[] Constants;

        public readonly MethodStruct Ordinal;
        public readonly MethodStruct? Fma;

        public CollisionTemplate(INamedTypeSymbol typeSymbol, TypeStruct[] outers, TypeStruct[] inners, TypeStruct[] constants, MethodStruct ordinal, MethodStruct? fma)
        {
            TypeSymbol = typeSymbol;
            Outers = outers;
            Inners = inners;
            Ordinal = ordinal;
            Fma = fma;
            Constants = constants;
        }

        public static CollisionTemplate? TryCreate(ISymbol collisionType, ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol collisionParameter, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            if (!InterpretCollisionType(collisionType, typeSymbol, comparer, out var outers, out var inners, out var constants))
            {
                return default;
            }

            if (!CollectCollisionMethodAndCollisionCloseMethod(collisionMethod, collisionCloseMethod, collisionParameter, typeSymbol, comparer, out var ordinal, out var fma))
            {
                return default;
            }

            return new(typeSymbol, outers, inners, constants, ordinal, fma);
        }

        private static bool CollectCollisionMethodAndCollisionCloseMethod(ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol collisionParameter, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, out MethodStruct ordinal, out MethodStruct? fma)
        {
            ordinal = default;
            List<CloseMethodStruct> ordinalCloses = new();
            List<CloseMethodStruct> fmaCloses = new();
            List<(IMethodSymbol, AttributeData)> executeCandidates = new();

            foreach (var member in typeSymbol.GetMembers())
            {
                if (member is not IMethodSymbol { IsStatic: true } methodSymbol)
                {
                    continue;
                }

                var attributes = methodSymbol.GetAttributes();
                if (attributes.IsDefaultOrEmpty)
                {
                    continue;
                }

                if (methodSymbol.ReturnsVoid)
                {
                    if (methodSymbol.Parameters.Any(x => x.RefKind != RefKind.Ref))
                    {
                        continue;
                    }

                    var attr = attributes.SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionMethod));
                    if (attr is null)
                    {
                        continue;
                    }

                    executeCandidates.Add((methodSymbol, attr));
                }
                else if (methodSymbol.Parameters.Length == 8)
                {
                    if (methodSymbol.Parameters.Any(x => !comparer.Equals(x.Type, methodSymbol.ReturnType)))
                    {
                        continue;
                    }

                    var attr = attributes.SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionCloseMethod));
                    if (attr is null)
                    {
                        continue;
                    }

                    var arguments = attr.ConstructorArguments;
                    if (arguments[0].Value is not int kind || arguments[1].Value is not bool isOuter || arguments[2].Value is not int index || arguments[3].Value is not string name)
                    {
                        continue;
                    }

                    var collisionIntrinsicsKind = (CollisionIntrinsicsKind)kind;
                    switch (collisionIntrinsicsKind)
                    {
                        case CollisionIntrinsicsKind.Ordinal:
                            ordinalCloses.Add(new(collisionIntrinsicsKind, methodSymbol, isOuter, index, name));
                            break;
                        case CollisionIntrinsicsKind.Fma:
                            fmaCloses.Add(new(collisionIntrinsicsKind, methodSymbol, isOuter, index, name));
                            break;
                        default:
                            continue;
                    }
                }
            }

            var isOrdinalInitialized = false;
            fma = default;
            ParameterStruct[] parameterOuters;
            ParameterStruct[] parameterInners;
            ParameterStruct[] parameterConstants;
            foreach (var (methodSymbol, attributeData) in executeCandidates)
            {
                var array = attributeData.ConstructorArguments;
                if (array[0].Value is not int kind || array[1].Value is not int outerCount || outerCount == 0 || array[2].Value is not int innerCount || innerCount == 0)
                {
                    continue;
                }

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
                    var parameter = methodSymbol.Parameters[i];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterOuters[i]))
                    {
                        return false;
                    }
                }

                parameterInners = new ParameterStruct[innerCount];
                for (var i = 0; i < parameterInners.Length; i++)
                {
                    var parameter = methodSymbol.Parameters[i + outerCount];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterInners[i]))
                    {
                        return false;
                    }
                }

                parameterConstants = new ParameterStruct[methodSymbol.Parameters.Length - outerCount - innerCount];
                for (var i = 0; i < parameterConstants.Length; i++)
                {
                    var parameter = methodSymbol.Parameters[i + outerCount + innerCount];
                    if (!InterpretCollisionParameter(collisionParameter, comparer, parameter, out parameterConstants[i]))
                    {
                        return false;
                    }
                }

                switch (collisionIntrinsicsKind)
                {
                    case CollisionIntrinsicsKind.Ordinal:
                        isOrdinalInitialized = true;
                        ordinal = new MethodStruct(methodSymbol, parameterOuters, parameterInners, parameterConstants, ordinalCloses.ToArray());
                        break;
                    case CollisionIntrinsicsKind.Fma:
                        fma = new MethodStruct(methodSymbol, parameterOuters, parameterInners, parameterConstants, fmaCloses.ToArray());
                        break;
                }
            }

            return isOrdinalInitialized;
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

        private static bool InterpretCollisionType(ISymbol collisionType, ISymbol typeSymbol, SymbolEqualityComparer comparer, out TypeStruct[] outers, out TypeStruct[] inners, out TypeStruct[] constants)
        {
            outers = Array.Empty<TypeStruct>();
            inners = Array.Empty<TypeStruct>();
            constants = Array.Empty<TypeStruct>();
            var typeAttr = typeSymbol.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionType));
            if (typeAttr is null)
            {
                return false;
            }

            var typeAttrConstructorArguments = typeAttr.ConstructorArguments;
            return typeAttrConstructorArguments.Length == 5
                   && InterpretCollisionTypeLoopFields(typeAttrConstructorArguments[0].Values, typeAttrConstructorArguments[1].Values, out outers)
                   && InterpretCollisionTypeLoopFields(typeAttrConstructorArguments[2].Values, typeAttrConstructorArguments[3].Values, out inners)
                   && InterpretCollisionTypeConstantFields(typeAttrConstructorArguments[4].Values, ref constants);
        }

        private static bool InterpretCollisionTypeConstantFields(ImmutableArray<TypedConstant> typedConstants, ref TypeStruct[] constants)
        {
            if (typedConstants.Length == 0)
            {
                return true;
            }

            constants = new TypeStruct[typedConstants.Length];
            for (var i = 0; i < constants.Length; i++)
            {
                if (typedConstants[i].Value is not INamedTypeSymbol namedTypeSymbol)
                {
                    return false;
                }

                constants[i] = new TypeStruct(namedTypeSymbol, true);
            }

            return true;
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

    public readonly struct MethodStruct
    {
        public readonly IMethodSymbol Symbol;
        public readonly ParameterStruct[] OuterLoopParameters;
        public readonly ParameterStruct[] InnerLoopParameters;
        public readonly ParameterStruct[] ConstantParameters;

        public readonly CloseMethodStruct[] Closers;

        public MethodStruct(IMethodSymbol symbol, ParameterStruct[] outerLoopParameters, ParameterStruct[] innerLoopParameters, ParameterStruct[] constantParameters, CloseMethodStruct[] closers)
        {
            Symbol = symbol;
            OuterLoopParameters = outerLoopParameters;
            InnerLoopParameters = innerLoopParameters;
            Closers = closers;
            ConstantParameters = constantParameters;
        }
    }

    public readonly struct CloseMethodStruct
    {
        public readonly CollisionIntrinsicsKind Kind;
        public readonly IMethodSymbol Symbol;
        public readonly bool IsOuter;
        public readonly int Index;
        public readonly string Name;

        public CloseMethodStruct(CollisionIntrinsicsKind kind, IMethodSymbol symbol, bool isOuter, int index, string name)
        {
            Kind = kind;
            Symbol = symbol;
            IsOuter = isOuter;
            Index = index;
            Name = name;
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
