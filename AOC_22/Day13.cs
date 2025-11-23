using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_code_22{ 
    class Day13{

        private static List<String[]> pairs = new List<string[]>();
        private static void parseData(String[] data){
            String[] pair = new String[2];
            for (int i = 0; i < data.Length; i++){                
                if ((i+1)%3 == 0){
                    pairs.Add(pair);
                    pair = new String[2];
                }
                else{
                    pair[i%3] = data[i];
                    String line = data[i];
                }
            }
            pairs.Add(pair);
        }

        private static String getElement(String side){
            // check if first element is digit
            if (side.Length<1){
                return "";
            }
            if(side[0] == ',') side = side.Substring(1);
            if (Char.IsAsciiDigit(side[0])){
                if (side.Length>1 && Char.IsAsciiDigit(side[1])){
                    return $"{side[0]}{side[1]}";
                }
                else return $"{side[0]}";
            }

            else if (side[0] == '['){
                int nestCount = 0, i = 0;
                String el = "";

                do {  
                    Char ch = side[i];
                        
                    if(ch == '[') nestCount++;
                    else if(ch ==']') nestCount--;
                    el += ch;
                    i++;
                } while (nestCount > 0);
                return el;
            }

            else return "";
        }

        private static bool compare(String left, String right){
            bool finished = false, order = false;

            compareElements(left, right);
            return order;

            void compareVals(String sL, String sR){
                if(sR.Length > 0 && sL.Length>0){
                    //Console.WriteLine(sL + " " + sR);
                    int l = int.Parse(sL),  r = int.Parse(sR);
                    if (l < r){
                        //Console.WriteLine("Left value was lower than right");
                        finished = true;
                        order = true;
                    }
                    else if (l>r){
                        //Console.WriteLine("Left value was greater than right");
                        finished = true;
                        order = false;
                    }
                }
                else if(sL.Length > 0 && sR.Length == 0){
                    //Console.WriteLine("Right list was empty but left had values");
                    finished = true;
                    order = false;
                }
                else if(sL.Length == 0 && sR.Length > 0){
                    //Console.WriteLine("Left list was empty but right had values");
                    finished = true;
                    order = true;
                }
            }

            bool isList(String element){
                if(element.Length ==0) return false;
                else if(element[0] == '[') return true;
                else return false;
            }

            String convertToList(String val){
                return $"[{val}]";
            }

            String trim(String str){
                return str.Substring(1, str.Length-2);
            }

            String[] getListElements(String lst){
                lst = trim(lst);
                List<String> els = new List<string>();
                //Console.WriteLine(lst);
                while(lst.Length > 0){
                    if(lst[0] == ',') lst = lst.Substring(1);
                    String next = getElement(lst);
                    els.Add(next);
                    lst = lst.Substring(next.Length);
                    //Console.WriteLine(next + " " + lst);
                    //Console.ReadLine();
                }
                return els.ToArray();
            }

            void printListElements(String[] els){
                foreach(var el in els){
                    //Console.Write(el + "; ");
                }
                //Console.WriteLine();
            }

            void compareElements(String l, String r){
                String[] lEls = getListElements(l);
                String[] rEls = getListElements(r);
                printListElements(lEls);
                printListElements(rEls);
                int lI = 0, rI=0;

                while(lI < lEls.Length && rI < rEls.Length){

                    if(!isList(lEls[lI]) && !isList(rEls[rI])){ // if both are digits compare values
                       compareVals(lEls[lI], rEls[rI]);
                    }
                    if(isList(lEls[lI]) && isList(rEls[rI])){ // if both are list compare lists  
                       compareElements(lEls[lI], rEls[rI]);
                    }
                    else if(isList(lEls[lI])){ // if only left is list convert right to list and compare
                        compareElements(lEls[lI], convertToList(rEls[rI])); 
                    }
                    else if(isList(rEls[rI])){ // if only right is list convert left to list and compare
                        compareElements(convertToList(lEls[lI]), rEls[rI]); 
                    }
                    // if no comparisons are found itterate to next element until one of lists run out
                    lI ++;
                    rI ++;
                    if(finished) break;
                }
                if(finished){
                  //Console.WriteLine("Done!");  
                }
                // if one of lists ran out and lengths are not equal
                else if(!(lI < lEls.Length && rI < rEls.Length) && lEls.Length != rEls.Length){  
                    if(lI == lEls.Length && rI < rEls.Length ){  // if left list ran out first 
                        //Console.WriteLine("Left ran out of items first");
                        finished = true;
                        order = true;
                    } 
                    else if(rI == rEls.Length && lI < lEls.Length){ // if right list ran out first
                        //Console.WriteLine("Right ran out of items first "+ lI + " "+ lEls.Length + " " + rI + " "+ rEls.Length);
                        finished = true;
                        order = false;
                    } 
                    else{
                        //Console.WriteLine("Error!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }

                //else Console.WriteLine("Lists ran out of items at same time. ");
            }
        }

        private static void result(String[] data){
            // 1 get list of pairs
            int count = 0;
            parseData(data);
            for (int i = 0; i< pairs.Count; i++){
                Console.Write(i*3+1 + ": ");
                if(compare(pairs[i][0], pairs[i][1])){
                    count += i+1;
                    Console.WriteLine(true);
                    //Console.ReadLine();
                } 
                else{
                    Console.WriteLine(false);
                    //Console.ReadLine(); 
                }
            } 
            Console.WriteLine("result: " + count);          
        }

        private static int insertVal(String element, List<String> sortedData){
            int low = 0, high = sortedData.Count-1, inserted = 0; 
            bool found = false;

            while(!found){
                if(sortedData.Count>0){        
                    //Console.WriteLine($"{low}, {high}");
                    String cElement = sortedData[(int)Math.Floor(low+(high-low)/2.0)];

                    if(high==low){ // if mid is same as low insert element at mid.
                        if(compare(element, cElement)){
                            //Console.WriteLine($"{element} inserted into {low+1}");
                            sortedData.Insert(low, element);
                            inserted = low;
                        } 
                        else {
                            sortedData.Insert(low+1, element);
                            inserted = low+1;
                            //Console.WriteLine($"{element} inserted into {low}");
                        }
                        found = true;
                    } 
                    else if(compare(element,cElement)){ // lower
                        //Console.Write("lower");
                        high = (int)Math.Floor(low+(high-low)/2.0);
                    }
                    else{ // higher
                        //Console.Write("higher");
                        low = (int)Math.Ceiling(low+(high-low)/2.0);
                    }
                }
                else {
                    sortedData.Add(element);
                    found = true;
                }
            }
            return inserted;
        }


        private static void result2(string[] data){
            List<String> unsortedData = new List<string>(), sortedData = new List<string>();
            foreach (var row in data){
                if(row.Length > 0){
                   unsortedData.Add(row);
                }
            }

            for(int i = 0; i< unsortedData.Count; i++){
                string element = unsortedData[i];
                //Console.WriteLine(i);
                insertVal(element, sortedData);
                //Console.WriteLine($"{low}, {high}");
                //foreach(var row in sortedData) Console.WriteLine(row);
                //Console.ReadLine();
            }

            int val = insertVal("[[2]]",sortedData)+1;
            val *= insertVal("[[6]]",sortedData)+1;


            foreach(var row in sortedData) Console.WriteLine(row);

            Console.WriteLine(val);
        }


        public static void run(){
            Console.WriteLine("Day 13");
            String[] data = Helpers.getData("./Data/d13.txt");
            //result(data);
            result2(data);
            //Console.WriteLine(compare("[]", "[9]"));
        }
    }
    
}