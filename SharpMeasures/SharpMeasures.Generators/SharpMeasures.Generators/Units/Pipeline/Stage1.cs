namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;

internal static class Stage1
{
    public static IncrementalValuesProvider<TypeDeclarationSyntax> Perform(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<TypeDeclarationSyntax> declarations = MarkedDeclarationSyntaxProvider.Attach<GeneratedUnitAttribute>(context.SyntaxProvider);
        declarations = PartialTypeDeclarationSyntaxProvider.AttachAndReport(context, declarations, typeof(GeneratedUnitAttribute));
        return declarations;
    }
}
