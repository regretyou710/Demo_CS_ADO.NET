using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_ADO.NET
{
    /*
     * 使用SqlParameters方式傳遞Sql語句
     */
    class Demo02
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
                    string sql = "select * from dbo.Employees where TitleOfCourtesy=@titleOfCourtesy and Country=@country";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    //方式一:
                    //cmd.Parameters.Add(new SqlParameter("@titleOfCourtesy", "Mr."));
                    //cmd.Parameters.Add(new SqlParameter("@country", "UK"));

                    //方式二:
                    //SqlParameter titleOfCourtesy = cmd.Parameters.Add("@titleOfCourtesy", SqlDbType.NVarChar, 25);
                    //titleOfCourtesy.Value = "Mr.";
                    //SqlParameter country = cmd.Parameters.Add("@country", SqlDbType.NVarChar, 15);
                    //country.Value = "UK";

                    //方式三:**
                    cmd.Parameters.AddWithValue("@titleOfCourtesy", "Mr.");
                    cmd.Parameters.AddWithValue("@country", "UK");

                    //方式四:
                    //SqlParameter titleOfCourtesy = new SqlParameter("@titleOfCourtesy", "Mr."); 
                    //SqlParameter country = new SqlParameter("@country", "UK");                    
                    //List<SqlParameter> lists = new List<SqlParameter>();
                    //lists.Add(titleOfCourtesy);
                    //lists.Add(country);                   
                    // cmd.Parameters.AddRange(lists.ToArray<SqlParameter>());

                    //方式五:
                    //SqlParameter titleOfCourtesy = new SqlParameter("@titleOfCourtesy", "Mr.");
                    //SqlParameter country = new SqlParameter("@country", "UK");
                    //SqlParameter[] parameters = { titleOfCourtesy, country };
                    //cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            str += string.Format("TitleOfCourtesy= {0}, Country= {1}{2}", dr[0], dr[1], "\r\n");
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
