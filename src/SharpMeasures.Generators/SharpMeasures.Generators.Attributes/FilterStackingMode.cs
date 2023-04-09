namespace SharpMeasures;

/// <summary>Determines how filters behave when stacked.</summary>
public enum FilterStackingMode
{
    /// <summary>The <see cref="FilterStackingMode"/> is unknown.</summary>
    Unknown,
    /// <summary>Applies the filter to the result of the previous filter.</summary>
    Keep,
    /// <summary>Applies the filter to the restored, original, state - discarding all previous filters.</summary>
    Restore
}
