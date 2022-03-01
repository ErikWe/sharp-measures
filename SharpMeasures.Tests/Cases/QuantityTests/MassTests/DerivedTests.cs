namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DensityVolumeEquivalence))]
    public void DensityVolume_ShouldBeEquivalent(Density density, Volume volume, Mass expected)
    {
        IEnumerable<Mass> actual = new Mass[]
        {
            Mass.From(density, volume),
            density.Multiply(volume),
            density * volume,
            volume.Multiply(density),
            volume * density
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> DensityVolumeEquivalence()
    {
        yield return new object[] { 3 * Density.OneKilogramPerCubicMetre, 9 * Volume.OneCubicMetre, 27 * Mass.OneKilogram };
    }
}
