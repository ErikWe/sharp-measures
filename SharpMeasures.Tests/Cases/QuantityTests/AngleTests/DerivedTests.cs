namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngleTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularSpeedTime))]
    public void AngularSpeedTime_ShouldBeEquivalent(AngularSpeed speed, Time time, Angle expected)
    {
        IEnumerable<Angle> actual = new Angle[]
        {
            Angle.From(speed, time),
            speed.Multiply(time),
            speed * time,
            time.Multiply(speed),
            time * speed
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngularSpeedTime()
    {
        yield return new object[] { 37 * AngularSpeed.OneRevolutionPerSecond, Time.OneMinute, 37 * 60 * Angle.OneTurn };
    }
}
