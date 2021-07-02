using System;
using static System.Environment;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacetedBuilder
{
    public class Person
    {
        public string FirstName, MiddleName, LastName;
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"Full Name: {FirstName} {MiddleName} {LastName} {NewLine}" +
                   $"{nameof(StreetAddress)}: {StreetAddress} {NewLine}" +
                   $"{nameof(Postcode)}: {Postcode} {NewLine}" +
                   $"{nameof(City)}: {City} {NewLine}" +
                   $"{nameof(CompanyName)}: {CompanyName} {NewLine}" +
                   $"{nameof(Position)}: {Position} {NewLine}" +
                   $"{nameof(AnnualIncome)}: {AnnualIncome} {NewLine}"; 
        }
    }

    public class PersonBuilder // facade
    {
        // reference!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        public PersonNameBuilder Name => new PersonNameBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonNameBuilder : PersonBuilder
    {
        public PersonNameBuilder(Person person)
        {
            this.person = person;
        }

        public PersonNameBuilder FirstName(string firstName)
        {
            person.FirstName = firstName;
            return this;
        }

        public PersonNameBuilder MiddleName(string middleName)
        {
            person.MiddleName = middleName;
            return this;
        }

        public PersonNameBuilder LastName(string lastName)
        {
            person.LastName = lastName;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        // might not work with a value type!
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        } 

        public PersonAddressBuilder WhitPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Name.FirstName("Ranieri")
                     .MiddleName("Michel Alves")
                     .LastName("Silva")
                .Lives.At("Street 1 number 1")
                      .In("City1")
                      .WhitPostcode("PostCode1")
                .Works.At("CompanyName1")
                      .AsA("Position1")
                      .Earning(123456);

            Console.WriteLine(person);
        }
    }
}
