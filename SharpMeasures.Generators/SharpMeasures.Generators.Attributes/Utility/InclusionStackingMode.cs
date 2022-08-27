namespace SharpMeasures.Generators.Utility;

/// <summary>Determines how inclusion filters behave when stacked.</summary>
public enum InclusionStackingMode
{
    /// <summary>Uses the union of the filters. Any item listed in at least one filter will be included.</summary>
    Union,
    /// <summary>Uses the intersection of the filters. Only items listed in all filters will be included.</summary>
    Intersection
}
