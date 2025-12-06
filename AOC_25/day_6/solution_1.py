import sys
import os  
from typing import *
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def parseData(data: List[str]):
    parsedData: List[List[int | str]] = []
    for line in data:
        row  = [int(el) if el.isnumeric() else el  for el in line.strip().split() if el.isnumeric() or el.isascii()]
        parsedData.append(row)
    return parsedData
    

def getEquations(data: List[str]):
    eqeuations = []
    for colI in range(len(data[0])):
        equation = []
        for rowI in range(len(data)):
            equation.append(data[rowI][colI])
        eqeuations.append(equation)
    return eqeuations

def calculate(equation: List[int | str]):
    operator = equation.pop()
    total = equation.pop(0)
    if(operator == '+'):
        for el in equation: 
            total += el
    elif(operator == '-'):
        for el in equation: 
            total -= el
    elif(operator == '*'):  
        for el in equation: 
            total *= el
    elif(operator == '/'):
        for el in equation: 
            total /= el
         
    return total

    
        


if(__name__ == '__main__'):
    parsedData =  parseData(data)
    equations = getEquations(parsedData)
    sum = 0
    for equation in equations:
        sum += calculate(equation)
    print(sum)