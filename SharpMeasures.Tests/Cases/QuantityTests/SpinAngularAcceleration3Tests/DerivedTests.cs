namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularVelocityTimeEquivalence))]
    public void AngularVelocityTime_ShouldBeEquivalent(SpinAngularVelocity3 spinAngularVelocity, Time time, SpinAngularAcceleration3 expected)
    {
        IEnumerable<SpinAngularAcceleration3> actual = new SpinAngularAcceleration3[]
        {
            SpinAngularAcceleration3.From(spinAngularVelocity, time),
            spinAngularVelocity.Divide(time),
            spinAngularVelocity / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> AngularVelocityTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * SpinAngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond,
            (1d / 0.003, 5d / 0.003, -1d / 0.003) * SpinAngularAcceleration.OneRadianPerSecondSquared };
    }
}
