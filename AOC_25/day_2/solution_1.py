import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def idRepeated(id:str):
    l = len(id)
    if((l % 2) == 1):
        return False
    mid = int(l/2)
    isRepeated =  id[: mid] == id[mid:]
    return isRepeated


if (__name__ == '__main__'):
    idRanges = parseCSV(data)
    sum = 0
    for idRange in idRanges:
        ids = expandRange(idRange)
        for id in ids:
            if(idRepeated(str(id))):
                sum += id

        
    print(sum)