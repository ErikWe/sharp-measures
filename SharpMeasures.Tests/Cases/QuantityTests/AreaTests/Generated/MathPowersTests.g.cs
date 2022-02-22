#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AreaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(AreaDataset))]
    public void SquareRoot(Area quantity)
    {
        Utility.QuantityTests.MathPowersTests.SquareRoot_ShouldMatchSystem(quantity, quantity.SquareRoot());
    }

}
