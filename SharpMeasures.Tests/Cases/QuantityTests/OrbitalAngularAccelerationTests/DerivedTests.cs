namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularSpeedTimeEquivalence))]
    public void AngularSpeedTime_ShouldBeEquivalent(OrbitalAngularSpeed orbitalAngularSpeed, Time time, OrbitalAngularAcceleration expected)
    {
        IEnumerable<OrbitalAngularAcceleration> actual = new OrbitalAngularAcceleration[]
        {
            OrbitalAngularAcceleration.From(orbitalAngularSpeed, time),
            orbitalAngularSpeed.Divide(time),
            orbitalAngularSpeed / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngularSpeedTimeEquivalence()
    {
        yield return new object[] { 7 * OrbitalAngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond, 7 / 0.003 * OrbitalAngularAcceleration.OneRadianPerSecondSquared };
    }
}
