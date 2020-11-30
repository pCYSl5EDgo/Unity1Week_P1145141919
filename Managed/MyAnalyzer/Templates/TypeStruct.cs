using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
    public readonly struct TypeStruct
    {
        public readonly INamedTypeSymbol Symbol;
        public readonly bool IsReadOnly;
        public readonly string Name;

        public TypeStruct(INamedTypeSymbol symbol, bool isReadOnly, string name)
        {
            Symbol = symbol;
            IsReadOnly = isReadOnly;
            Name = name;
        }

        public static bool InterpretCollisionTypeLoopFields(ImmutableArray<TypedConstant> types, ImmutableArray<TypedConstant> bools, string? prefix, out TypeStruct[] answer)
        {
            answer = Array.Empty<TypeStruct>();
            if (prefix is null)
            {
                return false;
            }

            if (types.Length == 0 || types.Length != bools.Length)
            {
                return false;
            }

            answer = new TypeStruct[types.Length];
            for (var i = 0; i < answer.Length; i++)
            {
                if (types[i].Value is not INamedTypeSymbol typeSymbol || bools[i].Value is not bool boolValue)
                {
                    return false;
                }

                answer[i] = new(typeSymbol, boolValue, prefix + typeSymbol.Name);
            }

            return true;
        }

        public static bool InterpretCollisionTypeLoopFields(ImmutableArray<TypedConstant> types, ImmutableArray<TypedConstant> bools, ImmutableArray<TypedConstant> names, out TypeStruct[] answer)
        {
            answer = Array.Empty<TypeStruct>();
            if (types.Length == 0 || types.Length != bools.Length)
            {
                return false;
            }

            answer = new TypeStruct[types.Length];
            for (var i = 0; i < answer.Length; i++)
            {
                if (types[i].Value is not INamedTypeSymbol typeSymbol)
                {
                    return false;
                }

                if (bools[i].Value is not bool boolValue)
                {
                    return false;
                }

                if (names[i].Value is not string nameValue)
                {
                    return false;
                }

                answer[i] = new(typeSymbol, boolValue, nameValue);
            }

            return true;
        }
    }
}
