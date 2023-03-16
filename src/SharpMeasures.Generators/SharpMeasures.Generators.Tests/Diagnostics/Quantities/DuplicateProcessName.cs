namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateProcessName
{
    [Fact]
    public Task VerifyDuplicateConstantNameDiagnosticsMessage() => AssertScalar().VerifyDiagnostics();

    [Fact]
    public void Scalar() => AssertScalar();

    [Fact]
    public void SpecializedScalar() => AssertSpecializedScalar();

    [Fact]
    public void Vector() => AssertVector();

    [Fact]
    public void SpecializedVector() => AssertSpecializedVector();

    [Fact]
    public void VectorGroupMember() => AssertVectorGroupMember();

    private static GeneratorVerifier AssertExactlyDuplicateProcessNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateProcessNameDiagnostics);
    private static IReadOnlyCollection<string> DuplicateProcessNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateProcessName };

    private static string ScalarText => """
        using SharpMeasures.Generators;

        [QuantityProcess("Name", "new(Magnitude)")]
        [QuantityProcess("Name", "new(Magnitude)")] // <-
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, "\"Name\"", postfix: ", \"new(Magnitude)\")] // <-");

        return AssertExactlyDuplicateProcessNameDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)")]
        [QuantityProcess("Name", "new(Magnitude)")] // <-
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, "\"Name\"", postfix: ", \"new(Magnitude)\")] // <-");

        return AssertExactlyDuplicateProcessNameDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Components)")]
        [QuantityProcess("Name", "new(Components)")] // <-
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, "\"Name\"", postfix: ", \"new(Components)\")] // <-");

        return AssertExactlyDuplicateProcessNameDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Components)")]
        [QuantityProcess("Name", "new(Components)")] // <-
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, "\"Name\"", postfix: ", \"new(Components)\")] // <-");

        return AssertExactlyDuplicateProcessNameDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Components)")]
        [QuantityProcess("Name", "new(Components)")] // <-
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, "\"Name\"", postfix: ", \"new(Components)\")] // <-");

        return AssertExactlyDuplicateProcessNameDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [QuantityProcess("Name", "new(Magnitude)")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Magnitude)")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Components)")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Components)")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityProcess("Name", "new(Components)")]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
