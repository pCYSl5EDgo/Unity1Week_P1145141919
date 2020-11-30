using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
    public partial class SingleLoopTemplate
    {
        public readonly INamedTypeSymbol TypeSymbol;
        public readonly TypeStruct[] Outers;
        public readonly TypeStruct[] Others;
        public readonly TypeStruct[] Tables;

        public readonly MethodStruct Ordinal;
        public readonly MethodStruct? Fma;

        public SingleLoopTemplate(INamedTypeSymbol typeSymbol, TypeStruct[] outers, TypeStruct[] others, TypeStruct[] tables, MethodStruct ordinal, MethodStruct? fma)
        {
            TypeSymbol = typeSymbol;
            Outers = outers;
            Others = others;
            Tables = tables;
            Ordinal = ordinal;
            Fma = fma;
        }

        public static SingleLoopTemplate? TryCreate(ISymbol loopType, ISymbol intrinsicsKindMethod, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            if (!InterpretLoopType(loopType, typeSymbol, comparer, out var outers, out var others, out var tables))
            {
                return default;
            }

            if (!CollectLoop(intrinsicsKindMethod, typeSymbol, comparer, outers, others, tables, out var ordinal, out var fma))
            {
                return default;
            }

            return new(typeSymbol, outers, others, tables, ordinal, fma);
        }

        private static bool InterpretLoopType(ISymbol loopType, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, out TypeStruct[] outers, out TypeStruct[] others, out TypeStruct[] tables)
        {
            outers = Array.Empty<TypeStruct>();
            others = Array.Empty<TypeStruct>();
            tables = Array.Empty<TypeStruct>();

            var typeAttr = typeSymbol.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, loopType));
            if (typeAttr is null)
            {
                return false;
            }

            var arguments = typeAttr.ConstructorArguments;
            var length = arguments.Length;
            if (length < 2)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[0].Values, arguments[1].Values, arguments[2].Value as string, out outers))
            {
                return false;
            }

            if (length == 3)
            {
                return true;
            }

            if (length < 6)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[3].Values, arguments[4].Values, arguments[5].Values, out others))
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

            return TypeStruct.InterpretCollisionTypeLoopFields(arguments[6].Values, arguments[7].Values, arguments[8].Values, out tables);
        }


        private static bool CollectLoop(ISymbol loopMethod, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, TypeStruct[] outers, TypeStruct[] others, TypeStruct[] tables, out MethodStruct ordinal, out MethodStruct? fma)
        {
            var isOrdinalInitialized = false;
            ordinal = default;
            fma = default;
            List<ParameterStruct> parameterOuters = new();
            List<ParameterStruct> parameterOthers = new();
            List<ParameterStruct> parameterTables = new();
            foreach (var member in typeSymbol.GetMembers())
            {
                if (member is not IMethodSymbol methodSymbol)
                {
                    continue;
                }

                var attributeData = member.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, loopMethod));
                var array = attributeData?.ConstructorArguments;
                if (array?[0].Value is not int kind)
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
                parameterOthers.Clear();
                parameterTables.Clear();
                var parameters = methodSymbol.Parameters;
                var parameterIndex = 0;
                for (var typeIndex = 0; typeIndex < outers.Length; ++typeIndex)
                {
                    var typeStruct = outers[typeIndex];
                    foreach (var member2 in typeStruct.Symbol.GetMembers())
                    {
                        if (member2 is not IFieldSymbol fieldSymbol || fieldSymbol.IsStatic)
                        {
                            continue;
                        }

                        parameterOuters.Add(new(parameters[parameterIndex++], typeIndex, fieldSymbol.Name));
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
                        ordinal = new MethodStruct(methodSymbol, parameterOuters.ToArray(), parameterOthers.ToArray(), parameterTables.ToArray());
                        break;
                    case IntrinsicsKind.Fma:
                        fma = new MethodStruct(methodSymbol, parameterOuters.ToArray(), parameterOthers.ToArray(), parameterTables.ToArray());
                        break;
                }
            }

            return isOrdinalInitialized;
        }

        public readonly struct MethodStruct
        {
            public readonly IMethodSymbol Symbol;
            public readonly ParameterStruct[] Outers;
            public readonly ParameterStruct[] Others;
            public readonly ParameterStruct[] Tables;

            public MethodStruct(IMethodSymbol symbol, ParameterStruct[] outers, ParameterStruct[] others, ParameterStruct[] tables)
            {
                Symbol = symbol;
                Outers = outers;
                Others = others;
                Tables = tables;
            }
        }
    }
}
