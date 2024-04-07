using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace C__NET5
{
    internal class StephenClearyCom
    {
        [Flags]
        public enum ConfigureAwaitOptions
        {
            None = 0x0,
            ContinueOnCapturedContext = 0x1,
            SuppressThrowing = 0x2,
            ForceYielding = 0x4,
        }

        public async Task Main1() 
        {
            Task task = Task.Run(() => { });

            // These all do the same thing
            await task;
            await task.ConfigureAwait(continueOnCapturedContext: true);
            //await task.ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);

            // These do the same thing
            await task.ConfigureAwait(continueOnCapturedContext: false);
            //await task.ConfigureAwait(ConfigureAwaitOptions.None);
        }
    }
}
