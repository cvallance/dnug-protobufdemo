using System;
using System.Diagnostics;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace DNUG.ProtobufDemoComplete
{
    class Program
    {
        private const string GarryFileName = "garry.dat";
        
        static void Main(string[] args)
        {
            SimpleGarryWrite();
            SimpleGarryRead();
        }

        private static void SimpleGarryWrite()
        {
            var garry = new Person
            {
                Id = 1245,
                Name = "Garry Garryson",
                Email = "garry@garrysoncorp.com",
                Phones =
                {
                    new Person.Types.PhoneNumber {Number = "555 1234 5678", Type = Person.Types.PhoneType.Home},
                    new Person.Types.PhoneNumber {Number = "555 1234 8765", Type = Person.Types.PhoneType.Work}
                },
                LastUpdated = Timestamp.FromDateTime(DateTime.UtcNow)
            };

            using (var output = File.Create(GarryFileName))
            {
                garry.WriteTo(output);
            }
            
            Console.WriteLine($"{garry.Name} written to {GarryFileName}");
        }
        
        private static void SimpleGarryRead()
        {
            Person garry;
            using (var input = File.OpenRead(GarryFileName))
            {
                garry = Person.Parser.ParseFrom(input);
            }
            
            Console.WriteLine($"{garry.Name} read from {GarryFileName}");
        }
    }
}
