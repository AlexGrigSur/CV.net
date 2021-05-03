using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using IPISserver.DataBase;

namespace IPISserver.Models
{
    public class TarrifsModel
    {
        public int id { get; set; }
        public string planName { get; set; }
        public int cost { get; set; }
        public int duration { get; set; }


        static public string tableName = "Tarrifs";
        static public List<string> columnsNames = new List<string>{ "id", "planName","cost","duration" };

        static public List<TarrifsModel> Factory(List<string[]> values)
        {   
            List<TarrifsModel> result = new List<TarrifsModel>(values.Count); 
            foreach(var row in values)
            {
                if(row.Length<4) 
                    throw new ArgumentException();
                result.Add(new TarrifsModel() 
                    { 
                        id = Convert.ToInt32(row[0]), 
                        planName = row[1],
                        cost = Convert.ToInt32(row[2]),
                        duration = Convert.ToInt32(row[3])
                    })
            }
            return result;
        }
    } 
}