namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AreaTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassArealDensityEquivalence))]
    public void MassArealDensity_ShouldBeEquivalent(Mass mass, ArealDensity arealDensity, Area expected)
    {
        IEnumerable<Area> actual = new Area[]
        {
            Area.From(mass, arealDensity),
            mass.Divide(arealDensity),
            mass / arealDensity
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassArealDensityEquivalence()
    {
        yield return new object[] { Mass.OneGram, 4 * ArealDensity.OneKilogramPerSquareMetre, 1d / 4000 * Area.OneSquareMetre };
    }
}
