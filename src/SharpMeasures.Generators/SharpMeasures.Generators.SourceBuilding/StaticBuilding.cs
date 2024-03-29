﻿namespace SharpMeasures.Generators.SourceBuilding;

using SharpMeasures.Generators.Configuration;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

public static class StaticBuilding
{
    public static string NullArgumentGuard(string parameter) => $"global::System.ArgumentNullException.ThrowIfNull({parameter});";

    public static void AppendHeaderAndDirectives(StringBuilder source, GeneratedFileHeaderContent generatedFileHeaderContent)
    {
        AppendAutoGeneratedHeader(source, Assembly.GetCallingAssembly(), generatedFileHeaderContent);

        AppendNullableDirective(source);
    }

    public static void AppendAutoGeneratedHeader(StringBuilder source, GeneratedFileHeaderContent generatedFileHeaderContent) => AppendAutoGeneratedHeader(source, Assembly.GetCallingAssembly(), generatedFileHeaderContent);
    private static void AppendAutoGeneratedHeader(StringBuilder source, Assembly callingAssembly, GeneratedFileHeaderContent generatedFileHeaderContent)
    {
        if (generatedFileHeaderContent is GeneratedFileHeaderContent.None or GeneratedFileHeaderContent.Version)
        {
            return;
        }

        source.AppendLine($$"""
            //----------------------
            // <auto-generated>
            //      This file was generated by {{toolText()}}{{versionText()}}{{timestampText()}}.
            //
            //      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
            // </auto-generated>
            //----------------------

            """);

        string toolText()
        {
            if (generatedFileHeaderContent.HasFlag(GeneratedFileHeaderContent.Tool))
            {
                return callingAssembly.GetName().Name;
            }

            return "a tool";
        }

        string versionText()
        {
            if (generatedFileHeaderContent.HasFlag(GeneratedFileHeaderContent.Tool | GeneratedFileHeaderContent.Version))
            {
                return $" v{callingAssembly.GetName().Version}";
            }

            return string.Empty;
        }

        string timestampText()
        {
            if (generatedFileHeaderContent.HasFlag(GeneratedFileHeaderContent.Date))
            {
                if (generatedFileHeaderContent.HasFlag(GeneratedFileHeaderContent.Time))
                {
                    return $" ({DateTime.Now} {TimeZoneInfo.Local.Id})";
                }

                return $" ({DateTime.Now.ToShortDateString()})";
            }

            if (generatedFileHeaderContent.HasFlag(GeneratedFileHeaderContent.Time))
            {
                return $" ({DateTime.Now.ToLongTimeString()} {TimeZoneInfo.Local.Id})";
            }

            return string.Empty;
        }
    }

    public static void AppendNullableDirective(StringBuilder source)
    {
        source.AppendLine($"#nullable enable");
        source.AppendLine();
    }

    public static void AppendNullArgumentGuard(StringBuilder source, Indentation indentation, string parameterName, bool finalNewLine = true)
    {
        source.AppendLine($"{indentation}{NullArgumentGuard(parameterName)}");

        if (finalNewLine)
        {
            source.AppendLine();
        }
    }

    public static void AppendNullArgumentGuardForReferenceTypeParameters(StringBuilder source, Indentation indentation, params (NamedType Type, string Name)[] parameters) => AppendNullArgumentGuardForReferenceTypeParameters(source, indentation, parameters: parameters);
    public static void AppendNullArgumentGuardForReferenceTypeParameters(StringBuilder source, Indentation indentation, bool finalNewLine = true, params (NamedType Type, string Name)[] parameters) => AppendNullArgumentGuardForReferenceTypeParameters(source, indentation, parameters, finalNewLine);
    public static void AppendNullArgumentGuardForReferenceTypeParameters(StringBuilder source, Indentation indentation, IEnumerable<(NamedType Type, string Name)> parameters, bool finalNewLine = true)
    {
        var anyReferenceType = false;

        foreach ((var parameterType, var parameterName) in parameters)
        {
            if (parameterType.IsReferenceType)
            {
                AppendNullArgumentGuard(source, indentation, parameterName, finalNewLine: false);

                anyReferenceType = true;
            }
        }

        if (anyReferenceType && finalNewLine)
        {
            source.AppendLine();
        }
    }

    public static void AppendSingleLineMethodWithPotentialNullArgumentGuards(StringBuilder source, Indentation indentation, string methodNameAndModifiers, string expression, params (NamedType Type, string Name)[] parameters)
        => AppendSingleLineMethodWithPotentialNullArgumentGuards(source, indentation, methodNameAndModifiers, expression, parameters as IEnumerable<(NamedType, string)>);

    public static void AppendSingleLineMethodWithPotentialNullArgumentGuards(StringBuilder source, Indentation indentation, string methodNameAndModifiers, string expression, IEnumerable<(NamedType Type, string Name)> parameters)
    {
        List<NamedType> types = new();
        List<string> signatureComponents = new();

        var anyReferenceTypes = false;

        foreach (var parameter in parameters)
        {
            types.Add(parameter.Type);
            signatureComponents.Add($"{parameter.Type.FullyQualifiedName} {parameter.Name}");

            if (parameter.Type.IsReferenceType)
            {
                anyReferenceTypes = true;
            }
        }

        DocumentationBuilding.AppendArgumentNullExceptionTagIfReferenceType(source, indentation, types);

        StringBuilder signature = new();
        IterativeBuilding.AppendEnumerable(signature, prefix: "(", signatureComponents, separator: ", ", postfix: ")", removeFixedIfEmpty: false);

        if (anyReferenceTypes)
        {
            source.AppendLine($$"""
                {{indentation}}{{methodNameAndModifiers}}{{signature}}
                {{indentation}}{
                """);

            AppendNullArgumentGuardForReferenceTypeParameters(source, indentation.Increased, parameters: parameters);

            source.AppendLine($$"""
                {{indentation.Increased}}return {{expression}};
                {{indentation}}}
                """);

            return;
        }

        source.AppendLine($"{indentation}{methodNameAndModifiers}{signature} => {expression};");
    }

    public static void AppendEqualsObjectMethod(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public override bool Equals(object? obj) => obj is {type} other && Equals(other);");
    public static void AppendReferenceTypeEqualityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator ==({type}? lhs, {type}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");
    public static void AppendReferenceTypeInequalityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator !=({type}? lhs, {type}? rhs) => (lhs == rhs) is false;");
    public static void AppendValueTypeEqualityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator ==({type} lhs, {type} rhs) => lhs.Equals(rhs);");
    public static void AppendValueTypeInequalityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator !=({type} lhs, {type} rhs) => (lhs == rhs) is false;");
}
