namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MomentOfInertiaAngularSpeedEquivalence))]
    public void MomentOfInertiaAngularSpeed_ShouldBeEquivalent(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed, AngularMomentum expected)
    {
        IEnumerable<AngularMomentum> actual = new AngularMomentum[]
        {
            AngularMomentum.From(momentOfInertia, angularSpeed),
            momentOfInertia.Multiply(angularSpeed),
            momentOfInertia * angularSpeed,
            angularSpeed.Multiply(momentOfInertia),
            angularSpeed * momentOfInertia
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MomentOfInertiaAngularSpeedEquivalence()
    {
        yield return new object[] { 4 * MomentOfInertia.OneKilogramSquareMetre, 7 * AngularSpeed.OneRadianPerSecond, 28 * AngularMomentum.OneKilogramSquareMetrePerSecond };
    }
}
