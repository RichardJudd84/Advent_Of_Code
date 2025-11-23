using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day5{ 
        private static List<String> stacks = new List<String>(); 
        private static List<int[]> instructions = new List<int[]>();

        private static void splitData(String[] data){
            List<String> tempStacks = new List<String>();
            foreach(var row in data){
                if (row.Length == 35) tempStacks.Add(row);
                else if (18 <= row.Length && row.Length <= 19) {
                    var tmp = row.Split(" ");
                    int[] vals = new int[3]{int.Parse(tmp[1]),int.Parse(tmp[3]),int.Parse(tmp[5])};
                    instructions.Add(vals);
                }
            }

            List<int> indices = new List<int>();
            String numRow = tempStacks[tempStacks.Count-1];
            for(int i = 0;  i < numRow.Length; i++){
                if (char.IsAsciiDigit(numRow[i])){
                    indices.Add(i);
                }
            }

            foreach(int x in indices){
                String stack = "";
                for(int y = tempStacks.Count-2; y>=0; y--){
                    if(char.IsAsciiLetter(tempStacks[y][x])){
                        stack += tempStacks[y][x];
                    }   
                }
                stacks.Add(stack);
            }
        }

        private static void result(){
           foreach(var row in instructions) {
                int qt = row[0], fr = row[1]-1, to = row[2]-1;
                for(int i = 0; i<qt; i++){
                    int ln = stacks[fr].Length-1;
                    char toMov = stacks[fr][ln];
                    stacks[fr] = stacks[fr].Remove(ln);
                    stacks[to] += toMov;
                } 
            }
                
            foreach(var row in stacks) Console.Write(row[row.Length-1]);
            Console.WriteLine();
        }

        private static void result2(){
            foreach(var row in instructions) {
                int qt = row[0]-1, fr = row[1]-1, to = row[2]-1;
                int ln = stacks[fr].Length-1;
                String toMov = stacks[fr].Substring(ln-qt);
                stacks[fr] = stacks[fr].Remove(ln-qt);
                stacks[to] += toMov;
                 
            }
                
            foreach(var row in stacks) Console.Write(row[row.Length-1]);
            Console.WriteLine();
        }


        public static void run(){
            Console.WriteLine("Day 4");
            var data = Helpers.getData("./Data/d5.txt");
            splitData(data);
            //result();
            result2();
        }
    }
    
}