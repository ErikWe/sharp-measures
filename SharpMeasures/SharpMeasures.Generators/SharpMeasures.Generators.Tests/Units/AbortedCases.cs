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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void InvalidTypeName_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public partial class struct { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void QuantityNotScalar_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void UnbiasedUnit_QuantityNotUnbiased_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[SharpMeasuresUnit(typeof(Temperature), AllowBias = false)]
public partial class UnitOfTemperature { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void BiasedUnit_QuantityNotUnbiased_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[SharpMeasuresUnit(typeof(Temperature), AllowBias = true)]
public partial class UnitOfTemperature { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void IncorrectAttributeSignature_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[SharpMeasuresUnit(3)]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void EmptyAttributeSignature_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[SharpMeasuresUnit]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }

    [Fact]
    public void NullQuantityArgument_NoSource()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[SharpMeasuresUnit(null)]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoSourceGenerated();
    }
}
