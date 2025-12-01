import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def turnDial( currentPosition: int , code: str )-> int: 
    direction = code[0]
    number = int(code[1:]) 
    nextPosition = 0

    if(direction == 'R'):
        nextPosition =  (currentPosition + number) % 100
    elif(direction == 'L'):
        nextPosition = (currentPosition - number) % 100
    
    return nextPosition

currentPosition = 50
count = 0

print('day 1')
print(testData)
for line in data:
    next = turnDial(currentPosition, line)
    if (next == 0):
        count +=1
    currentPosition = next

print(count)
