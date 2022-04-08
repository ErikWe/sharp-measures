namespace ErikWe.SharpMeasures.SourceGenerators.Units.SourceBuilding;

using ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;
using ErikWe.SharpMeasures.SourceGenerators.Units.Pipeline;
using ErikWe.SharpMeasures.SourceGenerators.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

internal static class UnitsComposer
{
    public static void Append(StringBuilder source, FifthStage.Result data, CancellationToken token)
    {
        HashSet<string> addedInstances = new();

        int initialLength = source.Length;

        AppendIndependentInstances(source, data, addedInstances, data.Instances.Fixed, AppendUnitExpression, token);
        AppendIndependentInstances(source, data, addedInstances, data.Instances.Derived, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, token);
        
        if (source.Length > initialLength)
        {
            source.Append(Environment.NewLine);
        }
    }

    private static void AppendIndependentInstances<T>(StringBuilder source, FifthStage.Result data, HashSet<string> addedInstances, IEnumerable<T> instances,
        Action<StringBuilder, FifthStage.Result, T> expressionAppender, CancellationToken token)
        where T : IAttributeParameters
    {
        foreach (T parameters in instances)
        {
            AppendInstance(source, data, addedInstances, parameters, expressionAppender, token);
        }
    }

    private static void AppendDependantInstances(StringBuilder source, FifthStage.Result data, HashSet<string> addedInstances, CancellationToken token)
    {
        List<IDependantAttribute> unaddedInstances = new();

        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.Instances.Alias, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.Instances.Scaled, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.Instances.Prefixed, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.Instances.Offset, AppendUnitExpression, token);

        AppendUnaddedInstances(source, data, addedInstances, unaddedInstances, token);
    }

    private static void AppendDependantInstances<T>(StringBuilder source, FifthStage.Result data, HashSet<string> addedInstances, List<IDependantAttribute> unaddedInstances,
        IEnumerable<T> instances, Action<StringBuilder, FifthStage.Result, T> expressionAppender, CancellationToken token)
        where T : IDependantAttribute
    {
        foreach (T parameters in instances)
        {
            if (addedInstances.Contains(parameters.DependantOn))
            {
                AppendInstance(source, data, addedInstances, parameters, expressionAppender, token);
            }
            else
            {
                unaddedInstances.Add(parameters);
            }
        }
    }

    private static void AppendUnaddedInstances(StringBuilder source, FifthStage.Result data, HashSet<string> addedInstances, List<IDependantAttribute> unaddedInstances,
        CancellationToken token)
    {
        int initialCount = unaddedInstances.Count;

        for (int i = 0; i < unaddedInstances.Count; i++)
        {
            if (addedInstances.Contains(unaddedInstances[i].DependantOn))
            {
                AppendInstance(source, data, addedInstances, unaddedInstances[i], AppendUnitExpression, token);
                unaddedInstances.RemoveAt(i--);
            }
        }

        if (unaddedInstances.Count < initialCount)
        {
            AppendUnaddedInstances(source, data, addedInstances, unaddedInstances, token);
        }
    }

    private static void AppendInstance<T>(StringBuilder source, FifthStage.Result data, HashSet<string> addedInstances, T instance,
        Action<StringBuilder, FifthStage.Result, T> expressionAppender, CancellationToken _)
        where T : IAttributeParameters
    {
        addedInstances.Add(instance.Name);

        source.Append($"\tpublic static {data.TypeSymbol.ToDisplayString()} {instance.Name} {{ get; }} = ");
        expressionAppender(source, data, instance);
        source.Append($";{Environment.NewLine}");
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, IDependantAttribute parameters)
    {
        switch (parameters)
        {
            case AliasUnitInstanceAttributeParameters aliasInstance:
                AppendUnitExpression(source, data, aliasInstance);
                break;
            case ScaledUnitInstanceAttributeParameters scaledInstance:
                AppendUnitExpression(source, data, scaledInstance);
                break;
            case PrefixedUnitInstanceAttributeParameters prefixedInstance:
                AppendUnitExpression(source, data, prefixedInstance);
                break;
            case OffsetUnitInstanceAttributeParameters offsetInstance:
                AppendUnitExpression(source, data, offsetInstance);
                break;
        }
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, FixedUnitInstanceAttributeParameters parameters)
    {
        source.Append("new(new ");

        if (data.Parameters.Biased)
        {
            source.Append($"{data.Parameters.BiasedQuantity?.ToDisplayString()}({parameters.Value}, new Scalar({parameters.Bias}))");
        }
        else
        {
            source.Append($"{data.Parameters.Quantity?.ToDisplayString()}({parameters.Value})");
        }

        source.Append(')');
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, DerivedUnitInstanceAttributeParameters parameters)
    {
        SourceBuildingUtility.AppendEnumerable(source, "From(", arguments(), ", ", ")");

        IEnumerable<string> arguments()
        {
            for (int i = 0; i < parameters.Signature.Length && i < parameters.InstanceNames.Length; i++)
            {
                yield return $"{parameters.Signature[i]?.ToDisplayString()}.{parameters.InstanceNames[i]}";
            }
        }
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, AliasUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.AliasOf}");
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, ScaledUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.From}.ScaledBy({parameters.Scale})");
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, PrefixedUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.From}.WithPrefix(MetricPrefix.{parameters.Prefix})");
    }

    private static void AppendUnitExpression(StringBuilder source, FifthStage.Result data, OffsetUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.From}.OffsetBy({parameters.Offset})");
    }
}
