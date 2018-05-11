using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Checkpoint07.Domain;

namespace Checkpoint07.Data
{
    public class ListManagerJourney : IDbManager<Journey>
    {
        private List<Journey> _list;

        public ListManagerJourney()
        {
            _list = new List<Journey>();
        }

        public Journey Add(Journey obj)
        {
            _list.Add(obj);
            return obj;
        }

        public Journey Get(Journey obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Need to search for something");
            }

            if (obj.Id != null)
            {
                return _list.Single(journey => journey.Id == obj.Id);
            }
            if (obj.Destination != null)
            {
                return _list.Single(journey => journey.Destination == obj.Destination);
            }
            if (obj.DepartureDate != null)
            {
                return _list
                    .Where(journey => journey.LastSignupDay > obj.DepartureDate)
                    .OrderByDescending(journey => journey.DepartureDate)
                    .First();
            }
            throw new ArgumentNullException(nameof(obj), "Need to search for something");
        }

        public Journey Get(Func<Journey, bool> method)
        {
            return _list.Single(method);
        }

        public Journey Update(Journey obj)
        {
            if (obj == null)
                throw new NullReferenceException("Journey can't be null.");

            if (obj.Id == null)
            {
                throw new ArgumentNullException(nameof(obj), "Id can't be null. Contact System Admin");
            }

            var journey = Get(j => j.Id == obj.Id);

            if (obj.Destination != null)
            {
                journey.Destination = obj.Destination;
            }
            if (obj.Description != null)
            {
                journey.Description = obj.Description;
            }
            if (obj.DepartureDate != null)
            {
                journey.DepartureDate = obj.DepartureDate;
            }
            if (obj.ReturnDate != null)
            {
                journey.ReturnDate = obj.ReturnDate;
            }




            return obj;
        }

        public Journey Remove(Journey obj)
        {

            if (_list.RemoveAll(journey => journey.Destination == obj.Destination) == 0)
            {
                throw new ArgumentException("Journey does not exist");
            }

            return obj;
        }

        public IEnumerable<Journey> GetAll()
        {
            return _list;
        }

        public IEnumerable<Journey> GetAll(Func<Journey, bool> method)
        {
            return _list.Where(method);
        }
    }
}
