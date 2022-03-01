﻿namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(RotationTimeEquivalence))]
    public void RotationTime_ShouldBeEquivalent(Rotation3 rotation, Time time, SpinAngularVelocity3 expected)
    {
        IEnumerable<SpinAngularVelocity3> actual = new SpinAngularVelocity3[]
        {
            SpinAngularVelocity3.From(rotation, time)
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> RotationTimeEquivalence()
    {
        yield return new object[] { (37, 14, -3) * Angle.OneRadian, Time.OneMinute, (37d / 60, 14d / 60, -3d / 60) * SpinAngularSpeed.OneRadianPerSecond };
    }
}
