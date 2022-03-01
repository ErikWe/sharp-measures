#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TorqueTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(TorqueDataset))]
    public void Energy(Torque quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(TorqueDataset))]
    public void Work(Torque quantity)
    {
        Work result = quantity.AsWork;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
