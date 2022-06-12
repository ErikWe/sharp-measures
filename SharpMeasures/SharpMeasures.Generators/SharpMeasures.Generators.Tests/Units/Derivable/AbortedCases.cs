namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void NoMatchingConstructor_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedScalar(typeof(UnitOfTime))]
public partial class Time { }

[GeneratedScalar(typeof(UnitOfSpeed))]
public partial class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(7)]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void ExpressionNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedScalar(typeof(UnitOfTime))]
public partial class Time { }

[GeneratedScalar(typeof(UnitOfSpeed))]
public partial class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(null, typeof(UnitOfLength), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void SignatureTypeNotUnit_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedScalar(typeof(UnitOfTime))]
public partial class Time { }

[GeneratedScalar(typeof(UnitOfSpeed))]
public partial class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", typeof(Length), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void EmptySignature_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedScalar(typeof(UnitOfTime))]
public partial class Time { }

[GeneratedScalar(typeof(UnitOfSpeed))]
public partial class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(string.Empty)]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void NullSignatureElement_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedScalar(typeof(UnitOfTime))]
public partial class Time { }

[GeneratedScalar(typeof(UnitOfSpeed))]
public partial class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", null, typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }
}
