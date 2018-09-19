using System;
using System.Diagnostics;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;

namespace DNUG.ProtobufDemoComplete
{
    class Program
    {
        private const string GarryFileName = "garry.dat";
        private const string AddressBookFileName = "addressbook.dat";
        private const string AddressBookJsonFileName = "addressbook.json";
        
        static void Main(string[] args)
        {
//            SimpleGarryWrite();
//            SimpleGarryRead();
//            SaveAddressBook();
//            ReadAddressBook();
        }

        private static void SimpleGarryWrite()
        {
            Console.WriteLine("SimpleGarryWrite");
            
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
            Console.WriteLine("SimpleGarryRead");
            
            Person garry;
            using (var input = File.OpenRead(GarryFileName))
            {
                garry = Person.Parser.ParseFrom(input);
            }
            
            Console.WriteLine($"{garry.Name} read from {GarryFileName}");
        }
        
        private static void SaveAddressBook()
        {
            Console.WriteLine("SaveAddressBook");

            var addressBook = AddressBookGenerator.Generate();
            
            var stopwatch = Stopwatch.StartNew();
            using (var pbOutput = File.Create(AddressBookFileName))
            {
                addressBook.WriteTo(pbOutput);
            }
            Console.WriteLine($"Protobuf time: \t{stopwatch.ElapsedMilliseconds}ms");

            
            stopwatch.Restart();
            using (var jsonOutput = File.CreateText(AddressBookJsonFileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonOutput, addressBook);
            }
            Console.WriteLine($"JSON time: \t{stopwatch.ElapsedMilliseconds}ms");
        }
        
        private static void ReadAddressBook()
        {
            Console.WriteLine("ReadAddressBook");
            
            var stopwatch = Stopwatch.StartNew();
            using (var pbFile = File.OpenRead(AddressBookFileName))
            {
                AddressBook.Parser.ParseFrom(pbFile);
            }
            Console.WriteLine($"Protobuf time: \t{stopwatch.ElapsedMilliseconds}ms");

            
            stopwatch.Restart();
            using (var jsonFile = File.OpenText(AddressBookJsonFileName))
            {
                var serializer = new JsonSerializer();
                serializer.Deserialize(jsonFile, typeof(AddressBook));
            }
            Console.WriteLine($"JSON time: \t{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
