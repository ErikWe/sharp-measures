namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(LinearDensityMassEquivalence))]
    public void LinearDensityMass_ShouldBeEquivalent(LinearDensity linearDensity, Mass mass, SpatialFrequency expected)
    {
        IEnumerable<SpatialFrequency> actual = new SpatialFrequency[]
        {
            SpatialFrequency.From(linearDensity, mass),
            linearDensity.Divide(mass),
            linearDensity / mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> LinearDensityMassEquivalence()
    {
        yield return new object[] { 15 * LinearDensity.OneKilogramPerMetre, 37 * Mass.OneGram, 15000d / 37 * SpatialFrequency.OnePerMetre };
    }
}
