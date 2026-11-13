using System.Diagnostics;

namespace ParallelProg02.Client
{
    internal class Program
    {
        static readonly object lockObj = new object();
        static readonly string port = "5216";
        static async Task Main(string[] args)
        {
            var client = new Client($"http://localhost:{port}", new HttpClient());

            var sw = Stopwatch.StartNew();
            await client.GetMoviesAsync();
            sw.Stop();
            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");

            Task[] tasks = new Task[100];

            ConsoleColor[] colors = new ConsoleColor[]
            {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Yellow,
                ConsoleColor.DarkCyan,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta
            };

            for (int i = 0; i < 100; i++)
            {
                int id = i;
                ConsoleColor color = colors[i % colors.Length];
                tasks[i] = Task.Run(() => AVG(client, color, id));
            }
            Task.WaitAll(tasks);

        }


        static void AVG(Client client, ConsoleColor color, int id)
        {
            var avg = client.GetMoviesAsync().Result.Average(m => m.Rating);

            lock (lockObj)
            {
                Console.BackgroundColor = color;
                Console.WriteLine($"{id} - Average rating: {avg}");
                Console.ResetColor();
                Console.Clear();
            }
        }
    }
}
