﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitName
{
    [Fact]
    public Task VerifyInvalidUnitNameDiagnosticsMessage_Null()
    {
        string source = SourceTexts.Fixed("null");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyInvalidUnitNameDiagnosticsMessage_Empty()
    {
        string source = SourceTexts.Fixed("\"\"");

        return AssertExactlyInvalidUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Alias_ExactList(string name)
    {
        string source = SourceTexts.Alias(name: name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Biased_ExactList(string name)
    {
        string source = SourceTexts.Biased(name: name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void DerivedWithID_ExactList(string name)
    {
        string source = SourceTexts.DerivedWithID(name: name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void DerivedWithoutID_ExactList(string name)
    {
        string source = SourceTexts.DerivedWithoutID(name: name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Fixed_ExactList(string name)
    {
        string source = SourceTexts.Fixed(name: name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Prefixed_ExactList(string name)
    {
        string source = SourceTexts.Prefixed(name:name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidUnitNames))]
    public void Scaled_ExactList(string name)
    {
        string source = SourceTexts.Scaled(name: name);

        AssertExactlyInvalidUnitNameDiagnostics(source);
    }

    private static IEnumerable<object[]> InvalidUnitNames() => new object[][]
    {
        new[] { "null" },
        new[] { "\"\"" }
    };

    private static GeneratorVerifier AssertExactlyInvalidUnitNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitName };
}
