namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Impulse3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(ForceTimeEquivalence))]
    public void ForceTime_ShouldBeEquivalent(Force3 force, Time time, Impulse3 expected)
    {
        IEnumerable<Impulse3> actual = new Impulse3[]
        {
            Impulse3.From(force, time),
            force.Multiply(time),
            force * time,
            time.Multiply(force),
            time * force
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> ForceTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Force.OneNewton, 7 * Time.OneMinute, (7 * 60, 7 * 60 * 5, -7 * 60) * Impulse.OneNewtonSecond };
    }
}
