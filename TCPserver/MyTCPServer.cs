using OpgaveAS301;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TCPserver
{
    public class MyTCPServer
    {
       
        public void Start(int PORT)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Client connected");
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        
                        DoClient(tmpSocket);

                    }
                );
            }
        }
        //Jitem_Players
        public void DoClient(TcpClient suckit)
        {
            bool running = true;
         
            string OutputMSG = "";
            using (StreamReader sr = new StreamReader(suckit.GetStream()))
            using (StreamWriter sw = new StreamWriter(suckit.GetStream()))
            {
                while (running)
                {
                    string comandString = sr.ReadLine();


                    string datastring = "def";
                    switch (comandString)
                    {
                        case "HentAlle":
                            OutputMSG = GetObj();
                            
                            break;
                        case "loc":
                            datastring = sr.ReadLine();
                            OutputMSG = GetObj(datastring);
                            Console.WriteLine("retrived:" + OutputMSG);
                            break;
                        case "Hent":
                            datastring = sr.ReadLine();
                            OutputMSG = GetObjByID(datastring);
                            Console.WriteLine("retrived:" + OutputMSG);
                            break;
                        case "Gem":
                            datastring = sr.ReadLine();
                            OutputMSG = SaveObj(datastring);
                            Console.WriteLine("saved:" + OutputMSG);
                            break;
                        case "Del":
                            datastring = sr.ReadLine();
                            OutputMSG = DelObj(datastring);
                            Console.WriteLine("deleted:" + OutputMSG);
                            break;
                        case "Quit":
                            running = false;
                            Console.WriteLine("client disconected");
                            break;
                        default:
                            OutputMSG = "invalied comand use: HentAlle,Hent <id>,Gem<Json>,Del<id>,Quit"; 
                            break;
                    }
                    sw.WriteLine(OutputMSG);
                    sw.Flush();

                }
            }
            suckit?.Close();
        }
        public string GetObj(string value = "none")
        {
            string outputMSG = "";
            int id = 0;
            try
            {
                id = int.Parse(value);
            }
            catch (Exception)
            {

                if (value == "none")
                {
                    FootballPlayer pla = null;
                    int len = Program.Jitem_Players.Count;
                    for (int i = 0; i < len; i++)
                    {
                        pla = Program.Jitem_Players[i];
                        outputMSG += JsonSerializer.Serialize<FootballPlayer>(pla);
                        Console.WriteLine($"retrived item: {pla.Name} of id:{pla.ID}");
                        //prevent last ',' in outpu} of id:{Program.Jitem_Players[i].Id}"t
                        if (i >= len - 1) break;
                        outputMSG += ",";

                    }
                    return outputMSG;
                }

                return "input value did not match any item id";
            }

            outputMSG = JsonSerializer.Serialize<FootballPlayer>(Program.Jitem_Players[id]);

            return outputMSG;
        }
        public string GetObjByID(string value = "0")
        {
           
            int id = 0;
            try
            {
                id = int.Parse(value);
            }
            catch (Exception)
            {
                return "input value did not match any item id";
            }
            string outputMSG = $"Item with ID: {id} not found";
            FootballPlayer pla = null;
            int len = Program.Jitem_Players.Count;
            for (int i = 0; i < len; i++)
            {
                pla = Program.Jitem_Players[i];
                if (id == pla.ID) {
                    outputMSG = JsonSerializer.Serialize<FootballPlayer>(pla);
 
                    return outputMSG;
                }
            }
            return outputMSG;
            
        }
        public string SaveObj(string jsonfile)
        {
            //making sure the input string is of the right format and can be saved
            FootballPlayer jitem = null;
            try
            {
                jitem = JsonSerializer.Deserialize<FootballPlayer>(jsonfile);
                
            }
            catch (Exception)
            {
                return "Invalid json format, could not be saved";
            }
            if (jitem == null) return "Data was empty, could not be saved";

            //basic id code protype, could be much better done
            //alwos for aouto id if id is -1
            if (jitem.ID == -1)
            {
                int id = 0;
                foreach (FootballPlayer item in Program.Jitem_Players)
                {
                    if ((int)item.ID == id)
                    {
                            id++;
                    }
                    else
                    {
                        
                        break;
                    }
                }
                jitem.ID = id;
            }

            Program.Jitem_Players.Add(jitem);

            return $"{jitem.Name} was succesfuly saved to list with id:{jitem.ID}";
    
        }
        public string DelObj(string value)
        {
            string outputMSG = "Unknown Error";
            int id = 0;
            try
            {
                id = int.Parse(value);
            }
            catch (Exception)
            {
                return "input value did not match any item id";
            }
            if (id >= Program.Jitem_Players.Count || id<0) return "number to out of range";
            outputMSG = $"Player id:{Program.Jitem_Players[id].ID} of name: {Program.Jitem_Players[id].Name} removed from database";
            Program.Jitem_Players.RemoveAt(id);
            return outputMSG;
        }
    }
}
