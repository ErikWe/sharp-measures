; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
Measures1000 | Usage | Warning | Expected a partial type
Measures1001 | Usage | Warning | Expected a scalar quantity
Measures1002 | Usage | Warning | Expected a vector quantity
Measures1003 | Usage | Warning | Expected a unit
Measures1004 | Usage | Warning | Type already defined
Measures1020 | Usage | Warning | Expected an unbiased scalar quantity
Measures1021 | Usage | Warning | Expected a biased scalar quantity
Measures1022 | Usage | Warning | Unit does not support biased quantities
Measures1080 | Usage | Info | Expected a non-empty list
Measures1081 | Usage | Info | Item has already been listed
Measures1100 | Naming | Warning | Invalid unit name
Measures1101 | Naming | Warning | Invalid plural form of unit name
Measures1102 | Naming | Warning | Duplicate unit name
Measures1103 | Naming | Warning | Duplicate unit plural form
Measures1104 | Usage | Warning | Expected the name of a unit
Measures1105 | Usage | Warning | Cyclic unit dependency
Measures1120 | Usage | Warning | Prefix not recognized
Measures1140 | Usage | Warning | Invalid unit derivation expression
Measures1141 | Usage | Warning | Expected a non-empty signature
Measures1142 | Usage | Warning | Duplicate derivation signature
Measures1143 | Usage | Warning | Derivation signature not recognized
Measures1144 | Usage | Warning | Unit list not matching signature
Measures1200 | Usage | Warning | Expected both unit name and symbol
Measures1220 | Naming | Warning | Invalid name of constant
Measures1221 | Naming | Warning | Invalid name for multiples of constant
Measures1222 | Naming | Warning | Duplicate name of constant
Measures1223 | Naming | Warning | Duplicate name for multiples of constant
Measures1224 | Naming | Warning | Constant shares name with unit
Measures1240 | Usage | Warning | Unrecognized cast operator behaviour
Measures1260 | Usage | Warning | Contradictory attributes
Measures1261 | Usage | Warning | Quantity group missing root quantity
Measures1420 | Usage | Warning | Invalid vector dimension
Measures1421 | Usage | Warning | Missing vector dimension
Measures1422 | Usage | Warning | Duplicate vector dimension
Measures1440 | Usage | Warning | Invalid dimension of vector constant
Measures1900 | Documentation | Info | Unresolved documentation dependency
Measures1901 | Documentation | Info | No matching documentation file
Measures1902 | Documentation | Info | Documentation tag not found