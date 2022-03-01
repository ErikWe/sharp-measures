namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.KineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(TranslationalRotationalKineticEnergyDensityEquivalence))]
    public void TranslationalRotationalKineticEnergy_ShouldBeEquivalent(TranslationalKineticEnergy translationalKineticEnergy,
        RotationalKineticEnergy rotationalKineticEnergy, KineticEnergy expected)
    {
        IEnumerable<KineticEnergy> actual = new KineticEnergy[]
        {
            KineticEnergy.From(translationalKineticEnergy, rotationalKineticEnergy),
            translationalKineticEnergy.Add(rotationalKineticEnergy),
            translationalKineticEnergy + rotationalKineticEnergy,
            rotationalKineticEnergy.Add(translationalKineticEnergy),
            rotationalKineticEnergy + translationalKineticEnergy
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> TranslationalRotationalKineticEnergyDensityEquivalence()
    {
        yield return new object[] { 7 * TranslationalKineticEnergy.OneKilojoule, 3 * RotationalKineticEnergy.OneJoule, 7003 * KineticEnergy.OneJoule };
    }
}
