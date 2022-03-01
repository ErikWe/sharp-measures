namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularMomentumMassEquivalence))]
    public void AngularMomentumMass_ShouldBeEquivalent(AngularMomentum3 angularMomentum, Mass mass, SpecificAngularMomentum3 expected)
    {
        IEnumerable<SpecificAngularMomentum3> actual = new SpecificAngularMomentum3[]
        {
            SpecificAngularMomentum3.From(angularMomentum, mass),
            angularMomentum.Divide(mass),
            angularMomentum / mass
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> AngularMomentumMassEquivalence()
    {
        yield return new object[] { (1, 5, -1) * AngularMomentum.OneKilogramSquareMetrePerSecond, 3 * Mass.OneGram,
            (1 / 0.003, 5 / 0.003, -1 / 0.003) * SpecificAngularMomentum.OneSquareMetrePerSecond };
    }
}
