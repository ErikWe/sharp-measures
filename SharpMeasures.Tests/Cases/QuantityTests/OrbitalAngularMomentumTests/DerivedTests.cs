namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularSpeedEquivalence))]
    public void MomentOfInertiaAngularSpeed_ShouldBeEquivalent(MomentOfInertia momentOfInertia, OrbitalAngularSpeed orbitalAngularSpeed, OrbitalAngularMomentum expected)
    {
        IEnumerable<OrbitalAngularMomentum> actual = new OrbitalAngularMomentum[]
        {
            OrbitalAngularMomentum.From(momentOfInertia, orbitalAngularSpeed),
            momentOfInertia.Multiply(orbitalAngularSpeed),
            momentOfInertia * orbitalAngularSpeed,
            orbitalAngularSpeed.Multiply(momentOfInertia),
            orbitalAngularSpeed * momentOfInertia
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularSpeedEquivalence()
    {
        yield return new object[] { 4 * MomentOfInertia.OneKilogramSquareMetre, 7 * OrbitalAngularSpeed.OneRadianPerSecond,
            28 * OrbitalAngularMomentum.OneKilogramSquareMetrePerSecond };
    }
}
