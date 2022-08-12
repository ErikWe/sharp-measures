namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

public static class DriverConstruction
{
    private static LanguageVersion LanguageVersion { get; } = LanguageVersion.Preview;
    
    private static CSharpParseOptions ParseOptions { get; } = new(languageVersion: LanguageVersion);

    public static GeneratorDriver ConstructAndRun<TGenerator>(string source, string documentationDirectory) where TGenerator : IIncrementalGenerator, new()
        => ConstructAndRun(source, new TGenerator(), documentationDirectory);

    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator, string documentationDirectory)
        => Run(source, Construct(generator, documentationDirectory));

    public static GeneratorDriver ConstructAndRun<TGenerator>(string source, string documentationDirectory, out Compilation compilation) where TGenerator : IIncrementalGenerator, new()
            => ConstructAndRun(source, new TGenerator(), documentationDirectory, out compilation);

    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator, string documentationDirectory, out Compilation compilation)
        => RunAndUpdateCompilation(source, Construct(generator, documentationDirectory), out compilation);

    public static GeneratorDriver Construct<TGenerator>(string documentationDirectory) where TGenerator : IIncrementalGenerator, new()
        => Construct(new TGenerator(), documentationDirectory);

    public static GeneratorDriver Construct(IIncrementalGenerator generator, string documentationDirectory)
    {
        ImmutableArray<AdditionalText> additionalFiles = GetAdditionalFiles(documentationDirectory);

        return CSharpGeneratorDriver.Create(generator).AddAdditionalTexts(additionalFiles).WithUpdatedParseOptions(ParseOptions);
    }

    public static Compilation CreateCompilation(string source)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source, ParseOptions);

        IEnumerable<MetadataReference> references = AssemblyLoader.ReferencedAssemblies
            .Where(static (assembly) => assembly.IsDynamic is false)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        return CSharpCompilation.Create("SharpMeasuresTesting", new[] { syntaxTree }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    public static GeneratorDriver Run(string source, GeneratorDriver driver)
    {
        return driver.RunGenerators(CreateCompilation(source));
    }

    public static GeneratorDriver RunAndUpdateCompilation(string source, GeneratorDriver driver, out Compilation compilation)
    {
        return driver.RunGeneratorsAndUpdateCompilation(CreateCompilation(source), out compilation, out _);
    }

    private static ImmutableArray<AdditionalText> GetAdditionalFiles(string documentationDirectory)
    {
        ImmutableArray<AdditionalText>.Builder builder = ImmutableArray.CreateBuilder<AdditionalText>();

        foreach (string additionalTextPath in GetDocumentationFiles())
        {
            builder.Add(new CustomAdditionalText(additionalTextPath));
        }

        return builder.ToImmutable();

        IEnumerable<string> GetDocumentationFiles()
        {
            try
            {
                return Directory.GetFiles(documentationDirectory, "*.txt", SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
