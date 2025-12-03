import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *

import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getHighestnumber(line:str):
    highest = ''
    hi = 0
    for i in range(11, 0, -1):
        next, nexti = getHighestnumberIndex(line[hi:-(i)])
        hi += (nexti + 1)
        highest+=next
    next, nexti = getHighestnumberIndex(line[hi:])
    highest+=next
    return int(highest)


def getHighestnumberIndex(line:str):
    highest = line[0]
    highestIndex = 0
    for i in range(1, len(line)):
        if(line[i] > highest):
            highest = line[i]
            highestIndex = i
  
    return highest, highestIndex


if(__name__ == '__main__'):
    sum = 0
    for line in data:
        sum += getHighestnumber(line)
    print(sum)
