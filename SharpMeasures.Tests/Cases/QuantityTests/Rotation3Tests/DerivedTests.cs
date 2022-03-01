namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Rotation3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularVelocityTimeEquivalence))]
    public void AngularVelocityTime_ShouldBeEquivalent(AngularVelocity3 angularVelocity, Time time, Rotation3 expected)
    {
        IEnumerable<Rotation3> actual = new Rotation3[]
        {
            Rotation3.From(angularVelocity, time),
            angularVelocity.Multiply(time),
            angularVelocity * time,
            time.Multiply(angularVelocity),
            time * angularVelocity
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> AngularVelocityTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * AngularSpeed.OneRadianPerSecond, 3 * Time.OneMillisecond, (0.003, 0.015, -0.003) * Angle.OneRadian };
    }
}
