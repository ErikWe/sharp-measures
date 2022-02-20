#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(GravitationalEnergyDataset))]
    public void ToDouble_ShouldMatchMagnitude(GravitationalEnergy quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalEnergyDataset))]
    public void CastToDouble_ShouldMatchMagnitude(GravitationalEnergy quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalEnergyDataset))]
    public void ToScalar_ShouldMatchMagnitude(GravitationalEnergy quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalEnergyDataset))]
    public void CastToScalar_ShouldMatchMagnitude(GravitationalEnergy quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        GravitationalEnergy result = GravitationalEnergy.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        GravitationalEnergy result = (GravitationalEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        GravitationalEnergy result = GravitationalEnergy.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        GravitationalEnergy result = (GravitationalEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
