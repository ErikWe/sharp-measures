namespace ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using ErikWe.SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct FixedUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant, double Value, double Bias)
    : IAttributeParameters
{
    private static Dictionary<string, AttributeProperty<FixedUnitInstanceAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<FixedUnitInstanceAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<FixedUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            Value,
            Bias
        };

        public static AttributeProperty<FixedUnitInstanceAttributeParameters> Name { get; }
            = AttributeParameters.Name<FixedUnitInstanceAttributeParameters>(static (parameters, name) => parameters with { Name = name });
        public static AttributeProperty<FixedUnitInstanceAttributeParameters> Plural { get; }
            = AttributeParameters.Plural<FixedUnitInstanceAttributeParameters>(static (parameters, plural) => parameters with { Plural = plural });
        public static AttributeProperty<FixedUnitInstanceAttributeParameters> Symbol { get; }
            = AttributeParameters.Symbol<FixedUnitInstanceAttributeParameters>(static (parameters, symbol) => parameters with { Symbol = symbol });
        public static AttributeProperty<FixedUnitInstanceAttributeParameters> IsSIUnit { get; }
            = AttributeParameters.IsSIUnit<FixedUnitInstanceAttributeParameters>(static (parameters, isSIUnit) => parameters with { IsSIUnit = isSIUnit });
        public static AttributeProperty<FixedUnitInstanceAttributeParameters> IsConstant { get; }
            = AttributeParameters.IsConstant<FixedUnitInstanceAttributeParameters>(static (parameters, isConstant) => parameters with { IsConstant = isConstant });
        public static AttributeProperty<FixedUnitInstanceAttributeParameters> Value { get; } = new("Value", typeof(double),
            static (parameters, obj) => obj is double value ? parameters with { Value = value } : parameters);
        public static AttributeProperty<FixedUnitInstanceAttributeParameters> Bias { get; } = new("Bias", typeof(double),
            static (parameters, obj) => obj is double bias ? parameters with { Bias = bias } : parameters);
    }

    public static FixedUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        FixedUnitInstanceAttributeParameters values = new(string.Empty, string.Empty, string.Empty, false, false, 0, 0);

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}