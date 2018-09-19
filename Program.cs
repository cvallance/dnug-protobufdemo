using System;
using System.Diagnostics;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace DNUG.ProtobufDemoComplete
{
    class Program
    {
        static void Main(string[] args)
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

            using (var output = File.Create("garry.dat"))
            {
                garry.WriteTo(output);
            }

            using (var input = File.OpenRead("garry.dat"))
            {
                var garryAgain = Person.Parser.ParseFrom(input);
                Console.WriteLine($"Itttttssss {garryAgain.Name}");
            }
        }
    }
}
