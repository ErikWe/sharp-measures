namespace SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

public static class IterativeBuilding
{
    public static void AppendEnumerable(StringBuilder source, IEnumerable<string> texts, string separator, bool removeFixedIfEmpty = true) => AppendEnumerable(source, string.Empty, texts, separator, string.Empty, removeFixedIfEmpty);
    public static void AppendEnumerable(StringBuilder source, string prefix, IEnumerable<string> texts, string separator, bool removeFixedIfEmpty = true) => AppendEnumerable(source, prefix, texts, separator, string.Empty, removeFixedIfEmpty);
    public static void AppendEnumerable(StringBuilder source, IEnumerable<string> texts, string separator, string postfix, bool removeFixedIfEmpty = true) => AppendEnumerable(source, string.Empty, texts, separator, postfix, removeFixedIfEmpty);
    public static void AppendEnumerable(StringBuilder source, string prefix, IEnumerable<string> texts, string separator, string postfix, bool removeFixedIfEmpty = true)
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

            return;
        }

        if (removeFixedIfEmpty)
        {
            source.Remove(source.Length - prefix.Length, prefix.Length);

            return;
        }

        source.Append(postfix);
    }
}
