#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneRadianPerSecond_ShouldMatchUnitScale()
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.OneRadianPerSecond;

        Assert.Equal(UnitOfAngularVelocity.RadianPerSecond.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDegreePerSecond_ShouldMatchUnitScale()
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.OneDegreePerSecond;

        Assert.Equal(UnitOfAngularVelocity.DegreePerSecond.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneRevolutionPerSecond_ShouldMatchUnitScale()
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.OneRevolutionPerSecond;

        Assert.Equal(UnitOfAngularVelocity.RevolutionPerSecond.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneRevolutionPerMinute_ShouldMatchUnitScale()
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.OneRevolutionPerMinute;

        Assert.Equal(UnitOfAngularVelocity.RevolutionPerMinute.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAngularVelocity unit)
    {
        SpinAngularSpeed quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAngularVelocity unit)
    {
        SpinAngularSpeed quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.AngularSpeed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        SpinAngularSpeed quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        SpinAngularSpeed quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpinAngularSpeed quantity = (SpinAngularSpeed)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpinAngularSpeed quantity = SpinAngularSpeed.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpinAngularSpeed quantity = (SpinAngularSpeed)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
