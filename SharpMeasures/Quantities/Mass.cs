namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Mass
{
    /// <summary>Computes <see cref="Mass"/> according to { <paramref name="density"/> ∙ <paramref name="volume"/> },
    /// where <paramref name="density"/> is the average <see cref="Density"/> of an object with <see cref="Volume"/> <paramref name="volume"/>.</summary>
    public static Mass From(Density density, Volume volume) => new(density.Magnitude * volume.Magnitude);

    /// <summary>Computes <see cref="Momentum"/> according to { <see langword="this"/> ∙ <paramref name="speed"/> }.</summary>
    public Momentum Multiply(Speed speed) => Momentum.From(this, speed);
    /// <summary>Computes <see cref="Momentum"/> according to { <paramref name="mass"/> ∙ <paramref name="speed"/> }.</summary>
    public static Momentum operator *(Mass mass, Speed speed) => mass.Multiply(speed);

    /// <summary>Computes <see cref="Momentum3"/> according to { <see langword="this"/> ∙ <paramref name="velocity"/> }.</summary>
    public Momentum3 Multiply(Velocity3 velocity) => Momentum3.From(this, velocity);
    /// <summary>Computes <see cref="Momentum3"/> according to { <paramref name="mass"/> ∙ <paramref name="velocity"/> }.</summary>
    public static Momentum3 operator *(Mass mass, Velocity3 velocity) => mass.Multiply(velocity);

    /// <summary>Computes <see cref="Force"/> according to { <see langword="this"/> ∙ <paramref name="acceleration"/> }.</summary>
    public Force Multiply(Acceleration acceleration) => Force.From(this, acceleration);
    /// <summary>Computes <see cref="Force"/> according to { <paramref name="mass"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Force operator *(Mass mass, Acceleration acceleration) => mass.Multiply(acceleration);

    /// <summary>Computes <see cref="Force3"/> according to { <see langword="this"/> ∙ <paramref name="acceleration"/> }.</summary>
    public Force3 Multiply(Acceleration3 acceleration) => Force3.From(this, acceleration);
    /// <summary>Computes <see cref="Force3"/> according to { <paramref name="mass"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Force3 operator *(Mass mass, Acceleration3 acceleration) => mass.Multiply(acceleration);

    /// <summary>Computes <see cref="Weight"/> according to { <see langword="this"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public Weight Multiply(GravitationalAcceleration gravitationalAcceleration) => Weight.From(this, gravitationalAcceleration);
    /// <summary>Computes <see cref="Weight"/> according to { <paramref name="mass"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public static Weight operator *(Mass mass, GravitationalAcceleration gravitationalAcceleration) => mass.Multiply(gravitationalAcceleration);

    /// <summary>Computes <see cref="Weight3"/> according to { <see langword="this"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public Weight3 Multiply(GravitationalAcceleration3 gravitationalAcceleration) => Weight3.From(this, gravitationalAcceleration);
    /// <summary>Computes <see cref="Weight3"/> according to { <paramref name="mass"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public static Weight3 operator *(Mass mass, GravitationalAcceleration3 gravitationalAcceleration) => mass.Multiply(gravitationalAcceleration);

    /// <summary>Computes average <see cref="Density"/> according to { <see langword="this"/> / <paramref name="volume"/> }.</summary>
    public Density Divide(Volume volume) => Density.From(this, volume);
    /// <summary>Computes average <see cref="Density"/> according to { <paramref name="mass"/> / <paramref name="volume"/> }.</summary>
    public static Density operator /(Mass mass, Volume volume) => mass.Divide(volume);

    /// <summary>Computes <see cref="Volume"/> according to { <see langword="this"/> / <paramref name="density"/> }.</summary>
    public Volume Divide(Density density) => Volume.From(this, density);
    /// <summary>Computes average <see cref="Volume"/> according to { <paramref name="mass"/> / <paramref name="density"/> }.</summary>
    public static Volume operator /(Mass mass, Density density) => mass.Divide(density);

    /// <summary>Computes average <see cref="ArealDensity"/> according to { <see langword="this"/> / <paramref name="area"/> }.</summary>
    public ArealDensity Divide(Area area) => ArealDensity.From(this, area);
    /// <summary>Computes average <see cref="ArealDensity"/> according to { <paramref name="mass"/> / <paramref name="area"/> }.</summary>
    public static ArealDensity operator /(Mass mass, Area area) => mass.Divide(area);

    /// <summary>Computes <see cref="Area"/> according to { <see langword="this"/> / <paramref name="arealDensity"/> }.</summary>
    public Area Divide(ArealDensity arealDensity) => Area.From(this, arealDensity);
    /// <summary>Computes average <see cref="Area"/> according to { <paramref name="mass"/> / <paramref name="arealDensity"/> }.</summary>
    public static Area operator /(Mass mass, ArealDensity arealDensity) => mass.Divide(arealDensity);

    /// <summary>Computes average <see cref="LinearDensity"/> according to { <see langword="this"/> / <paramref name="length"/> }.</summary>
    public LinearDensity Divide(Length length) => LinearDensity.From(this, length);
    /// <summary>Computes average <see cref="LinearDensity"/> according to { <paramref name="mass"/> / <paramref name="length"/> }.</summary>
    public static LinearDensity operator /(Mass mass, Length length) => mass.Divide(length);

    /// <summary>Computes <see cref="Length"/> according to { <see langword="this"/> / <paramref name="linearDensity"/> }.</summary>
    public Length Divide(LinearDensity linearDensity) => Length.From(this, linearDensity);
    /// <summary>Computes average <see cref="Length"/> according to { <paramref name="mass"/> / <paramref name="linearDensity"/> }.</summary>
    public static Length operator /(Mass mass, LinearDensity linearDensity) => mass.Divide(linearDensity);

    /// <summary>Computes average <see cref="MassFlowRate"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public MassFlowRate Divide(Time time) => MassFlowRate.From(this, time);
    /// <summary>Computes average <see cref="MassFlowRate"/> according to { <paramref name="mass"/> / <paramref name="time"/> }.</summary>
    public static MassFlowRate operator /(Mass mass, Time time) => mass.Divide(time);
}
