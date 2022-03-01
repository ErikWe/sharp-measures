namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(RotationTimeEquivalence))]
    public void RotationTime_ShouldBeEquivalent(Rotation3 rotation, Time time, OrbitalAngularVelocity3 expected)
    {
        IEnumerable<OrbitalAngularVelocity3> actual = new OrbitalAngularVelocity3[]
        {
            OrbitalAngularVelocity3.From(rotation, time)
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> RotationTimeEquivalence()
    {
        yield return new object[] { (37, 14, -3) * Angle.OneRadian, Time.OneMinute, (37d / 60, 14d / 60, -3d / 60) * OrbitalAngularSpeed.OneRadianPerSecond };
    }
}
