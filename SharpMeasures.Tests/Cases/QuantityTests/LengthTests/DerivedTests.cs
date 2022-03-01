namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LengthTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassLinearDensityEquivalence))]
    public void MassLinearDensity_ShouldBeEquivalent(Mass mass, LinearDensity linearDensity, Length expected)
    {
        IEnumerable<Length> actual = new Length[]
        {
            Length.From(mass, linearDensity),
            mass.Divide(linearDensity),
            mass / linearDensity
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassLinearDensityEquivalence()
    {
        yield return new object[] { Mass.OneGram, 4 * LinearDensity.OneKilogramPerMetre, 1d / 4000 * Length.OneMetre };
    }
}
