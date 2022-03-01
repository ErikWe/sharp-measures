namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ArealDensityTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassAreaEquivalence))]
    public void MassArea_ShouldBeEquivalent(Mass mass, Area area, ArealDensity expected)
    {
        IEnumerable<ArealDensity> actual = new ArealDensity[]
        {
            ArealDensity.From(mass, area),
            mass.Divide(area),
            mass / area
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassAreaEquivalence()
    {
        yield return new object[] { Mass.OneGram, 4 * Area.OneSquareMetre, 1d / 4000 * ArealDensity.OneKilogramPerSquareMetre };
    }
}
