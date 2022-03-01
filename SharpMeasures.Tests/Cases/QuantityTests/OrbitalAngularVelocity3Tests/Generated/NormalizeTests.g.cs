#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void MagnitudeShouldBeOne(OrbitalAngularVelocity3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(quantity);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void ComponentRatioShouldBePreserved(OrbitalAngularVelocity3 quantity)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(quantity);
    }
}
