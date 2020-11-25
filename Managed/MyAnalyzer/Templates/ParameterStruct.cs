using System.Linq;
using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
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

        public static bool InterpretLoopParameter(ISymbol collisionParameter, SymbolEqualityComparer comparer, IParameterSymbol parameter, out ParameterStruct answer)
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
    }
}
