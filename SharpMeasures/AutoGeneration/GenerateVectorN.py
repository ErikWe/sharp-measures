import re

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

def vectorNames(dimensionality):
    return ['X', 'Y', 'Z'][:dimensionality]

def getComponentName(quantityName, quantityData):
    if quantityData['component'] == '[name]':
        return quantityName
    else:
        return quantityData['component']

def getUnitName(data, quantityName, quantityData):
    if quantityData['unit'] == '[UnitOf]':
        return 'UnitOf' + quantityName
    elif quantityData['unit'] == '[component]':
        return getUnitName(data, getComponentName(quantityName, quantityData), data['scalars'][getComponentName(quantityName, quantityData)])
    else:
        return quantityData['unit']

def setComponentTexts(data, quantityName, quantityData, text, dimensionality):
    component = getComponentName(quantityName, quantityData) if quantityData['component'] else 'Scalar'
    
    componentsDefinition = ""
    components = ""
    zeroComponents = ""
    doubleDefinition = ""
    tupleAccess = ""
    componentsFromUnit = ""
    componentsFormatting = ""
    negateComponents = ""
    negateA = ""
    componentsTimesFactor = ""
    componentsDividedByDivisor = ""
    componentsRemainderDivisor = ""
    vectorATimesScalarB = ""
    scalarATimesVectorB = ""
    aDividedByB = ""
    aRemainderB = ""
    aAccess = ""
    componentsTimesFactorMagnitude = ""
    componentsDividedByDivisorMagnitude = ""
    vectorATimesScalarBMagnitude = ""
    scalarAMagnitudeTimesVectorB = ""
    aDividedByBMagnitude = ""
    
    for name in vectorNames(dimensionality):
        componentsDefinition += 'double ' +  name + ', '
        components += name + ', '
        zeroComponents += '0, '
        doubleDefinition += 'double ' + lowerCase(name) + ', '
        tupleAccess += 'components.' + lowerCase(name) + ', '
        componentsFormatting += '{' + name + '}, '
        negateComponents += '-' + name + ', '
        negateA += '-a.' + name + ', '
        componentsTimesFactor += name + ' * factor, '
        componentsDividedByDivisor += name + ' / divisor, '
        componentsRemainderDivisor += name + ' % divisor, '
        vectorATimesScalarB += 'a.' + name + ' * b, '
        scalarATimesVectorB += 'a * b.' + name + ', '
        aDividedByB += 'a.' + name + ' / b, '
        aRemainderB += 'a.' + name + ' % b, '
        aAccess += 'a.' + name + ', '
        componentsTimesFactorMagnitude += name + ' * factor.Magnitude, '
        componentsDividedByDivisorMagnitude += name + ' / divisor.Magnitude, '
        vectorATimesScalarBMagnitude += 'a.' + name + ' * b.Magnitude, '
        scalarAMagnitudeTimesVectorB += 'a.Magnitude * b.' + name + ', '
        aDividedByBMagnitude += 'a.' + name + ' / b.Magnitude, '

        if quantityData['unitBias'] == True or quantityData['unitBias'] == '[component]' and data['scalars'][getComponentName(quantityName, quantityData)]['unitBias']:
            componentsFromUnit += '(' + lowerCase(name) + ' * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale, '
        else:
            componentsFromUnit += lowerCase(name) + ' * #UnitVariable#.Factor, '

    text = text.replace('#ComponentsDefinition#', componentsDefinition[:-2])
    text = text.replace('#Components#', components[:-2])
    text = text.replace('#ZeroComponents#', zeroComponents[:-2])
    text = text.replace('#DoubleDefinition#', doubleDefinition[:-2])
    text = text.replace('#TupleAccess#', tupleAccess[:-2])
    text = text.replace('#ComponentsFromUnit#', componentsFromUnit[:-2])
    text = text.replace('#ComponentsFormatting#', componentsFormatting[:-2])
    text = text.replace('#NegateComponents#', negateComponents[:-2])
    text = text.replace('#NegateA#', negateA[:-2])
    text = text.replace('#ComponentsTimesFactor#', componentsTimesFactor[:-2])
    text = text.replace('#ComponentsDividedByDivisor#', componentsDividedByDivisor[:-2])
    text = text.replace('#ComponentsRemainderDivisor#', componentsRemainderDivisor[:-2])
    text = text.replace('#VectorATimesScalarB#', vectorATimesScalarB[:-2])
    text = text.replace('#ScalarATimesVectorB#', scalarATimesVectorB[:-2])
    text = text.replace('#ADividedByB#', aDividedByB[:-2])
    text = text.replace('#ARemainderB#', aRemainderB[:-2])
    text = text.replace('#aAccess#', aAccess[:-2])
    text = text.replace('#ComponentsTimesFactorMagnitude#', componentsTimesFactorMagnitude[:-2])
    text = text.replace('#ComponentsDividedByDivisorMagnitude#', componentsDividedByDivisorMagnitude[:-2])
    text = text.replace('#VectorATimesScalarBMagnitude#', vectorATimesScalarBMagnitude[:-2])
    text = text.replace('#ScalarAMagnitudeTimesVectorB#', scalarAMagnitudeTimesVectorB[:-2])
    text = text.replace('#ADividedByBMagnitude#', aDividedByBMagnitude[:-2])

    return text

