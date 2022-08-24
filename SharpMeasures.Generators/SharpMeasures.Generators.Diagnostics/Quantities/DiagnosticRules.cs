namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Utility;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor DefineQuantityDefaultUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DefineQuantityUnitAndSymbol,
        title: "Expected both unit name and symbol",
        messageFormat: "The symbol for the default unit of {0} was specified, but not the name of the default unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DefineQuantityDefaultSymbol = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DefineQuantityUnitAndSymbol,
        title: "Expected both unit name and symbol",
        messageFormat: "The name of the default unit of {0} was specified, but not the associated symbol",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "\"{0}\" can not be used as the name of a constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "The name of the constant must be defined",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: "\"{0}\" can not be used to construct the name for multiples of \"{1}\". Use the default value, write the name in full, or use a suitable notation " +
            $"from {typeof(CommonConstantMultiplesPropertyNotations).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: "The name for multiples of \"{0}\" must be defined. Use the default value, write the name in full, or use a suitable notation " +
            $"from {typeof(CommonConstantMultiplesPropertyNotations).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name of constant",
        messageFormat: "{0} already defines a constant \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantNameReservedByConstantMultiples = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name of constant",
        messageFormat: "{0} already defines a constant with multiples \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name for multiples of constant",
        messageFormat: "{0} already defines a constant with multiples \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantMultiplesNameReservedByConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name for multiples of constant",
        messageFormat: "{0} already defines a constant \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor IdenticalConstantNameAndConstantMultiples = new DiagnosticDescriptor
        (
            id: DiagnosticIDs.DuplicateConstantName,
            title: "Constant uses same name for multiples",
            messageFormat: "\"{0}\" is used for both the name of a constant and for multiples of that constant",
            category: "Naming",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

    public static readonly DiagnosticDescriptor ConstantSharesNameWithUnit = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ConstantSharesNameWithUnit,
        title: "Constant shares name with unit",
        messageFormat: "{0} already associates \"{1}\" with a unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantMultiplesDisabledButNameSpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ConstantMultiplesDisabledButNameSpecified,
        title: "Constant multiples is disabled",
        messageFormat: "The name for multiples of constant \"{0}\" was specified, but generating the property was explicitly disabled. Enable the property, "
            + "or do not specify the name.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnrecognizedCastOperatorBehaviour = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnrecognizedCastOperatorBehaviour,
        title: "Unrecognized cast operator behaviour",
        messageFormat: $"{{0}} was not recognized as a {typeof(ConversionOperatorBehaviour).Name}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor QuantityConvertibleToSelf = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityConvertibleToSelf,
        title: "Quantity convertible to itself",
        messageFormat: "{0} is marked as convertible to itself, which is not supported",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ContradictoryAttributes = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ContradictoryAttributes,
        title: "Contradictory attributes",
        messageFormat: "Contradictory attributes {0} and {1} were used",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor QuantityGroupMissingRoot = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityGroupMissingRoot,
        title: "Quantity group missing root quantity",
        messageFormat: "Could not identify the root of the group of associated quantities. Exactly one quantity in the group should be decorated with the attribute {0}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnitAlreadyIncluded = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: "Including \"{0}\" had no effect, as \"{0}\" was already included",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnitNotIncluded = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: "Excluding \"{0}\" had no effect, as \"{0}\" was not included",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnitAlreadyExcluded = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: "Excluding \"{0}\" had no effect, as \"{0}\" was already excluded",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnitNotExcluded = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: "Including \"{0}\" had no effect, as \"{0}\" was not excluded",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnitAlreadyListed = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: "Including or excluding \"{0}\" had no effect, as \"{0}\" was already listed",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor DifferenceDisabledButQuantitySpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DifferenceDisabledButQuantitySpecified,
        title: "Difference is disabled but a quantity was specified",
        messageFormat: "{0} does not implement difference, but a quantity representing difference was specified. Enable difference or do not specify the quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );
}
