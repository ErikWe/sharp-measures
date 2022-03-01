namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AccelerationTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(SpeedTimeEquivalence))]
    public void SpeedTime_ShouldBeEquivalent(Speed speed, Time time, Acceleration expected)
    {
        IEnumerable<Acceleration> actual = new Acceleration[]
        {
            Acceleration.From(speed, time),
            speed.Divide(time),
            speed / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> SpeedTimeEquivalence()
    {
        yield return new object[] { 37 * Speed.OneMetrePerSecond, Time.OneMinute, 37d / 60 * Acceleration.OneMetrePerSecondSquared };
    }
}
