namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class NormalizeTests
{
    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void MagnitudeShouldBeOne(Vector3 vector)
    {
        Utility.QuantityTests.NormalizeTests.ShouldBeLengthOne(vector);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void ComponentRatioShouldBePreserved(Vector3 vector)
    {
        Utility.QuantityTests.NormalizeTests.ComponentRatioShouldBePreserved(vector);
    }
}
