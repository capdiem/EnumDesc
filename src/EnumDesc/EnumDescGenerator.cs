using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumDesc;

[Generator]
public class EnumDescGenerator : IIncrementalGenerator
{
    private const string DescriptionAttributeName = "Description";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider
                              .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetTargetDataModelForGeneration)
                              .Where(e => e is not null);

        context.RegisterSourceOutput(provider.Collect(), (ctx, models) =>
        {
            if (models.IsDefaultOrEmpty)
            {
                return;
            }

            var sources = EnumDescHelper.GenerateSources(models.ToList()!);

            sources.ForEach(item => { ctx.AddSource(item.hintName, item.sourceText); });
        });
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node, CancellationToken token) =>
        node is EnumDeclarationSyntax enumNode &&
        enumNode.Members.Any(member =>
            member.AttributeLists.Any(list => list.Attributes.Any(attr => attr.Name.ToString() == DescriptionAttributeName)));

    static EnumDescModel? GetTargetDataModelForGeneration(GeneratorSyntaxContext context, CancellationToken token)
    {
        var enumNode = (EnumDeclarationSyntax)context.Node;

        var semanticModel = context.SemanticModel.Compilation.GetSemanticModel(enumNode.SyntaxTree);
        if (semanticModel.GetDeclaredSymbol(enumNode) is not INamedTypeSymbol enumSymbol)
        {
            return null;
        }

        var descriptionAttribute = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(DescriptionAttribute).FullName);

        var name = enumSymbol.Name;
        var @namespace = enumSymbol.ContainingNamespace.IsGlobalNamespace ? null : enumSymbol.ContainingNamespace.ToDisplayString();

        var model = new EnumDescModel(name, @namespace);

        foreach (var member in enumSymbol.GetMembers())
        {
            if (member.Kind != SymbolKind.Field)
            {
                continue;
            }

            string description;

            var attributeData = member.GetAttributes()
                                      .FirstOrDefault(ad =>
                                          ad.AttributeClass is not null &&
                                          ad.AttributeClass.Equals(descriptionAttribute, SymbolEqualityComparer.Default));

            description = attributeData is null 
                ? member.Name 
                : attributeData.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? string.Empty;

            model.Members.Add(new EnumDescModel.Member(member.Name, description));
        }

        return model;
    }
}