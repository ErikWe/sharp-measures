#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Displacement3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void MagnitudeShouldBeOne(Displacement3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void ComponentRatioShouldBePreserved(Displacement3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
