namespace SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public static partial class IterativeValidity
{
    public delegate IValidityWithDiagnostics DValidity();
    public delegate IValidityWithDiagnostics DValidity<T>(T argument);
    public delegate IValidityWithDiagnostics DValidity<T1, T2>(T1 argument1, T2 argument2);

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IEnumerable<DValidity> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.MergeOperation.AND, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(params DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(delegatedValidity as IEnumerable<DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.MergeOperation mergeOperation, IEnumerable<DValidity> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(ValidityWithDiagnostics.GetMergeOperationDelegate(mergeOperation), delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.MergeOperation mergeOperation, params DValidity[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, delegatedValidity as IEnumerable<DValidity>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(T argument, IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(ValidityWithDiagnostics.MergeOperations.AND, argument, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(T argument, params DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(argument, delegatedValidity as IEnumerable<DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(IValidityWithDiagnostics.MergeOperation mergeOperation, T argument,
        IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(ValidityWithDiagnostics.GetMergeOperationDelegate(mergeOperation), argument, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(IValidityWithDiagnostics.MergeOperation mergeOperation, T argument,
        params DValidity<T>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, argument, delegatedValidity as IEnumerable<DValidity<T>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(T1 argument1, T2 argument2, IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(ValidityWithDiagnostics.MergeOperations.AND, argument1, argument2, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(T1 argument1, T2 argument2, params DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(argument1, argument2, delegatedValidity as IEnumerable<DValidity<T1, T2>>);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(IValidityWithDiagnostics.MergeOperation mergeOperation, T1 argument1, T2 argument2,
        IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(ValidityWithDiagnostics.GetMergeOperationDelegate(mergeOperation), argument1, argument2, delegatedValidity);
    }

    public static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(IValidityWithDiagnostics.MergeOperation mergeOperation, T1 argument1, T2 argument2,
        params DValidity<T1, T2>[] delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, argument1, argument2, delegatedValidity as IEnumerable<DValidity<T1, T2>>);
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T>(IValidityWithDiagnostics.DMergeOperation mergeOperation, T argument,
        IEnumerable<DValidity<T>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, wrappedDelegates());

        IEnumerable<DValidity> wrappedDelegates()
        {
            foreach (DValidity<T> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(argument);
            }
        }
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeWhileValid<T1, T2>(IValidityWithDiagnostics.DMergeOperation mergeOperation, T1 argument1, T2 argument2,
        IEnumerable<DValidity<T1, T2>> delegatedValidity)
    {
        return DiagnoseAndMergeWhileValid(mergeOperation, wrappedDelegates());

        IEnumerable<DValidity> wrappedDelegates()
        {
            foreach (DValidity<T1, T2> delegatedDiagnosis in delegatedValidity)
            {
                yield return () => delegatedDiagnosis(argument1, argument2);
            }
        }
    }

    private static IValidityWithDiagnostics DiagnoseAndMergeWhileValid(IValidityWithDiagnostics.DMergeOperation mergeOperation, IEnumerable<DValidity> delegatedValidity)
    {
        IValidityWithDiagnostics result = ValidityWithDiagnostics.Valid;

        foreach (DValidity delegatedDiagnosis in delegatedValidity)
        {
            result = result.Merge(mergeOperation, delegatedDiagnosis());

            if (result.IsInvalid)
            {
                return result;
            }
        }

        return result;
    }
}
