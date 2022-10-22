namespace SharpMeasures.Astronomy;

/// <summary>Expresses a point on the celestial sphere using horizontal coordinates. Additionally, the <see cref="SharpMeasures.Distance"/> to the point is used to define the point in 3-dimensional space.</summary>
/// <remarks>A horizontal coordinate system is defined using the local horizon. A point on the celestial sphere is then defined by two quantities:
/// <list type="number">
/// <item><inheritdoc cref="Azimuth" path="/summary"/></item>
/// <item><inheritdoc cref="Elevation" path="/summary"/></item>
/// </list>
/// </remarks>
public readonly record struct HorizontalCoordinate
{
    /// <summary>The <see cref="Astronomy.Azimuth"/> of the point, describing the horizontal <see cref="Angle"/> between the point and a reference direction - typically north.</summary>
    public Azimuth Azimuth { get; }
    /// <summary>The <see cref="ElevationAngle"/> of the point, describing the vertical <see cref="Angle"/> between the point and the local horizon. A negative <see cref="ElevationAngle"/> indicates a point below the local horizon.</summary>
    public ElevationAngle Elevation { get; }
    /// <summary>The <see cref="SharpMeasures.Distance"/> to the point from the origin.</summary>
    public Distance Distance { get; }

    /// <summary>Computes the <see cref="Astronomy.Inclination"/> of the point, equivalent to { 90 ⋅ <see cref="Angle.OneDegree"/> - <see cref="Elevation"/> }. The <see cref="Astronomy.Inclination"/> represents the vertical <see cref="Angle"/> between the point and zenith.</summary>
    public Inclination Inclination => Elevation.AsInclination();

    /// <summary>Constructs a new <see cref="HorizontalCoordinate"/> representing { <paramref name="azimuth"/>, <paramref name="elevation"/>, <paramref name="distance"/> }.</summary>
    /// <param name="azimuth"><inheritdoc cref="Azimuth" path="/summary"/></param>
    /// <param name="elevation"><inheritdoc cref="Elevation" path="/summary"/></param>
    /// <param name="distance"><inheritdoc cref="Distance" path="/summary"/></param>
    public HorizontalCoordinate(Azimuth azimuth, ElevationAngle elevation, Distance distance)
    {
        Azimuth = azimuth;
        Elevation = elevation;
        Distance = distance;
    }

    /// <summary>Constructs a new <see cref="HorizontalCoordinate"/> representing { <paramref name="azimuth"/>, <paramref name="inclination"/>, <paramref name="distance"/> }.</summary>
    /// <param name="azimuth"><inheritdoc cref="Azimuth" path="/summary"/></param>
    /// <param name="inclination">The <see cref="Astronomy.Inclination"/> of the point, describing the vertical angle between the point and zenith.</param>
    /// <param name="distance"><inheritdoc cref="Distance" path="/summary"/></param>
    public HorizontalCoordinate(Azimuth azimuth, Inclination inclination, Distance distance)
    {
        Azimuth = azimuth;
        Elevation = inclination.AsElevation();
        Distance = distance;
    }
}
