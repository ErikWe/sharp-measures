namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Acceleration3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(VelocityTimeEquivalence))]
    public void VelocityTime_ShouldBeEquivalent(Velocity3 velocity, Time time, Acceleration3 expected)
    {
        IEnumerable<Acceleration3> actual = new Acceleration3[]
        {
            Acceleration3.From(velocity, time),
            velocity.Divide(time),
            velocity / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> VelocityTimeEquivalence()
    {
        yield return new object[] { (37, 14, -3) * Speed.OneMetrePerSecond, Time.OneMinute, (37d / 60, 14d / 60, -3d / 60) * Acceleration.OneMetrePerSecondSquared };
    }
}
