namespace SharpMeasures.Generators.Tests.Units.Definitions.Offset;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public Task NoMatchingConstructor()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[OffsetUnit(""Celcius"", ""Celcius"", ""Kelvin"")]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        return VerifyGenerator.VerifyMatch<UnitGenerator>(source);
    }

    [Fact]
    public Task BaseTypeNotFound()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[OffsetUnit(""Celcius"", ""Celcius"", ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        return VerifyGenerator.VerifyMatch<UnitGenerator>(source);
    }
}
