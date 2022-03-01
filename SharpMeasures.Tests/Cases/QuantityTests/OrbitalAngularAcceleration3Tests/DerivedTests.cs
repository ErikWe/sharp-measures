namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularVelocityTimeEquivalence))]
    public void AngularVelocityTime_ShouldBeEquivalent(OrbitalAngularVelocity3 orbitalAngularVelocity, Time time, OrbitalAngularAcceleration3 expected)
    {
        IEnumerable<OrbitalAngularAcceleration3> actual = new OrbitalAngularAcceleration3[]
        {
            OrbitalAngularAcceleration3.From(orbitalAngularVelocity, time),
            orbitalAngularVelocity.Divide(time),
            orbitalAngularVelocity / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> AngularVelocityTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * OrbitalAngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond,
            (1d / 0.003, 5d / 0.003, -1d / 0.003) * OrbitalAngularAcceleration.OneRadianPerSecondSquared };
    }
}
