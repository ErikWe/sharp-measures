namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

public static class DriverConstruction
{
    public static GeneratorDriver ConstructAndRun<TGenerator>(string source, string documentationDirectory) where TGenerator : IIncrementalGenerator, new()
        => ConstructAndRun(source, new TGenerator(), documentationDirectory);

    public static GeneratorDriver ConstructAndRun(string source, IIncrementalGenerator generator, string documentationDirectory)
        => Run(source, Construct(generator, documentationDirectory));

    public static GeneratorDriver Construct<TGenerator>(string documentationDirectory) where TGenerator : IIncrementalGenerator, new()
        => Construct(new TGenerator(), documentationDirectory);

    public static GeneratorDriver Construct(IIncrementalGenerator generator, string documentationDirectory)
    {
        ImmutableArray<AdditionalText> additionalFiles = GetAdditionalFiles(documentationDirectory);

        return CSharpGeneratorDriver.Create(generator).AddAdditionalTexts(additionalFiles);
    }

    public static Compilation CreateCompilation(string source)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        IEnumerable<MetadataReference> references = AssemblyLoader.ReferencedAssemblies
            .Where(static (assembly) => !assembly.IsDynamic)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        return CSharpCompilation.Create("SharpMeasuresTesting", new[] { syntaxTree }, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    public static GeneratorDriver Run(string source, GeneratorDriver driver)
    {
        ArgumentNullException.ThrowIfNull(driver, nameof(driver));

        return driver.RunGenerators(CreateCompilation(source));
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
