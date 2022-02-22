#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(TimeDataset))]
    public void Invert(Time quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(TimeDataset))]
    public void Square(Time quantity)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(quantity, quantity.Square());
    }

}
