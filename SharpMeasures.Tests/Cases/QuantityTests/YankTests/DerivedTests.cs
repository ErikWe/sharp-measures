namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.YankTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(ForceTimeEquivalence))]
    public void ForceTime_ShouldBeEquivalent(Force force, Time time, Yank expected)
    {
        IEnumerable<Yank> actual = new Yank[]
        {
            Yank.From(force, time),
            force.Divide(time),
            force / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> ForceTimeEquivalence()
    {
        yield return new object[] { 7 * Force.OneNewton, 3 * Time.OneMinute, 7d / (3 * 60) * Yank.OneNewtonPerSecond };
    }
}
