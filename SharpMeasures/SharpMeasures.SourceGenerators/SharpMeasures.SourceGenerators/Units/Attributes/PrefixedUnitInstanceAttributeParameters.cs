namespace SharpMeasures.SourceGenerators.Units.Attributes;

using SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct PrefixedUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant, string From, string Prefix)
    : IDependantAttribute, IAttributeParameters
{
    string IDependantAttribute.DependantOn => From;

    private static Dictionary<string, AttributeProperty<PrefixedUnitInstanceAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<PrefixedUnitInstanceAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<PrefixedUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Prefix
        };

        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Name { get; }
            = AttributeParameters.Name<PrefixedUnitInstanceAttributeParameters>(static (parameters, name) => parameters with { Name = name });
        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Plural { get; }
            = AttributeParameters.Plural<PrefixedUnitInstanceAttributeParameters>(static (parameters, plural) => parameters with { Plural = plural });
        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Symbol { get; }
            = AttributeParameters.Symbol<PrefixedUnitInstanceAttributeParameters>(static (parameters, symbol) => parameters with { Symbol = symbol });
        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> IsSIUnit { get; }
            = AttributeParameters.IsSIUnit<PrefixedUnitInstanceAttributeParameters>(static (parameters, isSIUnit) => parameters with { IsSIUnit = isSIUnit });
        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> IsConstant { get; }
            = AttributeParameters.IsConstant<PrefixedUnitInstanceAttributeParameters>(static (parameters, isConstant) => parameters with { IsConstant = isConstant });
        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> From { get; } = new("From", typeof(string),
            static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters);
        public static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Prefix { get; } = new("Prefix", typeof(string),
            static (parameters, obj) => obj is string prefix ? parameters with { Prefix = prefix } : parameters);
    }

    public static PrefixedUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        PrefixedUnitInstanceAttributeParameters values = new(string.Empty, string.Empty, string.Empty, false, false, string.Empty, string.Empty);

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}