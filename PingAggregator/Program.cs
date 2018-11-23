using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace PingAggregator {
    public static class Program {
        private static List<IPAddress> Addresses { get; set; }

        private static void Main(string[] args) {
            Addresses = new List<IPAddress>();
            Ping ping = new Ping();

            while (true) {
                PingReply pingReply;

                do {
                    pingReply = ping.Send("nl.torguardvpnaccess.com");
                } while (!pingReply.Status.Equals(IPStatus.Success));

                if (Addresses.Contains(pingReply.Address)) {
                    continue;
                }

                Addresses.Add(pingReply.Address);
                WriteAddresses();

                Thread.Sleep(30000);
            }
        }

        private static void WriteAddresses() {
            Console.Clear();

            foreach (string address in Addresses.Select(arg => Version.Parse(arg.ToString())).OrderBy(arg => arg).Select(arg => arg.ToString())) {
                Console.WriteLine(address);
            }
        }
    }
}
