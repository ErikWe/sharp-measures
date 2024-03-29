﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorGroupNameSuggestsDimension
{
    [Fact]
    public Task VectorGroup() => AssertVectorGroup().VerifyDiagnostics();

    [Fact]
    public void SpecializedVector() => AssertSpecializedVectorGroup();

    private static GeneratorVerifier AssertExactlyVectorGroupNameSuggestsDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorGroupNameSuggestsDimensionDiagnostics);
    private static IReadOnlyCollection<string> VectorGroupNameSuggestsDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.VectorGroupNameSuggestsDimension };

    private static string VectorGroupText => $$"""
        using SharpMeasures.Generators;
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "VectorGroup");

        return AssertExactlyVectorGroupNameSuggestsDimensionDiagnostics(VectorGroupText).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string SpecializedVectorGroupText => $$"""
        using SharpMeasures.Generators;
        
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "SpecializedVectorGroup");

        return AssertExactlyVectorGroupNameSuggestsDimensionDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(expectedLocation);
    }
}
