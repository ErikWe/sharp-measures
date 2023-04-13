namespace SharpMeasures.Generators.Parsing.Units.Common;

using Microsoft.CodeAnalysis;

/// <summary>An abstract <see cref="IUnitInstanceSyntax"/>.</summary>
internal abstract record class AUnitInstanceSyntax : IUnitInstanceSyntax
{
    private Location Name { get; }
    private Location PluralForm { get; }

    /// <summary>Instantiates a <see cref="AUnitInstanceSyntax"/>, representing syntactical information about a parsed attribute describing an instance of a unit.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    protected AUnitInstanceSyntax(Location name, Location pluralForm)
    {
        Name = name;
        PluralForm = pluralForm;
    }

    Location IUnitInstanceSyntax.Name => Name;
    Location IUnitInstanceSyntax.PluralForm => PluralForm;
}
