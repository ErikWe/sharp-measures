﻿namespace SharpMeasures.Generators.Tests.Verify;

using System;
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

    private static PathInfo DerivePathInfo(string sourceFile, string projectDirectory, Type type, MethodInfo method)
    {
        string[] path = new string[]
        {
            projectDirectory,
            @"Verify\Results"
        };

        path = path.Concat(type.FullName?.Split('.').Skip(3) ?? Array.Empty<string>()).ToArray();

        return new PathInfo
        (
            directory: Path.Combine(path),
            typeName: type.Name,
            methodName: method.Name
        );
    }

    private static Regex TimestampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators ).+", RegexOptions.ExplicitCapture);
}