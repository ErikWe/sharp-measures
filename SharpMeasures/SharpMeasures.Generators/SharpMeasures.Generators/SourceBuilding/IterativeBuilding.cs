namespace SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal static class IterativeBuilding
{
    public static void AppendEnumerable(StringBuilder source, IEnumerable<string> texts, string separator)
        => AppendEnumerable(source, string.Empty, texts, separator, string.Empty);

    public static void AppendEnumerable(StringBuilder source, string prefix, IEnumerable<string> texts, string separator)
        => AppendEnumerable(source, prefix, texts, separator, string.Empty);

    public static void AppendEnumerable(StringBuilder source, IEnumerable<string> texts, string separator, string postfix)
        => AppendEnumerable(source, string.Empty, texts, separator, postfix);

    public static void AppendEnumerable(StringBuilder source, string prefix, IEnumerable<string> texts, string separator, string postfix)
    {
        source.Append(prefix);

        bool anyTexts = false;
        foreach (string text in texts)
        {
            anyTexts = true;
            source.Append(text + separator);
        }

        if (anyTexts)
        {
            source.Remove(source.Length - separator.Length, separator.Length);
            source.Append(postfix);
        }
        else
        {
            source.Remove(source.Length - prefix.Length, prefix.Length);
        }
    }
}
