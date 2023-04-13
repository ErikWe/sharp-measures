namespace SharpMeasures.Generators.Parsing.Units.PrefixedUnitInstanceAttribute;

using OneOf;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IRawPrefixedUnitInstance"/>
internal sealed record class RawPrefixedUnitInstance : ARawModifiedUnitInstance<IPrefixedUnitInstanceSyntax>, IRawPrefixedUnitInstance
{
    private OneOf<MetricPrefixName, BinaryPrefixName> Prefix { get; }

    /// <summary>Instantiates a <see cref="RawPrefixedUnitInstance"/>, representing a parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IRawModifiedUnitInstance.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="prefix"><inheritdoc cref="IRawPrefixedUnitInstance.Prefix" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawPrefixedUnitInstance.Syntax" path="/summary"/></param>
    public RawPrefixedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<MetricPrefixName, BinaryPrefixName> prefix, IPrefixedUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax)
    {
        Prefix = prefix;
    }

    OneOf<MetricPrefixName, BinaryPrefixName> IRawPrefixedUnitInstance.Prefix => Prefix;

    IPrefixedUnitInstanceSyntax? IRawPrefixedUnitInstance.Syntax => Syntax;
}
