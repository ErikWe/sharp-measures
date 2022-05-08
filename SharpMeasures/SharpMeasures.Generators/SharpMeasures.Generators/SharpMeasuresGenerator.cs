namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[Generator]
public class SharpMeasuresGenerator : IIncrementalGenerator
{
    internal readonly record struct Result(DeclarationData Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static Result Construct(DeclarationData declaration, INamedTypeSymbol typeSymbol) => new(declaration, typeSymbol);
    }

    internal readonly record struct DeclarationData(TypeDeclarationSyntax TypeDeclaration, AttributeSyntax AttributeSyntax)
    {
        public static TypeDeclarationSyntax ExtractTypeDeclaration(DeclarationData declaration) => declaration.TypeDeclaration;
        public static AttributeSyntax ExtractAttributeSyntax(DeclarationData declaration) => declaration.AttributeSyntax;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        //AttachDebugger();
#endif

        var declarations = AttachDeclarationProvider(context);
        declarations = FilterPartialDeclarations(context, declarations);

        var declarationsAndSymbols = DeclarationSymbolProvider.AttachToValueType(declarations, context.CompilationProvider,
            DeclarationData.ExtractTypeDeclaration, Result.Construct);

        UnitGenerator.Initialize(context, declarationsAndSymbols);
        ScalarQuantityGenerator.Initialize(context, declarationsAndSymbols);
    }

    private static IncrementalValuesProvider<DeclarationData> AttachDeclarationProvider(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.AttachFirst(context.SyntaxProvider, markedDeclarationProviderOutputTransform,
            typeof(GeneratedUnitAttribute), typeof(GeneratedScalarQuantityAttribute));

        return declarations;

        static DeclarationData markedDeclarationProviderOutputTransform(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax)
            => new(declaration, attributeSyntax);
    }

    private static IncrementalValuesProvider<DeclarationData> FilterPartialDeclarations(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DeclarationData> declarationProvider)
    {
        return PartialDeclarationProvider.AttachAndReport(context, declarationProvider, partialProviderInputTransform, declarationToAttributeName);

        static TypeDeclarationSyntax partialProviderInputTransform(DeclarationData declaration) => declaration.TypeDeclaration;

        static string declarationToAttributeName(DeclarationData declaration)
        {
            return declaration.AttributeSyntax.Name.ToString();
        }
    }

#if DEBUG
    [SuppressMessage("CodeQuality", "IDE0051: Remove unused private members", Justification = "Non-production code, sporadically relevant")]
    private static void AttachDebugger()
    {
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
    }
#endif
}
