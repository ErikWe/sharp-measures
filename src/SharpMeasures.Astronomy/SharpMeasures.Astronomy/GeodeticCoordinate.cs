namespace SharpMeasures.Astronomy;

/// <summary>Expresses a point in space using geodetic coordinates.</summary>
/// <remarks>A geodetic coordinate system is defined using a reference ellipsoid. A point is then defined by three quantities:
/// <list type="number">
/// <item><inheritdoc cref="Longitude" path="/summary"/></item>
/// <item><inheritdoc cref="Latitude" path="/summary"/></item>
/// <item><inheritdoc cref="Height" path="/summary"/></item>
/// </list>
/// </remarks>
public readonly record struct GeodeticCoordinate
{
    /// <summary>The <see cref="Astronomy.Longitude"/> of the point, describing the horizontal <see cref="Angle"/> between the point and the prime meridian.</summary>
    public Longitude Longitude { get; }
    /// <summary>The <see cref="Astronomy.Latitude"/> of the point, describing the vertical <see cref="Angle"/> between the surface normal of the reference ellipsoid at the point, and the equator. A negative <see cref="Astronomy.Latitude"/> indicates a point below the equator.</summary>
    public Latitude Latitude { get; }
    /// <summary>The <see cref="SharpMeasures.Height"/> of the point, describing the distance between the point and the surface of the reference ellipsoid. A negative <see cref="SharpMeasures.Height"/> describes a point below the surface of the ellipsoid.</summary>
    public Height Height { get; }

    /// <summary>Constructs a new <see cref="GeodeticCoordinate"/> representing { <paramref name="longitude"/>, <paramref name="latitude"/>, <paramref name="height"/> }.</summary>
    /// <param name="latitude"><inheritdoc cref="Latitude" path="/summary"/></param>
    /// <param name="longitude"><inheritdoc cref="Longitude" path="/summary"/></param>
    /// <param name="height"><inheritdoc cref="Height" path="/summary"/></param>
    public GeodeticCoordinate(Longitude longitude, Latitude latitude, Height height)
    {
        Longitude = longitude;
        Latitude = latitude;
        Height = height;
    }
}
