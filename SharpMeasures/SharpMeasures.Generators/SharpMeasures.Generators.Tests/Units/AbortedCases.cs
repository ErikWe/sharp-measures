namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void TypeNotPartial()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }

    [Fact]
    public void InvalidTypeName()
    {
        string source = @"
using SharpMeasures.Generators;

[
[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class struct { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }

    [Fact]
    public void QuantityNotScalarQuantity()
    {
        string source = @"
using SharpMeasures.Generators;

public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }

    [Fact]
    public void QuantityNotUnbiasedScalarQuantity()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature))]
public partial class UnitOfTemperature { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }

    [Fact]
    public void QuantityWrongType()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit(3)]
public partial class UnitOfLength { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }

    [Fact]
    public void ArgumentMissing()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit]
public partial class UnitOfLength { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }

    [Fact]
    public void QuantityNull()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit(null)]
public partial class UnitOfLength { }";

        VerifyGenerator.AssertNoOutput<UnitGenerator>(source);
    }
}
