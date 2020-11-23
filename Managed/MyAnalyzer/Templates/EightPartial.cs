using Microsoft.CodeAnalysis;

namespace MyAnalyzer.Templates
{
    public partial class EightTemplate
    {
        public readonly INamedTypeSymbol TypeSymbol;

        public EightTemplate(INamedTypeSymbol typeSymbol)
        {
            TypeSymbol = typeSymbol;
        }
    }
}