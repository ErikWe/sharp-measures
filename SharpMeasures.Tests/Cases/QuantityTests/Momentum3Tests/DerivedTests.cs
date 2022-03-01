namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Momentum3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassVelocityEquivalence))]
    public void MassVelocity_ShouldBeEquivalent(Mass mass, Velocity3 velocity, Momentum3 expected)
    {
        IEnumerable<Momentum3> actual = new Momentum3[]
        {
            Momentum3.From(mass, velocity),
            mass.Multiply(velocity),
            mass * velocity,
            velocity.Multiply(mass),
            velocity * mass
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> MassVelocityEquivalence()
    {
        yield return new object[] { 4 * Mass.OneGram, (1, 5, -1) * Speed.OneMetrePerSecond, (0.004, 0.02, -0.004) * Momentum.OneKilogramMetrePerSecond };
    }
}
