using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C__NET5.async_await
{
    internal class ConcurrencyInCsharpCookBook
    {
        public class Node
        {
            public Node Left { get; set; }

            public Node Right { get; set; }
        }

        async Task ResumeOnContextAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            // This method resumes within the same context.
        }

        async Task ResumeWithoutContextAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            // This method discards its context when it resumes.
        }

        // Note: this is not the most efficient implementation.
        // This is just an example of using a lock to protect shared state.
        public static int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int result = 0;
            Parallel.ForEach(source: values,
            localInit: () => 0,
            body: (item, state, localValue) => localValue + item,
            localFinally: localValue =>
            {
                //lock (mutex)
                    result += localValue;
            });
            return result;
        }

        //static int ParallelSum(IEnumerable<int> values)
        //{
        //    return values.AsParallel().Sum();
        //}

        //static int ParallelSum(IEnumerable<int> values)
        //{
        //    return values.AsParallel().Aggregate(
        //    seed: 0,
        //    func: (sum, item) => sum + item
        //    );
        //}

        

        void Traverse(Node current)
        {
            //DoExpensiveActionOnNode(current);
            if (current.Left != null)
            {
                Task.Factory.StartNew(() => Traverse(current.Left), CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
            }
            if (current.Right != null)
            {
                Task.Factory.StartNew(() => Traverse(current.Right), CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
            }
        }

        public void ProcessTree(Node root)
        {
            var task = Task.Factory.StartNew(() => Traverse(root), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            task.Wait();
        }

        Task task = Task.Factory.StartNew(() => Thread.Sleep(TimeSpan.FromSeconds(2)), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        //Task continuation = task.ContinueWith(t => Trace.WriteLine("Task is done"), CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);

        //public static Task<string> DownloadStringTaskAsync(this WebClient client, Uri address)
        public static Task<string> DownloadStringTaskAsync(WebClient client, Uri address)
        {
            var tcs = new TaskCompletionSource<string>();
            // The event handler will complete the task and unregister itself.
            DownloadStringCompletedEventHandler handler = null;
            handler = (_, e) =>
            {
                client.DownloadStringCompleted -= handler;
                if (e.Cancelled)
                    tcs.TrySetCanceled();
                else if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else
                    tcs.TrySetResult(e.Result);
            };
            // Register for the event and *then* start the operation.
            client.DownloadStringCompleted += handler;
            client.DownloadStringAsync(address);
            return tcs.Task;
        }
        
    }
}
