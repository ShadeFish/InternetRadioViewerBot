using System;
using NAudio;
using NAudio.Wave;
using System.Threading;

namespace InternetRadioBot
{
    public class Program
    {
        public static int WorkingThreads = 0;
        public static int MaxThreads = 16;
        public static string url = "https://coderadio-relay-ffm.freecodecamp.org/radio/8010/low.mp3";
        public static List<MediaFoundationReader> foundationReaders = new List<MediaFoundationReader>();
        public static List<WasapiOut> wasapiOuts = new List<WasapiOut>();


        public static void Main()
        {
            Console.WriteLine("INTERNET RADIO VIEWER BOT");
            Console.Write("Set adress url: ");
            url = Console.ReadLine();
            for(int i = 0; i < MaxThreads; i++)
            {
                Thread thread = new Thread(ThreadLoop);
                thread.IsBackground = true;
                thread.Start();
                thread.Name = i.ToString();
                WorkingThreads++;
                
            }
            while (true)
            {
                Console.Title = "INSTANCES: " + wasapiOuts.Count + " THREADS: " + WorkingThreads;
            };
        }

        public static void ThreadLoop()
        {
            while(true)
            {
                try
                {
                    MediaFoundationReader reader = new MediaFoundationReader(url);
                    WasapiOut wo = new WasapiOut();
                    wo.Init(reader);
                    wo.Play();
                    wo.Volume = 0f;
                    wo.Pause();
                    foundationReaders.Add(reader);
                    wasapiOuts.Add(wo);
                    Console.WriteLine("Thread [{0}] add new instance!", Thread.CurrentThread.Name);
                }
                catch
                {
                    Console.WriteLine("Thread [{0}] ERROR!");
                }
            }
        }
    }    
}
