#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Absement3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Absement3Dataset))]
    public void MagnitudeShouldBeOne(Absement3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Absement3Dataset))]
    public void ComponentRatioShouldBePreserved(Absement3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
