#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.WorkTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(WorkDataset))]
    public void Energy(Work quantity)
    {
        Energy result = quantity.AsEnergy;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(WorkDataset))]
    public void Torque(Work quantity)
    {
        Torque result = quantity.AsTorque;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
