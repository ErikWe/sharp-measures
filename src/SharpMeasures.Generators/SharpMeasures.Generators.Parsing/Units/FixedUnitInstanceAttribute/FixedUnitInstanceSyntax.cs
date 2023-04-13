namespace SharpMeasures.Generators.Parsing.Units.FixedUnitInstanceAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IFixedUnitInstanceSyntax"/>
internal sealed record class FixedUnitInstanceSyntax : AUnitInstanceSyntax, IFixedUnitInstanceSyntax
{
    /// <summary>Instantiates a <see cref="FixedUnitInstanceSyntax"/>, representing syntactical information about a parsed <see cref="FixedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    public FixedUnitInstanceSyntax(Location name, Location pluralForm) : base(name, pluralForm) { }
}
