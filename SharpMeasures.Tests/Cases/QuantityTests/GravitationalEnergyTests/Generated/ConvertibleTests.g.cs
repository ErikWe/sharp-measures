#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(GravitationalEnergyDataset))]
    public void Energy(GravitationalEnergy quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalEnergyDataset))]
    public void PotentialEnergy(GravitationalEnergy quantity)
    {
        PotentialEnergy result = quantity.AsPotentialEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
