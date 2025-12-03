import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getHighstnumber(line:str):
    h1, h1i = getHighestnumberIndex(line[:-1])
    h2, h2i = getHighestnumberIndex(line[h1i+1:])
    return int(h1+h2)

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
        sum += getHighstnumber(line)
    print(sum)