import re
import GenerateVectorN
import Documentation.ScalarDocumenter

def lowerCase(text):
    return text[0].lower() + text[1:]

def parsePlural(singular, plural):
    if plural == '=':
        return singular
    if not '+' in plural:
        return plural
    if not '[' in plural:
        return singular + plural.split('+')[1]
    else:
        targetText = plural.split('[')[1].split(']')[0]
        if '[' in plural.split('+')[0]:
            return re.sub(targetText, targetText + plural.split('+')[1], singular);
        else:
            return re.sub(targetText, plural.split('+')[1].split(' ')[0] + targetText, singular);

def getUnitName(quantityName, quantityData):
    if quantityData['unit'] == '[UnitOf]':
        return 'UnitOf' + quantityName
    else:
        return quantityData['unit']

def getSIUnit(quantityData):
    for unit in quantityData['units']:
        if 'singular' in unit and unit['singular'] == quantityData['SI']:
            return unit
    return False

def getVectorName(quantityName, quantityData):
    if quantityData['vector'] == '[name]':
        return quantityName
    elif quantityData['vector']:
        return quantityData['vector']

def getVectorDimensionalities(data, quantityName, quantityData):
    if getVectorName(quantityName, quantityData):
        return data['vectors'][getVectorName(quantityName, quantityData)]['dimensionalities']
    else:
        return []

def getQuantityBases(quantityData):
    if quantityData['bases'] == '[units]':
        return quantityData['units']
    else:
        return quantityData['bases']

def composeInterfacesText(data, quantityName, quantityData):
    interfaces = []

    if quantityData['invert']:
        interfaces.append('IInvertibleScalarQuantity<' + quantityData['invert'] + '>')
    if quantityData['square']:
        interfaces.append('ISquarableScalarQuantity<' + quantityData['square'] + '>')
    if quantityData['cube']:
        interfaces.append('ICubableScalarQuantity<' + quantityData['cube'] + '>')
    if quantityData['squareRoot']:
        interfaces.append('ISquareRootableScalarQuantity<' + quantityData['squareRoot'] + '>')
    if quantityData['cubeRoot']:
        interfaces.append('ICubeRootableScalarQuantity<' + quantityData['cubeRoot'] + '>')
    if quantityData['additive']:
        interfaces.append('IAddableScalarQuantity<#Quantity#, #Quantity#>')
    if quantityData['subtractive']:
        interfaces.append('ISubtractableScalarQuantity<#Quantity#, #Quantity#>')
    if quantityData['cancels']:
        interfaces.append('IDivisibleScalarQuantity<Scalar, #Quantity#>')

    vectorName = getVectorName(quantityName, quantityData)
    for dimensionality in getVectorDimensionalities(data, quantityName, quantityData):
        interfaces.append('IVector' + str(dimensionality) + 'izableScalarQuantity<' + vectorName + str(dimensionality) + '>')

    interfacesText = ""
    for interface in interfaces:
        interfacesText += '\t' + interface + ',\n'

    return interfacesText[:-2]

def composeBasesText(quantityName, quantityData):
    basesText = ""

    for base in getQuantityBases(quantityData):
        if 'separator' in base and base['separator'] == True:
            basesText += '\n'
        else:
            basesText += '\t#Document:One' + base['singular'] + '#\n'
            basesText += '\tpublic static ' + quantityName + ' One' + base['singular'] + ' { get; } = new(1, ' + getUnitName(quantityName, quantityData) + '.' + base['singular'] + ');\n'

    return basesText

