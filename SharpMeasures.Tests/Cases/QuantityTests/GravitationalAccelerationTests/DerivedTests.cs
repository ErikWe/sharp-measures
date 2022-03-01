namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAccelerationTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(WeightMassEquivalence))]
    public void WeightMass_ShouldBeEquivalent(Weight weight, Mass mass, GravitationalAcceleration expected)
    {
        IEnumerable<GravitationalAcceleration> actual = new GravitationalAcceleration[]
        {
            GravitationalAcceleration.From(weight, mass),
            weight.Divide(mass),
            weight / mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> WeightMassEquivalence()
    {
        yield return new object[] { 9 * Weight.OneNewton, Mass.OneGram, 9000 * GravitationalAcceleration.OneMetrePerSecondSquared };
    }
}
