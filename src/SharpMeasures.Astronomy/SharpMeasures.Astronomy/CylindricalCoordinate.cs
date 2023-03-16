namespace SharpMeasures.Astronomy;

/// <summary>Expresses a point in space using cylindrical coordinates.</summary>
/// <remarks>A cylindrical coordinate system is defined by an origin, a longitudinal axis, and a polar axis orthogonal to the longitudinal axis. A point is then defined by three quantities:
/// <list type="number">
/// <item><inheritdoc cref="RadialDistance" path="/summary"/></item>
/// <item><inheritdoc cref="Azimuth" path="/summary"/></item>
/// <item><inheritdoc cref="Height" path="/summary"/></item>
/// </list>
/// </remarks>
public readonly record struct CylindricalCoordinate
{
    /// <summary>The <see cref="Distance"/> between the point and the longitudinal axis.</summary>
    public Distance RadialDistance { get; }
    /// <summary>The <see cref="Astronomy.Azimuth"/>, describing the horizontal <see cref="Angle"/> between the polar axis and the radial position of the point.</summary>
    public Azimuth Azimuth { get; }
    /// <summary>The <see cref="SharpMeasures.Height"/> of the point, describing the longitudinal position of the point relative to the origin. A negative <see cref="SharpMeasures.Height"/> describes a point below the origin.</summary>
    public Height Height { get; }

    /// <summary>Constructs a new <see cref="CylindricalCoordinate"/> representing { <paramref name="radialDistance"/>, <paramref name="azimuth"/>, <paramref name="height"/> }.</summary>
    /// <param name="radialDistance"><inheritdoc cref="RadialDistance" path="/summary"/></param>
    /// <param name="azimuth"><inheritdoc cref="Azimuth" path="/summary"/></param>
    /// <param name="height"><inheritdoc cref="Height" path="/summary"/></param>
    public CylindricalCoordinate(Distance radialDistance, Azimuth azimuth, Height height)
    {
        RadialDistance = radialDistance;
        Azimuth = azimuth;
        Height = height;
    }
}
