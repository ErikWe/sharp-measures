namespace SharpMeasures.SourceGenerators.Units.Attributes;

using SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct AliasUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant, string AliasOf)
    : IDependantAttribute, IAttributeParameters
{
    string IDependantAttribute.DependantOn => AliasOf;

    private static Dictionary<string, AttributeProperty<AliasUnitInstanceAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<AliasUnitInstanceAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<AliasUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            AliasOf
        };

        public static AttributeProperty<AliasUnitInstanceAttributeParameters> Name { get; }
            = AttributeParameters.Name<AliasUnitInstanceAttributeParameters>(static (parameters, name) => parameters with { Name = name });
        public static AttributeProperty<AliasUnitInstanceAttributeParameters> Plural { get; }
            = AttributeParameters.Plural<AliasUnitInstanceAttributeParameters>(static (parameters, plural) => parameters with { Plural = plural });
        public static AttributeProperty<AliasUnitInstanceAttributeParameters> Symbol { get; }
            = AttributeParameters.Symbol<AliasUnitInstanceAttributeParameters>(static (parameters, symbol) => parameters with { Symbol = symbol });
        public static AttributeProperty<AliasUnitInstanceAttributeParameters> IsSIUnit { get; }
            = AttributeParameters.IsSIUnit<AliasUnitInstanceAttributeParameters>(static (parameters, isSIUnit) => parameters with { IsSIUnit = isSIUnit });
        public static AttributeProperty<AliasUnitInstanceAttributeParameters> IsConstant { get; }
            = AttributeParameters.IsConstant<AliasUnitInstanceAttributeParameters>(static (parameters, isConstant) => parameters with { IsConstant = isConstant });
        public static AttributeProperty<AliasUnitInstanceAttributeParameters> AliasOf { get; } = new("AliasOf", typeof(string),
            static (parameters, obj) => obj is string aliasOf ? parameters with { AliasOf = aliasOf } : parameters);
    }

    public static AliasUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        AliasUnitInstanceAttributeParameters values = new(
            Properties.Name.DefaultValue as string ?? string.Empty,
            Properties.Plural.DefaultValue as string ?? string.Empty,
            Properties.Symbol.DefaultValue as string ?? string.Empty,
            Properties.IsSIUnit.DefaultValue as bool? ?? false,
            Properties.IsConstant.DefaultValue as bool? ?? false,
            Properties.AliasOf.DefaultValue as string ?? string.Empty
        );

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}