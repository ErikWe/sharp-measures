#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TranslationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void Energy(TranslationalKineticEnergy quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void KineticEnergy(TranslationalKineticEnergy quantity)
    {
        KineticEnergy result = quantity.AsKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(TranslationalKineticEnergyDataset))]
    public void RotationalKineticEnergy(TranslationalKineticEnergy quantity)
    {
        RotationalKineticEnergy result = quantity.AsRotationalKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
