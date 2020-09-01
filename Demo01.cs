using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_CS_ADO.NET
{
    class Demo01
    {
        public string Show1()
        {
            try
            {
                //從配置檔讀取字串
                string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                //格式化字串
                string connStrFormat = string.Format(connStr, "localhost", "1433", "Northwind", "sa", "sa");

                using (SqlConnection conn = new SqlConnection(connStrFormat))
                {
                    string str = "";
                    string sql = "select * from dbo.Employees";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            str += string.Format("EmployeeID= {0}, FirstName= {1}, Hiredate{2}{3}", dr["EmployeeID"], dr[1], dr[2], "\r\n");
                        }
                    }                   
                    return str;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
