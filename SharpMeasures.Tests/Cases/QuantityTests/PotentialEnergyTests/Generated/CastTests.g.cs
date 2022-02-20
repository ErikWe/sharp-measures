#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PotentialEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void ToDouble_ShouldMatchMagnitude(PotentialEnergy quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void CastToDouble_ShouldMatchMagnitude(PotentialEnergy quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void ToScalar_ShouldMatchMagnitude(PotentialEnergy quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void CastToScalar_ShouldMatchMagnitude(PotentialEnergy quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        PotentialEnergy result = PotentialEnergy.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        PotentialEnergy result = (PotentialEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        PotentialEnergy result = PotentialEnergy.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        PotentialEnergy result = (PotentialEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
