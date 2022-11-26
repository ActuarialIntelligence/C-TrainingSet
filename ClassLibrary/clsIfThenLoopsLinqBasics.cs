using System.Collections.Generic;
using AI.Infrastructure.Data.Dtos; // We have made a reference to a previous project for this tutorial
using System.Linq; // We are now using some advanced expressing that require the use of LINQ

namespace ClassLibrary
{
    public class clsIfThenLoopsLinqBasics
    {
        //We are going to make use of and implement loops with arrays and linked lists

        public static bool BasicComparisonIfStatement(int a, int b)
        {
            if(a>b)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        // reverse print string using loops and arrays

        public string ReverseString(string str)
        {
            var charArray = str.ToCharArray();
            var length = charArray.Length;  
            var reversedString = "";

            for(int i = length-1; i >= 0; i--)
            {
                reversedString += charArray[i];
            }

            return reversedString;
        }

        public void ForEachAndLoopingThroughLinkedLists(IList<string> strng) // The I in front of the list here is Indicative of an Interface
        {
            foreach(string str in strng)
            {
                //do something
            }
        }

        public void ForEachAndLoopingThroughLinkedLists(IList<DToTypeExample1> list) // This is called PolyMorphism, same method working in different type specific ways.

        {
            {
                foreach (var dto in list)
                {
                    //dto.dateTime ==
                    //do something
                   
                }
            }
        }

        // one can remove and add items to the list

        public void AddandRemoveLinkedListItems(IList<DToTypeExample1> list) 
        {
            //Add
            list.RemoveAt(3); // 3 here is the index
            list.Add(new DToTypeExample1());// Example of adding to list
            //Remove at a location
        }

        // There is another type of list called teh Dictionary 
        public void LoopingThroughDictionaryItems(IDictionary<int,DToTypeExample1> dict)
        {
            foreach(KeyValuePair<int, DToTypeExample1> kvp in dict)
            {
                //Do something
            }
        }

        // Now for some fun, lets play with Linq a Little

        public void WhereClauseDictionaryItems(IDictionary<int, DToTypeExample1> dict)
        {
            
                //Where Clause
                IEnumerable<KeyValuePair<int, DToTypeExample1>> find = dict.Where(a=> a.Key == 3);   // This outputs a KeyValuePair List
                var fid = dict.Where(a => a.Key == 3); // same as above in shorthand
        }


        public void DistinctClauseDictionaryItems(IDictionary<int, DToTypeExample1> dict)
        {

         
            IEnumerable<KeyValuePair<int, DToTypeExample1>> find = dict.Distinct();   // This outputs a KeyValuePair List

        }

        // Lets now say we want the distinct keys only in aa list
        public void ValueListDistinctClauseDictionaryItems(IDictionary<int, DToTypeExample1> dict)
        {

          
            var find = dict.Distinct().Where(a=>a.Key <12).ToList();   // This outputs a  List

        }
    }
}
