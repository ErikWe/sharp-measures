#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.RotationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void Energy(RotationalKineticEnergy quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void KineticEnergy(RotationalKineticEnergy quantity)
    {
        KineticEnergy result = quantity.AsKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(RotationalKineticEnergyDataset))]
    public void TranslationalKineticEnergy(RotationalKineticEnergy quantity)
    {
        TranslationalKineticEnergy result = quantity.AsTranslationalKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
