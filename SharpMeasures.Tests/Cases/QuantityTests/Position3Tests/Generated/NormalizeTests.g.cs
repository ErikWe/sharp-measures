#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Position3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void MagnitudeShouldBeOne(Position3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void ComponentRatioShouldBePreserved(Position3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
