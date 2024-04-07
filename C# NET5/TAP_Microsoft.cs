using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C__NET5
{
    //internal class TAP_Microsoft
    //{
    //    public Task<Bitmap> RenderAsync(ImageData data, CancellationToken cancellationToken)
    //    {
    //        return Task.Run(() =>
    //        {
    //            var bmp = new Bitmap(data.Width, data.Height);
    //            for (int y = 0; y < data.Height; y++)
    //            {
    //                cancellationToken.ThrowIfCancellationRequested();
    //                for (int x = 0; x < data.Width; x++)
    //                {
    //                    // render pixel [x,y] into bmp
    //                }
    //            }
    //            return bmp;
    //        }, cancellationToken);
    //    }

    //    public static Task<DateTimeOffset> Delay(int millisecondsTimeout)
    //    {
    //        var tcs = new TaskCompletionSource<DateTimeOffset>();
    //        new Timer(self =>
    //            {
    //                ((IDisposable)self).Dispose();
    //                tcs.TrySetResult(DateTimeOffset.UtcNow);
    //            }).Change(millisecondsTimeout, -1);
    //        return tcs.Task;
    //    }

    //    public static async Task Poll(Uri url, CancellationToken cancellationToken, IProgress<bool> progress)
    //    {
    //        while (true)
    //        {
    //            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
    //            bool success = false;
    //            try
    //            {
    //                await DownloadStringAsync(url);
    //                success = true;
    //            }
    //            catch { /* ignore errors */ }
    //            progress.Report(success);
    //        }
    //    }

    //    public static Task Delay(int millisecondsTimeout)
    //    {
    //        var tcs = new TaskCompletionSource<bool>();
    //        new Timer(self =>
    //        {
    //            ((IDisposable)self).Dispose();
    //            tcs.TrySetResult(true);
    //        }).Change(millisecondsTimeout, -1);
    //        return tcs.Task;
    //    }

    //    public async Task<Bitmap> DownloadDataAndRenderImageAsync(CancellationToken cancellationToken)
    //    {
    //        var imageData = await DownloadImageDataAsync(cancellationToken);
    //        return await RenderAsync(imageData, cancellationToken);
    //    }

    //    void Wrapper1()
    //    {
    //        Task.Run(async delegate
    //        {
    //            for (int i = 0; i < 1000000; i++)
    //            {
    //                await Task.Yield(); // fork the continuation into a separate work item
    //            }
    //        });

    //        var cts = new CancellationTokenSource();
    //        string result = await DownloadStringAsync(url, cts.Token);
    //        // at some point later, potentially on another thread
    //        cts.Cancel();

    //    }

    //    async Task Wrapper2()
    //    {
    //        var cts = new CancellationTokenSource();
    //        string result = await DownloadStringAsync(url, cts.Token);
    //        // at some point later, potentially on another thread
    //        cts.Cancel();
    //    }

    //    async Task Wrapper3()
    //    {
    //        var cts = new CancellationTokenSource();
    //        IList<string> results = await Task.WhenAll(from url in urls select DownloadStringAsync(url, cts.Token));
    //        cts.Cancel();
    //    }

    //    async Task Wrapper4()
    //    {
    //        var cts = new CancellationTokenSource();
    //        byte[] data = await DownloadDataAsync(url, cts.Token);
    //        await SaveToDiskAsync(outputPath, data, CancellationToken.None);
    //        cts.Cancel();
    //    }

    //    public Task<int> GetValueAsync(string key)
    //    {
    //        int cachedValue;
    //        return TryGetCachedValue(out cachedValue) ? Task.FromResult(cachedValue) : GetValueAsyncInternal();
    //    }

    //    async Task Wrapper5()
    //    {
    //        IEnumerable<Task> asyncOps = from addr in addrs select SendMailAsync(addr);
    //        await Task.WhenAll(asyncOps);
    //    }

    //    async Task Wrapper6()
    //    {
    //        var recommendations = new List<Task<bool>>()
    //        {
    //            GetBuyRecommendation1Async(symbol),
    //            GetBuyRecommendation2Async(symbol),
    //            GetBuyRecommendation3Async(symbol)
    //        };
    //        Task<bool> recommendation = await Task.WhenAny(recommendations);
    //        if (await recommendation) BuyStock(symbol);
    //    }

    //    async Task Wrapper7()
    //    {
    //        Task<bool>[] recommendations = …;
    //        while (recommendations.Count > 0)
    //        {
    //            Task<bool> recommendation = await Task.WhenAny(recommendations);
    //            try
    //            {
    //                if (await recommendation) BuyStock(symbol);
    //                break;
    //            }
    //            catch (WebException exc)
    //            {
    //                recommendations.Remove(recommendation);
    //            }
    //        }
    //    }

    //    async Task Wrapper7()
    //    {
    //        foreach (Task recommendation in recommendations)
    //        {
    //            var ignored = recommendation.ContinueWith(t => { if (t.IsFaulted) Log(t.Exception); });
    //        }

    //        foreach (Task recommendation in recommendations)
    //        {
    //            var ignored = recommendation.ContinueWith(t => Log(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
    //        }
    //    }

    //    private static async void LogCompletionIfFailed(IEnumerable<Task> tasks)
    //    {
    //        foreach (var task in tasks)
    //        {
    //            try { await task; }
    //            catch (Exception exc) { Log(exc); }
    //        }
    //    }

    //    async Task Wrapper8()
    //    {
    //        List<Task<Bitmap>> imageTasks = (from imageUrl in urls select GetBitmapAsync(imageUrl)).ToList();
    //        while (imageTasks.Count > 0)
    //        {
    //            try
    //            {
    //                Task<Bitmap> imageTask = await Task.WhenAny(imageTasks);
    //                imageTasks.Remove(imageTask);

    //                Bitmap image = await imageTask;
    //                panel.AddImage(image);
    //            }
    //            catch { }
    //        }
    //    }

    //    public static async Task<T> NeedOnlyOne(params Func<CancellationToken, Task<T>>[] functions)
    //    {
    //        var cts = new CancellationTokenSource();
    //        var tasks = (from function in functions select function(cts.Token)).ToArray();
    //        var completed = await Task.WhenAny(tasks).ConfigureAwait(false);
    //        cts.Cancel();
    //        foreach (var task in tasks)
    //        {
    //            var ignored = task.ContinueWith(t => Log(t), TaskContinuationOptions.OnlyOnFaulted);
    //        }
    //        return completed;
    //    }

    //    static IEnumerable<Task<T>> Interleaved<T>(IEnumerable<Task<T>> tasks)
    //    {
    //        var inputTasks = tasks.ToList();
    //        var sources = (from _ in Enumerable.Range(0, inputTasks.Count) select new TaskCompletionSource<T>()).ToList();
    //        int nextTaskIndex = -1;
    //        foreach (var inputTask in inputTasks)
    //        {
    //            inputTask.ContinueWith(completed =>
    //            {
    //                var source = sources[Interlocked.Increment(ref nextTaskIndex)];
    //                if (completed.IsFaulted)
    //                    source.TrySetException(completed.Exception.InnerExceptions);
    //                else if (completed.IsCanceled)
    //                    source.TrySetCanceled();
    //                else
    //                    source.TrySetResult(completed.Result);
    //            }, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    //        }
    //        return from source in sources select source.Task;
    //    }

    //    public static Task<T[]> WhenAllOrFirstException<T>(IEnumerable<Task<T>> tasks)
    //    {
    //        var inputs = tasks.ToList();
    //        var ce = new CountdownEvent(inputs.Count);
    //        var tcs = new TaskCompletionSource<T[]>();
    //        Action<Task> onCompleted = (Task completed) =>
    //        {
    //            if (completed.IsFaulted) tcs.TrySetException(completed.Exception.InnerExceptions);
    //            if (ce.Signal() && !tcs.Task.IsCompleted) tcs.TrySetResult(inputs.Select(t => t.Result).ToArray());
    //        };
    //        foreach (var t in inputs) t.ContinueWith(onCompleted);
    //        return tcs.Task;
    //    }

    //    public static Task<int> ReadAsync(this Stream stream, byte[] buffer, int offset, int count)
    //    {
    //        if (stream == null) throw new ArgumentNullException("stream");
    //        return Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, offset, count, null);
    //    }

    //    public static Task<int> ReadAsync(this Stream stream, byte[] buffer, int offset, int count)
    //    {
    //        if (stream == null) throw new ArgumentNullException("stream");
    //        var tcs = new TaskCompletionSource<int>();
    //        stream.BeginRead(buffer, offset, count, iar =>
    //        {
    //            try { tcs.TrySetResult(stream.EndRead(iar)); }
    //            catch (OperationCanceledException) { tcs.TrySetCanceled(); }
    //            catch (Exception exc) { tcs.TrySetException(exc); }
    //        }, null);
    //        return tcs.Task;
    //    }

    //    public static IAsyncResult AsApm<T>(this Task<T> task, AsyncCallback callback, object state)
    //    {
    //        if (task == null) throw new ArgumentNullException("task");
    //        var tcs = new TaskCompletionSource<T>(state);
    //        task.ContinueWith(t =>
    //        {
    //            if (t.IsFaulted) tcs.TrySetException(t.Exception.InnerExceptions);
    //            else if (t.IsCanceled) tcs.TrySetCanceled();
    //            else tcs.TrySetResult(t.Result);

    //            if (callback != null) callback(tcs.Task);
    //        }, TaskScheduler.Default);
    //        return tcs.Task;
    //    }

    //    public static Task<string> DownloadStringAsync(Uri url)
    //    {
    //        var tcs = new TaskCompletionSource<string>();
    //        var wc = new WebClient();
    //        wc.DownloadStringCompleted += (s, e) =>
    //        {
    //            if (e.Error != null) tcs.TrySetException(e.Error);
    //            else if (e.Cancelled) tcs.TrySetCanceled();
    //            else tcs.TrySetResult(e.Result);
    //        };
    //        wc.DownloadStringAsync(url);
    //        return tcs.Task;
    //    }

    //    public static Task WaitOneAsync(this WaitHandle waitHandle)
    //    {
    //        if (waitHandle == null) throw new ArgumentNullException("waitHandle");

    //        var tcs = new TaskCompletionSource<bool>();
    //        var rwh = ThreadPool.RegisterWaitForSingleObject(waitHandle,
    //            delegate { tcs.TrySetResult(true); }, null, -1, true);
    //        var t = tcs.Task;
    //        t.ContinueWith(_ => rwh.Unregister(null));
    //        return t;
    //    }

    //    class WrapperClass1
    //    {
    //        static Semaphore m_throttle = new Semaphore(N, N);

    //        static async Task DoOperation()
    //        {
    //            await m_throttle.WaitOneAsync();
    //            // do work
    //            m_throttle.ReleaseOne();
    //        }
    //    }

    //    class WrapperClass2
    //    {
    //        static SemaphoreSlim m_throttle = new SemaphoreSlim(N, N);

    //        static async Task DoOperation()
    //        {
    //            await m_throttle.WaitAsync();
    //            // do work
    //            m_throttle.Release();
    //        }
    //    }

    //    public static byte[] DownloadData(string url)
    //    {
    //        using (var request = WebRequest.Create(url))
    //        using (var response = request.GetResponse())
    //        using (var responseStream = response.GetResponseStream())
    //        using (var result = new MemoryStream())
    //        {
    //            responseStream.CopyTo(result);
    //            return result.ToArray();
    //        }
    //    }

    //    public static async Task<byte[]> DownloadDataAsync(string url)
    //    {
    //        using (var request = WebRequest.Create(url))
    //        {
    //            return await Task.Run(() =>
    //            {
    //                using (var response = request.GetResponse())
    //                using (var responseStream = response.GetResponseStream())
    //                using (var result = new MemoryStream())
    //                {
    //                    responseStream.CopyTo(result);
    //                    return result.ToArray();
    //                }
    //            });
    //        }
    //    }

    //    public static async Task<byte[]> DownloadDataAsync(string url)
    //    {
    //        using (var request = WebRequest.Create(url))
    //        using (var response = await request.GetResponseAsync())
    //        using (var responseStream = response.GetResponseStream())
    //        using (var result = new MemoryStream())
    //        {
    //            await responseStream.CopyToAsync(result);
    //            return result.ToArray();
    //        }
    //    }

    //    public static void CopyTo(this Stream source, Stream destination)
    //    {
    //        var buffer = new byte[0x1000];
    //        int bytesRead;
    //        while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
    //        {
    //            destination.Write(buffer, 0, bytesRead);
    //        }
    //    }

    //    public static async Task CopyToAsync(this Stream source, Stream destination)
    //    {
    //        var buffer = new byte[0x1000];
    //        int bytesRead;
    //        while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length)) > 0)
    //        {
    //            await destination.WriteAsync(buffer, 0, bytesRead);
    //        }
    //    }

    //    public static Task<int> ReadAsync(this Stream source, byte[] buffer, int offset, int count)
    //    {
    //        return Task<int>.Factory.FromAsync(source.BeginRead, source.EndRead, buffer, offset, count, null);
    //    }

    //    public static Task WriteAsync(this Stream destination, byte[] buffer, int offset, int count)
    //    {
    //        return Task.Factory.FromAsync(destination.BeginWrite, destination.EndWrite, buffer, offset, count, null);
    //    }

    //    public static async Task CopyToAsync(this Stream source, Stream destination, CancellationToken cancellationToken)
    //    {
    //        var buffer = new byte[0x1000];
    //        int bytesRead;
    //        while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length)) > 0)
    //        {
    //            await destination.WriteAsync(buffer, 0, bytesRead);
    //            cancellationToken.ThrowIfCancellationRequested();
    //        }
    //    }

    //    public static async Task CopyToAsync(this Stream source, Stream destination, CancellationToken cancellationToken, IProgress<long> progress)
    //    {
    //        var buffer = new byte[0x1000];
    //        int bytesRead;
    //        long totalRead = 0;
    //        while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length)) > 0)
    //        {
    //            await destination.WriteAsync(buffer, 0, bytesRead);
    //            cancellationToken.ThrowIfCancellationRequested();
    //            totalRead += bytesRead;
    //            progress.Report(totalRead);
    //        }
    //    }

    //    public static async Task<byte[]> DownloadDataAsync(string url, CancellationToken cancellationToken, IProgress<long> progress)
    //    {
    //        using (var request = WebRequest.Create(url))
    //        using (var response = await request.GetResponseAsync())
    //        using (var responseStream = response.GetResponseStream())
    //        using (var result = new MemoryStream())
    //        {
    //            await responseStream.CopyToAsync(result, cancellationToken, progress);
    //            return result.ToArray();
    //        }
    //    }

    //    public static async Task CopyToAsync(this Stream source, Stream destination)
    //    {
    //        int i = 0;
    //        var buffers = new[] { new byte[0x1000], new byte[0x1000] };
    //        Task writeTask = null;
    //        while (true)
    //        {
    //            var readTask = source.ReadAsync(buffers[i], 0, buffers[i].Length))> 0;
    //            if (writeTask != null) await Task.WhenAll(readTask, writeTask);
    //            int bytesRead = await readTask;
    //            if (bytesRead == 0) break;
    //            writeTask = destination.WriteAsync(buffers[i], 0, bytesRead);
    //            i ^= 1; // swap buffers
    //        }
    //    }

    //    public static async Task CopyToAsync(this Stream source, Stream destination)
    //    {
    //        var buffer = new byte[0x1000];
    //        int bytesRead;
    //        while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
    //        {
    //            await destination.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
    //        }
    //    }
    //}

    public class AsyncCache<TKey, TValue>
    {
        private readonly Func<TKey, Task<TValue>> _valueFactory;
        private readonly ConcurrentDictionary<TKey, Lazy<Task<TValue>>> _map;

        public AsyncCache(Func<TKey, Task<TValue>> valueFactory)
        {
            if (valueFactory == null) throw new ArgumentNullException("loader");
            _valueFactory = valueFactory;
            _map = new ConcurrentDictionary<TKey, Lazy<Task<TValue>>>();
        }

        public Task<TValue> this[TKey key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException("key");
                return _map.GetOrAdd(key, toAdd =>
                    new Lazy<Task<TValue>>(() => _valueFactory(toAdd))).Value;
            }
        }
    }

    public class AsyncProducerConsumerCollection<T>
    {
        private readonly Queue<T> m_collection = new Queue<T>();
        private readonly Queue<TaskCompletionSource<T>> m_waiting =
            new Queue<TaskCompletionSource<T>>();

        public void Add(T item)
        {
            TaskCompletionSource<T> tcs = null;
            lock (m_collection)
            {
                if (m_waiting.Count > 0) tcs = m_waiting.Dequeue();
                else m_collection.Enqueue(item);
            }
            if (tcs != null) tcs.TrySetResult(item);
        }

        public Task<T> Take()
        {
            lock (m_collection)
            {
                if (m_collection.Count > 0)
                {
                    return Task.FromResult(m_collection.Dequeue());
                }
                else
                {
                    var tcs = new TaskCompletionSource<T>();
                    m_waiting.Enqueue(tcs);
                    return tcs.Task;
                }
            }
        }
    }
}
