import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
from solution_1 import *

import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def isRepeatedMany(id:str):
    l = len(id)

    repeated = 2
    while (repeated <= l):
        repLength = l / repeated
        if(repLength % 2 == 0 or repLength % 2 == 1 ):
            parts = split_list(id, int(repLength))
            isRepeated = len(set(parts)) == 1
            if(isRepeated):
                return True
        repeated += 1
    return False
         
def split_list(lst, size):
    return [lst[i:i+size] for i in range(0, len(lst), size)]
 

if (__name__ == '__main__'):
    idRanges = parseCSV(data)
    sum = 0
    for idRange in idRanges:
        ids = expandRange(idRange)
        for id in ids:
            if(isRepeatedMany(str(id))):
                sum += id

        
    print(sum)