def setComponentSpecificBlocks(data, quantityName, quantityData, text):
    if quantityData['component']:

        text = text.replace('\n#ComponentMagnitude#', '')
        text = text.replace('\n#/ComponentMagnitude#', '')
        text = text.replace('\n#ComponentNormalization#', '')
        text = text.replace('\n#/ComponentNormalization#', '')

        text = re.sub('(\n#DoubleMagnitude#)(.+?)(#\/DoubleMagnitude#)', '', text, flags=re.S)
        text = re.sub('(\n#DoubleNormalization#)(.+?)(#\/DoubleNormalization#)', '', text, flags=re.S)
    else:
        text = text.replace('\n#DoubleMagnitude#', '')
        text = text.replace('\n#/DoubleMagnitude#', '')
        text = text.replace('\n#DoubleNormalization#', '')
        text = text.replace('\n#/DoubleNormalization', '')

        text = re.sub('(\n#ComponentMagnitude#)(.+?)(#\/ComponentMagnitude#)', '', text, flags=re.S)
        text = re.sub('(\n#ComponentNormalization#)(.+?)(#\/ComponentNormalization#)', '', text, flags=re.S)

    if quantityData['component'] and data['scalars'][getComponentName(quantityName, quantityData)]['square']:
        text = text.replace('\n#QuantitySquaredMagnitude#', '')
        text = text.replace('\n#/QuantitySquaredMagnitude#', '')

        text = re.sub('(\n#DoubleSquaredMagnitude#)(.+?)(#\/DoubleSquaredMagnitude#)', '', text, flags=re.S)
    else:
        text = text.replace('\n#DoubleSquaredMagnitude#', '')
        text = text.replace('\n#/DoubleSquaredMagnitude#', '')

        text = re.sub('(\n#QuantitySquaredMagnitude#)(.+?)(#\/QuantitySquaredMagnitude#)', '', text, flags=re.S)

    return text

def composeInterfacesText(data, quantityName, quantityData):
    interfaces = []

    if quantityData['additive']:
        interfaces.append('IAddableVector#Dimensionality#<#Quantity#, #Quantity#>')
    if quantityData['subtractive']:
        interfaces.append('ISubtractableVector#Dimensionality#<#Quantity#, #Quantity#>')

    interfacesText = ""
    for interface in interfaces:
        interfacesText += '\t' + interface + ',\n'

    return interfacesText[:-2]

def composeUnitsText(data, quantityName, quantityData):
    unitsText = ""

    if quantityData['units'] == '[component]':
        units = data['scalars'][getComponentName(quantityName, quantityData)]['units']
    else:
        units = quantityData['units']

    for unit in units:
        if 'separator' in unit and unit['separator'] == True:
            unitsText += '\n'
        else:
            unitsText += '\tpublic Vector#Dimensionality# ' + parsePlural(unit['singular'], unit['plural']) + ' => '
            unitsText += 'InUnit(' + getUnitName(data, quantityName, quantityData) + '.' + unit['singular'] + ');\n'

    return unitsText

def composeComponentsToUnit(data, quantityName, quantityData):
    if quantityData['unitBias'] == True or quantityData['unitBias'] == '[component]' and data['scalars'][getComponentName(quantityName, quantityData)]['unitBias']:
        return '(vector / #UnitVariable#.BaseScale - Vector#Dimensionality#.Ones * #UnitVariable#.Bias) / #UnitVariable#.Prefix.Scale'
    else:
        return 'vector / #UnitVariable#.Factor'

def composeSharedUnitsText(data, quantityData):
    sharedUnitsText = ""

    for sharesUnit in quantityData['sharesUnit']:
        for i in quantityData['dimensionalities']:
            if i in data['vectors'][sharesUnit]['dimensionalities']:
                sharedUnitsText += '\tpublic ' + sharesUnit + '#Dimensionality# As' + sharesUnit + '#Dimensionality#() => new('
                for name in vectorNames(i):
                    sharedUnitsText += name + ', '
        sharedUnitsText = sharedUnitsText[:-2] + ');\n'

    return sharedUnitsText[:-1]

