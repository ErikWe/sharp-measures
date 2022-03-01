namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularVelocityEquivalence))]
    public void MomentOfInertiaAngularVelocity_ShouldBeEquivalent(MomentOfInertia momentOfInertia, OrbitalAngularVelocity3 orbitalAngularVelocity, OrbitalAngularMomentum3 expected)
    {
        IEnumerable<OrbitalAngularMomentum3> actual = new OrbitalAngularMomentum3[]
        {
            OrbitalAngularMomentum3.From(momentOfInertia, orbitalAngularVelocity),
            momentOfInertia.Multiply(orbitalAngularVelocity),
            momentOfInertia * orbitalAngularVelocity,
            orbitalAngularVelocity.Multiply(momentOfInertia),
            orbitalAngularVelocity * momentOfInertia
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularVelocityEquivalence()
    {
        yield return new object[] { 4 * MomentOfInertia.OneKilogramSquareMetre, (1, 5, -1) * OrbitalAngularSpeed.OneRadianPerSecond,
            (4, 20, -4) * OrbitalAngularMomentum.OneKilogramSquareMetrePerSecond };
    }
}
