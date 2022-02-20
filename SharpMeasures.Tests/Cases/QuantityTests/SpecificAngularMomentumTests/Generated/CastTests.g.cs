#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(SpecificAngularMomentumDataset))]
    public void ToDouble_ShouldMatchMagnitude(SpecificAngularMomentum quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpecificAngularMomentumDataset))]
    public void CastToDouble_ShouldMatchMagnitude(SpecificAngularMomentum quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpecificAngularMomentumDataset))]
    public void ToScalar_ShouldMatchMagnitude(SpecificAngularMomentum quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpecificAngularMomentumDataset))]
    public void CastToScalar_ShouldMatchMagnitude(SpecificAngularMomentum quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpecificAngularMomentum result = SpecificAngularMomentum.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpecificAngularMomentum result = (SpecificAngularMomentum)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpecificAngularMomentum result = SpecificAngularMomentum.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpecificAngularMomentum result = (SpecificAngularMomentum)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
