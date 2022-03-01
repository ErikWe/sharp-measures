#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Acceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Acceleration3Dataset))]
    public void MagnitudeShouldBeOne(Acceleration3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Acceleration3Dataset))]
    public void ComponentRatioShouldBePreserved(Acceleration3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
