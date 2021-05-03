using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using IPISserver.DataBase;

namespace IPISserver.Models
{
    public class TrainingPacksModel
    {
        public int id { get; set; }
        public string packName { get; set; }
        public int groupTrainingCount { get; set; }
        public int groupIndividualCount { get; set; }

        static public string tableName = "Tarrifs";
        static public List<string> columnsNames = new List<string>{ "id", "packName","groupTrainingCount","groupIndividualCount" };

        static public List<TrainingPacksModel> Factory(List<string[]> values)
        {   
            List<TrainingPacksModel> result = new List<TrainingPacksModel>(values.Count); 
            foreach(var row in values)
            {
                if(row.Length<4) 
                    throw new ArgumentException();
                result.Add(new TrainingPacksModel() 
                    { 
                        id = Convert.ToInt32(row[0]), 
                        packName = row[1],
                        groupTrainingCount = Convert.ToInt32(row[2]),
                        groupIndividualCount = Convert.ToInt32(row[3])
                    })
            }
            return result;
        }
    } 
}