#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(AngularVelocity3Dataset))]
    public void MagnitudeShouldBeOne(AngularVelocity3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(AngularVelocity3Dataset))]
    public void ComponentRatioShouldBePreserved(AngularVelocity3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
