namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AbsementTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DistanceTimeEquivalence))]
    public void DistanceTime_ShouldBeEquivalent(Distance distance, Time time, Absement expected)
    {
        IEnumerable<Absement> actual = new Absement[]
        {
            Absement.From(distance, time),
            distance.Multiply(time),
            distance * time,
            time.Multiply(distance),
            time * distance
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> DistanceTimeEquivalence()
    {
        yield return new object[] { 7 * Distance.OneCentimetre, 3 * Time.OneMillisecond, 0.07 * 0.003 * Absement.OneMetreSecond };
    }
}
