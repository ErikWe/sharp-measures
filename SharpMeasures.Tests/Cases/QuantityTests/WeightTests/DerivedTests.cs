namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.WeightTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassGravitationalAccelerationEquivalence))]
    public void MassGravitationalAcceleration_ShouldBeEquivalent(Mass mass, GravitationalAcceleration gravitationalAcceleration, Weight expected)
    {
        IEnumerable<Weight> actual = new Weight[]
        {
            Weight.From(mass, gravitationalAcceleration),
            mass.Multiply(gravitationalAcceleration),
            mass * gravitationalAcceleration,
            gravitationalAcceleration.Multiply(mass),
            gravitationalAcceleration * mass
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassGravitationalAccelerationEquivalence()
    {
        yield return new object[] { 3 * Mass.OneGram, 14 * GravitationalAcceleration.OneMetrePerSecondSquared, 0.042 * Weight.OneNewton };
    }
}
