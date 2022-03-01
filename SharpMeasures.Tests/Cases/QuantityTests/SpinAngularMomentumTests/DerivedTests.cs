namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularSpeedEquivalence))]
    public void MomentOfInertiaAngularSpeed_ShouldBeEquivalent(MomentOfInertia momentOfInertia, SpinAngularSpeed spinAngularSpeed, SpinAngularMomentum expected)
    {
        IEnumerable<SpinAngularMomentum> actual = new SpinAngularMomentum[]
        {
            SpinAngularMomentum.From(momentOfInertia, spinAngularSpeed),
            momentOfInertia.Multiply(spinAngularSpeed),
            momentOfInertia * spinAngularSpeed,
            spinAngularSpeed.Multiply(momentOfInertia),
            spinAngularSpeed * momentOfInertia
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularSpeedEquivalence()
    {
        yield return new object[] { 4 * MomentOfInertia.OneKilogramSquareMetre, 7 * SpinAngularSpeed.OneRadianPerSecond,
            28 * SpinAngularMomentum.OneKilogramSquareMetrePerSecond };
    }
}
