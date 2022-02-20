#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.KineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void ToDouble_ShouldMatchMagnitude(KineticEnergy quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void CastToDouble_ShouldMatchMagnitude(KineticEnergy quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void ToScalar_ShouldMatchMagnitude(KineticEnergy quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void CastToScalar_ShouldMatchMagnitude(KineticEnergy quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        KineticEnergy result = KineticEnergy.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        KineticEnergy result = (KineticEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        KineticEnergy result = KineticEnergy.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        KineticEnergy result = (KineticEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
