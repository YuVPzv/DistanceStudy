﻿using System;
using System.Data.SqlClient;
using DbRepository.Classes.Entities;

namespace DbRepository.Classes
{
    /// <summary>
    ///     Вспомогательный класс для преобразование объектов из бд
    ///     к сущностям в программе
    /// </summary>
    internal static class DbHelper
    {
        // Метод-расширение для DataReader для преобразования информации из бд
        // в класс программный Тема
        public static Thema ToThema(this SqlDataReader reader)
        {
            return new Thema
            {
                Id = reader["Id_Thema"] != DBNull.Value ? (int) reader["Id_Thema"] : 0,
                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : null,
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null
            };
        }

        // Метод-расширение для DataReader для преобразования информации из бд
        // в класс программный Подтема
        public static Subthema ToSubthema(this SqlDataReader reader)
        {
            return new Subthema
            {
                Id_Thema = reader["Id_Thema"] != DBNull.Value ? (int) reader["Id_Thema"] : 0,
                Id = reader["Id_Subthema"] != DBNull.Value ? (int) reader["Id_Subthema"] : 0,
                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : null,
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null
            };
        }
    }
}