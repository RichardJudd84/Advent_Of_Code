using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day10{

        private static void result(String[] data){
            int cycle = 0, x=1, res = 0;
            List<int> sStrngths = new List<int>();

            foreach(var row in data){
                if(cycle > 220) break;
                String[] rstr = row.Split(" ");
                if (rstr[0] == "noop"){
                    cycle ++;
                    if((cycle+20)%40 == 0) {
                        sStrngths.Add(cycle * x);
                        res += (cycle * x);
                    }
                    Console.WriteLine("noop");
                }
                else if (rstr[0] == "addx"){
                    Console.WriteLine("addx");
                    for(int i = 0; i<2; i++){
                       cycle ++; 
                       
                       if((cycle+20)%40 == 0) {
                        sStrngths.Add(cycle * x);
                        res += (cycle * x);
                       }
                        
                    }

                    x += int.Parse(rstr[1]);
                    

                }
            }

            foreach(var e in sStrngths){
                Console.WriteLine(e);
            }
            Console.WriteLine(res);
        }

        private static void result2(String[] data){
            int x = 0, sprite = 1;

            void cycle(){
                if((x)%40 == 0) Console.WriteLine();
                if(x%40 >= sprite-1 &&  x%40 <= sprite+1) Console.Write('#');
                else Console.Write('.');
                x++;
            }

            foreach(var row in data){
                String[] rstr = row.Split(" ");
                cycle();                    
                if (rstr[0] == "addx"){ 
                    cycle();
                    sprite += int.Parse(rstr[1]);
                }
            }
        }

        public static void run(){
            Console.WriteLine("Day 10");
            String[] data = Helpers.getData("./Data/d10.txt");
            //result(data);
            result2(data);
        }
    }
    
}