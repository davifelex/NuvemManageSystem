

using NuvemManageSystem.Objects;
using NuvemManageSystem.SupportFunctions;

class Program
{
    static void Main()
    {
        DatabaseManager database = new DatabaseManager();
        WhereObject where = new WhereObject();
        where.AddCondition("column1", "15", "=", "");
        where.AddCondition("column2", "10", "=", "AND");

        //string sql = database.BuildSqlWriter("update", "TestTable", ["column1", "colum2"], ["2", "5"], where);
        string sql = database.BuildSqlReader("TestTable", ["column1", "column2"], where);
        Console.WriteLine(sql);
    }
}