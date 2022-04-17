﻿namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using VerifyXunit;

internal static class VerifyGenerator
{
    public static Task FromRawText<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => Verifier.Verify(ConstructDriver(source, new TGenerator()));

    private static GeneratorDriver ConstructDriver(string sourceCode, IIncrementalGenerator generator)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        IEnumerable<MetadataReference> references = AppDomain.CurrentDomain.GetAssemblies()
                                  .Where(static (assembly) => !assembly.IsDynamic)
                                  .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
                                  .Cast<MetadataReference>();

        CSharpCompilation compilation = CSharpCompilation.Create("SharpMeasuresTests", new[] { syntaxTree }, references,
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

        ImmutableArray<AdditionalText>.Builder builder = ImmutableArray.CreateBuilder<AdditionalText>();

        foreach (string additionalTextPath in GetDocumentationFiles())
        {
            builder.Add(new CustomAdditionalText(additionalTextPath));
        }

        return builder.ToImmutable();
    }
}
