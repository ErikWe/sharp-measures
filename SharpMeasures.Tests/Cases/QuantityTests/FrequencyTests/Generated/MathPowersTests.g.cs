#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(FrequencyDataset))]
    public void Invert(Frequency quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(FrequencyDataset))]
    public void Square(Frequency quantity)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(quantity, quantity.Square());
    }

}
