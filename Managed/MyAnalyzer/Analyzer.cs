using Microsoft.CodeAnalysis;
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
    public enum IntrinsicsKind
    {
        Ordinal,
        Fma,
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SingleLoopTypeAttribute : Attribute
    {
        public readonly Type[] TypeArray;
        public readonly bool[] IsReadOnlyArray;
        public readonly Type[] OtherTypeArray;
        public readonly bool[] OtherIsReadOnlyArray;
        public readonly Type[] TableTypeArray;
        public readonly bool[] TableIsReadOnlyArray;

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray) : this(typeArray, isReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<Type>(), Array.Empty<bool>()) { }

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray) : this(typeArray, isReadOnlyArray, otherTypeArray, otherIsReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>()) { }

        public SingleLoopTypeAttribute(Type[] typeArray, bool[] isReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, Type[] tableTypeArray, bool[] tableIsReadOnlyArray)
        {
            TypeArray = typeArray;
            IsReadOnlyArray = isReadOnlyArray;
            OtherTypeArray = otherTypeArray;
            OtherIsReadOnlyArray = otherIsReadOnlyArray;
            TableTypeArray = tableTypeArray;
            TableIsReadOnlyArray = tableIsReadOnlyArray;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SingleLoopMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly int OuterCount;
        public readonly int OtherCount;

        public SingleLoopMethodAttribute(IntrinsicsKind intrinsics, int outerCount, int otherCount)
        {
            Intrinsics = intrinsics;
            OuterCount = outerCount;
            OtherCount = otherCount;
        }

        public SingleLoopMethodAttribute(IntrinsicsKind kind, int outerCount) : this(kind, outerCount, 0) { }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CollisionTypeAttribute : Attribute
    {
        public readonly Type[] OuterTypeArray;
        public readonly bool[] OuterIsReadOnlyArray;
        public readonly Type[] InnerTypeArray;
        public readonly bool[] InnerIsReadOnlyArray;
        public readonly Type[] OtherTypeArray;
        public readonly bool[] OtherIsReadOnlyArray;
        public readonly Type[] TableTypeArray;
        public readonly bool[] TableIsReadOnlyArray;

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, Type[] innerTypeArray, bool[] innerIsReadOnlyArray) : this(outerTypeArray, outerIsReadOnlyArray, innerTypeArray, innerIsReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>(), Array.Empty<Type>(), Array.Empty<bool>()) { }

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray) : this(outerTypeArray, outerIsReadOnlyArray, innerTypeArray, innerIsReadOnlyArray, otherTypeArray, otherIsReadOnlyArray, Array.Empty<Type>(), Array.Empty<bool>()) { }

        public CollisionTypeAttribute(Type[] outerTypeArray, bool[] outerIsReadOnlyArray, Type[] innerTypeArray, bool[] innerIsReadOnlyArray, Type[] otherTypeArray, bool[] otherIsReadOnlyArray, Type[] tableTypeArray, bool[] tableIsReadOnlyArray)
        {
            OuterTypeArray = outerTypeArray;
            OuterIsReadOnlyArray = outerIsReadOnlyArray;
            InnerTypeArray = innerTypeArray;
            InnerIsReadOnlyArray = innerIsReadOnlyArray;
            OtherTypeArray = otherTypeArray;
            OtherIsReadOnlyArray = otherIsReadOnlyArray;
            TableTypeArray = tableTypeArray;
            TableIsReadOnlyArray = tableIsReadOnlyArray;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly int OuterCount;
        public readonly int InnerCount;
        public readonly int OtherCount;

        public CollisionMethodAttribute(IntrinsicsKind intrinsics, int outerCount, int innerCount, int otherCount)
        {
            Intrinsics = intrinsics;
            OuterCount = outerCount;
            InnerCount = innerCount;
            OtherCount = otherCount;
        }

        public CollisionMethodAttribute(IntrinsicsKind kind, int outerCount, int innerCount) : this(kind, outerCount, innerCount, 0) { }

        public CollisionMethodAttribute(IntrinsicsKind kind, int outerCount) : this(kind, outerCount, 0, 0) { }
    }

    public enum CollisionFieldKind
    {
        Outer,
        Inner,
        Other,
        Table,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CollisionCloseMethodAttribute : Attribute
    {
        public readonly IntrinsicsKind Intrinsics;
        public readonly CollisionFieldKind FieldKind;
        public readonly int FieldIndex;
        public readonly string FieldName;

        public CollisionCloseMethodAttribute(IntrinsicsKind intrinsics, CollisionFieldKind fieldKind, int fieldIndex, string fieldName)
        {
            Intrinsics = intrinsics;
            FieldKind = fieldKind;
            FieldIndex = fieldIndex;
            FieldName = fieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class LoopParameterAttribute : Attribute
    {
        public readonly int FieldIndex;
        public readonly string FieldName;

        public LoopParameterAttribute(int fieldIndex, string fieldName = """")
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
            var loopParameter = compilation.GetTypeByMetadataName("MyAttribute.LoopParameterAttribute") ?? throw new System.NullReferenceException();

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
                    var template = CollisionTemplate.TryCreate(collisionType, collisionMethod, collisionCloseMethod, loopParameter, type);
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
