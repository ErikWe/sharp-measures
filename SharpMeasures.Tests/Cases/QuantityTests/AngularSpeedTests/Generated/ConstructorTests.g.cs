#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        AngularSpeed quantity = AngularSpeed.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneRadianPerSecond_ShouldMatchUnitScale()
    {
        AngularSpeed quantity = AngularSpeed.OneRadianPerSecond;

        Assert.Equal(UnitOfAngularVelocity.RadianPerSecond.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDegreePerSecond_ShouldMatchUnitScale()
    {
        AngularSpeed quantity = AngularSpeed.OneDegreePerSecond;

        Assert.Equal(UnitOfAngularVelocity.DegreePerSecond.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneRevolutionPerSecond_ShouldMatchUnitScale()
    {
        AngularSpeed quantity = AngularSpeed.OneRevolutionPerSecond;

        Assert.Equal(UnitOfAngularVelocity.RevolutionPerSecond.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneRevolutionPerMinute_ShouldMatchUnitScale()
    {
        AngularSpeed quantity = AngularSpeed.OneRevolutionPerMinute;

        Assert.Equal(UnitOfAngularVelocity.RevolutionPerMinute.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAngularVelocity unit)
    {
        AngularSpeed quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAngularVelocity unit)
    {
        AngularSpeed quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        AngularSpeed quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        AngularSpeed quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        AngularSpeed quantity = AngularSpeed.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        AngularSpeed quantity = (AngularSpeed)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        AngularSpeed quantity = AngularSpeed.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        AngularSpeed quantity = (AngularSpeed)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

}
