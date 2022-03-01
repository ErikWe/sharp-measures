namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularVelocityTimeEquivalence))]
    public void AngularVelocityTime_ShouldBeEquivalent(AngularVelocity3 angularVelocity, Time time, AngularAcceleration3 expected)
    {
        IEnumerable<AngularAcceleration3> actual = new AngularAcceleration3[]
        {
            AngularAcceleration3.From(angularVelocity, time),
            angularVelocity.Divide(time),
            angularVelocity / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> AngularVelocityTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * AngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond,
            (1 / 0.003, 5 / 0.003, -1 / 0.003) * AngularAcceleration.OneRadianPerSecondSquared };
    }
}
