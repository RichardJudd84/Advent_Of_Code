using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day9{
        private static int[] h = new int[2]{0,0} , t = new int[2]{0,0};
        private static HashSet<String> tailVisits = new HashSet<string>{"0,0"};
        private static int tmove = 0, moves = 0;

        private static int[,] rope = new int[10,2];

        

        private static void moveRope(){
            rope[0,0] = h[0];
            rope[0,1] = h[1];
            Console.Write($"({rope[0,0]},{rope[0,1]}), ");
            for(int i = 0; i<rope.GetLength(0)-1; i++){
                
                int hy = rope[i,0], hx = rope[i,1], ty = rope[i+1,0], tx = rope[i+1,1];
                bool b = (Math.Abs(hy-ty) > 1 || Math.Abs(hx-tx) > 1);
                //Console.WriteLine(b);
                if(b){
                    if(ty < hy) ty++;
                    else if(ty > hy) ty--;
                    if(tx < hx) tx++;
                    else if(tx > hx) tx--;

                    rope[i+1, 0] = ty;
                    rope[i+1, 1] = tx;

                    
                    
                }
                Console.Write($"({tx},{ty}), ");
            } 
            Console.WriteLine(); 
            tailVisits.Add($"{rope[rope.GetLength(0) -1,0]},{rope[rope.GetLength(0)-1,1]}");
        }


        private static void movehead(String command){
            String[] coms = command.Split(" ");
            char direction = coms[0][0];
            int steps = int.Parse(coms[1]);
            for(int i = 0; i<steps; i++){
                moves ++;

               switch(direction){
                case 'U':
                    h[0]++;
                    break;
                case 'D':
                    h[0]--;
                    break;
                case 'L':
                    h[1]--;
                    break;
                case 'R':
                    h[1]++;
                    break;
                default:
                    Console.WriteLine("Something is wrong!");
                    Console.ReadLine();
                    break;
                } 
                moveTail();
                //moveRope();
            }
            
        }


        
        private static void moveTail(){
            int hy = h[0], hx = h[1], ty = t[0], tx = t[1];
            bool b = (Math.Abs(hy-ty) > 1 || Math.Abs(hx-tx) > 1);
            //Console.WriteLine(b);
            if(b){
                if(ty < hy) ty++;
                else if(ty > hy) ty--;
                if(tx < hx) tx++;
                else if(tx > hx) tx--;
                t[0] = ty;
                t[1] = tx;
                tmove++;
                tailVisits.Add($"{ty},{tx}");
            }
        }

        private static void result(String[] data){
            foreach(var line in data){
                //Console.WriteLine(line);
                movehead(line);
            }
            Console.WriteLine("total Visits: " + tailVisits.Count);
            Console.WriteLine("total tail moves: " + tmove);
            Console.WriteLine("total  moves: " + moves);
        }

        private static void result2(int[] data){
         
        }


        public static void run(){
            Console.WriteLine("Day 9");
            String[] data = Helpers.getData("./Data/d9.txt");
            result(data);
            //foreach(var loc in tailVisits) Console.WriteLine(loc);
            //int x = -1, y = -2;
            //Console.WriteLine(tailVisits.Count);
            //result2(cData);
        }
    }
    
}