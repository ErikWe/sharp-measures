namespace SharpMeasures.Generators.Parsing.Units.Common;

/// <summary>An abstract <see cref="IRawModifiedUnitInstance"/>.</summary>
/// <typeparam name="TSyntax">The type of the described <see cref="IModifiedUnitInstanceSyntax"/>.</typeparam>
internal abstract record class ARawModifiedUnitInstance<TSyntax> : ARawUnitInstance<TSyntax>, IRawModifiedUnitInstance where TSyntax : IModifiedUnitInstanceSyntax
{
    private string? OriginalUnitInstance { get; }

    /// <summary>Instantiates a <see cref="ARawModifiedUnitInstance{TSyntax}"/>, representing a parsed attribute describing a unit instance as a modified form of another unit instance.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IRawModifiedUnitInstance.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawModifiedUnitInstance.Syntax" path="/summary"/></param>
    protected ARawModifiedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, TSyntax? syntax) : base(name, pluralForm, syntax)
    {
        OriginalUnitInstance = originalUnitInstance;
    }

    string? IRawModifiedUnitInstance.OriginalUnitInstance => OriginalUnitInstance;

    IModifiedUnitInstanceSyntax? IRawModifiedUnitInstance.Syntax => Syntax;
}
