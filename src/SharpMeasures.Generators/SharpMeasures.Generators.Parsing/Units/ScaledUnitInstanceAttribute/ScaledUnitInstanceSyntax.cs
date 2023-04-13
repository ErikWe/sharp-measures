namespace SharpMeasures.Generators.Parsing.Units.ScaledUnitInstanceAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IScaledUnitInstanceSyntax"/>
internal sealed record class ScaledUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IScaledUnitInstanceSyntax
{
    private Location Scale { get; }

    /// <summary>Instantiates a <see cref="ScaledUnitInstanceSyntax"/>, representing syntactical information about a parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IModifiedUnitInstanceSyntax.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="scale"><inheritdoc cref="IScaledUnitInstanceSyntax.Scale" path="/summary"/></param>
    public ScaledUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance, Location scale) : base(name, pluralForm, originalUnitInstance)
    {
        Scale = scale;
    }

    Location IScaledUnitInstanceSyntax.Scale => Scale;
}
