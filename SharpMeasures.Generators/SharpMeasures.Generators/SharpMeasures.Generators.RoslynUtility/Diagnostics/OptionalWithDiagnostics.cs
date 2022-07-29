namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;

public interface IOptionalWithDiagnostics : IEnumerable<Diagnostic>
{
    public abstract bool HasResult { get; }
    public abstract bool LacksResult { get; }

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }
}

public interface IOptionalWithDiagnostics<T> : IOptionalWithDiagnostics
{
    public abstract T Result { get; }
    public abstract T? NullableResult { get; }
}

public static class OptionalWithDiagnostics
{
    public static IOptionalWithDiagnostics<T> EmptyWithoutDiagnostics<T>() => Empty<T>(Array.Empty<Diagnostic>());

    public static IOptionalWithDiagnostics<T> Result<T>(T result, IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleOptionalWithDiagnostics<T>(result, diagnostics);
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result, params Diagnostic[] diagnostics)
    {
        return Result(result, diagnostics as IEnumerable<Diagnostic>);
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result, Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Result(result);
        }

        return Result(result, new[] { diagnostics });
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result) => Result(result, Array.Empty<Diagnostic>());

    public static IOptionalWithDiagnostics<T> Empty<T>(IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleOptionalWithDiagnostics<T>(diagnostics);
    }

    public static IOptionalWithDiagnostics<T> Empty<T>(params Diagnostic[] diagnostics) => Empty<T>(diagnostics as IEnumerable<Diagnostic>);

    public static IOptionalWithDiagnostics<T> Empty<T>(Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return EmptyWithoutDiagnostics<T>();
        }

        return Empty<T>(new[] { diagnostics });
    }

    private class SimpleOptionalWithDiagnostics<T> : IOptionalWithDiagnostics<T>
    {
        private Optional<T> Optional { get; }

        public IEnumerable<Diagnostic> Diagnostics { get; }
        public T Result
        {
            get
            {
                if (LacksResult)
                {
                    throw new OptionalLacksResultException();
                }

                return Optional.Value;
            }
        }

        public T? NullableResult
        {
            get
            {
                if (LacksResult)
                {
                    return default;
                }

                return Optional.Value;
            }
        }

        public bool HasResult => Optional.HasValue;
        public bool LacksResult => HasResult is false;

        public SimpleOptionalWithDiagnostics(IEnumerable<Diagnostic> diagnostics) : this(new Optional<T>(), diagnostics) { }

        public SimpleOptionalWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics) : this((Optional<T>)result, diagnostics) { }

        private SimpleOptionalWithDiagnostics(Optional<T> optional, IEnumerable<Diagnostic> diagnostics)
        {
            Optional = optional;
            Diagnostics = diagnostics;
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

public sealed class OptionalLacksResultException : Exception
{
    public OptionalLacksResultException() : base() { }
    public OptionalLacksResultException(string message) : base(message) { }
    public OptionalLacksResultException(string message, Exception innerException) : base(message, innerException) { }
}
