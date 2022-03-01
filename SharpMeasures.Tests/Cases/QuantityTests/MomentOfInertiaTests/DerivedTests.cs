namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(AngularMomentumAngularSpeedEquivalence))]
    public void AngularMomentumAngularSpeed_ShouldBeEquivalent(AngularMomentum angularMomentum, AngularSpeed angularSpeed, MomentOfInertia expected)
    {
        IEnumerable<MomentOfInertia> actual = new MomentOfInertia[]
        {
            MomentOfInertia.From(angularMomentum, angularSpeed),
            angularMomentum.Divide(angularSpeed),
            angularMomentum / angularSpeed
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [MemberData(nameof(MassDistanceEquivalence))]
    public void MassDistance_ShouldBeEquivalent(Mass mass, Distance distance, MomentOfInertia expected)
    {
        IEnumerable<MomentOfInertia> actual = new MomentOfInertia[]
        {
            MomentOfInertia.From(mass, distance)
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> AngularMomentumAngularSpeedEquivalence()
    {
        yield return new object[] { 3 * AngularMomentum.OneKilogramSquareMetrePerSecond, 9 * AngularSpeed.OneRadianPerSecond, 3d / 9 * MomentOfInertia.OneKilogramSquareMetre };
    }

    public static IEnumerable<object[]> MassDistanceEquivalence()
    {
        yield return new object[] { 3 * Mass.OneGram, 9 * Distance.OneMetre, 0.003 * 9 * 9 * MomentOfInertia.OneKilogramSquareMetre };
    }
}
