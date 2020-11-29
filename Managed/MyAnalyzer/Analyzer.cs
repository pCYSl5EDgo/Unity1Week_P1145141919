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
            ExtractTypeSymbols(receiver, context.Compilation, out var eightBaseTypes, out var countableBaseTypes, out var collisionTemplates, out var singleLoopTemplates);

            GenerateEight(eightBaseTypes, buffer);
            GenerateCountable(countableBaseTypes, buffer);
            collisionTemplates.ForEach(template => buffer.AppendLine(template.TransformText()));
            singleLoopTemplates.ForEach(template => buffer.AppendLine(template.TransformText()));

            var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            var text = buffer.ToString();
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

        private static void ExtractTypeSymbols(SyntaxReceiver receiver, Compilation compilation, out List<INamedTypeSymbol> eightBaseTypes, out List<(INamedTypeSymbol, AttributeData)> countableBaseTypes, out List<CollisionTemplate> collisionTemplates, out List<SingleLoopTemplate> singleLoopTemplates)
        {
            var eight = compilation.GetTypeByMetadataName("MyAttribute.EightAttribute") ?? throw new System.NullReferenceException();
            var countable = compilation.GetTypeByMetadataName("MyAttribute.CountableAttribute") ?? throw new System.NullReferenceException();
            var collisionType = compilation.GetTypeByMetadataName("MyAttribute.CollisionTypeAttribute") ?? throw new System.NullReferenceException();
            var collisionMethod = compilation.GetTypeByMetadataName("MyAttribute.CollisionMethodAttribute") ?? throw new System.NullReferenceException();
            var collisionCloseMethod = compilation.GetTypeByMetadataName("MyAttribute.CollisionCloseMethodAttribute") ?? throw new System.NullReferenceException();
            var loopType = compilation.GetTypeByMetadataName("MyAttribute.SingleLoopTypeAttribute") ?? throw new System.NullReferenceException();
            var loopMethod = compilation.GetTypeByMetadataName("MyAttribute.SingleLoopMethodAttribute") ?? throw new System.NullReferenceException();
            var loopParameter = compilation.GetTypeByMetadataName("MyAttribute.LoopParameterAttribute") ?? throw new System.NullReferenceException();

            var candidateTypesCount = receiver.CandidateTypes.Count;
            eightBaseTypes = new(candidateTypesCount);
            countableBaseTypes = new(candidateTypesCount);
            collisionTemplates = new(candidateTypesCount);
            singleLoopTemplates = new(candidateTypesCount);
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
                    {
                        var template = CollisionTemplate.TryCreate(collisionType, collisionMethod, collisionCloseMethod, loopParameter, type);
                        if (template is not null)
                        {
                            collisionTemplates.Add(template);
                        }
                    }
                    {
                        var template = SingleLoopTemplate.TryCreate(loopType, loopMethod, loopParameter, type);
                        if (template is not null)
                        {
                            singleLoopTemplates.Add(template);
                        }
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
