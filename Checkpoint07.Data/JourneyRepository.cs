using System;
using System.Collections.Generic;
using System.Linq;
using Checkpoint07.Domain;

namespace Checkpoint07.Data
{
    public class JourneyRepository : IRepository<Journey>

    {
        private IDbManager<Journey> context;

        public JourneyRepository(IDbManager<Journey> context)
        {
            this.context = context;
        }

        public Journey Add(Journey journey)
        {
            return context.Add(journey);
        }

        public Journey Get(Journey journey)
        {
            return context.Get(journey);
        }

        public Journey Get(Func<Journey, bool> method)
        {
            return context.Get(method);
        }

        public Journey Update(Journey obj)
        {
            var journey = Get(obj);



            journey = context.Update(journey);

            return journey;
        }

        public Journey Remove(Journey obj)
        {
            var journey = Get(obj);

            return context.Remove(journey);
        }


        public decimal SignUp(Person person)
        {
            return Next().SignUp(person);
        }

        public Journey Next()
        {
            return GetAll(journey => journey.LastSignupDay > DateTime.Now).OrderBy(journey => journey.DepartureDate).First();
        }

        public IEnumerable<Journey> GetAll()
        {
            return context.GetAll();
        }

        public IEnumerable<Journey> GetAll(Func<Journey, bool> method)
        {
            return context.GetAll(method);
        }
    }
}