import sys
import os 
from typing import List
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getIngedientsAndRanges(lines: List[str]):
    ranges: List[tuple[str]] = []
    ingredients: List[str] = []

    for line in lines:
        if('-' in line):
            ranges.append(tuple(line.split('-')))
        elif(line.isdigit()):
            ingredients.append(line)
    
    return ranges, ingredients

def isValueInRange(range: tuple[str], value: str):
    start, end = range
    isInRange =  int(value) >= int(start) and int(value) <= int(end)
    return isInRange

def isValueInRanges(ranges: List[tuple[str]], value:str):
    for range in ranges:
        isInRange = isValueInRange(range, value)
        if(isInRange):
            return True
        
    return False


if(__name__ == '__main__'):
    ranges, ingredients = getIngedientsAndRanges(data)

    count = 0

    for item in ingredients:
       if(isValueInRanges(ranges, item)):
          count += 1

    print(count)