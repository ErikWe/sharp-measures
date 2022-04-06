namespace ErikWe.SharpMeasures.SourceGenerators.Tests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using VerifyXunit;

internal static class Utility
{
    public static Task VerifyGenerator<T>(string source) where T : IIncrementalGenerator, new()
        => Verifier.Verify(ConstructDriver(source, new T()));
    
    private static GeneratorDriver ConstructDriver(string sourceCode, IIncrementalGenerator generator)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        IEnumerable<MetadataReference> references = AppDomain.CurrentDomain.GetAssemblies()
                                  .Where(static (assembly) => !assembly.IsDynamic)
                                  .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
                                  .Cast<MetadataReference>();

        CSharpCompilation compilation = CSharpCompilation.Create("ScalarQuantityGeneratorTests", new[] { syntaxTree }, references,
                      new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        ImmutableArray<AdditionalText> additionalFiles = GetAdditionalFiles();

        return CSharpGeneratorDriver.Create(generator).AddAdditionalTexts(additionalFiles).RunGenerators(compilation);
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

        static IEnumerable<string> GetDefinitionFiles()
        {
            try
            {
                return Directory.GetFiles(@"..\..\..\Definitions", "*.json", SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
        }

        ImmutableArray<AdditionalText>.Builder builder = ImmutableArray.CreateBuilder<AdditionalText>();

        foreach (string additionalTextPath in GetDocumentationFiles().Concat(GetDefinitionFiles()))
        {
            builder.Add(new CustomAdditionalText(additionalTextPath));
        }

        return builder.ToImmutable();
    }
}
