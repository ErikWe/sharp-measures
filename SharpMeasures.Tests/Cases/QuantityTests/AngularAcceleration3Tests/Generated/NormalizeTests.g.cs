#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(AngularAcceleration3Dataset))]
    public void MagnitudeShouldBeOne(AngularAcceleration3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(AngularAcceleration3Dataset))]
    public void ComponentRatioShouldBePreserved(AngularAcceleration3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
