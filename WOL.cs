using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        public static class WakeOnLan
        {

            public static void Main(string[] args)
            {

                //the attempt to take task sched args to make it user friendly
                 try
                {
                    string macAddress = args.ElementAt(0);
                    string ipAddress = args.ElementAt(1);
                    string subnetMask = args.ElementAt(2);
                    string localBroadcast = args.ElementAt(3);

                    UdpClient client = new UdpClient();

                    Byte[] datagram = new byte[102];

                    for (int i = 0; i <= 5; i++)
                    {
                        datagram[i] = 0xff;
                    }

                    string[] macDigits = null;
                    if (macAddress.Contains("-"))
                    {
                        macDigits = macAddress.Split('-');
                    }
                    else
                    {
                        macDigits = macAddress.Split(':');
                    }

                    if (macDigits.Length != 6)
                    {
                        throw new ArgumentException("Incorrect MAC address supplied!");
                    }

                    int start = 6;
                    for (int i = 0; i < 16; i++)
                    {
                        for (int x = 0; x < 6; x++)
                        {
                            datagram[start + i * 6 + x] = (byte)Convert.ToInt32(macDigits[x], 16);
                        }
                    }

                    IPAddress address = IPAddress.Parse(ipAddress);
                    IPAddress mask = IPAddress.Parse(subnetMask);

                    //local subnet broadcast address
                    string broadcastAddress = IPAddress.Parse(localBroadcast).ToString();
                    client.Send(datagram, datagram.Length, broadcastAddress.ToString(), 3);

                }
                catch
                {
                    Console.WriteLine("enter args for mac,ip,lan-subnet mask,lan-bcast (mac separated by : or - and dotted quad for IP adds)in that order separated by a schpace character...");
                    Console.ReadLine();
                }
            }
        }
    }
}
    


