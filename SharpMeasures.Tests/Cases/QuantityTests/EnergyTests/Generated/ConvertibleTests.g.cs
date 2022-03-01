#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.EnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(EnergyDataset))]
    public void KineticEnergy(Energy quantity)
    {
        KineticEnergy result = quantity.AsKineticEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(EnergyDataset))]
    public void PotentialEnergy(Energy quantity)
    {
        PotentialEnergy result = quantity.AsPotentialEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(EnergyDataset))]
    public void Work(Energy quantity)
    {
        Work result = quantity.AsWork;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(EnergyDataset))]
    public void Torque(Energy quantity)
    {
        Torque result = quantity.AsTorque;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
