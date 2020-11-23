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

        public readonly MethodStruct Ordinal;
        public readonly MethodStruct? Fma;

        public CollisionTemplate(INamedTypeSymbol typeSymbol, TypeStruct[] outers, TypeStruct[] inners, MethodStruct ordinal, MethodStruct? fma)
        {
            TypeSymbol = typeSymbol;
            Outers = outers;
            Inners = inners;
            Ordinal = ordinal;
            Fma = fma;
        }

        public static CollisionTemplate? TryCreate(ISymbol collisionType, ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol collisionParameter, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            if (!Get(collisionType, typeSymbol, comparer, out var outers, out var inners))
            {
                return default;
            }

            if (!Collect(collisionMethod, collisionCloseMethod, collisionParameter, typeSymbol, comparer, out var ordinal, out var fma))
            {
                return default;
            }

            return new(typeSymbol, outers, inners, ordinal, fma);
        }

        private static bool Collect(ISymbol collisionMethod, ISymbol collisionCloseMethod, ISymbol collisionParameter, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, out MethodStruct ordinal, out MethodStruct? fma)
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
                        case CollisionIntrinsicsKind.None:
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
            List<ParameterStruct> parameterOuters = new();
            List<ParameterStruct> parameterInners = new();
            foreach (var (methodSymbol, attributeData) in executeCandidates)
            {
                if (attributeData.ConstructorArguments[0].Value is not int kind)
                {
                    continue;
                }

                parameterOuters.Clear();
                parameterInners.Clear();
                foreach (var parameter in methodSymbol.Parameters)
                {
                    var parameterAttribute = parameter.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionParameter));
                    if (parameterAttribute is null)
                    {
                        return false;
                    }

                    var arguments = parameterAttribute.ConstructorArguments;
                    if (arguments[0].Value is not bool isOuter || arguments[1].Value is not int fieldIndex || arguments[2].Value is not string fieldName)
                    {
                        return false;
                    }

                    if (isOuter)
                    {
                        parameterOuters.Add(new(parameter, isOuter, fieldIndex, fieldName));
                    }
                    else
                    {
                        parameterInners.Add(new(parameter, isOuter, fieldIndex, fieldName));
                    }
                }

                switch ((CollisionIntrinsicsKind)kind)
                {
                    case CollisionIntrinsicsKind.None:
                        isOrdinalInitialized = true;
                        ordinal = new MethodStruct(methodSymbol, parameterOuters.ToArray(), parameterInners.ToArray(), ordinalCloses.ToArray());
                        break;
                    case CollisionIntrinsicsKind.Fma:
                        fma = new MethodStruct(methodSymbol, parameterOuters.ToArray(), parameterInners.ToArray(), fmaCloses.ToArray());
                        break;
                    default:
                        continue;
                }
            }

            return isOrdinalInitialized;
        }

        private static bool Get(ISymbol collisionType, ISymbol typeSymbol, SymbolEqualityComparer comparer, out TypeStruct[] outers, out TypeStruct[] inners)
        {
            outers = Array.Empty<TypeStruct>();
            inners = Array.Empty<TypeStruct>();
            var typeAttr = typeSymbol.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, collisionType));
            if (typeAttr is null)
            {
                return false;
            }

            var typeAttrConstructorArguments = typeAttr.ConstructorArguments;
            if (typeAttrConstructorArguments.IsDefaultOrEmpty || typeAttrConstructorArguments.Length != 4)
            {
                return false;
            }

            var typedConstants0 = typeAttrConstructorArguments[0].Values;
            var typedConstants1 = typeAttrConstructorArguments[1].Values;
            if (!Make(typedConstants0, typedConstants1, out outers))
            {
                return false;
            }

            var typedConstants2 = typeAttrConstructorArguments[2].Values;
            var typedConstants3 = typeAttrConstructorArguments[3].Values;
            return Make(typedConstants2, typedConstants3, out inners);
        }

        private static bool Make(ImmutableArray<TypedConstant> types, ImmutableArray<TypedConstant> bools, out TypeStruct[] answer)
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
        None,
        Fma,
    }

    public readonly struct MethodStruct
    {
        public readonly IMethodSymbol Symbol;
        public readonly ParameterStruct[] OuterLoopParameters;
        public readonly ParameterStruct[] InnerLoopParameters;

        public readonly CloseMethodStruct[] Closers;

        public MethodStruct(IMethodSymbol symbol, ParameterStruct[] outerLoopParameters, ParameterStruct[] innerLoopParameters, CloseMethodStruct[] closers)
        {
            Symbol = symbol;
            OuterLoopParameters = outerLoopParameters;
            InnerLoopParameters = innerLoopParameters;
            Closers = closers;
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
        public readonly bool IsOuter;
        public readonly int Index;
        public readonly string Name;

        public ParameterStruct(IParameterSymbol symbol, bool isOuter, int index, string name)
        {
            Symbol = symbol;
            IsOuter = isOuter;
            Index = index;
            Name = name;
        }
    }
}
