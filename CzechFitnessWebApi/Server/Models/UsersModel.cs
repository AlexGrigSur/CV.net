using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using IPISserver.DataBase;

namespace IPISserver.Models
{
    public class UsersModel
    {
        public int id { get; set; }
        public int People_ID { get; set; }
        public DateTime beginDate { get; set; }
        public int Tarrif_ID { get; set; }
        public int trainingsLeft { get; set; }
        public string cardNumber { get; set; }
        public int TrainingPack_ID { get; set; }

        static public string tableName = "Users";
        static public List<string> columnsNames = new List<string>{ "id", "People_ID","beginDate","Tarrif_ID","trainingsLeft","cardNumber", "TrainingPack_ID"};

        static public List<UsersModel> Factory(List<string[]> values)
        {   
            List<UsersModel> result = new List<UsersModel>(values.Count); 
            foreach(var row in values)
            {
                if(row.Length<4) 
                    throw new ArgumentException();
                result.Add(new UsersModel()
                    {
                        id = Convert.ToInt32(row[0]),
                        People_ID = Convert.ToInt32(row[1]),
                        beginDate = Convert.ToDateTime(row[2]),
                        Tarrif_ID = Convert.ToInt32(row[3]),
                        trainingsLeft = Convert.ToInt32(row[4]),
                        cardNumber = row[5],
                        TrainingPack_ID = Convert.ToInt32(row[6])  
                    })
            }
            return result;
        }
    } 
}