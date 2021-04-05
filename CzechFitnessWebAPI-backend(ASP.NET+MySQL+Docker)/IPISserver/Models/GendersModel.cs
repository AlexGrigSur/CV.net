using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using IPISserver.DataBase;

namespace IPISserver.Models
{
    public class GendersModel
    {
        public int id { get; set; }
        public string gender { get; set; }

        /// <summary>
        /// Factory methods that create objects from select of `Genders` table 
        /// </summary>
        /// <param name="values">result of `Genders` select values</param>
        /// <returns>List of GenderModels objects</returns>
        static public List<GendersModel> Factory(List<string[]> values)
        {   
            List<GendersModel> result = new List<GendersModel>(values.Count); 
            foreach(var row in values)
            {
                if(row.Length<2) 
                    throw new ArgumentException();
                result.Add(new GendersModel() { id = Convert.ToInt32(row[0]), gender = row[1]});
            }
            return result;
        }
    } 
}