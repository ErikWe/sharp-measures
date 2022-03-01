namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularVelocityEquivalence))]
    public void MomentOfInertiaAngularVelocity_ShouldBeEquivalent(MomentOfInertia momentOfInertia, AngularVelocity3 angularVelocity, AngularMomentum3 expected)
    {
        IEnumerable<AngularMomentum3> actual = new AngularMomentum3[]
        {
            AngularMomentum3.From(momentOfInertia, angularVelocity),
            momentOfInertia.Multiply(angularVelocity),
            momentOfInertia * angularVelocity,
            angularVelocity.Multiply(momentOfInertia),
            angularVelocity * momentOfInertia
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularVelocityEquivalence()
    {
        yield return new object[] { 4 * MomentOfInertia.OneKilogramSquareMetre, (1, 5, -1) * AngularSpeed.OneRadianPerSecond,
            (4, 20, -4) * AngularMomentum.OneKilogramSquareMetrePerSecond };
    }
}
