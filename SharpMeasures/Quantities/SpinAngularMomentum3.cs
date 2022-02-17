namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpinAngularMomentum3
{
    /// <summary>Computes <see cref="SpinAngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="spinAngularVelocity"/> }.</summary>
    public static SpinAngularMomentum3 From(MomentOfInertia momentOfInertia, SpinAngularVelocity3 spinAngularVelocity)
        => new(momentOfInertia.Magnitude * spinAngularVelocity.Components);
}
