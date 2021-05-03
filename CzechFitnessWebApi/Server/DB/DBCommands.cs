using System;
using System.Collections.Generic;
using MySqlConnector;
using IPISserver.DataBase.Connection;

namespace IPISserver.DataBase
{
    static public class DBCommands
    {
        /// <summary>
        /// Initialize CzechFitness DataBase
        /// </summary>
        static public void CreateDataBase()
        {
            DB db = DB.GetInstance();
            db.ExecuteCommand("Create database if not exists `CzechFitness`");

            db.ExecuteCommand("Create table if not exists  `CzechFitness`.`Genders` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`gender` varchar(150) not null unique)");

            db.ExecuteCommand("Create table if not exists  `CzechFitness`.`People` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`name` varchar(150) not null," +
                "`surname` varchar(150) not null," +
                "`dateOfBirth` datetime not null," +
                "`weight` float not null," +
                "`height` float not null," +
                "`passport` varchar(11) not null unique," +
                "`Gender_ID` int(11) not null," +
                "constraint `Gender_FK` foreign key(`Gender_ID`) references  `CzechFitness`.`Genders`(`id`) on delete cascade)");

            db.ExecuteCommand("Create table if not exists  `CzechFitness`.`Tarrifs` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`planName` varchar(150) not null unique," +
                "`cost` float not null," +
                "`duration` int(11) not null)");

            db.ExecuteCommand("Create table if not exists `CzechFitness`.`TrainingPacks` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`packName` varchar(150) not null unique," +
                "`groupTrainingCount` int(11) not null," +
                "`groupIndividualCount` int(11) not null)");

            db.ExecuteCommand("Create table if not exists  `CzechFitness`.`Users` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`People_ID` int(11) not null," +
                "`beginDate` datetime not null," +
                "`Tarrif_ID` int(11) not null," +
                "`trainingsLeft` int(11) not null," +
                "`cardNumber` int(11) not null," +
                "`TrainingPack_ID` int(11) not null," +
                "constraint `People_FK` foreign key(`People_ID`) references  `CzechFitness`.`People`(`id`) on delete cascade," +
                "constraint `Tarrif_FK` foreign key(`Tarrif_ID`) references  `CzechFitness`.`Tarrifs`(`id`) on delete cascade," +
                "constraint `TrainingPack_FK` foreign key(`TrainingPack_ID`) references  `CzechFitness`.`TrainingPacks`(`id`) on delete cascade)");
        }
        /// <summary>
        /// Command that allow run custom select SQL request
        /// </summary>
        /// <param name="sqlCommand">sql command</param>
        /// <returns>List of rows(arrays) values</returns>
        static public List<string[]> CustomSelectRequest(string sqlCommand)
        {
            DB db = DB.GetInstance();
            List<string[]> result = db.ExecuteReader(sqlCommand);
            return result;
        }
        /// <summary>
        /// Insert multiple row in specific table
        /// </summary>
        /// <param name="table">Table name</param>
        /// <param name="values">List of rows(arrays) values</param>
        /// <returns>null if insert completed successfully. Else - error message</returns>
        static public string? Insert(string table, List<string[]> values)
        {
            DB db = DB.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(values.Count);
            string sql = $"Insert into `CzechFitness`.`{table}` values";

            int iterator = 0;
            foreach (var row in values)
            {
                sql += "( null,";
                foreach (var column in row)
                {
                    sql += $"@param_{iterator},";
                    paramList.Add(new MySqlParameter($"@param_{iterator}", column));
                    iterator++;
                }
                sql = sql.Remove(sql.Length - 1);
                sql += "),";
            }
            sql = sql.Remove(sql.Length - 1);

            try
            {
                db.ExecuteCommand(sql, paramList);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// Insert row in specific table
        /// </summary>
        /// <param name="table">Table name</param>
        /// <param name="values">List of row values</param>
        /// <returns>null if insert completed successfully. Else - error message</returns>
        static public string? Insert(string table, List<string> values)
        {
            DB db = DB.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(values.Count);
            string sql = $"Insert into `CzechFitness`.`{table}` values";

            int iterator = 0;
            sql += "( null,";
            foreach (var column in values)
            {
                sql += $"@param_{iterator},";
                paramList.Add(new MySqlParameter($"@param_{iterator}", column));
                iterator++;
            }
            sql = sql.Remove(sql.Length - 1);
            sql += ")";

            try
            {
                return db.ExecuteCommand(sql, paramList);
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// Update row in specific table
        /// </summary>
        /// <param name="table">Table name</param>
        /// <param name="id">updating row id </param>
        /// <param name="columnsName">List of row columns names</param>
        /// <param name="values">List of row values</param>
        /// <returns>null if insert completed successfully. Else - error message</returns>
        static public string? Update(string table, int id, List<string> columnsName ,List<string> values)
        {
            DB db = DB.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(values.Count);
            string sql = $"Update `CzechFitness`.`{table}` set ";
            for (int i=0; i<columnsName.Count;++i)
            {
                sql+= $"`{columnsName[i]}`=@param_{i},";
                paramList.Add(new MySqlParameter($"@param_{i}", values[i]));
            }
            sql = sql.Remove(sql.Length - 1);
            sql += $" WHERE `id`=@param_id";
            paramList.Add(new MySqlParameter($"@param_id", id));
            try
            {
                db.ExecuteCommand(sql, paramList);
                return null;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// Delete row in specific table
        /// </summary>
        /// <param name="table">Table name</param>
        /// <param name="values">List of rows id to be deleted</param>
        /// <returns>null if insert completed successfully. Else - error message</returns>
        static public string? Delete(string table, List<int> values)
        {
            DB db = DB.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(values.Count);
            string sql = $"Delete from `CzechFitness`.`{table}` where `id` in ";

            sql += "(";
            for (int i=0;i<values.Count;++i)
            {
                sql += $"@param_{i},";
                paramList.Add(new MySqlParameter($"@param_{i}", values[i]));
            }
            sql = sql.Remove(sql.Length - 1);
            sql += ")";

            try
            {
                db.ExecuteCommand(sql, paramList);
                return null;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}