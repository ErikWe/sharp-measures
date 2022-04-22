namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

internal static class DriverUtility
{
    public static GeneratorDriver ConstructAndRunDriver<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => ConstructAndRunDriver(source, new TGenerator());

    public static GeneratorDriver ConstructAndRunDriver(string source, IIncrementalGenerator generator)
        => RunDriver(source, ConstructDriver(generator));

    public static GeneratorDriver ConstructDriver<TGenerator>() where TGenerator : IIncrementalGenerator, new()
        => ConstructDriver(new TGenerator());

    public static GeneratorDriver ConstructDriver(IIncrementalGenerator generator)
    {
        ImmutableArray<AdditionalText> additionalFiles = GetAdditionalFiles();

        return CSharpGeneratorDriver.Create(generator).AddAdditionalTexts(additionalFiles);
    }

    public static GeneratorDriver RunDriver(string source, GeneratorDriver driver)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        IEnumerable<MetadataReference> references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(static (assembly) => !assembly.IsDynamic)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        CSharpCompilation compilation = CSharpCompilation.Create("SharpMeasuresTests", new[] { syntaxTree }, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return driver.RunGenerators(compilation);
    }

    private static ImmutableArray<AdditionalText> GetAdditionalFiles()
    {
        static IEnumerable<string> GetDocumentationFiles()
        {
            try
            {
                return Directory.GetFiles(@"..\..\..\Documentation", "*.txt", SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
        }

        ImmutableArray<AdditionalText>.Builder builder = ImmutableArray.CreateBuilder<AdditionalText>();

        foreach (string additionalTextPath in GetDocumentationFiles())
        {
            builder.Add(new CustomAdditionalText(additionalTextPath));
        }

        return builder.ToImmutable();
    }
}