def composeFromText(data, quantityData):
    fromText = ""

    if quantityData['invert']:
        variable = lowerCase(quantityData['invert'])
        SI = getSIUnit(data['scalars'][quantityData['invert']])
        
        fromText += '\t#Document:FromInverse#\n'
        fromText += '\tpublic static #Quantity# From(' + quantityData['invert'] + ' ' + variable + ') => new(1 / '
        fromText += variable + '.In' + parsePlural(SI['singular'], SI['plural']) + ');\n'

    if quantityData['square']:
        variable = lowerCase(quantityData['square'])
        SI = getSIUnit(data['scalars'][quantityData['square']])

        fromText += '\t#Document:FromSquare#\n'
        fromText += '\tpublic static #Quantity# From(' + quantityData['square'] + ' ' + variable + ') => new(Math.Sqrt('
        fromText += variable + '.In' + parsePlural(SI['singular'], SI['plural']) + '));\n'

    if quantityData['cube']:
        variable = lowerCase(quantityData['cube'])
        SI = getSIUnit(data['scalars'][quantityData['cube']])

        fromText += '\t#Document:FromCube#\n'
        fromText += '\tpublic static #Quantity# From(' + quantityData['cube'] + ' ' + variable + ') => new(Math.Cbrt('
        fromText += variable + '.In' + parsePlural(SI['singular'], SI['plural']) + '));\n'

    if quantityData['squareRoot']:
        variable = lowerCase(quantityData['squareRoot'])
        SI = getSIUnit(data['scalars'][quantityData['squareRoot']])

        fromText += '\t#Document:FromSquareRoot#\n'
        fromText += '\tpublic static #Quantity# From(' + quantityData['squareRoot'] + ' ' + variable + ') => new(Math.Pow('
        fromText += variable + '.In' + parsePlural(SI['singular'], SI['plural']) + ', 2));\n'

    if quantityData['cubeRoot']:
        variable = lowerCase(quantityData['cubeRoot'])
        SI = getSIUnit(data['scalars'][quantityData['cubeRoot']])

        fromText += '\t#Document:FromCubeRoot#\n'
        fromText += '\tpublic static #Quantity# From(' + quantityData['cubeRoot'] + ' ' + variable + ') => new(Math.Pow('
        fromText += variable + '.In' + parsePlural(SI['singular'], SI['plural']) + ', 3));\n'

    return fromText

def composeUnitsText(quantityName, quantityData):
    unitsText = ""

    for unit in quantityData['units']:
        if 'separator' in unit and unit['separator'] == True:
            unitsText += '\n'
        else:
            unitsText += '\t#Document:In' + parsePlural(unit['singular'], unit['plural']) + '#\n'
            unitsText += '\tpublic Scalar In' + parsePlural(unit['singular'], unit['plural']) + ' => InUnit(' + getUnitName(quantityName, quantityData) + '.' + unit['singular'] + ');\n'

    return unitsText

def composePowersText(quantityData):
    powersText = ""

    if quantityData['invert']:
        powersText += '\t#Document:Invert#\n'
        powersText += '\tpublic ' + quantityData['invert'] + ' Invert() => ' + quantityData['invert'] + '.From(this);\n'
    if quantityData['square']:
        powersText += '\t#Document:Square#\n'
        powersText += '\tpublic ' + quantityData['square'] + ' Square() => ' + quantityData['square'] + '.From(this);\n'
    if quantityData['cube']:
        powersText += '\t#Document:Cube#\n'
        powersText += '\tpublic ' + quantityData['cube'] + ' Cube() => ' + quantityData['cube'] + '.From(this);\n'
    if quantityData['squareRoot']:
        powersText += '\t#Document:SquareRoot#\n'
        powersText += '\tpublic ' + quantityData['squareRoot'] + ' SquareRoot() => ' + quantityData['squareRoot'] + '.From(this);\n'
    if quantityData['cubeRoot']:
        powersText += '\t#Document:CubeRoot#\n'
        powersText += '\tpublic ' + quantityData['cubeRoot'] + ' CubeRoot() => ' + quantityData['cubeRoot'] + '.From(this);\n'

    return powersText

def composeInversionOperatorDoubleText(quantityName, quantityData):
    invertText = ""

    if quantityData['invert']:
        invertText += '\t#Document:DivideDoubleOperatorRHS#\n'
        invertText += '\tpublic static ' + quantityData['invert'] + ' operator /(double x, ' + quantityName + ' y) => x * y.Invert();\n'

    return invertText

def composeInversionOperatorScalarText(quantityName, quantityData):
    invertText = ""

    if quantityData['invert']:
        invertText += '\t#Document:DivideScalarOperatorRHS#\n'
        invertText += '\tpublic static ' + quantityData['invert'] + ' operator /(Scalar x, ' + quantityName + ' y) => x * y.Invert();\n'

    return invertText

def composeMagnitudeFromUnitDoubleText(quantityData):
    if quantityData['unitBias']:
        return '(magnitude * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale'
    else:
        return 'magnitude * #UnitVariable#.Factor'

def composeMagnitudeFromUnitScalarText(quantityData):
    if quantityData['unitBias']:
        return '(magnitude.Magnitude * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale'
    else:
        return 'magnitude.Magnitude * #UnitVariable#.Factor'

def composeMagnitudeToUnitText(quantityData):
    if quantityData['unitBias']:
        return '(magnitude / #UnitVariable#.BaseScale - #UnitVariable#.Bias) / #UnitVariable#.Prefix.Scale'
    else:
        return 'magnitude / #UnitVariable#.Factor'

