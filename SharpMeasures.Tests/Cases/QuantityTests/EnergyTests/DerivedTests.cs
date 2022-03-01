namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.EnergyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(PotentialKineticEnergyDensityEquivalence))]
    public void PotentialKineticEnergy_ShouldBeEquivalent(PotentialEnergy potentialEnergy, KineticEnergy kineticEnergy, Energy expected)
    {
        IEnumerable<Energy> actual = new Energy[]
        {
            Energy.From(potentialEnergy, kineticEnergy),
            potentialEnergy.Add(kineticEnergy),
            potentialEnergy + kineticEnergy,
            kineticEnergy.Add(potentialEnergy),
            kineticEnergy + potentialEnergy
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> PotentialKineticEnergyDensityEquivalence()
    {
        yield return new object[] { 7 * PotentialEnergy.OneJoule, 3 * KineticEnergy.OneKilojoule, 3007 * Energy.OneJoule };
    }
}
