namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;

public static class BlockBuilding
{
    public static void AppendBlock(StringBuilder source, Action<StringBuilder, Indentation> blockContentAppender, int originalIndentationLevel = 0, bool initialNewLine = false, bool finalNewLine = true)
    {
        AppendBlock(source, blockContentAppender, new Indentation(originalIndentationLevel), initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, string header, Action<StringBuilder, Indentation> blockContentAppender, int originalIndentationLevel = 0, bool initialNewLine = false, bool finalNewLine = true)
    {
        Indentation originalIndentation = new(originalIndentationLevel);

        AppendBlock(source, header, blockContentAppender, originalIndentation, initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, string header, Action<StringBuilder, Indentation> blockContentAppender, Indentation originalIndentation, bool initialNewLine = false, bool finalNewLine = true)
    {
        if (initialNewLine)
        {
            source.AppendLine();
        }

        source.Append($"{originalIndentation}{header}");

        AppendBlock(source, blockContentAppender, originalIndentation, initialNewLine: true, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, Action<StringBuilder, Indentation> blockContentAppender, Indentation originalIndentation, bool initialNewLine = false, bool finalNewLine = true)
    {
        AppendOpeningBracket(source, originalIndentation, initialNewLine, finalNewLine: true);
        blockContentAppender(source, originalIndentation.Increased);

        bool newLineAfterContent = source.ToString().EndsWith(Environment.NewLine, StringComparison.Ordinal) is false;
        AppendClosingBracket(source, originalIndentation, newLineAfterContent, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, Action<Indentation> blockContentAppender, int originalIndentationLevel = 0, bool initialNewLine = false, bool finalNewLine = true)
    {
        AppendBlock(source, (_, indentation) => blockContentAppender(indentation), originalIndentationLevel, initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, string header, Action<Indentation> blockContentAppender, int originalIndentationLevel = 0, bool initialNewLine = false, bool finalNewLine = true)
    {
        AppendBlock(source, header, (_, indentation) => blockContentAppender(indentation), originalIndentationLevel, initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, string header, Action<Indentation> blockContentAppender, Indentation originalIndentation, bool initialNewLine = false, bool finalNewLine = true)
    {
        AppendBlock(source, header, (_, indentation) => blockContentAppender(indentation), originalIndentation, initialNewLine, finalNewLine);
    }

    public static void AppendBlock(StringBuilder source, Action<Indentation> blockContentAppender, Indentation originalIndentation, bool initialNewLine = false, bool finalNewLine = true)
    {
        AppendBlock(source, (_, indentation) => blockContentAppender(indentation), originalIndentation, initialNewLine, finalNewLine);
    }

    public static void AppendOpeningBracket(StringBuilder source, Indentation indentation, bool initialNewLine = false, bool finalNewLine = true)
    {
        if (initialNewLine)
        {
            source.AppendLine();
        }

        source.Append($"{indentation}{{");

        if (finalNewLine)
        {
            source.AppendLine();
        }
    }

    public static void AppendClosingBracket(StringBuilder source, Indentation indentation, bool initialNewLine = false, bool finalNewLine = false)
    {
        if (initialNewLine)
        {
            source.AppendLine();
        }

        source.Append($"{indentation}}}");

        if (finalNewLine)
        {
            source.AppendLine();
        }
    }
}
