namespace SharpMeasures.Generators.Parsing.Units.DerivableUnitAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IDerivableUnitSyntax"/>
internal sealed record class DerivableUnitSyntax : IDerivableUnitSyntax
{
    private Location DerivationID { get; }
    private Location Expression { get; }
    private Location SignatureCollection { get; }
    private IReadOnlyList<Location> SignatureElements { get; }

    /// <summary>Instantiates a <see cref="DerivableUnitSyntax"/>, representing syntactial information about a parsed <see cref="DerivableUnitAttribute"/>.</summary>
    /// <param name="derivationID"><inheritdoc cref="IDerivableUnitSyntax.DerivationID" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="IDerivableUnitSyntax.Expression" path="/summary"/></param>
    /// <param name="signatureCollection"><inheritdoc cref="IDerivableUnitSyntax.SignatureCollection" path="/summary"/></param>
    /// <param name="signatureElements"><inheritdoc cref="IDerivableUnitSyntax.SignatureElements" path="/summary"/></param>
    public DerivableUnitSyntax(Location derivationID, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements)
    {
        DerivationID = derivationID;
        Expression = expression;
        SignatureCollection = signatureCollection;
        SignatureElements = signatureElements;
    }

    Location IDerivableUnitSyntax.DerivationID => DerivationID;
    Location IDerivableUnitSyntax.Expression => Expression;

    Location IDerivableUnitSyntax.SignatureCollection => SignatureCollection;
    IReadOnlyList<Location> IDerivableUnitSyntax.SignatureElements => SignatureElements;
}
