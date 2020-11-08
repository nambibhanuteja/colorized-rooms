using System;
using System.IO;

namespace ColorizedRooms
{
    class Program
    {
        private static int n, m;
        private static string[,] array;
        private static bool[,] visited;
        private static ConsoleColor[,] colored;
        private static ConsoleColor currentColor = ConsoleColor.Red;
        private static Random _random = new Random();
        static void Main(string[] args)
        {
            String input = File.ReadAllText("../../../Input.txt");

            Console.WriteLine("Input:");
            m = input.IndexOf("\n") - 1;
            n = input.Split("\n").Length;

            int k = 0, l = 0;
            array = new string[n, m];
            foreach (var row in input.Split('\n'))
            {
                l = 0;
                foreach (var col in row)
                {
                    if (col == '\r')
                        break;
                    if (col.ToString() != "#" && col.ToString() != " ")
                        continue;
                    if (l >= m)
                        break;
                    array[k, l] = col.ToString();
                    l++;
                }
                k++;
            }


            visited = new bool[n, m];
            colored = new ConsoleColor[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    colored[i, j] = ConsoleColor.Black;
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }

            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if(array[i,j] == " " && visited[i,j] == false)
                    {
                        colored[i, j] = currentColor;
                        DFS(i, j);
                        count++;
                        currentColor = (ConsoleColor)(_random.Next(1, 15));
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Total number of rooms: " + count);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.BackgroundColor = colored[i, j];
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }

        static bool isValid(int x, int y)
        {
            if (x < 1 || x >= n || y < 1 || y >= m) return false;
            if (visited[x,y] == true || array[x,y] == "#") return false;
            
            
            if (y + 1 < m && array[x, y + 1] == "#" && (y > 0 && array[x, y - 1] == "#"))
            {
                visited[x, y] = true;
                return false;
            }

            colored[x, y] = currentColor;
            return true;
        }

        static void DFS(int x, int y)
        {
            visited[x,y] = true;
            
            if (isValid(x - 1, y))
            {
                DFS(x - 1, y);
            }

            if (isValid(x, y + 1))
            {
                DFS(x, y + 1); 
            }

            if (isValid(x + 1, y))
            {
                DFS(x + 1, y); 
            }

            if (isValid(x, y - 1))
            {
                DFS(x, y - 1); 
            }
        }
    }
}
