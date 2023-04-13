namespace SharpMeasures.Generators.Parsing.Units.PrefixedUnitInstanceAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IPrefixedUnitInstanceSyntax"/>
internal sealed record class PrefixedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IPrefixedUnitInstanceSyntax
{
    private Location Prefix { get; }

    /// <summary>Instantiates a <see cref="PrefixedUnitInstanceSyntax"/>, representing syntactical information about a parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IModifiedUnitInstanceSyntax.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="prefix"><inheritdoc cref="IPrefixedUnitInstanceSyntax.Prefix" path="/summary"/></param>
    public PrefixedUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance, Location prefix) : base(name, pluralForm, originalUnitInstance)
    {
        Prefix = prefix;
    }

    Location IPrefixedUnitInstanceSyntax.Prefix => Prefix;
}
