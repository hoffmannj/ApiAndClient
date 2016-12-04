using System.Collections.Generic;
using DrinkApi.Common.Models;

namespace DrinkApi.Common.Interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> GetAll();
        Drink Get(string name);
        bool Delete(string name);
        Drink Update(Drink newValue);
        Drink Create(Drink newEntry);
    }
}
