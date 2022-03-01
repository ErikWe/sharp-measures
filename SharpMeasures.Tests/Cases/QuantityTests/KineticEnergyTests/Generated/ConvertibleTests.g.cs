#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.KineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void Energy(KineticEnergy quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void PotentialEnergy(KineticEnergy quantity)
    {
        PotentialEnergy result = quantity.AsPotentialEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void TranslationalKineticEnergy(KineticEnergy quantity)
    {
        TranslationalKineticEnergy result = quantity.AsTranslationalKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(KineticEnergyDataset))]
    public void RotationalKineticEnergy(KineticEnergy quantity)
    {
        RotationalKineticEnergy result = quantity.AsRotationalKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
