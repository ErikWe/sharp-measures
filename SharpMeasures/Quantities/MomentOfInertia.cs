namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct MomentOfInertia
{
    /// <summary>Computes <see cref="MomentOfInertia"/> according to { <paramref name="angularMomentum"/> / <paramref name="angularSpeed"/> }.</summary>
    public static MomentOfInertia From(AngularMomentum angularMomentum, AngularSpeed angularSpeed) => new(angularMomentum.Magnitude / angularSpeed.Magnitude);

    /// <summary>Computes <see cref="MomentOfInertia"/> of a point object according to { <paramref name="mass"/> ∙ <paramref name="distance"/>² },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object at <see cref="Distance"/> <paramref name="distance"/> from the pivot.</summary>
    public static MomentOfInertia From(Mass mass, Distance distance) => new(mass.Magnitude * Math.Pow(distance.Magnitude, 2));

    /// <summary>Computes <see cref="AngularMomentum"/> according to { <see langword="this"/> ∙ <paramref name="angularSpeed"/> }.</summary>
    public AngularMomentum Multiply(AngularSpeed angularSpeed) => AngularMomentum.From(this, angularSpeed);
    /// <summary>Computes <see cref="AngularMomentum"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="angularSpeed"/> }.</summary>
    public static AngularMomentum operator *(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed) => momentOfInertia.Multiply(angularSpeed);

    /// <summary>Computes <see cref="OrbitalAngularMomentum"/> according to { <see langword="this"/> ∙ <paramref name="orbitalAngularSpeed"/> }.</summary>
    public OrbitalAngularMomentum Multiply(OrbitalAngularSpeed orbitalAngularSpeed) => OrbitalAngularMomentum.From(this, orbitalAngularSpeed);
    /// <summary>Computes <see cref="OrbitalAngularMomentum"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="orbitalAngularSpeed"/> }.</summary>
    public static OrbitalAngularMomentum operator *(MomentOfInertia momentOfInertia, OrbitalAngularSpeed orbitalAngularSpeed) => momentOfInertia.Multiply(orbitalAngularSpeed);

    /// <summary>Computes <see cref="SpinAngularMomentum"/> according to { <see langword="this"/> ∙ <paramref name="spinAngularSpeed"/> }.</summary>
    public SpinAngularMomentum Multiply(SpinAngularSpeed spinAngularSpeed) => SpinAngularMomentum.From(this, spinAngularSpeed);
    /// <summary>Computes <see cref="SpinAngularMomentum"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="spinAngularSpeed"/> }.</summary>
    public static SpinAngularMomentum operator *(MomentOfInertia momentOfInertia, SpinAngularSpeed spinAngularSpeed) => momentOfInertia.Multiply(spinAngularSpeed);

    /// <summary>Computes <see cref="AngularMomentum3"/> according to { <see langword="this"/> ∙ <paramref name="angularVelocity"/> }.</summary>
    public AngularMomentum3 Multiply(AngularVelocity3 angularVelocity) => AngularMomentum3.From(this, angularVelocity);
    /// <summary>Computes <see cref="AngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="angularVelocity"/> }.</summary>
    public static AngularMomentum3 operator *(MomentOfInertia momentOfInertia, AngularVelocity3 angularVelocity) => momentOfInertia.Multiply(angularVelocity);

    /// <summary>Computes <see cref="OrbitalAngularMomentum3"/> according to { <see langword="this"/> ∙ <paramref name="orbitalAngularVelocity"/> }.</summary>
    public OrbitalAngularMomentum3 Multiply(OrbitalAngularVelocity3 orbitalAngularVelocity) => OrbitalAngularMomentum3.From(this, orbitalAngularVelocity);
    /// <summary>Computes <see cref="OrbitalAngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="orbitalAngularVelocity"/> }.</summary>
    public static OrbitalAngularMomentum3 operator *(MomentOfInertia momentOfInertia, OrbitalAngularVelocity3 orbitalAngularVelocity)
        => momentOfInertia.Multiply(orbitalAngularVelocity);

    /// <summary>Computes <see cref="SpinAngularMomentum3"/> according to { <see langword="this"/> ∙ <paramref name="spinAngularVelocity"/> }.</summary>
    public SpinAngularMomentum3 Multiply(SpinAngularVelocity3 spinAngularVelocity) => SpinAngularMomentum3.From(this, spinAngularVelocity);
    /// <summary>Computes <see cref="SpinAngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="spinAngularVelocity"/> }.</summary>
    public static SpinAngularMomentum3 operator *(MomentOfInertia momentOfInertia, SpinAngularVelocity3 spinAngularVelocity) => momentOfInertia.Multiply(spinAngularVelocity);
}
