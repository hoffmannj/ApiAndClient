using System;
using System.Collections.Generic;
using System.Linq;
using DrinkApi.Common.Interfaces;
using DrinkApi.Common.Models;

namespace DrinApi.MemoryStorage
{
    public class Storage : IDrinkRepository
    {
        private static readonly Dictionary<string, int> Drinks = new Dictionary<string, int>();
        private static readonly object Locker = new Object();

        public IEnumerable<Drink> GetAll()
        {
            lock (Locker)
            {
                return Drinks.Select(entry => new Drink { Name = entry.Key, Quantity = entry.Value });
            }
        }

        public Drink Get(string name)
        {
            lock (Locker)
            {
                if (!Drinks.ContainsKey(name)) return null;
                return new Drink { Name = name, Quantity = Drinks[name] };
            }
        }

        public bool Delete(string name)
        {
            lock (Locker)
            {
                if (!Drinks.ContainsKey(name)) return false;
                Drinks.Remove(name);
                return true;
            }
        }

        public Drink Update(Drink newValue)
        {
            lock (Locker)
            {
                if (!Drinks.ContainsKey(newValue.Name)) return null;
                Drinks[newValue.Name] = newValue.Quantity;
                return new Drink { Name = newValue.Name, Quantity = newValue.Quantity };
            }
        }

        public Drink Create(Drink newEntry)
        {
            lock (Locker)
            {
                if (Drinks.ContainsKey(newEntry.Name)) return null;
                Drinks[newEntry.Name] = newEntry.Quantity;
                return new Drink { Name = newEntry.Name, Quantity = newEntry.Quantity };
            }
        }
    }
}
