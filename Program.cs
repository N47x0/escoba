using System;

namespace escoba
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = System.Net.Dns.GetHostName();
            var ip = System.Net.Dns.Resolve(host);
            foreach (var h in ip.AddressList) {
                Console.WriteLine("The addressses: {0} - {1}",h ,System.Net.Dns.GetHostEntry(h).HostName);
            }
            Console.WriteLine("Hello World! {0} and {1}",host, ip.ToString());
        }
    }
}
