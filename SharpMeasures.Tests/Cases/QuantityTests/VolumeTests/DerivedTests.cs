namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumeTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassDensityEquivalence))]
    public void MassDensity_ShouldBeEquivalent(Mass mass, Density density, Volume expected)
    {
        IEnumerable<Volume> actual = new Volume[]
        {
            Volume.From(mass, density),
            mass.Divide(density),
            mass / density
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [MemberData(nameof(AreaLengthEquivalence))]
    public void AreaLength_ShouldBeEquivalent(Area area, Length length, Volume expected)
    {
        IEnumerable<Volume> actual = new Volume[]
        {
            Volume.From(area, length),
            area.Multiply(length),
            area * length,
            length.Multiply(area),
            length * area
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassDensityEquivalence()
    {
        yield return new object[] { Mass.OneGram, 4 * Density.OneKilogramPerCubicMetre, 1d / 4000 * Volume.OneCubicMetre };
    }

    public static IEnumerable<object[]> AreaLengthEquivalence()
    {
        yield return new object[] { 33 * Area.OneSquareMetre, 2 * Length.OneCentimetre, 33 * 0.02 * Volume.OneCubicMetre };
    }
}
