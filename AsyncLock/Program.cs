namespace AsyncLock
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        private static readonly SemaphoreSlim semaphore =
            AsyncLock.CreateSemaphore();

        private static Random rand = new Random(Guid.NewGuid().GetHashCode());
        private static int counter = 0;


        public static void Main()
        {
            var tasks = new List<Task>();

            for(int i = 0; i < 10; ++i)
            {
                tasks.Add(Test());
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static async Task Test()
        {
            using (await AsyncLock.Enter(semaphore))
            {
                int ms = rand.Next(0, 100);
                await Task.Delay(ms);
                ++counter;
                Console.WriteLine($"{counter}: Hello, World!");
            }
        }
    }
}
