namespace SharpMeasures.Generators.Parsing.Units.BiasedUnitInstanceAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IBiasedUnitInstanceSyntax"/>
internal sealed record class BiasedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IBiasedUnitInstanceSyntax
{
    private Location Bias { get; }

    /// <summary>Instantiates a <see cref="BiasedUnitInstanceSyntax"/>, representing syntactical information about a parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IModifiedUnitInstanceSyntax.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="bias"><inheritdoc cref="IBiasedUnitInstanceSyntax.Bias" path="/summary"/></param>
    public BiasedUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance, Location bias) : base(name, pluralForm, originalUnitInstance)
    {
        Bias = bias;
    }

    Location IBiasedUnitInstanceSyntax.Bias => Bias;
}
