using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;

            double[] total_score = {0, 0, 0};

            for(int j = 2; j <= 4; j++) {
                for (int i = 1; i <= stdCount; i++) {
                    total_score[j - 2] += double.Parse(data[i, j]);
                }
            }
            Console.WriteLine("Average Scores:"); 

            for(int i = 2; i <= 4; i++) {
                Console.WriteLine($"{data[0, i]} : {total_score[i-2] / stdCount}");
            }

            double[] max = {0, 0, 0};
            double[] min = {100, 100, 100};

            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < stdCount; j++) {
                    if(max[i] <= double.Parse(data[j + 1, i + 2]))
                    max[i] = double.Parse(data[j + 1, i + 2]); 
                    if(min[i] >= double.Parse(data[j + 1, i + 2]))
                    min[i] = double.Parse(data[j + 1, i + 2]);
                }
            }
            Console.WriteLine("Max and Min scores:");

            for(int i = 0; i < 3; i++) {
                Console.WriteLine($"{data[0, i + 2]} : ({max[i]}, {min[i]})");
            }

            var stdscores = new (string Name, double total)[stdCount];
            for(int i = 1; i <= stdCount; i++) {
                double total = double.Parse(data[i, 2]) + double.Parse(data[i, 3]) + double.Parse(data[i, 4]);
                stdscores[i - 1] = (data[i,1], total);
            }
            Array.Sort(stdscores, (a, b) => b.total.CompareTo(a.total));


            for(int i = 0; i < stdCount; i++) {
                int rank = i + 1;
                if(rank == 1) {
                    Console.WriteLine($"{stdscores[i].Name} : {rank}st");
                }
                else if(rank == 2) {
                    Console.WriteLine($"{stdscores[i].Name} : {rank}nd");
                }
                else if(rank == 3) {
                    Console.WriteLine($"{stdscores[i].Name} : {rank}rd");
                }
                else {
                    Console.WriteLine($"{stdscores[i].Name} : {rank}th");
                }
            }
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3rd

*/
