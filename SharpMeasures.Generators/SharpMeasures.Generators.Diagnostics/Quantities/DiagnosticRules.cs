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
        messageFormat: "Expected a name for the constant",
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

    public static readonly DiagnosticDescriptor InvalidExpressionForConstant = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidExpressionForConstant,
        title: "Invalid expression for constant",
        messageFormat: "The expression describing the value of the constant must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
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

    public static readonly DiagnosticDescriptor InvalidQuantityOperationName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidQuantityOperationName,
        title: "Invalid name of quantity operation",
        messageFormat: "Expected a name for the quantity operation. Provide a name, or use the default name.",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor QuantityOperationMethodDisabledButNameSpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityOperationMethodDisabledButNameSpecified,
        title: "Quantity operation name redundant",
        messageFormat: "The quantity operation is not implemented as a method, making the name redundant",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor DuplicateQuantityOperationOperator = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateQuantityOperation,
        title: "Duplicate quantity operation",
        messageFormat: "{0} already defines an identical operator",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateQuantityOperationMethod = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateQuantityOperation,
        title: "Duplicate quantity operation",
        messageFormat: "{0} already defines a quantity operation \"{1}\" with the same signature",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidQuantityOperation = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidQuantityOperation,
        title: "Invalid quantity operation",
        messageFormat: "The operation could not be interpreted. This could be caused by for example trying to add a scalar quantity and a vector quantity.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor QuantityOperationNotMirrorable = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityOperationNotMirrorable,
        title: "Quantity operation cannot be mirrored",
        messageFormat: "The operation cannot be mirrored. This is likely caused by a symmetric operation.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor QuantityOperationMethodNotMirrorable = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityOperationMethodNotMirrorable,
        title: "Quantity operation method cannot be mirrored",
        messageFormat: "The method describing the operation cannot be mirrored. This is likely caused by a symmetric or cummutative operation.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor QuantityOperationMirrorDisabledButNameSpecified = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.QuantityOperationMirrorDisabledButNameSpecified,
        title: "Quantity operation not mirrored",
        messageFormat: "The operation is not mirrored, making the mirrored name redundant",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        customTags: WellKnownDiagnosticTags.Unnecessary
    );

    public static readonly DiagnosticDescriptor InvalidProcessName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidProcessName,
        title: "Invalid name of quantity process",
        messageFormat: "Expected a name for the quantity process",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateProcessName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateProcessName,
        title: "Duplicate name of quantity process",
        messageFormat: "{0} already defines a process \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidProcessExpression = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidProcessExpression,
        title: "Invalid processing expression",
        messageFormat: "The expression describing the process must be defined",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor ProcessPropertyIncompatibleWithParameters = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.ProcessPropertyIncompatibleWithParameters,
        title: "Process with parameter cannot be a property",
        messageFormat: "As \"{0}\" defines parameters, it can not be defined as a property. Remove all parameters or do not define the process as a property.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor UnmatchedProcessParameterDefinitions = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.UnmatchedProcessParameterDefinitions,
        title: "Process has unmatched parameter definitions",
        messageFormat: "\"{0}\" specifies {1} parameter types, but {2} parameter names",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NullProcessParameterType = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.NullProcessParameterType,
        title: "Invalid process parameter type",
        messageFormat: "Expected a non-null type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor InvalidProcessParameterName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.InvalidProcessParameterName,
        title: "Invalid name of process parameter",
        messageFormat: "The name of the process parameter must be defined",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor DuplicateProcessParameterName = new DiagnosticDescriptor
    (
        id: DiagnosticIDs.DuplicateProcessParameterName,
        title: "Duplicate process parameter name",
        messageFormat: "\"{0}\" already defines a parameter \"{1}\"",
        category: "Naming",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
