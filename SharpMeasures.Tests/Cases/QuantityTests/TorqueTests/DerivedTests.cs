namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TorqueTests;

using ErikWe.SharpMeasures.Quantities;

using System;
using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DistanceForceAngleEquivalence))]
    public void DistanceForceAngle_ShouldBeEquivalent(Distance distance, Force force, Angle angle, Torque expected)
    {
        IEnumerable<Torque> actual = new Torque[]
        {
            Torque.From(distance, force, angle)
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> DistanceForceAngleEquivalence()
    {
        yield return new object[] { 37 * Distance.OneCentimetre, 14 * Force.OneNewton, 37 * Angle.OneRadian, 0.37 * 14 * Math.Sin(37) * Torque.OneNewtonMetre };
    }
}
