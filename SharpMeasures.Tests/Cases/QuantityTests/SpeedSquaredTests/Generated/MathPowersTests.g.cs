#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void SquareRoot(SpeedSquared quantity)
    {
        Utility.QuantityTests.MathPowersTests.SquareRoot_ShouldMatchSystem(quantity, quantity.SquareRoot());
    }

}
