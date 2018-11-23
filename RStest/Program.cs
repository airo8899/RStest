using System;
using System.Collections.Generic;

namespace RStest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> myDict = new Dictionary<string, Dictionary<string, int>>();
            myDict.Add("Lisa Rose", new Dictionary<string, int>() { { "Lady in the Water", 5 } });
            myDict["Lisa Rose"].Add("Snakes on a Plane", 7);
            myDict["Lisa Rose"].Add("Just My Luck", 6);
            myDict["Lisa Rose"].Add("Superman Returns", 7);
            myDict["Lisa Rose"].Add("You, Me and Dupree", 5);
            myDict["Lisa Rose"].Add("The Night Listener", 6);

            myDict.Add("Gene Seymour", new Dictionary<string, int>() { { "Lady in the Water", 6 } });
            myDict["Gene Seymour"].Add("Snakes on a Plane", 7);
            myDict["Gene Seymour"].Add("Just My Luck", 3);
            myDict["Gene Seymour"].Add("Superman Returns", 10);
            myDict["Gene Seymour"].Add("You, Me and Dupree", 7);
            myDict["Gene Seymour"].Add("The Night Listener", 6);

            myDict.Add("Michael Phillips", new Dictionary<string, int>() { { "Lady in the Water", 5 } });
            myDict["Michael Phillips"].Add("Snakes on a Plane", 6);
            myDict["Michael Phillips"].Add("Superman Returns", 7);
            myDict["Michael Phillips"].Add("The Night Listener", 8);

            myDict.Add("Claudia Puig", new Dictionary<string, int>() { { "Just My Luck", 6 } });
            myDict["Claudia Puig"].Add("Snakes on a Plane", 7);
            myDict["Claudia Puig"].Add("Superman Returns", 8);
            myDict["Claudia Puig"].Add("The Night Listener", 9);
            myDict["Claudia Puig"].Add("You, Me and Dupree", 5);

            myDict.Add("Mick LaSalle", new Dictionary<string, int>() { { "Lady in the Water", 6 } });
            myDict["Mick LaSalle"].Add("Snakes on a Plane", 8);
            myDict["Mick LaSalle"].Add("Superman Returns", 6);
            myDict["Mick LaSalle"].Add("The Night Listener", 6);
            myDict["Mick LaSalle"].Add("You, Me and Dupree", 4);
            myDict["Mick LaSalle"].Add("Just My Luck", 4);

            myDict.Add("Jack Matthews", new Dictionary<string, int>() { { "Lady in the Water", 6 } });
            myDict["Jack Matthews"].Add("Snakes on a Plane", 8);
            myDict["Jack Matthews"].Add("Superman Returns", 10);
            myDict["Jack Matthews"].Add("The Night Listener", 6);
            myDict["Jack Matthews"].Add("You, Me and Dupree", 7);

            myDict.Add("Toby", new Dictionary<string, int>() { { "Snakes on a Plane", 7 } });
            myDict["Toby"].Add("Superman Returns", 8);
            myDict["Toby"].Add("You, Me and Dupree", 2);


            //foreach (var x in myDict.Keys)
            //{
            //    Console.WriteLine("\n" + x + ":");
            //    foreach (var i in myDict[x].Keys)
            //    {
            //        Console.WriteLine(i + ' ' + myDict[x][i]);
            //    }
            //}

            Console.WriteLine(CoefR(myDict, "Lisa Rose", "Gene Seymour"));

            
            Console.ReadKey();
        }


        static double CoefR(Dictionary<string, Dictionary<string, int>> dict, string person1, string person2)
        {
            List<int> rating1 = new List<int>();
            List<int> rating2 = new List<int>();

            double SumRating1 = 0;
            double SumRating2 = 0;
            double SumMultiRating = 0;
            double SumSqRating1 = 0;
            double SumSqRating2= 0;
            double n = 0;
            

            foreach(var f1 in dict[person1].Keys)
            {
                foreach(var f2 in dict[person2].Keys)
                {
                    if (f1 == f2)
                    {
                        SumRating1 = SumRating1 + dict[person1][f1];
                        SumRating2 = SumRating2 + dict[person2][f2];
                        SumMultiRating = SumMultiRating + dict[person1][f1] * dict[person2][f2];
                        SumSqRating1 = SumSqRating1 + Math.Pow(dict[person1][f1], 2);
                        SumSqRating2 = SumSqRating2 + Math.Pow(dict[person2][f2], 2);
                        n = n + 1;
                    }
                }
            }


            double Cxy = SumMultiRating - (SumRating1 * SumRating2) / n;
            double Cx = SumSqRating1 - Math.Pow(SumRating1, 2) / n;
            double Cy = SumSqRating2 - Math.Pow(SumRating2, 2) / n;

            double R = Cxy / (Math.Sqrt(Cx * Cy));
            //Console.WriteLine(S.ToString());
            return R;
        }
    }
}
