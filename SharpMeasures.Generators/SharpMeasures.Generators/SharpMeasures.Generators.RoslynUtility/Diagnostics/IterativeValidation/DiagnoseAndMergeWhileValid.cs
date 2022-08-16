namespace SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public static partial class IterativeValidation
{
    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IEnumerable<IValidityWithDiagnostics> validities)
    {
        return DiagnoseAndMergeWhileValid(wrappedValidities());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedValidities()
        {
            foreach (var validity in validities)
            {
                yield return () => validity;
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(params IValidityWithDiagnostics[] validities)
    {
        return DiagnoseAndMergeWhileValid(validities as IEnumerable<IValidityWithDiagnostics>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IEnumerable<IValidityWithDiagnostics.DValidity> delegatedValidity)
    {
        IValidityWithDiagnostics result = ValidityWithDiagnostics.Valid;

        foreach (var delegatedDiagnosis in delegatedValidity)
        {
            result = result.Validate(delegatedDiagnosis());

            if (result.IsInvalid)
            {
                return result;
            }
        }

        return result;
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(params IValidityWithDiagnostics.DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(T parameter, IEnumerable<IValidityWithDiagnostics.DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(wrappedDelegates());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedDelegates()
        {
            foreach (var delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(parameter);
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(T parameter, params IValidityWithDiagnostics.DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(parameter, delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(T1 parameter1, T2 parameter2, IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(wrappedDelegates());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedDelegates()
        {
            foreach (IValidityWithDiagnostics.DValidity<T1, T2> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(parameter1, parameter2);
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(T1 parameter1, T2 parameter2, params IValidityWithDiagnostics.DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(parameter1, parameter2, delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2, T3>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(wrappedDelegates());

        IEnumerable<IValidityWithDiagnostics.DValidity> wrappedDelegates()
        {
            foreach (IValidityWithDiagnostics.DValidity<T1, T2, T3> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(parameter1, parameter2, parameter3);
            }
        }
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, params IValidityWithDiagnostics.DValidity<T1, T2, T3>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(parameter1, parameter2, parameter3, delegatedValidity as IEnumerable<IValidityWithDiagnostics.DValidity<T1, T2, T3>>);
    }
}
