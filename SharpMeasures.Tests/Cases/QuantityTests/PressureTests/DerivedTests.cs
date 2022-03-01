namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PressureTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(ForceAreaEquivalence))]
    public void ForceArea_ShouldBeEquivalent(Force force, Area area, Pressure expected)
    {
        IEnumerable<Pressure> actual = new Pressure[]
        {
            Pressure.From(force, area),
            force.Divide(area),
            force / area
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> ForceAreaEquivalence()
    {
        yield return new object[] { 15 * Force.OneNewton, 37 * Area.OneSquareMetre, 15d / 37 * Pressure.OnePascal };
    }
}
