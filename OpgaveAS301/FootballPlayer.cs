
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OpgaveAS301
{
    public class FootballPlayer
    {
        private int _id;
        private string _name;
        private int _price;
        private int _shirtNumber;
        /// <summary>
        /// empty instance
        /// </summary>
        public FootballPlayer()
        {

        }
        /// <summary>
        /// loaded instance
        /// </summary>
        /// <param name="id">any int number</param>
        /// <param name="name">must a string with more than 4 chars</param>
        /// <param name="price">any int number higher that 0 (defualt 0)</param>
        /// <param name="shirt">any int number (defualt 0)</param>
        public FootballPlayer(int id, string name, int price = 0, int shirt = 0)
        {
            _id = id;
            _name = name;
            _price = price;
            _shirtNumber = shirt;
        }

       

        [JsonProperty("ID")]
        public int ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        
       

        [JsonProperty("Name")]
        public string Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        
        

        [JsonProperty("Price")]
        public int Price
        {
            set
            {
                _price = value;
            }
            get
            {
                return _price;
            }
        }

        
        [JsonProperty("Shirt")]
        public int Shirt
        {
            set
            {
                _shirtNumber = value;
            }
            get
            {
                return _shirtNumber;
            }
        }


        //System.Text.Json.Serialization.JsonIgnore was the only thing that worked for me
        /// <summary>
        /// a string, will throw ArgumentException if less than 4 chars
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 4)
                {
                    throw new ArgumentException("Your name was to short");
                }

                try
                {
                    _name = value;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Input was not a String");
                }
            }
        }
        /// <summary>
        /// a int number, will throw ArgumentException if not int
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public object id
        {
            get
            {
                return _id;
            }
            set
            {

                try
                {
                    _id = (int)value;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Input was not a number");
                }
            }
        }
        /// <summary>
        /// a int number, will throw ArgumentException if not int
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public object price
        {
            get
            {
                return _price;
            }
            set
            {
                int val;
                try
                {
                    val = (int)value;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Input was not a number");
                }
                if (val < 1)
                {
                    throw new ArgumentException("Price cannot be less than 0");
                }
                _price = val;


            }
        }
        /// <summary>
        /// a int number, will throw ArgumentException if not int
        /// </summary>

        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public object shirtNumber
        {
            get
            {
                return _shirtNumber;
            }
            set
            {
                int val;
                try
                {
                    val = (int)value;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Input was not a number");
                }
                if (val < 1 || val > 100) {
                    throw new ArgumentException("Outside of valid number range");
                }
              
                _shirtNumber = val;
            }
        }
    
    }
}