def composeAdditiveText(quantityData, dimensionality):
    additiveText = ""

    if quantityData['additive']:
        additiveText = '\tpublic #Quantity# Add(#Quantity# term) => new('
        for name in vectorNames(dimensionality):
            additiveText += name + ' + term.' + name + ', '
        additiveText = additiveText[:-2] + ');\n'
    
    if quantityData['subtractive']:
        additiveText += '\tpublic #Quantity# Subtract(#Quantity# term) => new('
        for name in vectorNames(dimensionality):
            additiveText += name + ' - term.' + name + ', '
        additiveText = additiveText[:-2] + ');\n'

    if quantityData['additive']:
        additiveText += '\tpublic static #Quantity# operator +(#Quantity# a, #Quantity# b) => new('
        for name in vectorNames(dimensionality):
            additiveText += 'a.' + name + ' + b.' + name + ', '
        additiveText = additiveText[:-2] + ');\n'

    if quantityData['subtractive']:
        additiveText += '\tpublic static #Quantity# operator -(#Quantity# a, #Quantity# b) => new('
        for name in vectorNames(dimensionality):
            additiveText += 'a.' + name + ' - b.' + name + ', '
        additiveText = additiveText[:-2] + ');\n'

    return additiveText

def generate(targetDirectory, templateRaw, data, quantityName, quantityData):
    for dimensionality in quantityData['dimensionalities']:
        generateForDimensionality(targetDirectory, templateRaw, data, quantityName, quantityData, dimensionality)

def generateForDimensionality(targetDirectory, templateRaw, data, quantityName, quantityData, dimensionality):
    modified = templateRaw

    modified = setComponentTexts(data, quantityName, quantityData, modified, dimensionality)
    modified = setComponentSpecificBlocks(data, quantityName, quantityData, modified)

    interfacesText = composeInterfacesText(data, quantityName, quantityData)
    modified = modified.replace('#Interfaces#', interfacesText)

    if len(interfacesText) > 0:
        modified = modified.replace('#CommaIfInterface#', ',')
    else:
        modified = modified.replace('#CommaIfInterface#\n', '')
    
    sharedUnitsText = composeSharedUnitsText(data, quantityData)
    modified = modified.replace('#SharedUnits#', sharedUnitsText)

    componentsToUnitText = composeComponentsToUnit(data, quantityName, quantityData)
    modified = modified.replace('#ComponentsToUnit#', componentsToUnitText)

    unitsText = composeUnitsText(data, quantityName, quantityData)
    modified = modified.replace('#Units#', unitsText)

    additiveText = composeAdditiveText(quantityData, dimensionality)
    modified = modified.replace('#Additive#', additiveText)

    if dimensionality == 3:
        modified = modified.replace('\n#Vector3#', '')
        modified = modified.replace('\n#/Vector3#', '')
    else:
        modified = re.sub('(\n#Vector3#)(.+?)(#\/Vector3#)', '', modified, flags=re.S)

    modified = modified.replace('#Quantity#', quantityName + str(dimensionality))
    modified = modified.replace('#Dimensionality#', str(dimensionality))

    if quantityData['symbol'] == '[component]':
        if data['scalars'][getComponentName(quantityName, quantityData)]['symbol'] == '[baseUnits]':
            modified = modified.replace('#Abbreviation#', data['scalars'][getComponentName(quantityName, quantityData)]['baseUnits'])
        else:
            modified = modified.replace('#Abbreviation#', data['scalars'][getComponentName(quantityName, quantityData)]['symbol'])
    else:
        modified = modified.replace('#Abbreviation#', quantityData['symbol'])
        
    modified = modified.replace('#Unit#', getUnitName(data, quantityName, quantityData))
    modified = modified.replace('#UnitVariable#', lowerCase(getUnitName(data, quantityName, quantityData)))

    if quantityData['component']:
        modified = modified.replace('#Component#', getComponentName(quantityName, quantityData))
    else:
        modified = modified.replace('#Component#', 'Scalar')

    if quantityData['component'] and data['scalars'][getComponentName(quantityName, quantityData)]['square']:
        modified = modified.replace('#SquaredComponent#', data['scalars'][getComponentName(quantityName, quantityData)]['square'])

    while True:
        replaced = modified.replace('\n' * 3, '\n' * 2)
        if replaced == modified:
            modified = replaced
            break
        modified = replaced

    f = open(targetDirectory + '\\Generated' + quantityName + str(dimensionality) + '.cs', 'w')
    f.write(modified)
    f.close()