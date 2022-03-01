namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DensityTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassVolumeEquivalence))]
    public void MassVolume_ShouldBeEquivalent(Mass mass, Volume volume, Density expected)
    {
        IEnumerable<Density> actual = new Density[]
        {
            Density.From(mass, volume),
            mass.Divide(volume),
            mass / volume
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassVolumeEquivalence()
    {
        yield return new object[] { Mass.OneGram, 4 * Volume.OneCubicMetre, 1d / 4000 * Density.OneKilogramPerCubicMetre };
    }
}
