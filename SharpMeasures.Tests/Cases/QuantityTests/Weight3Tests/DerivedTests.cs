namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Weight3Tests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassGravitationalAccelerationEquivalence))]
    public void MassGravitationalAcceleration_ShouldBeEquivalent(Mass mass, GravitationalAcceleration3 gravitationalAcceleration, Weight3 expected)
    {
        IEnumerable<Weight3> actual = new Weight3[]
        {
            Weight3.From(mass, gravitationalAcceleration),
            mass.Multiply(gravitationalAcceleration),
            mass * gravitationalAcceleration,
            gravitationalAcceleration.Multiply(mass),
            gravitationalAcceleration * mass
        };

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    public static IEnumerable<object[]> MassGravitationalAccelerationEquivalence()
    {
        yield return new object[] { 3 * Mass.OneGram, (37, 14, -3) * GravitationalAcceleration.OneMetrePerSecondSquared, (0.111, 0.042, -0.009) * Weight.OneNewton };
    }
}
