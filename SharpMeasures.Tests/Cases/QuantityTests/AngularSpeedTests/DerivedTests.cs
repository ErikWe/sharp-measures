namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngleTimeEquivalence))]
    public void AngleTime_ShouldBeEquivalent(Angle angle, Time time, AngularSpeed expected)
    {
        IEnumerable<AngularSpeed> actual = new AngularSpeed[]
        {
            AngularSpeed.From(angle, time),
            angle.Divide(time),
            angle / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngleTimeEquivalence()
    {
        yield return new object[] { 4 * Angle.OneTurn, 37 * Time.OneMillisecond, 4 / 0.037 * AngularSpeed.OneRevolutionPerSecond };
    }
}
