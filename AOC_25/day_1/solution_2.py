import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *

import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

count = 0
currentPos = 50
for line in data: 
    direction = line[0]
    clicks = int(line[1:])
    
    offset = 1 if (direction == 'R') else -1

    for click in  range(clicks):
        currentPos = (currentPos + offset)%100
        if (currentPos == 0):
            count += 1


print(count)
