namespace SharpMeasures.Generators.Parsing.Units.AliasedUnitInstanceAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IAliasedUnitInstanceSyntax"/>
internal sealed record class AliasedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IAliasedUnitInstanceSyntax
{
    /// <summary>Instantiates a <see cref="AliasedUnitInstanceSyntax"/>, representing syntactical information about a parsed <see cref="UnitInstanceAliasAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IModifiedUnitInstanceSyntax.OriginalUnitInstance" path="/summary"/></param>
    public AliasedUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance) : base(name, pluralForm, originalUnitInstance) { }
}
