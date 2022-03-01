namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(WeightMassEquivalence))]
    public void WeightMass_ShouldBeEquivalent(Weight3 weight, Mass mass, GravitationalAcceleration3 expected)
    {
        IEnumerable<GravitationalAcceleration3> actual = new GravitationalAcceleration3[]
        {
            GravitationalAcceleration3.From(weight, mass),
            weight.Divide(mass),
            weight / mass,
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> WeightMassEquivalence()
    {
        yield return new object[] { (37, 14, -3) * Weight.OneNewton, Mass.OneGram, (37000, 14000, -3000) * GravitationalAcceleration.OneMetrePerSecondSquared };
    }
}
