using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpgaveAS301;

namespace FootballRestTest.Manger
{
    interface IFootBallManger
    {
        IEnumerable<FootballPlayer> Get();

        FootballPlayer Get(int id);
       
        IEnumerable<FootballPlayer> GetName(string name);

        IEnumerable<FootballPlayer> GetPrice(int min,int max);

        IEnumerable<FootballPlayer> GetShirt(int min, int max);

        IEnumerable<FootballPlayer> Search(Predicate<FootballPlayer> filter);

        bool Create(FootballPlayer player);

        bool Update(int id,FootballPlayer player);


        bool Delete(int id);
    }
}
