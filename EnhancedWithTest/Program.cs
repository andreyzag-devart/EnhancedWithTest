using Devart.Data.Oracle;
using System;

namespace EnhancedWithTest
{
  internal class Program
  {
    private const string ConnectionString = "User Id=sys;Password=Manager123;Server=DBFORACLE19;Connect Mode=SysDba;Unicode=True;Direct=True;Service Name=oracle19;";
    private const string Script = @"WITH
PROCEDURE with_procedure(p_id IN NUMBER) IS
BEGIN
DBMS_OUTPUT.put_line('p_id=' || p_id);
END;

FUNCTION with_function(p_id IN NUMBER) RETURN NUMBER IS
BEGIN
with_procedure(p_id);
RETURN p_id;
END;
SELECT with_function(student_number)
FROM students
fetch FIRST 500 ROWS ONLY
;";

    static void Main(string[] args)
    {
      ExecuteScript();
    }

    private static void ExecuteScript()
    {
      using (var oracleConnection = GetConnection())
      {
        try
        {
          oracleConnection.Open();
          OracleCommand command = new OracleCommand(Script);
          command.Connection = oracleConnection;
          command.ExecuteNonQuery();
        }
        catch (OracleException ex)
        {
          Console.WriteLine("Exception occurs: {0}", ex.Message);
        }
        finally
        {
          Console.ReadLine();
        }
      }
    }

    private static OracleConnection GetConnection() => new OracleConnection(ConnectionString);
  }
}
