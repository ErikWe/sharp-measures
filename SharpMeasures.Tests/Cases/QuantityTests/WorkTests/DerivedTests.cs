namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.WorkTests;

using ErikWe.SharpMeasures.Quantities;

using System;
using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(ForceDistanceEquivalence))]
    public void ForceDistance_ShouldBeEquivalent(Force force, Distance distance, Work expected)
    {
        IEnumerable<Work> actual = new Work[]
        {
            Work.From(force, distance),
            force.Multiply(distance),
            force * distance,
            distance.Multiply(force),
            distance * force
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ForceDistanceAngleEquivalence))]
    public void ForceDistanceAngle_ShouldBeEquivalent(Force force, Distance distance, Angle angle, Work expected)
    {
        Work actual = Work.From(force, distance, angle);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [MemberData(nameof(PowerTimeEquivalence))]
    public void PowerTime_ShouldBeEquivalent(Power power, Time time, Work expected)
    {
        Work actual = Work.From(power, time);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> ForceDistanceEquivalence()
    {
        yield return new object[] { 3 * Force.OneNewton, 14 * Distance.OneCentimetre, 0.42 * Work.OneJoule };
    }

    public static IEnumerable<object[]> ForceDistanceAngleEquivalence()
    {
        yield return new object[] { 3 * Force.OneNewton, 14 * Distance.OneCentimetre, 37 * Angle.OneRadian, 0.42 * Math.Cos(37) * Work.OneJoule };
    }

    public static IEnumerable<object[]> PowerTimeEquivalence()
    {
        yield return new object[] { 3 * Power.OneKilowatt, 3 * Time.OneMinute, 3000 * 3 * 60 * Work.OneJoule };
    }
}
