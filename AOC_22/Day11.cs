using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    

    class Monkey{
        public static int commondivider = 1;
        public List<long> items{get; set;}
        public String opp{get; set;}
        public String opStr{get; set;}
        public int testVal{get; set;}

        public int nThrown{get; set;}

        public Monkey tMonkey{get; set;} 
        public Monkey fMonkey{get; set;}

        public Monkey(){
        }
        public Monkey(List<long> items, String opp, String opStr, int testVal, Monkey tMonkey, Monkey fMonkey){
            this.items = items;
            this.opp = opp;
            this.opStr = opStr;
            this.testVal = testVal;
            this.nThrown = 0;
            this.tMonkey = tMonkey;
            this.fMonkey = fMonkey;
        }

        

        public void throwItems(bool thrw){
            long newVal = 0, opVal;
            int tTest = this.testVal;
            this.items.Reverse();
            foreach(var item in this.items){

                if (opStr == "old") {
                    opVal = item;
                    }
                else opVal = (long)int.Parse(opStr);

                if (opp == "*") {
                    //newVal = item * opVal;
                    newVal = item * opVal;
                    tTest = this.testVal; 
                } 

                else if (opp == "+") newVal = item + opVal; 

                if (thrw) newVal = (long)Math.Floor((double)newVal/3);
                else{
                    newVal = newVal%commondivider;
                    //Console.WriteLine(newVal);
                }
                
                //Console.Write(newVal + " " + newVal%this.testVal + "| ");                      
                //Console.Write(commondivider);                      
                if(newVal%tTest == 0){
                    //newVal=tTest; 
                    this.tMonkey.grab(newVal);
                    }
                else this.fMonkey.grab(newVal);
                this.nThrown +=1;
            }
            //Console.WriteLine();
            this.items.Clear();
        }


        public void grab(long item){
            this.items.Add(item);
        } 

    }

    class Day11{

        private static List<Monkey> createMonkeys(String[] data){
            List<Monkey> monkeys = new List<Monkey>();
            List<List<String>> instructions = new List<List<string>>();
            instructions.Add(new List<string>());
            monkeys.Add(new Monkey());
            foreach(var row in data){
                if(row.Length ==  0) {
                    instructions.Add(new List<string>());
                    monkeys.Add(new Monkey());
                    }
                else instructions[instructions.Count-1].Add(row);
            }

            for(int i = 0; i < instructions.Count; i++){
                List<long> items = new List<long>();
                String opp, opVal;
                int test;
                Monkey monkeyT, monkeyF;

                // get items
                var itemsStr = instructions[i][1].Split(' ', ',');
                foreach(var itemStr in itemsStr ) {
                    int item;
                    if (int.TryParse(itemStr, out item)) items.Add((int)item);
                }
                monkeys[i].items = items;

                // get operator and operator value
                var ops = instructions[i][2].Split(' ').Reverse().ToList();
                //foreach(var val in ops) Console.WriteLine(val);
                opVal = ops[0];
                opp = ops[1];
                monkeys[i].opp = opp;
                monkeys[i].opStr = opVal;

                // get test val
                test = int.Parse(instructions[i][3].Split(' ').Reverse().ToList()[0]);
                monkeys[i].testVal = test;
                Monkey.commondivider *= test;

                
                // get t and f monkeys
                monkeyT = monkeys[int.Parse(instructions[i][4].Split(' ').Reverse().ToList()[0])];
                monkeyF = monkeys[int.Parse(instructions[i][5].Split(' ').Reverse().ToList()[0])];

                monkeys[i].tMonkey = monkeyT;
                monkeys[i].fMonkey = monkeyF;
            }
            return monkeys;
        }

        private static void result(String[] data){
            var monkeys = createMonkeys(data);

            for (int i = 0; i<20; i++){
                foreach (Monkey m in monkeys){
                    m.throwItems(true);
                }
            }
            
            List<int> res = new List<int>();
            foreach (Monkey m in monkeys) res.Add(m.nThrown);
            res.Sort();
            Console.WriteLine(res[res.Count-1]*res[res.Count-2]);
        }

        private static void result2(String[] data){
                      var monkeys = createMonkeys(data);

            for (int i = 0; i<10000; i++){
                foreach (Monkey m in monkeys){
                    m.throwItems(false);
                }
            }
            
            List<int> res = new List<int>();
            foreach (Monkey m in monkeys) res.Add(m.nThrown);
            foreach(var v in res)Console.WriteLine(v);
            res.Sort();
            Console.WriteLine((long)res[res.Count-1]*(long)res[res.Count-2]);

        }


        public static void run(){
            Console.WriteLine("Day 11");
            String[] data = Helpers.getData("./Data/d11.txt");
            //result(data);
            result2(data);
        }
    }
    
}