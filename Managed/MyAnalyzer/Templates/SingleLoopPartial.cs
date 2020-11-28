﻿using System;
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

        public static SingleLoopTemplate? TryCreate(ISymbol loopType, ISymbol loopMethod, ISymbol loopParameter, INamedTypeSymbol typeSymbol)
        {
            var comparer = SymbolEqualityComparer.Default;
            if (!InterpretLoopType(loopType, typeSymbol, comparer, out var outers, out var others, out var tables))
            {
                return default;
            }

            if (!CollectLoop(loopMethod, loopParameter, typeSymbol, comparer, out var ordinal, out var fma))
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

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[0].Values, arguments[1].Values, out outers))
            {
                return false;
            }

            if (length == 2)
            {
                return true;
            }

            if (length < 4)
            {
                return false;
            }

            if (!TypeStruct.InterpretCollisionTypeLoopFields(arguments[2].Values, arguments[3].Values, out others))
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

            return TypeStruct.InterpretCollisionTypeLoopFields(arguments[4].Values, arguments[5].Values, out tables);
        }


        private static bool CollectLoop(ISymbol loopMethod, ISymbol loopParameter, INamedTypeSymbol typeSymbol, SymbolEqualityComparer comparer, out MethodStruct ordinal, out MethodStruct? fma)
        {
            var isOrdinalInitialized = false;
            ordinal = default;
            fma = default;
            ParameterStruct[] parameterOuters;
            ParameterStruct[] parameterOthers;
            ParameterStruct[] parameterTables;
            foreach (var member in typeSymbol.GetMembers())
            {
                if (member is not IMethodSymbol methodSymbol)
                {
                    continue;
                }

                var attributeData = member.GetAttributes().SingleOrDefault(x => comparer.Equals(x.AttributeClass, loopMethod));
                if (attributeData is null)
                {
                    continue;
                }

                var array = attributeData.ConstructorArguments;
                if (array[0].Value is not int kind || array[1].Value is not int outerCount || outerCount == 0)
                {
                    continue;
                }

                var parameters = methodSymbol.Parameters;
                var otherCount = parameters.Length - outerCount;

                if (array.Length >= 3)
                {
                    if (array[2].Value is not int x)
                    {
                        return false;
                    }

                    otherCount = x;
                }

                var tableCount = parameters.Length - outerCount - otherCount;

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

                parameterOthers = otherCount == 0 ? Array.Empty<ParameterStruct>() : new ParameterStruct[otherCount];
                for (var i = 0; i < parameterOthers.Length; i++)
                {
                    var parameter = parameters[i + outerCount];
                    if (!ParameterStruct.InterpretLoopParameter(loopParameter, comparer, parameter, out parameterOthers[i]))
                    {
                        return false;
                    }
                }

                parameterTables = tableCount == 0 ? Array.Empty<ParameterStruct>() : new ParameterStruct[tableCount];
                for (var i = 0; i < parameterTables.Length; i++)
                {
                    var parameter = parameters[i + outerCount + otherCount];
                    if (!ParameterStruct.InterpretLoopParameter(loopParameter, comparer, parameter, out parameterTables[i]))
                    {
                        return false;
                    }
                }

                switch (intrinsicsKind)
                {
                    case IntrinsicsKind.Ordinal:
                        isOrdinalInitialized = true;
                        ordinal = new MethodStruct(methodSymbol, parameterOuters, parameterOthers, parameterTables);
                        break;
                    case IntrinsicsKind.Fma:
                        fma = new MethodStruct(methodSymbol, parameterOuters, parameterOthers, parameterTables);
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