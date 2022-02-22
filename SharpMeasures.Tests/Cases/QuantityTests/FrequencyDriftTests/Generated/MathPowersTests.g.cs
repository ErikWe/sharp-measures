#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyDriftTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void Invert(FrequencyDrift quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void SquareRoot(FrequencyDrift quantity)
    {
        Utility.QuantityTests.MathPowersTests.SquareRoot_ShouldMatchSystem(quantity, quantity.SquareRoot());
    }

}
