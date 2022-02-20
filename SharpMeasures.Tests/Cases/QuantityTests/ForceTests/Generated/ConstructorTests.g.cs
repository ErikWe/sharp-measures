#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ForceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Force quantity = Force.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneNewton_ShouldMatchUnitScale()
    {
        Force quantity = Force.OneNewton;

        Assert.Equal(UnitOfForce.Newton.Force.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePoundForce_ShouldMatchUnitScale()
    {
        Force quantity = Force.OnePoundForce;

        Assert.Equal(UnitOfForce.PoundForce.Force.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfForceDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfForce unit)
    {
        Force quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Force.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfForceDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfForce unit)
    {
        Force quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Force.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Force quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Force quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Force quantity = Force.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Force quantity = (Force)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Force quantity = Force.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Force quantity = (Force)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
