namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;

internal static class BlockBuilding
{
    public static void AppendBlock(StringBuilder source, Action<StringBuilder, Indentation> blockContentAppender,
        int originalIndentationLevel = 0, bool initialNewLine = true, bool finalNewLine = true)
        => AppendBlock(source, blockContentAppender, new Indentation(originalIndentationLevel), initialNewLine, finalNewLine);

    public static void AppendBlock(StringBuilder source, string header, Action<StringBuilder, Indentation> blockContentAppender,
        int originalIndentationLevel = 0, bool initialNewLine = true, bool finalNewLine = true)
    {
        Indentation originalIndentation = new(originalIndentationLevel);

        AppendBlock(source, header, blockContentAppender, originalIndentation, initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, string header, Action<StringBuilder, Indentation> blockContentAppender,
        Indentation originalIndentation, bool initialNewLine = true, bool finalNewLine = true)
    {
        source.Append($"{originalIndentation}{header}{Environment.NewLine}");

        AppendBlock(source, blockContentAppender, originalIndentation, initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, Action<StringBuilder, Indentation> blockContentAppender,
        Indentation originalIndentation, bool initialNewLine = true, bool finalNewLine = true)
    {
        AppendOpeningBracket(source, originalIndentation, initialNewLine, finalNewLine: true);
        blockContentAppender(source, originalIndentation.Increased);
        AppendClosingBracket(source, originalIndentation, initialNewLine: !source.ToString().EndsWith(Environment.NewLine, StringComparison.Ordinal), finalNewLine);
    }

    public static void AppendOpeningBracket(StringBuilder source, Indentation indentation, bool initialNewLine = true, bool finalNewLine = true)
    {
        if (initialNewLine)
        {
            source.Append(Environment.NewLine);
        }

        source.Append($"{indentation}{{");

        if (finalNewLine)
        {
            source.Append(Environment.NewLine);
        }
    }

    public static void AppendClosingBracket(StringBuilder source, Indentation indentation, bool initialNewLine = false, bool finalNewLine = false)
    {
        if (initialNewLine)
        {
            source.Append(Environment.NewLine);
        }

        source.Append($"{indentation}}}");

        if (finalNewLine)
        {
            source.Append(Environment.NewLine);
        }
    }
}
