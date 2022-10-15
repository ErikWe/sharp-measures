namespace SharpMeasures.Generators.Tests.Scalars;

using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Immutable;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AttributeAlias
{
    [Fact]
    public void Default() => Construct().AssertNoMatchingSourceNameGenerated($"Distance.Common.g.cs");

    [Fact]
    public void Enabled() => Construct("true").AssertAllListedSourceNamesGenerated($"Distance.Common.g.cs");

    [Fact]
    public void Disabled() => Construct("false").AssertNoMatchingSourceNameGenerated($"Distance.Common.g.cs");

    private static GeneratorVerifier Construct() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported();
    private static GeneratorVerifier Construct(string allowAliases) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, Options(allowAliases)).AssertNoDiagnosticsReported();

    private static string Text => """
        using SharpMeasures.Generators;

        using Scalar = SharpMeasures.Generators.ScalarQuantityAttribute;

        [Scalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static AnalyzerConfigOptionsProvider Options(string allowAliases)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, string>();

        builder.Add("SharpMeasures_AllowAttributeAliases", allowAliases);

        return new CustomAnalyzerConfigOptionsProvider(new CustomAnalyzerConfigOptions(builder.ToImmutable()));
    }
}
