﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Text;
using MyAnalyzer.Templates;

namespace MyAnalyzer
{
    [Generator]
    public class MyGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxReceiver is SyntaxReceiver receiver))
            {
                return;
            }

            var buffer = new StringBuilder();
            buffer.AppendLine(@"using System;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;

namespace MyAttribute
{
    public static class RotateHelper
    {
        public static void Rotate(float4x2 value, out float4x2 value1, out float4x2 value2, out float4x2 value3, out float4x2 value4, out float4x2 value5, out float4x2 value6, out float4x2 value7)
        {
            var c0 = value.c0;
            var c1 = value.c1;
            value4 = new float4x2(c1, c0);
            c0 = c0.wxyz;
            c1 = c1.wxyz;
            value1 = new float4x2(c0, c1);
            value5 = new float4x2(c1, c0);
            c0 = c0.wxyz;
            c1 = c1.wxyz;
            value2 = new float4x2(c0, c1);
            value6 = new float4x2(c1, c0);
            c0 = c0.wxyz;
            c1 = c1.wxyz;
            value3 = new float4x2(c0, c1);
            value7 = new float4x2(c1, c0);
        }

        public static void RotateM1(ref float4x2 value1, ref float4x2 value2, ref float4x2 value3, ref float4x2 value4, ref float4x2 value5, ref float4x2 value6, ref float4x2 value7)
        {
            value1.c0 = value1.c0.yzwx;
            value1.c1 = value1.c1.yzwx;
            value2.c0 = value2.c0.zwxy;
            value2.c1 = value2.c1.zwxy;
            value3.c0 = value3.c0.wxyz;
            value3.c1 = value3.c1.wxyz;
            value4 = new float4x2(value4.c1, value4.c0);
            value5 = new float4x2(value5.c1.yzwx, value5.c0.yzwx);
            value6 = new float4x2(value6.c1.zwxy, value6.c0.zwxy);
            value7 = new float4x2(value7.c1.wxyz, value7.c0.wxyz);
        }

        public static void Rotate(int4x2 value, out int4x2 value1, out int4x2 value2, out int4x2 value3, out int4x2 value4, out int4x2 value5, out int4x2 value6, out int4x2 value7)
        {
            var c0 = value.c0;
            var c1 = value.c1;
            value4 = new int4x2(c1, c0);
            c0 = c0.wxyz;
            c1 = c1.wxyz;
            value1 = new int4x2(c0, c1);
            value5 = new int4x2(c1, c0);
            c0 = c0.wxyz;
            c1 = c1.wxyz;
            value2 = new int4x2(c0, c1);
            value6 = new int4x2(c1, c0);
            c0 = c0.wxyz;
            c1 = c1.wxyz;
            value3 = new int4x2(c0, c1);
            value7 = new int4x2(c1, c0);
        }

        public static void RotateM1(ref int4x2 value1, ref int4x2 value2, ref int4x2 value3, ref int4x2 value4, ref int4x2 value5, ref int4x2 value6, ref int4x2 value7)
        {
            value1.c0 = value1.c0.yzwx;
            value1.c1 = value1.c1.yzwx;
            value2.c0 = value2.c0.zwxy;
            value2.c1 = value2.c1.zwxy;
            value3.c0 = value3.c0.wxyz;
            value3.c1 = value3.c1.wxyz;
            value4 = new int4x2(value4.c1, value4.c0);
            value5 = new int4x2(value5.c1.yzwx, value5.c0.yzwx);
            value6 = new int4x2(value6.c1.zwxy, value6.c0.zwxy);
            value7 = new int4x2(value7.c1.wxyz, value7.c0.wxyz);
        }
    }

    public enum CollisionIntrinsicsKind
    {
        Ordinal,
        Fma,
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CollisionTypeAttribute : Attribute
    {
        public readonly Type[] OuterLoopTypeArray;
        public readonly bool[] OuterLoopIsReadOnlyArray;
        public readonly Type[] InnerLoopTypeArray;
        public readonly bool[] InnerLoopIsReadOnlyArray;
        public readonly Type[] ConstantTypeArray;

        public CollisionTypeAttribute(Type[] outerLoopTypeArray, bool[] outerLoopIsReadOnlyArray, Type[] innerLoopTypeArray, bool[] innerLoopIsReadOnlyArray, Type[] constantTypeArray)
        {
            OuterLoopTypeArray = outerLoopTypeArray;
            OuterLoopIsReadOnlyArray = outerLoopIsReadOnlyArray;
            InnerLoopTypeArray = innerLoopTypeArray;
            InnerLoopIsReadOnlyArray = innerLoopIsReadOnlyArray;
            ConstantTypeArray = constantTypeArray;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionMethodAttribute : Attribute
    {
        public readonly CollisionIntrinsicsKind Kind;
        public readonly int OuterCount;
        public readonly int InnerCount;

        public CollisionMethodAttribute(CollisionIntrinsicsKind kind, int outerCount, int innerCount)
        {
            Kind = kind;
            OuterCount = outerCount;
            InnerCount = innerCount;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionCloseMethodAttribute : Attribute
    {
        public readonly CollisionIntrinsicsKind Kind;
        public readonly bool IsOuter;
        public readonly int FieldIndex;
        public readonly string FieldName;

        public CollisionCloseMethodAttribute(CollisionIntrinsicsKind kind, bool isOuter, int fieldIndex, string fieldName)
        {
            Kind = kind;
            IsOuter = isOuter;
            FieldIndex = fieldIndex;
            FieldName = fieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class CollisionParameterAttribute : Attribute
    {
        public readonly int FieldIndex;
        public readonly string FieldName;

        public CollisionParameterAttribute(int fieldIndex, string fieldName = """")
        {
            FieldIndex = fieldIndex;
            FieldName = fieldName;
        }
    }

    public class EightAttribute : Attribute { }

    public class CountableAttribute : Attribute
    {
        public readonly Type[] AdditionalMemberTypes;
        public readonly string[] AdditionalArgumentMemberNames;
        public readonly string[] AdditionalFieldMemberNames;
        public readonly string IsAliveFunctionString;

        public CountableAttribute()
        {
            AdditionalMemberTypes = Array.Empty<Type>();
            AdditionalArgumentMemberNames = Array.Empty<string>();
            AdditionalFieldMemberNames = Array.Empty<string>();
            IsAliveFunctionString = """";
        }

        public CountableAttribute(Type[] additionalMemberTypes, string[] additionalArgumentMemberNames, string[] additionalFieldMemberNames, string isAliveFunctionString)
        {
            AdditionalMemberTypes = additionalMemberTypes;
            AdditionalArgumentMemberNames = additionalArgumentMemberNames;
            AdditionalFieldMemberNames = additionalFieldMemberNames;
            IsAliveFunctionString = isAliveFunctionString;
        }
    }
}
");

            var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            SourceText attributeText = SourceText.From(buffer.ToString(), encoding);
            context.AddSource("MyAttributeResult.cs", attributeText);

            CSharpParseOptions options = (CSharpParseOptions)((CSharpCompilation)context.Compilation).SyntaxTrees[0].Options;

            Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(attributeText, options));

            buffer.Clear();
            ExtractTypeSymbols(receiver, compilation, out var eightBaseTypes, out var countableBaseTypes, out var collisionTemplates);

            GenerateEight(eightBaseTypes, buffer);
            GenerateCountable(countableBaseTypes, buffer);
            collisionTemplates.ForEach(template => buffer.AppendLine(template.TransformText()));

            string text = buffer.ToString();
            context.AddSource("MyAnalyzerResult.cs", SourceText.From(text, encoding));
        }

        private static void GenerateEight(List<INamedTypeSymbol> eightBaseTypes, StringBuilder buffer)
        {
            foreach (var namedTypeSymbol in eightBaseTypes)
            {
                var template = new EightTemplate(namedTypeSymbol);
                buffer.AppendLine(template.TransformText());
            }
        }

        private static void GenerateCountable(List<(INamedTypeSymbol, AttributeData)> countableBaseTypes, StringBuilder buffer)
        {
            foreach (var (namedTypeSymbol, attributeData) in countableBaseTypes)
            {
                var template = new CountableTemplate(namedTypeSymbol, attributeData);
                buffer.AppendLine(template.TransformText());
            }
        }

        private static void ExtractTypeSymbols(SyntaxReceiver receiver, Compilation compilation, out List<INamedTypeSymbol> eightBaseTypes, out List<(INamedTypeSymbol, AttributeData)> countableBaseTypes, out List<CollisionTemplate> collisionTemplates)
        {
            var eight = compilation.GetTypeByMetadataName("MyAttribute.EightAttribute") ?? throw new System.NullReferenceException();
            var countable = compilation.GetTypeByMetadataName("MyAttribute.CountableAttribute") ?? throw new System.NullReferenceException();
            var collisionType = compilation.GetTypeByMetadataName("MyAttribute.CollisionTypeAttribute") ?? throw new System.NullReferenceException();
            var collisionMethod = compilation.GetTypeByMetadataName("MyAttribute.CollisionMethodAttribute") ?? throw new System.NullReferenceException();
            var collisionCloseMethod = compilation.GetTypeByMetadataName("MyAttribute.CollisionCloseMethodAttribute") ?? throw new System.NullReferenceException();
            var collisionParameter = compilation.GetTypeByMetadataName("MyAttribute.CollisionParameterAttribute") ?? throw new System.NullReferenceException();

            var candidateTypesCount = receiver.CandidateTypes.Count;
            eightBaseTypes = new(candidateTypesCount);
            countableBaseTypes = new(candidateTypesCount);
            collisionTemplates = new(candidateTypesCount);
            foreach (var candidate in receiver.CandidateTypes)
            {
                var model = compilation.GetSemanticModel(candidate.SyntaxTree);
                var type = model.GetDeclaredSymbol(candidate);
                if (type is null)
                {
                    continue;
                }

                if (type.IsUnmanagedType)
                {
                    foreach (var attributeData in type.GetAttributes())
                    {
                        var attributeClass = attributeData.AttributeClass;
                        if (attributeClass is null)
                        {
                            continue;
                        }

                        if (SymbolEqualityComparer.Default.Equals(attributeClass, eight))
                        {
                            eightBaseTypes.Add(type);
                            break;
                        }

                        if (SymbolEqualityComparer.Default.Equals(attributeClass, countable))
                        {
                            countableBaseTypes.Add((type, attributeData));
                            break;
                        }
                    }
                }
                else
                {
                    var template = CollisionTemplate.TryCreate(collisionType, collisionMethod, collisionCloseMethod, collisionParameter, type);
                    if (template is null)
                    {
                        continue;
                    }

                    collisionTemplates.Add(template);
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //System.Diagnostics.Debugger.Launch();
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }
    }
}
