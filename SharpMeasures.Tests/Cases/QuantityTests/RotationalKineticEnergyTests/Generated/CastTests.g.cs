#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.RotationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void ToDouble_ShouldMatchMagnitude(RotationalKineticEnergy quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void CastToDouble_ShouldMatchMagnitude(RotationalKineticEnergy quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void ToScalar_ShouldMatchMagnitude(RotationalKineticEnergy quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void CastToScalar_ShouldMatchMagnitude(RotationalKineticEnergy quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        RotationalKineticEnergy result = RotationalKineticEnergy.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        RotationalKineticEnergy result = (RotationalKineticEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        RotationalKineticEnergy result = RotationalKineticEnergy.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        RotationalKineticEnergy result = (RotationalKineticEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
