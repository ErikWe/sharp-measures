namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void TypeNotPartial_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void InvalidTypeName_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class struct { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void QuantityNotScalar_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void UnbiasedUnit_QuantityNotUnbiased_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature), AllowBias = false)]
public partial class UnitOfTemperature { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void BiasedUnit_QuantityNotUnbiased_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature), AllowBias = true)]
public partial class UnitOfTemperature { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void IncorrectAttributeSignature_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[GeneratedUnit(3)]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void EmptyAttributeSignature_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[GeneratedUnit]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void NullQuantityArgument_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[GeneratedUnit(null)]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }
}
