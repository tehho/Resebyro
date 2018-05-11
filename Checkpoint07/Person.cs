using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint07.Domain
{
    public class Person
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public List<Journey> UnpayedJourneys;

        public bool CanGo => UnpayedJourneys.Count < 3;

        public bool HasUnpayedJourneys => UnpayedJourneys.Count > 0;

        public Journey LastUnpayedJourney
        {
            get
            {
                if (!HasUnpayedJourneys)
                    throw new InvalidOperationException("No unpayed journeys");
                return UnpayedJourneys[0];
            }
        }

        public Person(string name, Address address)
        {
            Id = null;
            Name = name;
            Address = address;
            UnpayedJourneys = new List<Journey>();
        }

        public void GoOnJourney(Journey journey)
        {
            UnpayedJourneys.Add(journey);
        }
        public void PayJourney(Journey journey)
        {
            UnpayedJourneys.Remove(journey);
        }

        public decimal CalculateLateFees(DateTime date)
        {
            if (!HasUnpayedJourneys)
                return 0;
            var cost = UnpayedJourneys.Sum(journey =>
                journey.Cost * (decimal) Math.Pow(1.05, (date - journey.DepartureDate).Value.Days));
            return cost;
        }

        public decimal CalculateLateRate(DateTime date)
        {
            if (HasUnpayedJourneys)
                return (decimal)Math.Pow(1.05,(date - LastUnpayedJourney.DepartureDate).Value.Days);
            
            return 1;
        }

    }
}
