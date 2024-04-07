using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace C__NET5
{
    internal class TaskRunMicrosoft
    {
        //public static void Main()
        //{
        //    ShowThreadInfo("Application");

        //    var t = Task.Run(() => ShowThreadInfo("Task"));
        //    t.Wait();
        //}

        static void ShowThreadInfo(String s)
        {
            Console.WriteLine("{0} thread ID: {1}", s, Thread.CurrentThread.ManagedThreadId);
        }

        public static void Main2()
        {
            Console.WriteLine("Application thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
            var t = Task.Run(() => {
                Console.WriteLine("Task thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
            });
            t.Wait();
        }

        public static void Main3()
        {
            var list = new ConcurrentBag<string>();
            string[] dirNames = { ".", ".." };
            List<Task> tasks = new List<Task>();

            foreach (var dirName in dirNames)
            {
                Task t = Task.Run(() => {
                    foreach (var path in Directory.GetFiles(dirName))
                        list.Add(path);
                });
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (Task t in tasks)
                Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

            Console.WriteLine("Number of files read: {0}", list.Count);
        }

        class A : CancellationTokenSource
        {
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }
        }

        public static async Task Main4()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var files = new List<Tuple<string, string, long, DateTime>>();

            var t = Task.Run(() => {
                string dir = "C:\\Windows\\System32\\";
                object obj = new Object();

                if (Directory.Exists(dir))
                {
                    Parallel.ForEach(Directory.GetFiles(dir),
                    f => {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();

                        var fi = new FileInfo(f);
                        lock (obj)
                        {
                            files.Add(Tuple.Create(fi.Name, fi.DirectoryName, fi.Length, fi.LastWriteTimeUtc));
                        }
                    });
                }
            }, token);
            await Task.Yield();
            tokenSource.Cancel();
            try
            {
                await t;
                Console.WriteLine("Retrieved information for {0} files.", files.Count);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");
                foreach (var ie in e.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);

                Console.WriteLine("\nTask status: {0}", t.Status);
            }
            finally
            {
                tokenSource.Dispose();
            }
        }

        public static void Main5()
        {
            string pattern = @"\p{P}*\s+";
            string[] titles = { "Sister Carrie", "The Financier" };
            Task<int>[] tasks = new Task<int>[titles.Length];

            for (int ctr = 0; ctr < titles.Length; ctr++)
            {
                string s = titles[ctr];
                tasks[ctr] = Task.Run(() => {
                    // Number of words.
                    int nWords = 0;
                    // Create filename from title.
                    string fn = s + ".txt";
                    if (File.Exists(fn))
                    {
                        StreamReader sr = new StreamReader(fn);
                        string input = sr.ReadToEndAsync().Result;
                        nWords = Regex.Matches(input, pattern).Count;
                    }
                    return nWords;
                });
            }
            Task.WaitAll(tasks);

            Console.WriteLine("Word Counts:\n");
            for (int ctr = 0; ctr < titles.Length; ctr++)
                Console.WriteLine("{0}: {1,10:N0} words", titles[ctr], tasks[ctr].Result);
        }

        public static void Main6()
        {
            var tasks = new List<Task<int>>();
            var source = new CancellationTokenSource();
            var token = source.Token;
            int completedIterations = 0;

            for (int n = 0; n <= 19; n++)
                tasks.Add(Task.Run(() => {
                    
                    int iterations = 0;
                    
                    for (int ctr = 1; ctr <= 2000000; ctr++)
                    {
                        token.ThrowIfCancellationRequested();
                        iterations++;
                    }
                    Interlocked.Increment(ref completedIterations);

                    if (completedIterations >= 10)
                        source.Cancel();

                    return iterations;
                }, token));

            Console.WriteLine("Waiting for the first 10 tasks to complete...\n");

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException)
            {
                Console.WriteLine("Status of tasks:\n");
                Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id", "Status", "Iterations");
                foreach (var t in tasks)
                    Console.WriteLine("{0,10} {1,20} {2,14}",t.Id, t.Status, t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
            }
        }
    }
}
