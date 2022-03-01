namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Yank3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(ForceTimeEquivalence))]
    public void ForceTime_ShouldBeEquivalent(Force3 force, Time time, Yank3 expected)
    {
        IEnumerable<Yank3> actual = new Yank3[]
        {
            Yank3.From(force, time),
            force.Divide(time),
            force / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> ForceTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Force.OneNewton, 3 * Time.OneMinute, (1d / (3 * 60), 5d / (3 * 60), -1d / (3 * 60)) * Yank.OneNewtonPerSecond };
    }
}
