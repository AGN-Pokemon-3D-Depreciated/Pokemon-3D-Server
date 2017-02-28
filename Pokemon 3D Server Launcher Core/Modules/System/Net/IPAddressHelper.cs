using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Modules.System.Net
{
    public static class IPAddressHelper
    {
        public async static Task<string> GetPublicIPAsync()
        {
            using (WebClient client = new WebClient() { CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache), Encoding = Encoding.UTF8 })
            {
                Task<string> downloadTask = client.DownloadStringTaskAsync("http://api.ipify.org");

                if (await Task.WhenAny(downloadTask, Task.Delay(5000)) == downloadTask)
                    return await downloadTask;
                else
                    return null;
            }
        }

        public async static Task<string> GetPrivateIPAsync()
        {
            Task<IPHostEntry> task = Dns.GetHostEntryAsync(Dns.GetHostName());

            if (await Task.WhenAny(task, Task.Delay(5000)) == task)
            {
                IPHostEntry host = await task;
                return host.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();
            }
            else
                return null;
        }
    }
}