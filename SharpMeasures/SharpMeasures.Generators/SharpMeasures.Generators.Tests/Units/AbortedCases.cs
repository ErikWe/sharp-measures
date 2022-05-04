namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void TypeNotPartial()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void InvalidTypeName()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedUnit(typeof(Length))]
public partial class struct { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void QuantityNotScalarQuantity()
    {
        string source = @"
using SharpMeasures.Generators.Units;

public class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void Unbiased_QuantityNotUnbiasedScalarQuantity()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public class Temperature { }

[GeneratedUnit(typeof(Temperature), AllowBias = false)]
public partial class UnitOfTemperature { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void Biased_QuantityNotUnbiasedScalarQuantity()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public class Temperature { }

[GeneratedUnit(typeof(Temperature), AllowBias = true)]
public partial class UnitOfTemperature { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void QuantityWrongType()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[GeneratedUnit(3)]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void ArgumentMissing()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[GeneratedUnit]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }

    [Fact]
    public void QuantityNull()
    {
        string source = @"
using SharpMeasures.Generators.Units;

[GeneratedUnit(null)]
public partial class UnitOfLength { }";

        GeneratorVerifier.Construct<UnitGenerator>(source).NoOutput();
    }
}
