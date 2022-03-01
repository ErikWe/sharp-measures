namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.JerkTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AccelerationTimeEquivalence))]
    public void AccelerationTime_ShouldBeEquivalent(Acceleration acceleration, Time time, Jerk expected)
    {
        IEnumerable<Jerk> actual = new Jerk[]
        {
            Jerk.From(acceleration, time),
            acceleration.Divide(time),
            acceleration / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AccelerationTimeEquivalence()
    {
        yield return new object[] { 7 * Acceleration.OneMetrePerSecondSquared, 3 * Time.OneMillisecond, 7 / 0.003 * Jerk.OneMetrePerSecondCubed };
    }
}
