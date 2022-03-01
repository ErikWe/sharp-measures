namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularVelocityEquivalence))]
    public void MomentOfInertiaAngularVelocity_ShouldBeEquivalent(MomentOfInertia momentOfInertia, SpinAngularVelocity3 spinAngularVelocity, SpinAngularMomentum3 expected)
    {
        IEnumerable<SpinAngularMomentum3> actual = new SpinAngularMomentum3[]
        {
            SpinAngularMomentum3.From(momentOfInertia, spinAngularVelocity),
            momentOfInertia.Multiply(spinAngularVelocity),
            momentOfInertia * spinAngularVelocity,
            spinAngularVelocity.Multiply(momentOfInertia),
            spinAngularVelocity * momentOfInertia
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularVelocityEquivalence()
    {
        yield return new object[] { 4 * MomentOfInertia.OneKilogramSquareMetre, (1, 5, -1) * SpinAngularSpeed.OneRadianPerSecond,
            (4, 20, -4) * SpinAngularMomentum.OneKilogramSquareMetrePerSecond };
    }
}
