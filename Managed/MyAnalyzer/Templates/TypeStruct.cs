using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
    public readonly struct TypeStruct
    {
        public readonly ITypeSymbol Symbol;
        public readonly bool IsReadOnly;

        public TypeStruct(ITypeSymbol symbol, bool isReadOnly)
        {
            Symbol = symbol;
            IsReadOnly = isReadOnly;
        }

        public static bool InterpretCollisionTypeLoopFields(ImmutableArray<TypedConstant> types, ImmutableArray<TypedConstant> bools, out TypeStruct[] answer)
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
}