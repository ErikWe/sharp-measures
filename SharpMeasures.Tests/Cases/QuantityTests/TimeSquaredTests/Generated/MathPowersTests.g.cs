#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(TimeSquaredDataset))]
    public void Invert(TimeSquared quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(TimeSquaredDataset))]
    public void SquareRoot(TimeSquared quantity)
    {
        Utility.QuantityTests.MathPowersTests.SquareRoot_ShouldMatchSystem(quantity, quantity.SquareRoot());
    }

}
