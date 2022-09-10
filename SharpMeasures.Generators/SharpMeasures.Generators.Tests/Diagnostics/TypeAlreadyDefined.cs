﻿namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeAlreadyDefined
{
    [Fact]
    public Task Scalar_DefinedAsUnit() => AssertScalar(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsUnit() => AssertSpecializedScalar(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsScalar() => AssertSpecializedScalar(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsUnit() => AssertVector(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsScalar() => AssertVector(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsSpecializedScalar() => AssertVector(SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsUnit() => AssertSpecializedVector(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsScalar() => AssertSpecializedVector(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsSpecializedScalar() => AssertSpecializedVector(SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsVector() => AssertSpecializedVector(VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVectorGroup_DefinedAsVectorGroup() => AssertSpecializedVectorGroup(VectorGroupDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsUnit() => AssertVectorGroupMember(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsScalar() => AssertVectorGroupMember(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedScalar() => AssertVectorGroupMember(SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsVector() => AssertVectorGroupMember(VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedVector() => AssertVectorGroupMember(SpecializedVectorDefinition).VerifyDiagnostics();

    private static string UnitDefinition { get; } = "SharpMeasuresUnit(typeof(Length))";
    private static string ScalarDefinition { get; } = "SharpMeasuresScalar(typeof(UnitOfLength))";
    private static string SpecializedScalarDefinition { get; } = "SpecializedSharpMeasuresScalar(typeof(Length))";
    private static string VectorDefinition { get; } = "SharpMeasuresVector(typeof(UnitOfLength))";
    private static string SpecializedVectorDefinition { get; } = "SpecializedSharpMeasuresVector(typeof(Displacement2))";
    private static string VectorGroupDefinition { get; } = "SharpMeasuresVectorGroup(typeof(UnitOfLength))";

    private static GeneratorVerifier AssertExactlyTypeAlreadyDefinedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeAlreadyDefinedDiagnostics);
    private static IReadOnlyCollection<string> TypeAlreadyDefinedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeAlreadyDefined };

    private static string ScalarText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))] // <-
        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(string otherDefinition)
    {
        var source = ScalarText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresScalar", postfix: "(typeof(UnitOfLength))] // <-");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(otherDefinition));
    }

    private static string SpecializedScalarText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(string otherDefinition)
    {
        var source = SpecializedScalarText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SpecializedSharpMeasuresScalar");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(otherDefinition));
    }

    private static string VectorText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        [{{otherDefinition}}]
        public partial class Position2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string otherDefinition)
    {
        var source = VectorText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVector");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical(otherDefinition));
    }

    private static string SpecializedVectorText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(UnitOfLength))]
        [{{otherDefinition}}]
        public partial class Displacement2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(string otherDefinition)
    {
        var source = SpecializedVectorText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SpecializedSharpMeasuresVector");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical(otherDefinition));
    }

    private static string SpecializedVectorGroupText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        [{{otherDefinition}}]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(string otherDefinition)
    {
        var source = SpecializedVectorGroupText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SpecializedSharpMeasuresVectorGroup");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical(otherDefinition));
    }

    private static string VectorGroupMemberText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        [{{otherDefinition}}]
        public partial class Position2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string otherDefinition)
    {
        var source = VectorGroupMemberText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVectorGroupMember");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical(otherDefinition));
    }

    private static GeneratorVerifier ScalarIdentical(string otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(otherDefinition));
    private static GeneratorVerifier SpecializedScalarIdentical(string otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(otherDefinition));
    private static GeneratorVerifier VectorIdentical(string otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText(otherDefinition));
    private static GeneratorVerifier SpecializedVectorIdentical(string otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText(otherDefinition));
    private static GeneratorVerifier SpecializedVectorGroupIdentical(string otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText(otherDefinition));
    private static GeneratorVerifier VectorGroupMemberIdentical(string otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText(otherDefinition));

    private static string ScalarIdenticalText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{otherDefinition}}]
        public partial class Position2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{otherDefinition}}]
        public partial class Displacement2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{otherDefinition}}]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText(string otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{otherDefinition}}]
        public partial class Position2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
