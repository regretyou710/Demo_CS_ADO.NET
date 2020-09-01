using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_ADO.NET
{
    /*
     DataTable的使用
     */
    class Demo03
    {
        public string Show1()
        {
            //單獨創建DataTable
            DataTable dt1 = new DataTable("EmployeeInfo");

            //創建欄位方式一
            DataColumn dc = new DataColumn();
            dc.ColumnName = "EmpID";
            dc.DataType = typeof(int);
            //欄位加入表中
            dt1.Columns.Add(dc);

            //創建欄位方式二
            dt1.Columns.Add("Ename", typeof(string));
            dt1.Columns.Add("Age", typeof(int));

            //設置約束
            dt1.PrimaryKey = new DataColumn[] { dt1.Columns["EmpID"] };
            dt1.Constraints.Add(new UniqueConstraint(dt1.Columns["Ename"])); //dt1.Columns[1]

            //DataRowState
            //設置橫列數據(狀態為Detached)
            DataRow dr = dt1.NewRow();
            dr["Empid"] = 1;
            dr[1] = "Eason";
            dr[2] = 15;


            //橫列添加到表中(狀態為Added)
            dt1.Rows.Add(dr);


            dr[2] = 22;
            //提交前回滾(狀態為Detached)
            //dr.RejectChanges();
            //Console.WriteLine(dr.RowState);


            //提交記錄更改(狀態為Modified)
            dr.AcceptChanges();
            Console.WriteLine(dr.RowState);


            dr = dt1.NewRow();
            dr["Empid"] = 2;
            dr[1] = "Judy";
            dr[2] = 16;
            dt1.Rows.Add(dr);
            //未呼叫AcceptChanges()前(狀態為Unchanged)
            //dt1.Rows.Remove(dr);//效果相同於dr.Delete();


            //清除表數據
            // dt1.Clear();
            //複製表含數據及結構
            DataTable dt2 = dt1.Copy();
            //複製表只含結構
            //DataTable dt3 = dt1.Clone();


            DataRow dr2 = dt2.NewRow();
            dr2[0] = 5;
            dr2[1] = "Tom";
            dr2[2] = 46;
            dt2.Rows.Add(dr2);
            //合併表後dt1中與dt2相同的會覆蓋
            // dt1.Merge(dt2);


            //取得所有的橫列
            DataRow[] rows1 = dt1.Select();
            //取得條件篩選橫列
            DataRow[] rows2 = dt1.Select("Age<18");

            string str = "";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    str += dt1.Columns[j].ToString() + "=" + dt1.Rows[i][j].ToString() + "\t";
                    Console.Write(dt1.Columns[j].ToString() + "=" + dt1.Rows[i][j].ToString() + "\t");
                }
                Console.WriteLine();
                str += "\r\n";
            }

            return str;
        }
    }
}
