using System;
using System.Collections.Generic;
using System.Linq;

namespace BT1._3
{
    internal class Program
    {
        private static Random rand = new Random();
        static T FindMin<T>(T[] arr) where T : IComparable<T>
        {
            T min = arr[0];
            if (arr[0].GetType() == typeof(string))
            {
                foreach (T item in arr)
                    if (min.ToString().Length > item.ToString().Length)
                        min = item;
            }
            else
            {
                foreach (T item in arr)
                    if (min.CompareTo(item) > 0)
                        min = item;
            }
            return min;
        }
        static T FindMax<T>(T[] arr) where T : IComparable<T>
        {
            T max = arr[0];
            if (arr[0].GetType() == typeof(string))
            {
                foreach (T item in arr)
                    if (max.ToString().Length < item.ToString().Length)
                        max = item;
            }
            else
            {
                foreach (T item in arr)
                    if (max.CompareTo(item) < 0)
                        max = item;
            }
            return max;
        }
        static void PrintArr<T>(T[] arr)
        {
            foreach (T item in arr) Console.Write(item + " ");
            Console.WriteLine();
            Console.WriteLine();
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
        static void Main(string[] args)
        {
            int n = 10;
            int[] intArr = new int[n];
            double[] doubleArr = new double[n];
            string[] stringArr = new string[n];
            for (int i = 0; i < n; i++)
            {
                intArr[i] = rand.Next(-50, 50);
                doubleArr[i] = Math.Round(rand.NextDouble() * 20 - 10, 2);
                stringArr[i] = RandomString(rand.Next(1, 10));
            }

            Console.Write("int array: "); PrintArr(intArr);
            Console.Write("double array: "); PrintArr(doubleArr);
            Console.Write("string array: "); PrintArr(stringArr);

            Console.WriteLine("Min (int): " + FindMin(intArr) + ", Max (int): " + FindMax(intArr));
            Console.WriteLine("Min (float): " + FindMin(doubleArr) + ", Max (float): " + FindMax(doubleArr));
            Console.WriteLine("Min (string): " + FindMin(stringArr) + ", Max (string): " + FindMax(stringArr));
        }
    }
}   