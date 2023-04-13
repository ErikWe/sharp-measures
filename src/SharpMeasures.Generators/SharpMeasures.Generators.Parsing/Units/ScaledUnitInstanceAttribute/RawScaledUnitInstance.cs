namespace SharpMeasures.Generators.Parsing.Units.ScaledUnitInstanceAttribute;

using OneOf;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IRawScaledUnitInstance"/>
internal sealed record class RawScaledUnitInstance : ARawModifiedUnitInstance<IScaledUnitInstanceSyntax>, IRawScaledUnitInstance
{
    private OneOf<double, string?> Scale { get; }

    /// <summary>Instantiates a <see cref="RawScaledUnitInstance"/>, representing a parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IRawModifiedUnitInstance.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="scale"><inheritdoc cref="IRawScaledUnitInstance.Scale" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawScaledUnitInstance.Syntax" path="/summary"/></param>
    public RawScaledUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<double, string?> scale, IScaledUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax)
    {
        Scale = scale;
    }

    OneOf<double, string?> IRawScaledUnitInstance.Scale => Scale;

    IScaledUnitInstanceSyntax? IRawScaledUnitInstance.Syntax => Syntax;
}
