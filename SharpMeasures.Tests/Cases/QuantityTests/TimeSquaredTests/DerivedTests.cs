namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeSquaredTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DistanceAccelerationEquivalence))]
    public void DistanceAcceleration_ShouldBeEquivalent(Distance distance, Acceleration acceleration, TimeSquared expected)
    {
        IEnumerable<TimeSquared> actual = new TimeSquared[]
        {
            TimeSquared.From(distance, acceleration),
            distance.Divide(acceleration),
            distance / acceleration
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> DistanceAccelerationEquivalence()
    {
        yield return new object[] { 15 * Distance.OneCentimetre, 37 * Acceleration.OneMetrePerSecondSquared, 0.15 / 37 * TimeSquared.OneSquareSecond };
    }
}
