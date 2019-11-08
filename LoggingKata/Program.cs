using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;


namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            //logger.LogInfo("Log initialized"); // print logs to console

            var lines = File.ReadAllLines(csvPath); //array designed to read locations a

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable taco1 = null;
            ITrackable taco2 = null;
            double distance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                // logger.LogInfo($"Lines: {lines[i]}");

                ITrackable locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                for (int k = i + 1; k < locations.Length; k++)
                {
                    ITrackable locB = locations[k];
                    GeoCoordinate corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    if (corA.GetDistanceTo(corB) >= distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        taco1 = locA;
                        taco2 = locB;
                    }
                }
            }
            Console.WriteLine(taco1.Name);
            Console.WriteLine(taco2.Name);

            // TODO:  Find the two Taco Bells in Alabama that are the furthest from one another.
            // HINT:  You'll need two nested forloops
        }
    }
}