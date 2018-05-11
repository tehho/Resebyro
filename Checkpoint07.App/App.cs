using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint07.Data;
using Checkpoint07.Domain;

namespace Checkpoint07.App
{
    public class App
    {
        private bool _running;

        private JourneyRepository Journeys;

        private PersonRepository Persons;

        public App()
        {
            Journeys = new JourneyRepository(new ListManagerJourney());
            for (int i = 0; i < 11; i++)
            {
                DateTime start = new DateTime(2018, 02, 01);
                start = start.AddMonths(i);
                Journeys.Add(new Journey("Göteborg", "Tur i hamnen", 10, 5, start,
                    start.AddDays(7)));
            }

        }

        public void Run()
        {
            _running = true;
            while (_running)
            {
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("0 - Avsluta");
                Console.WriteLine("1 - Lägga till en ny resa");
                Console.WriteLine("2 - Anmäl en person till nästa resa");
                Console.WriteLine("3 - Visa resa");
                Console.WriteLine("4 - Visa alla resor");

                var input = Console.ReadLine().ToLower();
                if (input == "0" || input == "avsluta")
                    _running = false;
                else
                {
                    if (input == "1" || input == "lägga till resa")
                    {
                        Console.WriteLine("Lägga till resa");
                    }
                    else if (input == "2" || input == "lägga till person")
                    {
                        SignPerson();
                    }
                    else if (input == "3" || input == "nästa")
                    {
                        Print(Journeys.Next());
                    }

                    else if (input == "4" || input == "lägga till person")
                    {
                        Journeys.GetAll().ToList().ForEach(Print);

                        PressAnyKey();

                    }
                    else
                    {
                        Console.WriteLine($"Okänt kommando: {input}");
                    }

                    
                }

            }
        }

        private void SignPerson()
        {
            Console.WriteLine("Vill du anmäla en ny person?");
            var input = Console.ReadKey().ToString().ToLower();
            if (input == "y" || input == "j")
            {
                SignNewPerson();
            }
            else
            {
                Person person = null;

                SignExistingPerson(person);
            }
        }

        private void SignExistingPerson(Person person)
        {

        }

        private void SignNewPerson()
        {

            Console.WriteLine("Personens namn:");
            var name = Console.ReadLine();

            var address = new Address();

            Console.WriteLine("Personens gatunamn:");
            address.StreetName = Console.ReadLine();

            Console.WriteLine("Personens gatunummer:");
            address.StreetNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Personens stad:");
            address.City = Console.ReadLine();

            Console.WriteLine("Personens postnummer:");
            address.StreetNumber = Convert.ToInt32(Console.ReadLine());

            SignExistingPerson(Persons.Add(new Person(name, address)));
        }

        private void Print(Journey journey)
        {

            Console.WriteLine($"{journey.Destination}");
            Console.WriteLine($"{journey.Description}");
            Console.WriteLine($"{journey.DepartureDate.Value.Date}");
            Console.WriteLine($"{journey.ReturnDate.Value.Date}");
            Console.WriteLine();
        }

        public void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
