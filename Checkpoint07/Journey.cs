using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint07.Domain
{
    public class Journey
    {
        [Key]
        public int? Id;
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Description { get; set; }

        public List<Person> People { get; set; }
        [Required]
        public int MaxPeople { get; private set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public DateTime? DepartureDate { get; set; }
        [Required]
        public DateTime? ReturnDate { get; set; }

        public Journey()
        {
            Id = null;
            Destination = null;
            Description = null;
            People = new List<Person>();
            MaxPeople = 0;
            Cost = 0;
            DepartureDate = null;
            ReturnDate = null;
        }

        public Journey(string destination, string description, int cost, int maxPeople, DateTime startDate, DateTime endDate)
        {
            Destination = destination;
            Description = description;
            Cost = cost;
            MaxPeople = maxPeople;
            DepartureDate = startDate;
            ReturnDate = endDate;
            People = new List<Person>();
        }

        public DateTime LastSignupDay => (DateTime)DepartureDate?.AddDays(-5);

        public bool IsFull => People.Count >= MaxPeople;

        private bool CanSignUp(DateTime date)
        {
            return date < LastSignupDay;
        }

        public decimal SignUp(Person person)
        {
            if (IsFull)
                throw new InvalidOperationException("No free spaces on journey");

            if (!CanSignUp(DateTime.Now))
                throw new InvalidOperationException("To close to depaturedate");

            if (!person.CanGo)
                throw new InvalidOperationException("Persons debt is to large");

            var cost = Cost + person.CalculateLateFees(DateTime.Now);

            People.Add(person);
            person.GoOnJourney(this);
            return cost;
        }

    }
}
