#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumetricFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        VolumetricFlowRate quantity = VolumetricFlowRate.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneCubicMetrePerSecond_ShouldMatchUnitScale()
    {
        VolumetricFlowRate quantity = VolumetricFlowRate.OneCubicMetrePerSecond;

        Assert.Equal(UnitOfVolumetricFlowRate.CubicMetrePerSecond.VolumetricFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneLitrePerSecond_ShouldMatchUnitScale()
    {
        VolumetricFlowRate quantity = VolumetricFlowRate.OneLitrePerSecond;

        Assert.Equal(UnitOfVolumetricFlowRate.LitrePerSecond.VolumetricFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVolumetricFlowRateDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfVolumetricFlowRate unit)
    {
        VolumetricFlowRate quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.VolumetricFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVolumetricFlowRateDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfVolumetricFlowRate unit)
    {
        VolumetricFlowRate quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.VolumetricFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        VolumetricFlowRate quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        VolumetricFlowRate quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        VolumetricFlowRate quantity = VolumetricFlowRate.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        VolumetricFlowRate quantity = (VolumetricFlowRate)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        VolumetricFlowRate quantity = VolumetricFlowRate.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        VolumetricFlowRate quantity = (VolumetricFlowRate)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

}
