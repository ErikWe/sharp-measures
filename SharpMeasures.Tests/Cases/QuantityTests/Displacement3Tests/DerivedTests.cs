namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Displacement3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(VelocityTimeEquivalence))]
    public void VelocityTime_ShouldBeEquivalent(Velocity3 velocity, Time time, Displacement3 expected)
    {
        IEnumerable<Displacement3> actual = new Displacement3[]
        {
            Displacement3.From(velocity, time),
            velocity.Multiply(time),
            velocity * time,
            time.Multiply(velocity),
            time * velocity
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> VelocityTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Speed.OneFootPerSecond, Time.OneMinute, (60, 300, -60) * Length.OneFoot };
    }
}
