namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalEnergyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(WeightDistanceEquivalence))]
    public void WeightDistance_ShouldBeEquivalent(Weight weight, Distance distance, GravitationalEnergy expected)
    {
        IEnumerable<GravitationalEnergy> actual = new GravitationalEnergy[]
        {
            GravitationalEnergy.From(weight, distance),
            weight.Multiply(distance),
            weight * distance,
            distance.Multiply(weight),
            distance * weight
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> WeightDistanceEquivalence()
    {
        yield return new object[] { 19 * Weight.OneNewton, 3 * Distance.OneKilometre, 19 * 3000 * GravitationalEnergy.OneJoule };
    }
}
