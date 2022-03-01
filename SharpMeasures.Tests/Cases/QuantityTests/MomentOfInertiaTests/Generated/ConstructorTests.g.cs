#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        MomentOfInertia quantity = MomentOfInertia.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneKilogramSquareMetre_ShouldMatchUnitScale()
    {
        MomentOfInertia quantity = MomentOfInertia.OneKilogramSquareMetre;

        Assert.Equal(UnitOfMomentOfInertia.KilogramSquareMetre.MomentOfInertia.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMomentOfInertiaDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfMomentOfInertia unit)
    {
        MomentOfInertia quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.MomentOfInertia.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMomentOfInertiaDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfMomentOfInertia unit)
    {
        MomentOfInertia quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.MomentOfInertia.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        MomentOfInertia quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        MomentOfInertia quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        MomentOfInertia quantity = MomentOfInertia.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        MomentOfInertia quantity = (MomentOfInertia)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        MomentOfInertia quantity = MomentOfInertia.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        MomentOfInertia quantity = (MomentOfInertia)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

}
