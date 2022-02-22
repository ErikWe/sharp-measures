#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DistanceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void Invert(Distance quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void Square(Distance quantity)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(quantity, quantity.Square());
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void Cube(Distance quantity)
    {
        Utility.QuantityTests.MathPowersTests.Cube_ShouldMatchPower3(quantity, quantity.Cube());
    }

}
