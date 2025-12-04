import sys
import os  
from typing import List
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
from solution_1 import *

import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def removeRolls(data: List[str]):
    dataCopy = data.copy()
    removed = 0
    for col in range(len(data)):
        for row in range(len(data[col])):
            if(data[row][col] == '.'):
                continue
            numberAdjacentRolls = getNumberAdjacentRolls(data, row, col)
            if(numberAdjacentRolls < 4):
                rowString = dataCopy[row]
                dataCopy[row] = rowString[:col] + '.' + rowString[col+1:]
                removed += 1
    return dataCopy, removed

                
if (__name__ == '__main__'):
    d = data
    next = True
    count = 0

    while(next):
        d, nRemoved = removeRolls(d)
        count += nRemoved
        if(nRemoved == 0):
            next = False

    print(count)
    

