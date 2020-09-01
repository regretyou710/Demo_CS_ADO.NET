using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CS_ADO.NET
{
    /*
     DataSet的使用
     */
    class Demo04
    {
        public void Show1()
        {
            //創建DataSet加上名稱
            DataSet ds1 = new DataSet("ds1");

            //將table加入DataSet
            DataTable dt1 = new DataTable("Employee");
            ds1.Tables.Add(dt1);

            //取得DataSet中的表
            DataTable employee = ds1.Tables["Employee"];

            //取得關聯集合
            //ds1.Relations.Add();

            //常用方法
            //ds1.AcceptChanges();
            //ds1.RejectChanges();
            //ds1.Clone();
            //ds1.Copy();
            //ds1.Clear();
            //ds1.Merge();
            //ds1.Reset();
            //ds1.Load();
        }
    }
}
