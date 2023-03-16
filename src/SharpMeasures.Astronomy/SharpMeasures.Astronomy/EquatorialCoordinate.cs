namespace SharpMeasures.Astronomy;

/// <summary>Expresses a point on the celestial sphere using equatorial coordinates. Additionally, the <see cref="SharpMeasures.Distance"/> to the point is used to define the point in 3-dimensional space.</summary>
/// <remarks>An equatorial coordinate system is defined using the celestial equator and the vernal equinox, at a given epoch. A point on the celestial sphere is then defined by two quantities:
/// <list type="number">
/// <item><inheritdoc cref="RightAscension" path="/summary"/></item>
/// <item><inheritdoc cref="Declination" path="/summary"/></item>
/// </list>
/// </remarks>
public readonly record struct EquatorialCoordinate
{
    /// <summary>The <see cref="Astronomy.RightAscension"/> of the point, describing the horizontal <see cref="Angle"/> between the point and the vernal equinox.</summary>
    public RightAscension RightAscension { get; }
    /// <summary>The <see cref="Astronomy.Declination"/> of the point, describing the vertical <see cref="Angle"/> between the point and the celestial equator. A negative <see cref="Astronomy.Declination"/> indicates a point below the celestial equator.</summary>
    public Declination Declination { get; }
    /// <summary>The <see cref="SharpMeasures.Distance"/> to the point from the origin.</summary>
    public Distance Distance { get; }

    /// <summary>Constructs a new <see cref="RightAscension"/> representing { <paramref name="rightAscension"/>, <paramref name="declination"/>, <paramref name="distance"/> }.</summary>
    /// <param name="rightAscension"><inheritdoc cref="RightAscension" path="/summary"/></param>
    /// <param name="declination"><inheritdoc cref="Declination" path="/summary"/></param>
    /// <param name="distance"><inheritdoc cref="Distance" path="/summary"/></param>
    public EquatorialCoordinate(RightAscension rightAscension, Declination declination, Distance distance)
    {
        RightAscension = rightAscension;
        Declination = declination;
        Distance = distance;
    }
}
