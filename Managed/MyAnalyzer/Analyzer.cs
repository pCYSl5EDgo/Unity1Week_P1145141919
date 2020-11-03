using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Data;
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
            buffer.AppendLine(@"namespace MyAttribute
{
    public class EightAttribute : global::System.Attribute { }

    public class CountableAttribute : global::System.Attribute
    {
        public readonly global::System.Type[] AdditionalMemberTypes;
        public readonly string[] AdditionalArgumentMemberNames;
        public readonly string[] AdditionalFieldMemberNames;
        public readonly string IsAliveFunctionString;

        public CountableAttribute()
        {
            AdditionalMemberTypes = global::System.Array.Empty<global::System.Type>();
            AdditionalArgumentMemberNames = global::System.Array.Empty<string>();
            AdditionalFieldMemberNames = global::System.Array.Empty<string>();
            IsAliveFunctionString = """";
        }

        public CountableAttribute(global::System.Type[] additionalMemberTypes, string[] additionalArgumentMemberNames, string[] additionalFieldMemberNames, string isAliveFunctionString)
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
            ExtractTypeSymbols(receiver, compilation, out var eightBaseTypes, out var countableBaseTypes);

            GenerateEight(eightBaseTypes, buffer);
            GenerateCountable(countableBaseTypes, buffer);

            string text = buffer.ToString();
            context.AddSource("MyAnalyzerResult.cs", SourceText.From(text, encoding));
        }

        private static void GenerateEight(List<INamedTypeSymbol> eightBaseTypes, StringBuilder buffer)
        {
            foreach (var namedTypeSymbol in eightBaseTypes)
            {
                var template = new EightTemplate { TypeSymbol = namedTypeSymbol };
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

        private static void ExtractTypeSymbols(SyntaxReceiver receiver, Compilation compilation, out List<INamedTypeSymbol> eightBaseTypes, out List<(INamedTypeSymbol, AttributeData)> countableBaseTypes)
        {
            var eight = compilation.GetTypeByMetadataName("MyAttribute.EightAttribute") ?? throw new System.NullReferenceException();
            var countable = compilation.GetTypeByMetadataName("MyAttribute.CountableAttribute") ?? throw new System.NullReferenceException();

            eightBaseTypes = new(receiver.CandidateTypes.Count);
            countableBaseTypes = new(receiver.CandidateTypes.Count);
            foreach (var candidate in receiver.CandidateTypes)
            {
                var model = compilation.GetSemanticModel(candidate.SyntaxTree);
                var type = model.GetDeclaredSymbol(candidate);
                if (type is null || !type.IsUnmanagedType)
                {
                    continue;
                }

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
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //System.Diagnostics.Debugger.Launch();
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }
    }
}
