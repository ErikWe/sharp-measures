namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;

internal static class Stage1
{
    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Attach<GeneratedScalarQuantityAttribute>(context.SyntaxProvider);
        declarations = PartialDeclarationProvider.AttachAndReport(context, declarations, typeof(GeneratedScalarQuantityAttribute));

        return declarations;
    }
}
