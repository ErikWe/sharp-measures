namespace SharpMeasures.Generators.Parsing.Units.DerivedUnitInstanceAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

using System.Collections.Generic;

/// <inheritdoc cref="IDerivedUnitInstanceSyntax"/>
internal sealed record class DerivedUnitInstanceSyntax : AUnitInstanceSyntax, IDerivedUnitInstanceSyntax
{
    private Location DerivationID { get; }
    private Location UnitsCollection { get; }
    private IReadOnlyList<Location> UnitsElements { get; }

    /// <summary>Instantiates a <see cref="DerivedUnitInstanceSyntax"/>, representing syntactical information about a parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    /// <param name="derivationID"><inheritdoc cref="IDerivedUnitInstanceSyntax.DerivationID" path="/summary"/></param>
    /// <param name="unitsCollection"><inheritdoc cref="IDerivedUnitInstanceSyntax.UnitsCollection" path="/summary"/></param>
    /// <param name="unitsElements"><inheritdoc cref="IDerivedUnitInstanceSyntax.UnitsElements" path="/summary"/></param>
    public DerivedUnitInstanceSyntax(Location name, Location pluralForm, Location derivationID, Location unitsCollection, IReadOnlyList<Location> unitsElements) : base(name, pluralForm)
    {
        DerivationID = derivationID;

        UnitsCollection = unitsCollection;
        UnitsElements = unitsElements;
    }

    Location IDerivedUnitInstanceSyntax.DerivationID => DerivationID;
    Location IDerivedUnitInstanceSyntax.UnitsCollection => UnitsCollection;
    IReadOnlyList<Location> IDerivedUnitInstanceSyntax.UnitsElements => UnitsElements;
}
