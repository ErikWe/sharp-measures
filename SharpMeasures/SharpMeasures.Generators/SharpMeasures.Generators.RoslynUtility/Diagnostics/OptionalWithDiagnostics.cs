namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IOptionalWithDiagnostics : IEnumerable<Diagnostic>
{
    public abstract bool HasResult { get; }
    public abstract bool LacksResult { get; }

    public abstract IEnumerable<Diagnostic> Diagnostics { get; }
}

public interface IOptionalWithDiagnostics<T> : IOptionalWithDiagnostics
{
    public abstract T Result { get; }

    public abstract IResultWithDiagnostics<T> Update(T result);
    public abstract IOptionalWithDiagnostics<T> Update(IOptionalWithDiagnostics<T> update);
    public abstract IResultWithDiagnostics<T> Update(IResultWithDiagnostics<T> update);
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
        return Result<T>(result, diagnostics as IEnumerable<Diagnostic>);
    }

    public static IOptionalWithDiagnostics<T> Result<T>(T result) => Result(result, Array.Empty<Diagnostic>());

    public static IOptionalWithDiagnostics<T> Empty<T>(IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleOptionalWithDiagnostics<T>(diagnostics);
    }

    public static IOptionalWithDiagnostics<T> Empty<T>(params Diagnostic[] diagnostics) => Empty<T>(diagnostics as IEnumerable<Diagnostic>);

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

        public bool HasResult => Optional.HasValue;
        public bool LacksResult => HasResult is false;

        public SimpleOptionalWithDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            Optional = new();
            Diagnostics = diagnostics;
        }

        public SimpleOptionalWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics)
        {
            Optional = result;
            Diagnostics = diagnostics;
        }

        private SimpleOptionalWithDiagnostics(Optional<T> optional, IEnumerable<Diagnostic> diagnostics)
        {
            Optional = optional;
            Diagnostics = diagnostics;
        }

        public IResultWithDiagnostics<T> Update(T result)
        {
            return ResultWithDiagnostics.Construct(result, Diagnostics);
        }

        public IOptionalWithDiagnostics<T> Update(IOptionalWithDiagnostics<T> update)
        {
            if (update.HasResult)
            {
                return new SimpleOptionalWithDiagnostics<T>(update.Result, Diagnostics.Concat(update.Diagnostics));
            }

            return new SimpleOptionalWithDiagnostics<T>(Optional, Diagnostics.Concat(update.Diagnostics));
        }

        public IResultWithDiagnostics<T> Update(IResultWithDiagnostics<T> update)
        {
            return ResultWithDiagnostics.Construct(update.Result, Diagnostics.Concat(update.Diagnostics));
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