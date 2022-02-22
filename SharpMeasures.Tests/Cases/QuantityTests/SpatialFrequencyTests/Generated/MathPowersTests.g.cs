#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void Invert(SpatialFrequency quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

}
