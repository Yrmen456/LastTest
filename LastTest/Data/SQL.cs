using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Data
{
    public class SQL
    {

        public static string Conect;
        
        public static Output Output(string query)
        {
            Output Output = new Output();
            DataSet DataSet = new DataSet();
            try
            {
                SqlConnection sql = new SqlConnection(SQL.Conect);
                sql.Open();
                sql.Close();
            }
            catch (Exception ex)
            {
                Output.Result = false;
                Output.Error = ex.ToString();
                Output.Message = "Что то пошло не так (проверьте подключение)";
            }
            try
            {
                SqlDataAdapter a = new SqlDataAdapter(query, SQL.Conect);
                a.Fill(DataSet);
                Output.Result = true;
                Output.DataSet = DataSet;
            }
            catch (Exception ex)
            {
                Output.Result = false;
                Output.Error = ex.ToString() ;
                Output.Message = "Что то пошло не так (проверьте запрос)";
            }
            return Output;
        }

        public static Output Output(string query, List<SqlParameter> ListSqlParamete = null)
        {
            Output Output = new Output();
            DataSet DataSet = new DataSet();
            try
            {
                SqlConnection sql = new SqlConnection(SQL.Conect);
                sql.Open();
                sql.Close();
            }
            catch (Exception ex)
            {
                Output.Result = false;
                Output.Error = ex.ToString();
                Output.Message = "Что то пошло не так (проверьте подключение)";
            }
            try
            {
                SqlDataAdapter a = new SqlDataAdapter(query, SQL.Conect);
                a.SelectCommand.Parameters.AddRange(ListSqlParamete.ToArray());
                a.Fill(DataSet);
                Output.Result = true;
                Output.DataSet = DataSet;
            }
            catch (Exception ex)
            {
                Output.Result = false;
                Output.Error = ex.ToString();
                Output.Message = "Что то пошло не так (проверьте запрос)";
            }
            return Output;
        }
    }

    public class Output
    {
        public DataSet DataSet { get; set; }
        public bool Result { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}