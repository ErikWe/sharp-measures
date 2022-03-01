namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ImpulseTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(ForceTimeEquivalence))]
    public void ForceTime_ShouldBeEquivalent(Force force, Time time, Impulse expected)
    {
        IEnumerable<Impulse> actual = new Impulse[]
        {
            Impulse.From(force, time),
            force.Multiply(time),
            force * time,
            time.Multiply(force),
            time * force
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> ForceTimeEquivalence()
    {
        yield return new object[] { 3 * Force.OneNewton, 7 * Time.OneMinute, 7 * 60 * 3 * Impulse.OneNewtonSecond };
    }
}
