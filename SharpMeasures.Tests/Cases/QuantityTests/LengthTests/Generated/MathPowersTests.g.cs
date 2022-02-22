#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LengthTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(LengthDataset))]
    public void Invert(Length quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(LengthDataset))]
    public void Square(Length quantity)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(quantity, quantity.Square());
    }

    [Theory]
    [ClassData(typeof(LengthDataset))]
    public void Cube(Length quantity)
    {
        Utility.QuantityTests.MathPowersTests.Cube_ShouldMatchPower3(quantity, quantity.Cube());
    }

}
