namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

public record class DriverConstructionConfiguration(bool AddDocumentationFiles, string DocumentationDirectory, AnalyzerConfigOptionsProvider OptionsProvider)
{
    public static DriverConstructionConfiguration Empty { get; } = new(false, string.Empty, CustomAnalyzerConfigOptionsProvider.Empty);
}

public static class DriverConstruction
{
    private static LanguageVersion LanguageVersion { get; } = LanguageVersion.Preview;

    private static CSharpParseOptions ParseOptions { get; } = new(languageVersion: LanguageVersion);
    private static CSharpCompilationOptions CompilationOptions { get; } = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

    public static GeneratorDriver ConstructAndRun<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new() => ConstructAndRun(source, new TGenerator());
    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator) => ConstructAndRun(source, generator, DriverConstructionConfiguration.Empty);
    public static GeneratorDriver ConstructAndRun<TGenerator>(string source, out Compilation compilation) where TGenerator : IIncrementalGenerator, new() => ConstructAndRun(source, new TGenerator(), out compilation);
    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator, out Compilation compilation) => ConstructAndRun(source, generator, DriverConstructionConfiguration.Empty, out compilation);

    public static GeneratorDriver ConstructAndRun<TGenerator>(string source, DriverConstructionConfiguration configuration) where TGenerator : IIncrementalGenerator, new() => ConstructAndRun(source, new TGenerator(), configuration);
    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator, DriverConstructionConfiguration configuration) => Run(source, Construct(generator, configuration));
    public static GeneratorDriver ConstructAndRun<TGenerator>(string source, DriverConstructionConfiguration configuration, out Compilation compilation) where TGenerator : IIncrementalGenerator, new() => ConstructAndRun(source, new TGenerator(), configuration, out compilation);
    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator, DriverConstructionConfiguration configuration, out Compilation compilation) => RunAndUpdateCompilation(source, Construct(generator, configuration), out compilation);

    public static GeneratorDriver Construct<TGenerator>() where TGenerator : IIncrementalGenerator, new() => Construct(new TGenerator());
    public static GeneratorDriver Construct(IIncrementalGenerator generator) => Construct(generator, DriverConstructionConfiguration.Empty);
    public static GeneratorDriver Construct<TGenerator>(DriverConstructionConfiguration configuration) where TGenerator : IIncrementalGenerator, new() => Construct(new TGenerator(), configuration);
    public static GeneratorDriver Construct(IIncrementalGenerator generator, DriverConstructionConfiguration configuration)
    {
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        if (configuration.AddDocumentationFiles)
        {
            var additionalFiles = GetAdditionalFiles(configuration.DocumentationDirectory);

            driver = driver.AddAdditionalTexts(additionalFiles);
        }

        return driver.WithUpdatedAnalyzerConfigOptions(configuration.OptionsProvider).WithUpdatedParseOptions(ParseOptions);
    }

    public static Compilation CreateCompilation(string source)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source, ParseOptions);

        IEnumerable<MetadataReference> references = AssemblyLoader.ReferencedAssemblies
            .Where(static (assembly) => assembly.IsDynamic is false)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        return CSharpCompilation.Create("SharpMeasuresTesting", new[] { syntaxTree }, references, CompilationOptions);
    }

    public static GeneratorDriver Run(string source, GeneratorDriver driver) => driver.RunGenerators(CreateCompilation(source));
    public static GeneratorDriver RunAndUpdateCompilation(string source, GeneratorDriver driver, out Compilation compilation) => driver.RunGeneratorsAndUpdateCompilation(CreateCompilation(source), out compilation, out _);

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
