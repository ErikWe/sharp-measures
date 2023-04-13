namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures vector quantities, allowing the names of the Cartesian components to be customized.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class VectorComponentNamesAttribute : Attribute
{
    /// <summary>The names of the Cartesian components.</summary>
    public string[] Names { get; }

    /// <inheritdoc cref="VectorComponentNamesAttribute"/>
    /// <param name="names"><inheritdoc cref="Names" path="/summary"/></param>
    public VectorComponentNamesAttribute(params string[] names)
    {
        Names = names;
    }
}
