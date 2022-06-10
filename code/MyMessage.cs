using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;


/*
 *  功能:
 *  为其他模块提供类接口,代码复用
 *  方便用户修改数据库配置
 */
namespace code
{
    class MyMessage
    {
        static SqlConnection helpCN;
        static SqlDataAdapter helpDA;
        static DataSet helpDS;
        public static string adminID;

        public static string  link = "Server=STDWANG;Database=收费管理;User=sa;Password=123456";

        //获取余额api  参数1:学生卡号 返回值:当前余额
        public static double GetRemain(String id)
        {
            helpCN = new SqlConnection(link);
            helpDA = new SqlDataAdapter("select * from 上机表", helpCN);
            helpDS = new DataSet();
            helpDA.Fill(helpDS, "student");
            
            double reMoney=0;
            for (int i = 0; i < helpDS.Tables[0].Rows.Count; i++)
            {
                if (id == helpDS.Tables[0].Rows[i]["卡号"].ToString())
                {
                    reMoney = Convert.ToDouble(helpDS.Tables[0].Rows[i]["余额"].ToString());
                    break;
                }
            }
            return reMoney;
        }
    }
}
