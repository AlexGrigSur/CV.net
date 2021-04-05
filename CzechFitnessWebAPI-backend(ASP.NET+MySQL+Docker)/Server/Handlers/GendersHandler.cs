using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using IPISserver.DataBase;
using IPISserver.Models;

namespace IPISserver.Handlers
{
    public class GendersHandler
    {
        private string tableName = "Genders";
        private List<string> columnsNames = new List<string>{ "gender"};

        /// <summary>
        /// Select request to `Genders` table 
        /// </summary>
        /// <returns>Json if select result has value, else null</returns>
        public string? Select()
        { 
            List<GendersModel> models = GendersModel.Factory(DBCommands.CustomSelectRequest($"SELECT * FROM `CzechFitness`.`Genders`"));
            return (models.Count==0)? null: JsonConvert.SerializeObject(new { Genders = models });
        }

        /// <summary>
        /// Select request to specific row in `Genders` table 
        /// </summary>
        /// <param name="id">specific row id</param>
        /// <returns>Json if select result has value, else null</returns>
        public string? Select(int id) 
        {
            List<GendersModel> models = GendersModel.Factory(DBCommands.CustomSelectRequest($"SELECT * FROM `CzechFitness`.`Genders` where `id`='{id}'"));
            return (models.Count==0)? null: JsonConvert.SerializeObject(new { Genders = models });
        }

        /// <summary>
        /// Insert new row in `Genders` table 
        /// </summary>
        /// <param name="model">Model of `Genders` table object</param>
        /// <returns>null if request done successfully. Else - error message</returns>
        public string? InsertNewRow(GendersModel model) => DBCommands.Insert(tableName,new List<string> {model.gender});

        /// <summary>
        /// Insert new row in `Genders` table 
        /// </summary>
        /// <param name="model">Model of `Genders` table object</param>
        /// <returns>null if request done successfully. Else - error message</returns>
        public string? UpdateRow(GendersModel model) => DBCommands.Update(tableName, model.id, columnsNames ,new List<string> {model.gender});

        /// <summary>
        /// Delete request to specific row in `Genders` table 
        /// </summary>
        /// <param name="id">specific row id</param>
        /// <returns>null if request done successfully. Else - error message</returns>
        public string? DeleteRow(int id) => DBCommands.Delete(tableName, new List<int>{ id });
    }
}