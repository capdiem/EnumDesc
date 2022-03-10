using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace EnumDesc;

[Generator]
public class EnumDescGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif

        var provider = context.SyntaxProvider
                              .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetSemanticTargetForGeneration)
                              .Where(static e => e is not null);

        var output = context.AnalyzerConfigOptionsProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(output, (context, source) => Execute(source.Right, source.Left, context));
    }

    static void Execute(ImmutableArray<EnumDeclarationSyntax> enums, AnalyzerConfigOptionsProvider options, SourceProductionContext context)
    {
        if (enums.IsDefaultOrEmpty)
        {
            return;
        }

        IEnumerable<EnumDeclarationSyntax> distinctEnums = enums.Distinct();
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node, CancellationToken token) =>
        node is EnumDeclarationSyntax enumNode &&
        enumNode.Members.Any(member => member.AttributeLists.Count > 0); // can i filter [description] here?

    static EnumDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken token)
    {
        var enumNode = (EnumDeclarationSyntax)context.Node;

        foreach (AttributeListSyntax attributeListSyntax in enumNode.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;

                // why need to check IMethodSymbol

                INamedTypeSymbol namedTypeSymbol = symbol.ContainingType;
                string fullName = namedTypeSymbol.ToDisplayString();

                if (fullName == typeof(DescriptionAttribute).FullName)
                {
                    return enumNode;
                }
            }
        }

        return null;
    }
}