#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void ToDouble_ShouldMatchMagnitude(MomentOfInertia quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void CastToDouble_ShouldMatchMagnitude(MomentOfInertia quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void ToScalar_ShouldMatchMagnitude(MomentOfInertia quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(MomentOfInertiaDataset))]
    public void CastToScalar_ShouldMatchMagnitude(MomentOfInertia quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        MomentOfInertia result = MomentOfInertia.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        MomentOfInertia result = (MomentOfInertia)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        MomentOfInertia result = MomentOfInertia.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        MomentOfInertia result = (MomentOfInertia)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
