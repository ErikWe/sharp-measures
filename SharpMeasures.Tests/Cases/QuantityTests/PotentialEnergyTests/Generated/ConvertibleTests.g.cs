#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PotentialEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void Energy(PotentialEnergy quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void GravitationalEnergy(PotentialEnergy quantity)
    {
        GravitationalEnergy result = quantity.AsGravitationalEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(PotentialEnergyDataset))]
    public void KineticEnergy(PotentialEnergy quantity)
    {
        KineticEnergy result = quantity.AsKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
