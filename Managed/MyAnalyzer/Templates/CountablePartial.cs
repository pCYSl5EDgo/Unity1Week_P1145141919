using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
    public partial class CountableTemplate
    {
        public readonly INamedTypeSymbol TypeSymbol;
        public readonly AttributeData AttributeData;

        public CountableTemplate(INamedTypeSymbol typeSymbol, AttributeData attributeData)
        {
            TypeSymbol = typeSymbol;
            AttributeData = attributeData;
        }
    }
}