﻿namespace SharpMeasures.Generators.Tests.Verify;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

using VerifyTests;

internal static class Initializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifySourceGenerators.Enable();
        VerifierSettings.DerivePathInfo(DerivePathInfo);
        VerifierSettings.AddScrubber(ScrubTimestamp);
        VerifierSettings.RegisterFileConverter<GeneratedSourceResult>(ConvertGeneratedSourceResult);
        VerifierSettings.RegisterFileConverter<IEnumerable<GeneratedSourceResult>>(ConvertGeneratedSourceResult);
    }

    private static void ScrubTimestamp(StringBuilder source)
    {
        MatchCollection matches = TimestampRegex.Matches(source.ToString());

        if (matches.Count == 0)
        {
            return;
        }

        Match match = matches[0];
        source.Remove(match.Index, match.Length);
        source.Insert(match.Index, match.Result("${header}<stamp>"));
    }

    private static ConversionResult ConvertGeneratedSourceResult(GeneratedSourceResult target, IReadOnlyDictionary<string, object> _)
    {
        return new(null, new Target[] { SourceToTarget(target) });
    }

    private static ConversionResult ConvertGeneratedSourceResult(IEnumerable<GeneratedSourceResult> target, IReadOnlyDictionary<string, object> _)
    {
        return new(null, target.Select(static (source) => SourceToTarget(source)).ToArray());
    }

    private static Target SourceToTarget(GeneratedSourceResult source)
    {
        var data = $"""
            //HintName: {source.HintName}
            {source.SourceText}
            """;

        return new("cs", data);
    }

    private static PathInfo DerivePathInfo(string sourceFile, string projectDirectory, Type type, MethodInfo method)
    {
        string[] path = new string[]
        {
            projectDirectory,
            "Verify/Snapshots"
        };

        path = path.Concat(type.FullName?.Split('.').Skip(3) ?? Array.Empty<string>()).ToArray();

        return new PathInfo
        (
            directory: Path.Combine(path),
            typeName: type.Name,
            methodName: method.Name
        );
    }

    private static Regex TimestampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators(?:(\.[a-zA-Z]*)?) ).+", RegexOptions.ExplicitCapture);
}
