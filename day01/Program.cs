﻿namespace day01
{
    public class Program
    {
        public static void Main()
        {
            //read input.txt file
            var lines = File.ReadAllLines("input.txt");
            //format each lines to two lists of integers

            var listA = new List<int>();
            var listB = new List<int>();

            foreach (var line in lines)
            {
                var parts = line.Split("   ");
                listA.Add(int.Parse(parts[0]));
                listB.Add(int.Parse(parts[1]));
            }

            Console.WriteLine(ReturnTotalDistance(listA, listB));
        }

        public static long ReturnTotalDistance(List<int> listA, List<int> listB)
        {
            //sort list A ascending
            listA.Sort();

            //sort list B ascending
            listB.Sort();

            long totalDistance = 0;
            for (var i = 0; i < listA.Count; i++)
            {
                totalDistance += Math.Abs(listA[i] - listB[i]);
            }

            return totalDistance;
        }
    }
}



