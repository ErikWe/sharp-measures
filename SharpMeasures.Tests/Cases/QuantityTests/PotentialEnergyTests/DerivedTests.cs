namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PotentialEnergyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(PotentialEnergyWorkEquivalence))]
    public void PotentialEnergyWork_ShouldBeEquivalent(PotentialEnergy initialPotentialEnergy, Work work, PotentialEnergy expected)
    {
        IEnumerable<PotentialEnergy> actual = new PotentialEnergy[]
        {
            PotentialEnergy.From(initialPotentialEnergy, work),
            initialPotentialEnergy.Subtract(work),
            initialPotentialEnergy - work
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> PotentialEnergyWorkEquivalence()
    {
        yield return new object[] { 7 * PotentialEnergy.OneKilojoule, 37 * Work.OneJoule, 6963 * PotentialEnergy.OneJoule };
    }
}
