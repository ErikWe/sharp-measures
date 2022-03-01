namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TranslationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassSpeedEquivalence))]
    public void MassSpeed_ShouldBeEquivalent(Mass mass, Speed speed, TranslationalKineticEnergy expected)
    {
        IEnumerable<TranslationalKineticEnergy> actual = new TranslationalKineticEnergy[]
        {
            TranslationalKineticEnergy.From(mass, speed),
            TranslationalKineticEnergy.From(mass.Multiply(speed), speed),
            0.5 * mass.Multiply(speed).Multiply(speed),
            0.5 * mass * speed * speed
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassSpeedEquivalence()
    {
        yield return new object[] { 7 * Mass.OneGram, 37 * Speed.OneMetrePerSecond, 1d / 2 * 0.007 * 37 * 37 * TranslationalKineticEnergy.OneJoule };
    }
}
