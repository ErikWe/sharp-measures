#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(SpeedDataset))]
    public void Square(Speed quantity)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(quantity, quantity.Square());
    }

}
