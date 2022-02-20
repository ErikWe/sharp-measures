#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        MassFlowRate quantity = MassFlowRate.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneKilogramPerSecond_ShouldMatchUnitScale()
    {
        MassFlowRate quantity = MassFlowRate.OneKilogramPerSecond;

        Assert.Equal(UnitOfMassFlowRate.KilogramPerSecond.MassFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePoundPerSecond_ShouldMatchUnitScale()
    {
        MassFlowRate quantity = MassFlowRate.OnePoundPerSecond;

        Assert.Equal(UnitOfMassFlowRate.PoundPerSecond.MassFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMassFlowRateDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfMassFlowRate unit)
    {
        MassFlowRate quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.MassFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMassFlowRateDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfMassFlowRate unit)
    {
        MassFlowRate quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.MassFlowRate.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        MassFlowRate quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        MassFlowRate quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        MassFlowRate quantity = MassFlowRate.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        MassFlowRate quantity = (MassFlowRate)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        MassFlowRate quantity = MassFlowRate.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        MassFlowRate quantity = (MassFlowRate)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
