using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace MyAnalyzer
{
    internal class SyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> CandidateTypes { get; } = new List<TypeDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (!(syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
                || typeDeclarationSyntax.AttributeLists.Count <= 0)
            {
                return;
            }

            foreach (var modifier in typeDeclarationSyntax.Modifiers)
            {
                if (modifier.Text != "partial")
                {
                    continue;
                }

                CandidateTypes.Add(typeDeclarationSyntax);
                return;
            }
        }
    }
}
