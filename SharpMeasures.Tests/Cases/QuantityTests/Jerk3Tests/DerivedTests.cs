namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Jerk3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AccelerationTimeEquivalence))]
    public void AccelerationTime_ShouldBeEquivalent(Acceleration3 acceleration, Time time, Jerk3 expected)
    {
        IEnumerable<Jerk3> actual = new Jerk3[]
        {
            Jerk3.From(acceleration, time),
            acceleration.Divide(time),
            acceleration / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> AccelerationTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Acceleration.OneMetrePerSecondSquared, 3 * Time.OneMillisecond,
            (1 / 0.003, 5 / 0.003, -1 / 0.003) * Jerk.OneMetrePerSecondCubed };
    }
}
