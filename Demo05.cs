using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_ADO.NET
{
    /*
     DataSet中DataTable之間的關係
     */
    class Demo05
    {
        public string Show1()
        {
            DataSet ds = new DataSet();
            DataTable emp = new DataTable("Emp");
            DataTable dept = new DataTable("Dept");
            ds.Tables.Add(emp);
            ds.Tables.Add(dept);

            emp.Columns.Add("Empno", typeof(int));
            emp.Columns.Add("Ename", typeof(string));
            emp.Columns.Add("Age", typeof(int));
            emp.Columns.Add("Deptno", typeof(int));
            emp.PrimaryKey = new DataColumn[] { emp.Columns["Empno"] };

            dept.Columns.Add("Deptno", typeof(int));
            dept.Columns.Add("Dname", typeof(string));
            dept.PrimaryKey = new DataColumn[] { dept.Columns["Deptno"] };
            dept.Constraints.Add(new UniqueConstraint("uk_danme", dept.Columns["Dname"]));

            emp.Constraints.Add(new ForeignKeyConstraint("fk_deptno", dept.Columns["Deptno"], emp.Columns["Deptno"]));

            //使用DataRelation建立兩表間關聯
            //默認情況下，系統自動為父表直行建立唯一約束，子表直行建立外鍵約束
            DataRelation relation = new DataRelation("relation", dept.Columns[0], emp.Columns[3]);
            ds.Relations.Add(relation);

            //向DataTable增加數據
            InitDataTable(emp, dept);


            string str = "";
            //從父表迭代出子表關聯(一對多)
            foreach (DataRow row in dept.Rows)
            {
                DataRow[] rows = row.GetChildRows(relation);
                foreach (var empRow in rows)
                {
                   
                    str += "部門編號:" + row["Deptno"].ToString() + " ,員工編號:" + empRow["Empno"] + " ,姓名:" + empRow["Ename"] + "\n";

                    Console.WriteLine($"部門編號:{row[0].ToString()}, 員工編號:{empRow[0].ToString()}, 姓名:{empRow[1].ToString()}");
                }                
            }

            Console.WriteLine("------------------------");

            //從子表迭代出父表關聯(多對一)
            foreach (DataRow row in emp.Rows)
            {
                DataRow[] rows = row.GetParentRows(relation);
                foreach (var deptRow in rows)
                {
                    str += "員工編號:" + row["Empno"].ToString() + " ,部門編號:" + deptRow["Deptno"] + " ,名稱:" + deptRow["Dname"];
                    Console.WriteLine($"員工編號:{row[0].ToString()}, 部門編號:{deptRow[0].ToString()}, 名稱:{deptRow[1].ToString()}");
                }
                str += "\r\n";
            }
            return str;
        }


        void InitDataTable(DataTable dt1, DataTable dt2)
        {
            //先增加父表再增加子表才不會拋空異常
            DataRow deptRow = dt2.NewRow();
            deptRow[0] = 1;
            deptRow[1] = "SalesMan";
            dt2.Rows.Add(deptRow);

            deptRow = dt2.NewRow();
            deptRow[0] = 2;
            deptRow[1] = "Manager";
            dt2.Rows.Add(deptRow);

            deptRow = dt2.NewRow();
            deptRow[0] = 3;
            deptRow[1] = "Clerk";
            dt2.Rows.Add(deptRow);

            DataRow empRow = dt1.NewRow();
            empRow[0] = 1;
            empRow[1] = "Eason";
            empRow[2] = 22;
            empRow[3] = 1;
            dt1.Rows.Add(empRow);

            empRow = dt1.NewRow();
            empRow[0] = 2;
            empRow[1] = "Judy";
            empRow[2] = 21;
            empRow[3] = 3;
            dt1.Rows.Add(empRow);

            empRow = dt1.NewRow();
            empRow[0] = 3;
            empRow[1] = "Tom";
            empRow[2] = 19;
            empRow[3] = 1;
            dt1.Rows.Add(empRow);

            empRow = dt1.NewRow();
            empRow[0] = 4;
            empRow[1] = "Mary";
            empRow[2] = 26;
            empRow[3] = 2;
            dt1.Rows.Add(empRow);

            empRow = dt1.NewRow();
            empRow[0] = 5;
            empRow[1] = "Rose";
            empRow[2] = 17;
            empRow[3] = 3;
            dt1.Rows.Add(empRow);

            empRow = dt1.NewRow();
            empRow[0] = 6;
            empRow[1] = "Ken";
            empRow[2] = 33;
            empRow[3] = 3;
            dt1.Rows.Add(empRow);
        }
    }
}
