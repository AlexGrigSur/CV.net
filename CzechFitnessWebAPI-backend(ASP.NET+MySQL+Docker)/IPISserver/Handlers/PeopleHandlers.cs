using System;
using System.Collections.Generic;
using IPISserver.Models;
using IPISserver.DataBase;
using Newtonsoft.Json;

namespace IPISserver.Handlers
{
    public class PeopleHandler
    {
        private string tableName = "People";
        private List<string> columnsName = new List<string>{ "name", "surname", "dateOfBirth", "weight", "height", "passport","gender_ID"};

        /// <summary>
        /// Select request to `People` table 
        /// </summary>
        /// <returns>Json if select result has value, else null</returns>
        public string? Select()
        {
            List<GendersModel> gendersModelSelect = GendersModel.Factory(DBCommands.CustomSelectRequest($"SELECT * FROM `CzechFitness`.`Genders` where `id` in (select DISTINCT(`Gender_ID`) FROM `CzechFitness`.`People`)"));     
            return (gendersModelSelect.Count==0)? null : JsonConvert.SerializeObject(
            new 
            {
                Genders = gendersModelSelect, 
                People =  PeopleModel.Factory(DBCommands.CustomSelectRequest($"SELECT * FROM `CzechFitness`.`People`"))
            });
        }

        /// <summary>
        /// Select request to specific row in `People` table 
        /// </summary>
        /// <param name="id">specific row id</param>
        /// <returns>Json if select result has value, else null</returns>
        public string? Select(int id)
        {
            List<GendersModel> gendersModelSelect = GendersModel.Factory(DBCommands.CustomSelectRequest($"SELECT * FROM `CzechFitness`.`Genders` where `id` in (select DISTINCT(`Gender_ID`) FROM `CzechFitness`.`People` WHERE `id`='{id}')"));
            return (gendersModelSelect.Count==0)? null : JsonConvert.SerializeObject(
                new 
                {
                    Genders = gendersModelSelect, 
                    People =  PeopleModel.Factory(DBCommands.CustomSelectRequest($"SELECT * FROM `CzechFitness`.`People` WHERE `id`='{id}'"))
                }
            );
        }

        /// <summary>
        /// Insert new row in `People` table 
        /// </summary>
        /// <param name="model">Model of `Genders` table object</param>
        /// <returns>null if request done successfully. Else - error message</returns>
        public string? InsertNewRow(PeopleModel model) => DBCommands.Insert(tableName, new List<string> { model.name, model.surname, $"{model.dateOfBirth.Year}.{model.dateOfBirth.Month}.{model.dateOfBirth.Day}", model.weight.ToString() ,model.height.ToString(), model.passport, model.genderID.ToString()});

        /// <summary>
        /// Insert new row in `People` table 
        /// </summary>
        /// <param name="model">Model of `People` table object</param>
        /// <returns>null if request done successfully. Else - error message</returns>
        public string? UpdateRow(PeopleModel model) => DBCommands.Update(tableName, model.id, columnsName, new List<string> { model.name, model.surname, $"{model.dateOfBirth.Year}.{model.dateOfBirth.Month}.{model.dateOfBirth.Day}", model.weight.ToString() ,model.height.ToString(), model.passport, model.genderID.ToString()});

        /// <summary>
        /// Delete request to specific row in `People` table 
        /// </summary>
        /// <param name="id">specific row id</param>
        /// <returns>null if request done successfully. Else - error message</returns>
        public string? DeleteRow(int id) => DBCommands.Delete(tableName, new List<int>{ id });
    }
}