namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularSpeedTimeEquivalence))]
    public void AngularSpeedTime_ShouldBeEquivalent(SpinAngularSpeed spinAngularSpeed, Time time, SpinAngularAcceleration expected)
    {
        IEnumerable<SpinAngularAcceleration> actual = new SpinAngularAcceleration[]
        {
            SpinAngularAcceleration.From(spinAngularSpeed, time),
            spinAngularSpeed.Divide(time),
            spinAngularSpeed / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngularSpeedTimeEquivalence()
    {
        yield return new object[] { 7 * SpinAngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond, 7 / 0.003 * SpinAngularAcceleration.OneRadianPerSecondSquared };
    }
}
