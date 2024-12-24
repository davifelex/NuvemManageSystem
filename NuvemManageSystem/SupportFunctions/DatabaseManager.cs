using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NuvemManageSystem.Objects;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace NuvemManageSystem.SupportFunctions
{
    public class DatabaseManager
    {
        private readonly string? _connectionString;

        public DatabaseManager()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                Console.WriteLine("Connection ok");
                return connection;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error in open connection: {ex.Message}");
                throw;
            }
        }

        public void ExecuteCommand(string sql)
        {
            using (SqlConnection connection = OpenConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    int affectedRows = command.ExecuteNonQuery();
                    Console.WriteLine($"Comando executado com sucesso, linhas afetadas: {affectedRows}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao tentar executar comando sql: {ex.Message}");
                }
            }
        }

        public DataTable? ExecuteQuery(string sql)
        {
            using (SqlConnection connection = OpenConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
                catch (Exception ex)
                { 
                    Console.WriteLine($"Erro ao tentar executar a consulta: {ex.Message}");
                    return null;
                }
            }
        }

        public void ExecuteInsert(string table, string[] columns, string[] values)
        {
            if (columns.Length != values.Length)
            {
                Console.WriteLine("Erro, para cada coluna deve aver um valor");
                return;
            }
            try
            {
                using (SqlConnection connection = OpenConnection())
                {
                    string columnsList = string.Join(", ", columns);
                    string parametersList = string.Join(", ", columns.Select((col, index) => "@" + col));

                    string sql = $"INSERT INTO {table} ({columnsList}) VALUES ({parametersList})";

                    SqlCommand command = new SqlCommand(sql, connection);

                    for (int i = 0; i < columns.Length; i++)
                    {
                        command.Parameters.AddWithValue("@" + columns[i], values[i]);
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"Inserção realizada com sucesso! Linhas afetadas: {rowsAffected}");
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Erro ao tentar realizar um INSERT: {ex.Message}");
            }
        }

        private bool ExecuteSqlWriter(string typeCommand, string table, string[] columns, string[] values, WhereObject? Where=null)
        {
            if (columns.Length != values.Length)
            {
                Console.WriteLine($"Quantidade de valores diferente das colunas:\nColunas: {columns.Length}\nValores: {values.Length}");
                return false;
            }
            string sql = "";
            string columnsList = string.Join(", ", columns);
            string parametersList = string.Join(", ", columns.Select((col, index) => "@" + col));

            switch (typeCommand.ToLower())
            {
                case "insert":
                    sql = $"INSERT INTO {table} ({columnsList}) VALUES ({parametersList})";
                    break;
                case "update":
                    sql = $"UPDATE {table}";
                    break;
                case "delete":
                    break;

                default:
                    Console.WriteLine("TypeCommand invalido");
                    return false;
            }
            if (Where != null)
            {
                sql += " WHERE ";
                sql += Where.GetWhere();
            }


            return true;
        }
    }
}