def composeSharedUnitsText(quantityData):
    sharedUnitsText = ""

    for sharesUnit in quantityData['sharesUnit']:
        sharedUnitsText += '\tpublic ' + sharesUnit + ' As' + sharesUnit + ' => new(Magnitude);\n'

    return sharedUnitsText

def composeAdditiveText(quantityData):
    additiveText = ""

    if quantityData['additive']:
        additiveText += '\t#Document:AddMethod#\n'
        additiveText += '\tpublic #Quantity# Add(#Quantity# term) => new(Magnitude + term.Magnitude);\n'
    
    if quantityData['subtractive']:
        additiveText += '\t#Document:SubtractMethod#\n'
        additiveText += '\tpublic #Quantity# Subtract(#Quantity# term) => new(Magnitude - term.Magnitude);\n'

    if quantityData['additive']:
        additiveText += '\t#Document:AddOperator#\n'
        additiveText += '\tpublic static #Quantity# operator +(#Quantity# x, #Quantity# y) => x.Add(y);\n'
    
    if quantityData['subtractive']:
        additiveText += '\t#Document:SubtractOperator#\n'
        additiveText += '\tpublic static #Quantity# operator -(#Quantity# x, #Quantity# y) => x.Subtract(y);\n'

    return additiveText

def composeCancelsText(quantityData):
    cancelsText = ""

    if quantityData['cancels']:
        cancelsText += '\t#Document:CancelsMethod#\n'
        cancelsText += '\tpublic Scalar Divide(#Quantity# divisor) => new(Magnitude / divisor.Magnitude);\n'
        cancelsText += '\t#Document:CancelsOperator#\n'
        cancelsText += '\tpublic static Scalar operator /(#Quantity# x, #Quantity# y) => x.Divide(y)\n;'

    return cancelsText

def composeToVectorText(data, quantityName, quantityData):
    toVectorMethods = ""
    toVectorOperations = ""

    vectorName = getVectorName(quantityName, quantityData)

    for dimensionality in getVectorDimensionalities(data, quantityName, quantityData):
        toVectorMethods += '\t#Document:MultiplyVector' + str(dimensionality) + 'Method#\n'
        toVectorMethods += '\tpublic ' + vectorName + str(dimensionality) + ' Multiply(Vector' + str(dimensionality) + ' vector) #newline#=> new(vector * Magnitude);\n'
        toVectorMethods += '\t#Document:MultiplyTuple' + str(dimensionality) + 'Method#\n'
        toVectorMethods += '\tpublic ' + vectorName + str(dimensionality) + ' Multiply(('
        for name in GenerateVectorN.vectorNames(dimensionality):
            toVectorMethods += 'double ' + lowerCase(name) + ', '
        toVectorMethods = toVectorMethods[:-2] + ') vector) #newline#=> new('
        for name in GenerateVectorN.vectorNames(dimensionality):
            toVectorMethods += 'Magnitude * vector.' + lowerCase(name) + ', '
        toVectorMethods = toVectorMethods[:-2] + ');\n'

        toVectorOperations += '\t#Document:MultiplyVector' + str(dimensionality) + 'OperatorLHS#\n'
        toVectorOperations += '\tpublic static ' + vectorName + str(dimensionality) + ' operator *(#Quantity# a, Vector' + str(dimensionality) + ' b) #newline#=> a.Multiply(b);\n'
        toVectorOperations += '\t#Document:MultiplyVector' + str(dimensionality) + 'OperatorRHS#\n'
        toVectorOperations += '\tpublic static ' + vectorName + str(dimensionality) + ' operator *(Vector' + str(dimensionality) + ' a, #Quantity# b) #newline#=> b.Multiply(a);\n'
        toVectorOperations += '\t#Document:MultiplyTuple' + str(dimensionality) + 'OperatorLHS#\n'
        toVectorOperations += '\tpublic static ' + vectorName + str(dimensionality) + ' operator *(#Quantity# a, ('
        for name in GenerateVectorN.vectorNames(dimensionality):
            toVectorOperations += 'double ' + lowerCase(name) + ', '
        toVectorOperations = toVectorOperations[:-2] + ') b) #newline#=> a.Multiply(b);\n'

        toVectorOperations += '\t#Document:MultiplyTuple' + str(dimensionality) + 'OperatorRHS#\n'
        toVectorOperations += '\tpublic static ' + vectorName + str(dimensionality) + ' operator *(('
        for name in GenerateVectorN.vectorNames(dimensionality):
            toVectorOperations += 'double ' + lowerCase(name) + ', '
        toVectorOperations = toVectorOperations[:-2] + ') a, #Quantity# b) #newline#=> b.Multiply(a);\n'

    return toVectorMethods + toVectorOperations

