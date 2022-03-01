#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Rotation3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Rotation3Dataset))]
    public void MagnitudeShouldBeOne(Rotation3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Rotation3Dataset))]
    public void ComponentRatioShouldBePreserved(Rotation3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
