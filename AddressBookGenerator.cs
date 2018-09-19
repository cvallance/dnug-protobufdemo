using System;
using System.Collections.Generic;
using Bogus;
using Google.Protobuf.WellKnownTypes;

namespace DNUG.ProtobufDemoComplete
{
    public static class AddressBookGenerator
    {
        private const short NumberOfPeople = 50;
        private const byte NumberOfPhoneNums = 2;
        public static AddressBook Generate()
        {
            var people = GeneratePeople();
            var addressBook = new AddressBook();
            addressBook.People.Add(people);

            return addressBook;
        }

        private static IEnumerable<Person> GeneratePeople()
        {
            var lastUpdated = Timestamp.FromDateTime(DateTime.UtcNow);
            var personFaker = new Faker<Person>()
                .RuleFor(x => x.Name, f => f.Person.FullName)
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.LastUpdated, lastUpdated);
            
            var phoneNumberFaker = new Faker<Person.Types.PhoneNumber>()
                .RuleFor(x => x.Number, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.Type, f => f.PickRandom<Person.Types.PhoneType>());
            
            for (var i = 0; i < NumberOfPeople; i++)
            {
                var person = personFaker.Generate();
                var phoneNumbers = phoneNumberFaker.Generate(NumberOfPhoneNums);
                person.Phones.Add(phoneNumbers);
                
                yield return person;
            }
        }
    }
}