def insertAppropriateNewlines(text):
    rebuilt = ''

    for line in text.split('\n'):
        if '#newline#' in line:
            indent = re.findall('[ ]*', line)[0]
            comment = line.strip()[:3] == '///'
            length = 0
            components = line.split('#newline#')
            for component in components:
                if length > 0 and length + len(component) > 175:
                    if comment:
                        rebuilt += '\n' + indent + '/// '
                    else:
                        rebuilt += '\n' + indent + '\t'
                    length = 0
                rebuilt += component
                length += len(component)

            rebuilt += '\n'
        else:
            rebuilt += line + '\n'

    return rebuilt[:-1]

def removeConsecutiveNewlines(text):
    rebuilt = ''
    previousWasEmpty = True
    for line in text.split('\n'):
        if line.strip() == '':
            if previousWasEmpty:
                continue
            else:
                previousWasEmpty = True
                rebuilt += line + '\n'
        else:
            previousWasEmpty = False
            rebuilt += line + '\n'

    if previousWasEmpty:
        rebuilt = rebuilt[:-1]

    return rebuilt[:-1]

def generate(targetDirectory, templateRaw, data, quantityName, quantityData):
    modified = templateRaw

    interfacesText = composeInterfacesText(data, quantityName, quantityData)
    modified = modified.replace('#Interfaces#', interfacesText)

    if len(interfacesText) > 0:
        modified = modified.replace('#CommaIfInterface#', ',')
    else:
        modified = modified.replace('#CommaIfInterface#\n', '')

    sharedUnitsText = composeSharedUnitsText(quantityData)
    modified = modified.replace('#SharedUnits#', sharedUnitsText)

    magnitudeFromUnitDoubleText = composeMagnitudeFromUnitDoubleText(quantityData)
    modified = modified.replace('#MagnitudeFromUnitDouble#', magnitudeFromUnitDoubleText)

    magnitudeFromUnitScalarText = composeMagnitudeFromUnitScalarText(quantityData)
    modified = modified.replace('#MagnitudeFromUnitScalar#', magnitudeFromUnitScalarText)

    magnitudeToUnitText = composeMagnitudeToUnitText(quantityData)
    modified = modified.replace('#MagnitudeToUnit#', magnitudeToUnitText)

    basesText = composeBasesText(quantityName, quantityData)
    modified = modified.replace('#Bases#', basesText)

    fromText = composeFromText(data, quantityData)
    modified = modified.replace('#From#', fromText)

    unitsText = composeUnitsText(quantityName, quantityData)
    modified = modified.replace('#Units#', unitsText)

    additiveText = composeAdditiveText(quantityData)
    modified = modified.replace('#Additive#', additiveText)

    cancelsText = composeCancelsText(quantityData)
    modified = modified.replace('#Cancels#', cancelsText)

    toVectorText = composeToVectorText(data, quantityName, quantityData)
    modified = modified.replace('#ToVector#', toVectorText)

    if quantityData['symbol'][0] == '[':
        if quantityData['symbol'] == '[baseUnits]':
            modified = modified.replace('#Abbreviation#', quantityData['baseUnits'])
    else:
        modified = modified.replace('#Abbreviation#', quantityData['symbol'])

    if quantityData['unit'][0] == '[':
        if quantityData['unit'] == '[UnitOf]':
            modified = modified.replace('#Unit#', 'UnitOf' + quantityName)
            modified = modified.replace('#UnitVariable#', lowerCase('UnitOf') + quantityName)
    else:
        modified = modified.replace('#Unit#', quantityData['unit'])
        modified = modified.replace('#UnitVariable#', lowerCase(quantityData['unit']))

    powersText = composePowersText(quantityData)
    modified = modified.replace('#Powers#', powersText)

    invertDoubleText = composeInversionOperatorDoubleText(quantityName, quantityData)
    modified = modified.replace('#InversionOperatorDouble#', invertDoubleText)

    invertScalarText = composeInversionOperatorScalarText(quantityName, quantityData)
    modified = modified.replace('#InversionOperatorScalar#', invertScalarText)

    modified = modified.replace('#Quantity#', quantityName)

    modified = modified.replace('\t', ' ' * 4)

    modified = Documentation.ScalarDocumenter.document(modified, quantityName, quantityData)

    modified = insertAppropriateNewlines(modified)
    modified = removeConsecutiveNewlines(modified)

    f = open(targetDirectory + '\\Generated' + quantityName + '.cs', 'w')
    f.write(modified)
    f.close()