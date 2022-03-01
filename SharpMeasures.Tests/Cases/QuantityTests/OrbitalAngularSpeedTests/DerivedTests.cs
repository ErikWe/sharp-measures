namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngleTimeEquivalence))]
    public void AngleTime_ShouldBeEquivalent(Angle angle, Time time, OrbitalAngularSpeed expected)
    {
        IEnumerable<OrbitalAngularSpeed> actual = new OrbitalAngularSpeed[]
        {
            OrbitalAngularSpeed.From(angle, time)
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngleTimeEquivalence()
    {
        yield return new object[] { 4 * Angle.OneTurn, 37 * Time.OneMillisecond, 4 / 0.037 * OrbitalAngularSpeed.OneRevolutionPerSecond };
    }
}
