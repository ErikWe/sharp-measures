#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Force3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Force3Dataset))]
    public void MagnitudeShouldBeOne(Force3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(Force3Dataset))]
    public void ComponentRatioShouldBePreserved(Force3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
