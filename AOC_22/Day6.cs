using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day6{

        private static void getMarker(String msg, int mLn){
           
            for (int i = 0, start = mLn; i < msg.Length-mLn-1; i++, start++){
                String marker = msg.Substring(i, mLn);
                
                Boolean isMarker = true;
                for(int a = 0; a < mLn-1; a++){
                    for(int b = a+1; b < mLn; b++){
                        if(marker[a] == marker[b]){
                            isMarker = false;
                            break;
                        }
                    }
                }
                Console.WriteLine(marker + " " + start + " " + isMarker);
                if(isMarker){
                    break;
                }
            }
        }

        private static void result(String[] data){
           getMarker(data[0],4);
        }

        private static void result2(String[] data){
            getMarker(data[0],14);
        }


        public static void run(){
            Console.WriteLine("Day 6");
            String[] data = Helpers.getData("./Data/d6.txt");
            result(data);
            result2(data);
        }
    }
    
}