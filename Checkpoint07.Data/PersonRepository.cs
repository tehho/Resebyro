using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint07.Domain;

namespace Checkpoint07.Data
{

    public class PersonRepository : IRepository<Person>
    {
        private IDbManager<Person> context;

        public PersonRepository(IDbManager<Person> context)
        {
            this.context = context;
        }
        
        public Person Add(Person obj)
        {
            if (obj == null)
                throw new NullReferenceException("Person can't be null.");

            if (obj.Name == null || obj.Address == null)
                throw new ArgumentNullException(nameof(obj), "Name or Address can't be null.");

            return context.Add(obj);
        }

        public Person Get(Person obj)
        {
            if (obj == null)
                throw new NullReferenceException("Person can't be null.");

            if (obj.Name == null)
                throw new ArgumentNullException(nameof(obj), "Name or Address can't be null.");

            return Get(person => person.Name == obj.Name);
        }

        public Person Get(Func<Person, bool> method)
        {
            return context.Get(method);
        }

        public Person Update(Person obj)
        {
            var person = Get(obj);

            if (obj == null)
                throw new NullReferenceException();

            if (obj.Name != null)
            {
                person.Name = obj.Name;
            }
            if (obj.Address != null)
            {
                person.Address = obj.Address;
            }

            return context.Update(person);
        }

        public Person Remove(Person obj)
        {
            var person = Get(obj);

            if (person.HasUnpayedJourneys)
                throw new InvalidOperationException("Person has unpayed journeys.");

            return context.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return context.GetAll();
        }

        public IEnumerable<Person> GetAll(Func<Person, bool> method)
        {
            return context.GetAll(method);
        }
    }
}
