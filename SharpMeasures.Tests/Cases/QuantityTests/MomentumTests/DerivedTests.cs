namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentumTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassVelocityEquivalence))]
    public void MassVelocity_ShouldBeEquivalent(Mass mass, Speed speed, Momentum expected)
    {
        IEnumerable<Momentum> actual = new Momentum[]
        {
            Momentum.From(mass, speed),
            mass.Multiply(speed),
            mass * speed,
            speed.Multiply(mass),
            speed * mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassVelocityEquivalence()
    {
        yield return new object[] { 4 * Mass.OneGram, 7 * Speed.OneMetrePerSecond, 0.028 * Momentum.OneKilogramMetrePerSecond };
    }
}
