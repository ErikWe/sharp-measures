namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.RotationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularSpeedEquivalence))]
    public void MomentOfInertiaAngularSpeed_ShouldBeEquivalent(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed, RotationalKineticEnergy expected)
    {
        IEnumerable<RotationalKineticEnergy> actual = new RotationalKineticEnergy[]
        {
            RotationalKineticEnergy.From(momentOfInertia, angularSpeed),
            RotationalKineticEnergy.From(momentOfInertia.Multiply(angularSpeed), angularSpeed),
            0.5 * momentOfInertia.Multiply(angularSpeed).Multiply(angularSpeed),
            0.5 * momentOfInertia * angularSpeed * angularSpeed
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularSpeedEquivalence()
    {
        yield return new object[] { 7 * MomentOfInertia.OneKilogramSquareMetre, 37 * AngularSpeed.OneRadianPerSecond, 1d / 2 * 7 * 37 * 37 * RotationalKineticEnergy.OneJoule };
    }
}
