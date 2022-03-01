namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AccelerationTimeEquivalence))]
    public void AccelerationTime_ShouldBeEquivalent(Acceleration acceleration, Time time, Speed expected)
    {
        IEnumerable<Speed> actual = new Speed[]
        {
            Speed.From(acceleration, time),
            acceleration.Multiply(time),
            acceleration * time,
            time.Multiply(acceleration),
            time * acceleration
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AccelerationTimeEquivalence()
    {
        yield return new object[] { 3 * Acceleration.OneFootPerSecondSquared, 4 * Time.OneSecond, 12 * Speed.OneFootPerSecond };
    }
}
