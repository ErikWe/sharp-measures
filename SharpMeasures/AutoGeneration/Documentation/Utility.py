import re
from . import DocsReader

def lowerCase(text):
    return text[0].lower() + text[1:]

def getUnitName(quantityName, quantityData):
    if quantityData['unit'] == '[UnitOf]':
        return 'UnitOf' + quantityName
    else:
        return quantityData['unit']

def getInverseQuantity(quantityName, quantityData):
    return quantityData['invert']

def getSquareQuantity(quantityName, quantityData):
    return quantityData['square']

def getCubeQuantity(quantityName, quantityData):
    return quantityData['cube']

def getSquareRootQuantity(quantityName, quantityData):
    return quantityData['squareRoot']

def getCubeRootQuantity(quantityName, quantityData):
    return quantityData['cubeRoot']

def getVectorName(quantityName, quantityData):
    if quantityData['vector'] == '[name]':
        return quantityName
    elif quantityData['vector']:
        return quantityData['vector']

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

def getBases(quantityName, quantityData):
    singular = []
    plural = []

    if quantityData['bases'] == '[units]':
        bases = quantityData['units']
    else:
        bases = quantityData['bases']

    if DocsReader.readScalarTag(quantityName, 'Bases'):
        appliedBases = DocsReader.readScalarTag(quantityName, 'Bases').split(' ')

        for appliedBase in appliedBases:
            for base in bases:
                if 'singular' in base and base['singular'] == appliedBase:
                    singular.append(base['singular'])
                    if 'plural' in base:
                        plural.append(parsePlural(base['singular'], base['plural']))
                    else:
                        plural.append(base['singular'])
    else:
        for base in bases:
            if 'singular' in base:
                singular.append(base['singular'])
                if 'plural' in base:
                    plural.append(parsePlural(base['singular'], base['plural']))
                else:
                    plural.append(base['singular'])

    return { 'singular': singular, 'plural': plural }