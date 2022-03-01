namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Absement3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DisplacementTimeEquivalence))]
    public void DisplacementTime_ShouldBeEquivalent(Displacement3 displacement, Time time, Absement3 expected)
    {
        IEnumerable<Absement3> actual = new Absement3[]
        {
            Absement3.From(displacement, time),
            displacement.Multiply(time),
            displacement * time,
            time.Multiply(displacement),
            time * displacement
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> DisplacementTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Length.OneCentimetre, 3 * Time.OneMillisecond, (0.01 * 0.003, 0.05 * 0.003, -0.01 * 0.003) * Absement.OneMetreSecond };
    }
}
