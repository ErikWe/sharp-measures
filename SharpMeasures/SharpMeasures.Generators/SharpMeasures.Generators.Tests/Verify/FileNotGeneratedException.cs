namespace SharpMeasures.Generators.Tests.Verify;

using System;

public class FileNotGeneratedException : Exception
{
    public FileNotGeneratedException() : base() { }
    public FileNotGeneratedException(string message) : base(message) { }
    public FileNotGeneratedException(string message, Exception innerException) : base(message, innerException) { }
}
