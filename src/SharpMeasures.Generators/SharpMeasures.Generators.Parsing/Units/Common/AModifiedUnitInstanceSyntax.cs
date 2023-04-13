namespace SharpMeasures.Generators.Parsing.Units.Common;

using Microsoft.CodeAnalysis;

/// <summary>An abstract <see cref="IModifiedUnitInstanceSyntax"/>.</summary>
internal abstract record class AModifiedUnitInstanceSyntax : AUnitInstanceSyntax, IModifiedUnitInstanceSyntax
{
    private Location OriginalUnitInstance { get; }

    /// <summary>Instantiates a <see cref="AModifiedUnitInstanceSyntax"/>, representing syntactical information about a parsed attribute describing an instance of a unit as a modified form of another unit instance.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IModifiedUnitInstanceSyntax.OriginalUnitInstance" path="/summary"/></param>
    protected AModifiedUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance) : base(name, pluralForm)
    {
        OriginalUnitInstance = originalUnitInstance;
    }

    Location IModifiedUnitInstanceSyntax.OriginalUnitInstance => OriginalUnitInstance;
}
