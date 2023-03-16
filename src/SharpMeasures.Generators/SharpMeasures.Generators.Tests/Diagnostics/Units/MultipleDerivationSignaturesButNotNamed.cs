namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class MultipleDerivationSignaturesButNotNamed
{
    [Fact]
    public Task VerifyMultipleDerivationSignaturesButNotNamedDiagnosticsMessage() => Assert_First(IgnoredName, ValidName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(FirstInvalid))]
    public void First(string firstID, string secondID) => Assert_First(firstID, secondID);

    [Theory]
    [MemberData(nameof(SecondInvalid))]
    public void Second(string firstID, string secondID) => Assert_Second(firstID, secondID);

    [Theory]
    [MemberData(nameof(BothInvalid))]
    public void Both(string firstID, string secondID) => Assert_Both(firstID, secondID);

    private static IEnumerable<object[]> InvalidNames => new object[][]
    {
        new[] { IgnoredName },
        new[] { NullName },
        new[] { EmptyName }
    };

    public static IEnumerable<object[]> FirstInvalid()
    {
        foreach (var invalidName in InvalidNames)
        {
            yield return new[] { invalidName[0], ValidName };
        }
    }

    public static IEnumerable<object[]> SecondInvalid()
    {
        foreach (var invalidName in InvalidNames)
        {
            yield return new[] { ValidName, invalidName[0] };
        }
    }

    public static IEnumerable<object[]> BothInvalid()
    {
        foreach (var firstInvalidName in InvalidNames)
        {
            foreach (var secondInvalidName in InvalidNames)
            {
                yield return new[] { firstInvalidName[0], secondInvalidName[0] };
            }
        }
    }

    private static string IgnoredName { get; } = string.Empty;
    private static string NullName { get; } = "null, ";
    private static string EmptyName { get; } = "\"\", ";

    private static string ValidName { get; } = "\"2\", ";

    private static GeneratorVerifier AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(OneMultipleDerivationSignaturesButNotNamedDiagnostics);
    private static GeneratorVerifier AssertExactlyTwoMultipleDerivationSignaturesButNotNamedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TwoMultipleDerivationSignaturesButNotNamedDiagnostics);
    private static IReadOnlyCollection<string> OneMultipleDerivationSignaturesButNotNamedDiagnostics { get; } = new string[] { DiagnosticIDs.MultipleDerivationSignaturesButNotNamed };
    private static IReadOnlyCollection<string> TwoMultipleDerivationSignaturesButNotNamedDiagnostics { get; } = new string[] { DiagnosticIDs.MultipleDerivationSignaturesButNotNamed, DiagnosticIDs.MultipleDerivationSignaturesButNotNamed };

    private static string Text(string firstIDWithoutComma, string secondIDWithoutComma) => $$"""
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit({{firstIDWithoutComma}}"{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivableUnit({{secondIDWithoutComma}}"{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier Assert_First(string firstIDWithoutComma, string secondIDWithoutComma)
    {
        var source = Text(firstIDWithoutComma, secondIDWithoutComma);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivableUnit", postfix: $"({firstIDWithoutComma}\"{{0}} / {{1}}\"");

        return AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical_First);
    }

    private static GeneratorVerifier Assert_Second(string firstIDWithoutComma, string secondIDWithoutComma)
    {
        var source = Text(firstIDWithoutComma, secondIDWithoutComma);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivableUnit", postfix: $"({secondIDWithoutComma}\"{{1}} / {{0}}\"");

        return AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical_Second);
    }

    private static GeneratorVerifier Assert_Both(string firstIDWithoutComma, string secondIDWithoutComma)
    {
        var source = Text(firstIDWithoutComma, secondIDWithoutComma);

        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivableUnit", postfix: $"({firstIDWithoutComma}\"{{0}} / {{1}}\""),
            ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivableUnit", postfix: $"({secondIDWithoutComma}\"{{1}} / {{0}}\"")
        };

        return AssertExactlyTwoMultipleDerivationSignaturesButNotNamedDiagnostics(source).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical_None);
    }

    private static GeneratorVerifier Identical_None => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText_None);
    private static GeneratorVerifier Identical_First => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText_First);
    private static GeneratorVerifier Identical_Second => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText_Second);

    private static string IdenticalText_None => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string IdenticalText_First => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("2", "{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string IdenticalText_Second => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("2", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
