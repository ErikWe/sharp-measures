#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(VolumeDataset))]
    public void CubeRoot(Volume quantity)
    {
        Utility.QuantityTests.MathPowersTests.CubeRoot_ShouldMatchSystem(quantity, quantity.CubeRoot());
    }
}
