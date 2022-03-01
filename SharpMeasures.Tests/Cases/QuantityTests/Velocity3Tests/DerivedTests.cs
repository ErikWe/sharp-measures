namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Velocity3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(DisplacementTimeEquivalence))]
    public void DisplacementTime_ShouldBeEquivalent(Displacement3 displacement, Time time, Velocity3 expected)
    {
        IEnumerable<Velocity3> actual = new Velocity3[]
        {
            Velocity3.From(displacement, time),
            displacement.Divide(time),
            displacement / time
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> DisplacementTimeEquivalence()
    {
        yield return new object[] { (1, 5, -1) * Length.OneCentimetre, 3 * Time.OneMillisecond, (0.01 / 0.003, 0.05 / 0.003, -0.01 / 0.003) * Speed.OneMetrePerSecond };
    }
}
