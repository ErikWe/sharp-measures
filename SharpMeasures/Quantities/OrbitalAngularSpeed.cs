namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularSpeed
{
    /// <summary>Computes average <see cref="OrbitalAngularSpeed"/> according to { <paramref name="angle"/> / <paramref name="time"/> }, where <paramref name="angle"/> is the change in
    /// <see cref="Angle"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static OrbitalAngularSpeed From(Angle angle, Time time) => new(angle.Magnitude / time.Magnitude);

    /// <summary>Computes <see cref="OrbitalAngularMomentum"/> according to { <see langword="this"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public OrbitalAngularMomentum Multiply(MomentOfInertia momentOfInertia) => OrbitalAngularMomentum.From(momentOfInertia, this);
    /// <summary>Computes <see cref="OrbitalAngularMomentum"/> according to { <paramref name="orbitalAngularSpeed"/> ∙ <paramref name="momentOfInertia"/>  }.</summary>
    public static OrbitalAngularMomentum operator *(OrbitalAngularSpeed orbitalAngularSpeed, MomentOfInertia momentOfInertia) => orbitalAngularSpeed.Multiply(momentOfInertia);

    /// <summary>Computes average <see cref="OrbitalAngularAcceleration"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public OrbitalAngularAcceleration Divide(Time time) => OrbitalAngularAcceleration.From(this, time);
    /// <summary>Computes average <see cref="OrbitalAngularAcceleration"/> according to { <paramref name="orbitalAngularSpeed"/> / <paramref name="time"/> }.</summary>
    public static OrbitalAngularAcceleration operator /(OrbitalAngularSpeed orbitalAngularSpeed, Time time) => orbitalAngularSpeed.Divide(time);
}
