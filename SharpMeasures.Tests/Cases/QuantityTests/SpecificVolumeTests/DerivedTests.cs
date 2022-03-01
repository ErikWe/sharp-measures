namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificVolumeTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(VolumeMassEquivalence))]
    public void VolumeMass_ShouldBeEquivalent(Volume volume, Mass mass, SpecificVolume expected)
    {
        IEnumerable<SpecificVolume> actual = new SpecificVolume[]
        {
            SpecificVolume.From(volume, mass),
            volume.Divide(mass),
            volume / mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> VolumeMassEquivalence()
    {
        yield return new object[] { 7 * Volume.OneCubicMetre, 3 * Mass.OneGram, 7 / 0.003 * SpecificVolume.OneCubicMetrePerKilogram };
    }
}
