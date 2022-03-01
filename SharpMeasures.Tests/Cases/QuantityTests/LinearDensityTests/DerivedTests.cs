namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LinearDensityTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassLengthEquivalence))]
    public void MassLength_ShouldBeEquivalent(Mass mass, Length length, LinearDensity expected)
    {
        IEnumerable<LinearDensity> actual = new LinearDensity[]
        {
            LinearDensity.From(mass, length),
            mass.Divide(length),
            mass / length
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassLengthEquivalence()
    {
        yield return new object[] { Mass.OneGram, 4 * Length.OneMetre, 1d / 4000 * LinearDensity.OneKilogramPerMetre };
    }
}
