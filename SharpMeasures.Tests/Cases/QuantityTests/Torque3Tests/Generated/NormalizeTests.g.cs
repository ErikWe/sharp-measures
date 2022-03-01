#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Torque3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Torque3Dataset))]
    public void MagnitudeShouldBeOne(Torque3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Torque3Dataset))]
    public void ComponentRatioShouldBePreserved(Torque3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
