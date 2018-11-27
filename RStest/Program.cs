using System;
using System.Collections.Generic;
using System.Linq;

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

            myDict.Add("Toby", new Dictionary<string, int>() { { "Snakes on a Plane", 9 } });
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

            foreach(var film in GetRecomendations(myDict, "Toby"))
            {
                Console.WriteLine(film);
            }

            Console.ReadKey();
        }


        static double CoefR(Dictionary<string, Dictionary<string, int>> dict, string person1, string person2)
        {
            List<int> rating1 = new List<int>();
            List<int> rating2 = new List<int>();

            double sumRating1 = 0;
            double sumRating2 = 0;
            double sumMultiRating = 0;
            double sumSqRating1 = 0;
            double sumSqRating2 = 0;
            double n = 0;


            foreach (var f1 in dict[person1].Keys)
            {
                foreach (var f2 in dict[person2].Keys)
                {
                    if (f1 == f2)
                    {
                        sumRating1 = sumRating1 + dict[person1][f1];
                        sumRating2 = sumRating2 + dict[person2][f2];
                        sumMultiRating = sumMultiRating + dict[person1][f1] * dict[person2][f2];
                        sumSqRating1 = sumSqRating1 + Math.Pow(dict[person1][f1], 2);
                        sumSqRating2 = sumSqRating2 + Math.Pow(dict[person2][f2], 2);
                        n = n + 1;
                        break;
                    }
                }
            }


            double Cxy = sumMultiRating - (sumRating1 * sumRating2) / n;
            double Cx = sumSqRating1 - Math.Pow(sumRating1, 2) / n;
            double Cy = sumSqRating2 - Math.Pow(sumRating2, 2) / n;

            double R = Cxy / (Math.Sqrt(Cx * Cy));
            //Console.WriteLine(S.ToString());
            return R;
        }

        static List<string> GetRecomendations(Dictionary<string, Dictionary<string, int>> dict, string person)
        {
            Dictionary<string, double> sumScore = new Dictionary<string, double>();
            Dictionary<string, double> sumR = new Dictionary<string, double>();
            Dictionary<string, double> recomendationsDict = new Dictionary<string, double>();
            List<string> recomendationsList = new List<string>();

            foreach (var other in dict.Keys)
            {
                if (other == person)
                {
                    continue;
                }
                double r = CoefR(dict, other, person);

                if (r > 0)
                {
                    foreach (var film in dict[other].Keys)
                    {
                        if (!dict[person].ContainsKey(film))
                        {
                            if (sumR.ContainsKey(film))
                            {
                                sumR[film] = sumR[film] + r;
                            }
                            else
                            {
                                sumR[film] = r;
                            }

                            if (sumScore.ContainsKey(film))
                            {
                                sumScore[film] = sumScore[film] + r * dict[other][film];
                            }
                            else
                            {
                                sumScore[film] = r * dict[other][film];
                            }
                        }
                    }
                }
            }

            foreach (var film in sumR.Keys)
            {
                recomendationsDict.Add(film, sumScore[film] / sumR[film]);
            }


            foreach(var film in recomendationsDict.OrderByDescending(x => x.Key))
            {
                recomendationsList.Add(film.Key);
            }

            return recomendationsList;
        }
    }
}
