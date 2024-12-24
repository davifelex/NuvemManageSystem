using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NuvemManageSystem.Objects
{
    internal class WhereObject
    {
        private Dictionary<string, Condition> Conditions = new Dictionary<string, Condition>();
        private Dictionary<string, string> Connections = new Dictionary<string, string>();
        private Condition TempCondition = new Condition("", "", "");
        private string WhereResult = "";
        private Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public bool AddCondition(string column, string value, string relation, string Connection)
        {
            try
            {
                TempCondition.Column = column;
                TempCondition.Value = value;
                TempCondition.Relation = relation;
                Conditions.Add(column, TempCondition);
                Connections.Add(column, Connection);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Ocorreu um erro ao adicionar condição: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool RemoveCondition(string column)
        {
            try
            {
                Conditions.Remove(column);
                Connections.Remove(column);
            }
            catch (Exception ex)
            { 
                Console.WriteLine ($"Ocorreu um erro ao tentar remover condição: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool BuildWhere()
        {
            try
            {
                foreach (string conditionKey in Conditions.Keys)
                {
                    WhereResult += $"{Connections[conditionKey]} {conditionKey} = @{conditionKey} ";
                    Parameters[$"@{conditionKey}"] = Conditions[conditionKey].Value;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao tentar criar o Where:{ex.Message}");
                return false;
            }
            Conditions.Clear();
            Connections.Clear();
            Parameters.Clear();
            return true;
        }
        public string GetWhere()
        {
            if (Conditions.Count > 0)
            {
                BuildWhere();
            }
            return WhereResult;
        }
        public Dictionary<string, string> GetParameters()
        {
            if (Parameters.Count > 0)
            {
                BuildWhere();
            }
            return Parameters;
        }

    }
}

struct Condition
{
    public string Column;
    public string Value;
    public string Relation;
    public Condition(string Column, string Value, string Relation)
    {
        this.Column = Column;
        this.Value = Value;
        this.Relation = Relation;
    }
}