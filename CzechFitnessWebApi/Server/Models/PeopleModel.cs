using System;
using System.Collections.Generic;

namespace IPISserver.Models
{
    public class PeopleModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime dateOfBirth { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public string passport { get; set; }
        public int genderID { get; set; }


        public string tableName = "People";
        public List<string> columnsNames = new List<string>{ "id","name","surname","dateOfBirth", "weight", "height", "passport", "genderID" };
        /// <summary>
        /// Factory methods that create objects from select of `People` table 
        /// </summary>
        /// <param name="values">result of `People` select values</param>
        /// <returns>List of GenderModels objects</returns>
        static public List<PeopleModel> Factory(List<string[]> values)
        {   
            List<PeopleModel> result = new List<PeopleModel>(values.Count); 
            foreach(var row in values)
            {
                if(row.Length<8) 
                    throw new ArgumentException();
                result.Add(new PeopleModel() { id = Convert.ToInt32(row[0]), name = row[1], surname=row[2], dateOfBirth = Convert.ToDateTime(row[3]), weight = Convert.ToDouble(row[4]), height = Convert.ToDouble(row[5]), passport = row[6], genderID = Convert.ToInt32(row[7])});
            }
            return result;
        }
    }
}