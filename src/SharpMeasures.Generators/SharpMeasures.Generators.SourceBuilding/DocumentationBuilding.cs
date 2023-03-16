namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class DocumentationBuilding
{
    public static string ExceptionTag<TException>() => $"""/// <exception cref="global::{typeof(TException).FullName}"/>""";
    public static string ArgumentNullExceptionTag => ExceptionTag<ArgumentNullException>();

    private static Regex NewLineRegex { get; } = new(@"(?:\r\n|\r|\n|^)", RegexOptions.Singleline | RegexOptions.Compiled);

    public static void AppendDocumentation(StringBuilder source, Indentation indentation, string text)
    {
        if (text.Length is 0)
        {
            return;
        }

        var indentedTagContent = NewLineRegex.Replace(text, $"$0{indentation}");

        source.AppendLine(indentedTagContent);
    }

    public static void AppendExceptionTag<TException>(StringBuilder source, Indentation indentation) => source.AppendLine($"{indentation}{ExceptionTag<TException>()}");
    public static void AppendArgumentNullExceptionTag(StringBuilder source, Indentation indentation) => AppendExceptionTag<ArgumentNullException>(source, indentation);
    public static void AppendArgumentNullExceptionTagIfReferenceType(StringBuilder source, Indentation indentation, params NamedType[] parameterTypes) => AppendArgumentNullExceptionTagIfReferenceType(source, indentation, parameterTypes as IEnumerable<NamedType>);
    public static void AppendArgumentNullExceptionTagIfReferenceType(StringBuilder source, Indentation indentation, IEnumerable<NamedType> parameterTypes)
    {
        if (parameterTypes.Where(static (parameter) => parameter.IsReferenceType).Any())
        {
            AppendArgumentNullExceptionTag(source, indentation);
        }
    }
}
