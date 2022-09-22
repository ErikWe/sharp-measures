namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticRules
{
    public static readonly DiagnosticDescriptor DefineQuantityDefaultUnitInstanceName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DefineQuantityUnitAndSymbol,
        title: "Expected both unit name and symbol",
        messageFormat: "The symbol for the default unit of {0} was specified, but not the default unit",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DefineQuantityDefaultUnitInstanceSymbol = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DefineQuantityUnitAndSymbol,
        title: "Expected both unit name and symbol",
        messageFormat: "The name of the default unit of {0} was specified, but not the associated symbol",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor QuantityGroupMissingRoot = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityGroupMissingRoot,
        title: "Quantity group missing root quantity",
        messageFormat: "Could not identify the root of the group of associated quantities. Exactly one quantity in the group should be decorated with the attribute {0}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
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

    public static readonly DiagnosticDescriptor InvalidConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "\"{0}\" can not be used as the name of a constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantName,
        title: "Invalid name of constant",
        messageFormat: "The name of the constant must be defined",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: $"\"{{0}}\" can not be used to construct the name for multiples of \"{{1}}\". Use the default value, write the name in full, or use a suitable notation from {typeof(CommonPluralNotation).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidConstantMultiplesName,
        title: "Invalid name for multiples of constant",
        messageFormat: $"The name for multiples of \"{{0}}\" must be defined. Use the default value, write the name in full, or use a suitable notation from {typeof(CommonPluralNotation).FullName}.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name of constant",
        messageFormat: "{0} already defines a constant \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantNameReservedByConstantMultiples = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name of constant",
        messageFormat: "{0} already defines a constant with multiples \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateConstantMultiplesName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name for multiples of constant",
        messageFormat: "{0} already defines a constant with multiples \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantMultiplesNameReservedByConstantName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Duplicate name for multiples of constant",
        messageFormat: "{0} already defines a constant \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor IdenticalConstantNameAndConstantMultiples = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateConstantName,
        title: "Constant uses same name for multiples",
        messageFormat: "\"{0}\" is used for both the name of a constant and for multiples of that constant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantSharesNameWithUnitInstance = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ConstantSharesNameWithUnit,
        title: "Constant shares name with unit",
        messageFormat: "{0} already associates \"{1}\" with a unit",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ConstantMultiplesDisabledButNameSpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ConstantMultiplesDisabledButNameSpecified,
        title: "Constant multiples is disabled",
        messageFormat: "The name for multiples of constant \"{0}\" was specified, but multiples was explicitly disabled. Enable multiples, or do not specify the name.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor QuantityConvertibleToSelf = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityConvertibleToSelf,
        title: "Quantity convertible to itself",
        messageFormat: "{0} is marked as convertible to itself, which is not supported",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ContradictoryAttributes = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ContradictoryAttributes,
        title: "Contradictory attributes",
        messageFormat: "Contradictory attributes {0} and {1} were used, making {2} redundant",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor IncludingAlreadyIncludedUnitInstanceWithIntersection = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: $"Including \"{{0}}\" had no effect, as the unit was already included",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor IncludingAlreadyIncludedUnitInstanceWithUnion = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: $"Including \"{{0}}\" had no effect, as the unit was already included. To modify the set of included units, change the stacking mode to {typeof(InclusionStackingMode).FullName}.{nameof(InclusionStackingMode.Intersection)} - or instead use {{1}}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor ExcludingAlreadyExcludedUnitInstance = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: "Excluding \"{0}\" had no effect, as the unit was already excluded",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor IncludingExcludedUnitInstance = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InclusionOrExclusionHadNoEffect,
        title: "Inclusion or exclusion had no effect",
        messageFormat: $"Could not include \"{{0}}\" as the unit was already excluded. To include an excluded unit, change the stacking mode to {typeof(InclusionStackingMode).FullName}.{nameof(InclusionStackingMode.Union)}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor UnionInclusionStackingModeRedundant = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnionInclusionStackingModeRedundant,
        title: "Union inclusion stacking mode is redundant",
        messageFormat: $"As {{0}} includes all units, using {typeof(InclusionStackingMode).FullName}.{nameof(InclusionStackingMode.Union)} is redundant",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor DerivationOperatorsIncompatibleExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DerivationOperatorsIncompatibleExpression,
        title: "Expression cannot be implemented with operators",
        messageFormat: "Quantity derivation through operators require the expression to involve at most two quantities, and one of the trivial operators { +, -, *, / }. Modify the expression or disable operator implementation.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnmatchedDerivationExpressionQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnmatchedDerivationExpressionQuantity,
        title: "Unmatched derivation expression quantity",
        messageFormat: "The signature does not contain a quantity with index {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ExpressionDoesNotIncludeQuantity = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ExpressionDoesNotIncludeQuantity,
        title: "Quantity is not included in expression",
        messageFormat: $"The expression does not include the quantity with index {{0}}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor MalformedDerivationExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.MalformedDerivationExpression,
        title: "Expression could not be interpreted",
        messageFormat: "The expression could not be interpreted - separate each element by one of the operators { +, -, *, /, ., x }",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DerivationExpressionContainsConstant = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DerivationExpressionContainsConstant,
        title: "Expression contains constant",
        messageFormat: "The expression may not contain constants",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor IncompatibleQuantitiesInDerivation = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.IncompatibleQuantitiesInDerivation,
        title: "Derivation contains incompatible quantities",
        messageFormat: "The combination of expression and signature does not yield a valid result",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DerivationUnexpectedlyResultInScalar = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnexpectedResultFromDerivation,
        title: "Derivation unexpectedly results in a scalar quantity",
        messageFormat: "The derivation results in a scalar quantity, but a vector quantity was expected",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DerivationUnexpectedlyResultInVector = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnexpectedResultFromDerivation,
        title: "Derivation unexpectedly results in a vector quantity",
        messageFormat: "The derivation results in a vector quantity, but a scalar quantity was expected",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DerivationResultsInNonOverlappingDimension = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnexpectedResultFromDerivation,
        title: "Unexpected dimension of result from derivation",
        messageFormat: "The dimension of the resulting quantity never overlaps with the dimensions supported by {0}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
