namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularSpeedTimeEquivalence))]
    public void AngularSpeedTime_ShouldBeEquivalent(AngularSpeed angularSpeed, Time time, AngularAcceleration expected)
    {
        IEnumerable<AngularAcceleration> actual = new AngularAcceleration[]
        {
            AngularAcceleration.From(angularSpeed, time),
            angularSpeed.Divide(time),
            angularSpeed / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngularSpeedTimeEquivalence()
    {
        yield return new object[] { 7 * AngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond, 7 / 0.003 * AngularAcceleration.OneRadianPerSecondSquared };
    }
}
