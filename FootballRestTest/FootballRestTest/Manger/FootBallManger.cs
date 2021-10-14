using OpgaveAS301;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballRestTest.Manger
{
    public class FootBallManger : IFootBallManger
    {
        //Debug content fill
        private static List<FootballPlayer> _data = ((Func<List<FootballPlayer>>)(() => {

            List<FootballPlayer> data = new List<FootballPlayer>();
            for (int i = 0; i< 10; i++)
            {
                data.Add(
                        new FootballPlayer(i, "player" + i, Math.Abs((5-i) * 100), i*10));
            }
            return data;

        }))();

        public bool Create(FootballPlayer player)
        {
            //find a id not taken and use that, not fast but efective
            int id = 0;
            for (int i = 0; i < _data.Count; i++)
            {
                bool test = false;

                for (int j = 0; j < _data.Count; j++)
                {
                    FootballPlayer play = _data[j];
                    test = (play.ID == id);
                    if (test) break;
                }
                if (test)
                {
                    id++;
                    continue;
                }
                player.ID = id;
                break;
            }
            //potental place for return false; if test for iligal data is true 

            _data.Add(player);

            return true;
        }
     

        /// <summary>
        /// Gets all entrys
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FootballPlayer> Get()
        {
            return new List<FootballPlayer>(_data);
        }
        /// <summary>
        /// Gets a entry with id of input value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FootballPlayer Get(int id)
        {
            if (_data.Exists(p => p.ID == id))
            {
                return _data.Find(p => p.ID == id);
            }

            throw new KeyNotFoundException($"Id {id} findes ikke");
        }
        /// <summary>
        /// Find all with name starting with input value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<FootballPlayer> GetName(string name)
        {
            return _data.FindAll(p => p.Name.StartsWith(name));
        }
        /// <summary>
        /// Finde all with shirtnumbers between min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IEnumerable<FootballPlayer> GetPrice(int min, int max)
        {
            return _data.FindAll(p => (p.Price >= min && p.Price<= max));
        }
        /// <summary>
        /// Finde all with shirtnumbers between min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IEnumerable<FootballPlayer> GetShirt(int min, int max)
        {
            return _data.FindAll(p => (p.Shirt >= min && p.Shirt <= max));
        }
        /// <summary>
        /// Find all of [insert filter Predicate]
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<FootballPlayer> Search(Predicate<FootballPlayer> filter)
        {
            return _data.FindAll(filter);
        }
        /// <summary>
        /// Update datavalue
        /// </summary>
        /// <param name="id">the id</param>
        /// <param name="player">the data</param>
        /// <returns>returns fales if a new entry was created</returns>
        public bool Update(int id, FootballPlayer player)
        {
            FootballPlayer oldplayer = null;
            try
            {
                oldplayer = this.Get(id);
            }
            catch (Exception)
            {
                _data.Add(new FootballPlayer(player.ID, player.Name, player.Price, player.Shirt));
                return false;
            }

            oldplayer.ID = player.ID;
            oldplayer.Name = player.Name;
            oldplayer.Shirt = player.Shirt;
            oldplayer.Price = player.Price;

            return true;
        }

        public bool Delete(int id)
        {
            List<FootballPlayer> newList = new List<FootballPlayer>();
            foreach (FootballPlayer item in _data)
            {
                if (item.ID != id) newList.Add(item);
            }
            _data.Clear();
            _data.AddRange(newList);
            return true;
        }
    }
}
