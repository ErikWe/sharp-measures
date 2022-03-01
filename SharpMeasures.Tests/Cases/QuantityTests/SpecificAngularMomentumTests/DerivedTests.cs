namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularMomentumMassEquivalence))]
    public void AngularMomentumMass_ShouldBeEquivalent(AngularMomentum angularMomentum, Mass mass, SpecificAngularMomentum expected)
    {
        IEnumerable<SpecificAngularMomentum> actual = new SpecificAngularMomentum[]
        {
            SpecificAngularMomentum.From(angularMomentum, mass),
            angularMomentum.Divide(mass),
            angularMomentum / mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngularMomentumMassEquivalence()
    {
        yield return new object[] { 7 * AngularMomentum.OneKilogramSquareMetrePerSecond, 3 * Mass.OneGram, 7 / 0.003 * SpecificAngularMomentum.OneSquareMetrePerSecond };
    }
}
