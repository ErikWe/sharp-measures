#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TranslationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void ToDouble_ShouldMatchMagnitude(TranslationalKineticEnergy quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void CastToDouble_ShouldMatchMagnitude(TranslationalKineticEnergy quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void ToScalar_ShouldMatchMagnitude(TranslationalKineticEnergy quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void CastToScalar_ShouldMatchMagnitude(TranslationalKineticEnergy quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        TranslationalKineticEnergy result = TranslationalKineticEnergy.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        TranslationalKineticEnergy result = (TranslationalKineticEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        TranslationalKineticEnergy result = TranslationalKineticEnergy.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        TranslationalKineticEnergy result = (TranslationalKineticEnergy)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
