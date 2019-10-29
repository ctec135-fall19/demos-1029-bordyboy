using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with LINQ to objects ****\n");

            //define an array of strings
            string[] currentVideoGames = { "Morrowind", "Uncharted 2", "Fallout 3", "Dexter", "System Shock2" };

            //desired query: Games that have a space in the title

            #region First let's try the old fashioned way

            string[] result = QueryOverStringsLongHand(currentVideoGames);

            Console.WriteLine("Returned results from longheand version");
            foreach(string s in result)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion

            #region Let's try the same thing using LINQ

            QueryOverStrings(currentVideoGames);

            #endregion


        }

        #region olde fashioned way

        static string[] QueryOverStringsLongHand(string[] s)
        {
            string[] resultsWithSpaces = new string[s.Length];

            //find results
            for(int i=0; i<s.Length; i++)
            {
                if (s[i].Contains(" "))
                    resultsWithSpaces[i] = s[i];
            }

            //sort results
            Array.Sort(resultsWithSpaces);

            //print results
            Console.WriteLine("Immediate results from longhand version");
            foreach(string s1 in resultsWithSpaces){
                if (s1 != null)
                    Console.WriteLine("Item: {0}", s1);
            }
            Console.WriteLine();

            //generate a return array
            //figure out size
            int count = 0;
            foreach(string s2 in resultsWithSpaces)
            {
                if (s2 != null) count++;

            }

            //create output array
            string[] outputArray = new string[count];

            //populate output array
            count = 0;
            foreach (string s3 in resultsWithSpaces)
            {
                if(s3 != null)
                {
                    outputArray[count] = s3;
                    count++;
                }
            }

            return outputArray;
        }
        #endregion

        #region Let's try the same thing using LINQ

        static void QueryOverStrings(string[] inputArray)
        {
            //build query
            //IEnumerable<string> subset = form ...
            var subset = from game in inputArray
                         where game.Contains(" ")
                         orderby game
                         select game;

            //print results
            ReflectOverQueryResults(subset, "Query Expression");

            //print results
            Console.WriteLine("  Immediate results using LINQ query");
            foreach(var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            //demonstrate reuse of query
            inputArray[0] = "some string";
            Console.WriteLine("  Immediate results using LINQ query");
            foreach (var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            // demonstrate returning results - immediate execution
            List<string> outputList = (from game in inputArray
                                       where game.Contains(" ")
                                       orderby game
                                       select game).ToList<string>();
        }

        static void ReflectOverQueryResults(object resultSet, string queryType)
        {
            Console.WriteLine("**** Query type: {0} ****", queryType);
            Console.WriteLine("resultsSet is of type {0}", resultSet.GetType().Name);
            Console.WriteLine("resultsSet location: {0}", resultSet.GetType().Assembly.GetName().Name);
        }

        #endregion
    }
}
