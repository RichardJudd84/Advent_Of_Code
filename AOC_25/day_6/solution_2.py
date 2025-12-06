import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *

testData = getLineStringsWithWhiteSpace(input_paths.input1ExamplePath)
data = getLineStringsWithWhiteSpace(input_paths.input1Path)

def getEquationsFromData(data: List[str]):
    maxRowWidth = max([len(row) for row in data])
    operatorWidth = len(data[-1])

    equations: List[List[int | str]] = []

    currentOperator = '';
    currentEquation = []
    
    for colI in range(maxRowWidth):
        if(colI < operatorWidth and data[-1][colI] in ['+', '*']):
            #next equation found
            if(len(currentEquation) > 0):
                currentEquation.append(currentOperator)
                equations.append(currentEquation)
                currentEquation = []

            currentOperator = data[-1][colI]
        numberString = ""

        for rowI in range(len(data)-1):
            if(colI >= len(data[rowI])):
                continue
            cell = data[rowI][colI]

            if(cell.isnumeric()):
                numberString += cell

        if(numberString.isnumeric()):
            currentEquation.append(int(numberString))
    
    currentEquation.append(currentOperator)
    equations.append(currentEquation)

    return equations



    
   

def parseEquation(equation: List[int | str]):
    operator = equation.pop()
    maxElement = len(str(max(equation)))
    parsedEquation = []
    for elIndex in range(1,maxElement+1):
        parsedElement = int("".join([str(el)[-elIndex] for el in equation if len(str(el))>=elIndex]))
        parsedEquation.append(parsedElement)
    parsedEquation.append(operator)
    print(parsedEquation)
         

if(__name__ == '__main__'):
    
    equations = getEquationsFromData(data)
    sum = 0
    for equation in equations:
        sum += calculate(equation)
    print(sum)

