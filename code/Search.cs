using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


/*
 * 
 * 完成功能:
 * 根据卡号查询余额
 * 根据姓名,班级,开始结束日期,状态 任一或多项筛选记录,将结果呈现在datagridview上
 * 完成top3专业上机时间查询
 */
namespace code
{
    public partial class Search : Form
    {
        SqlConnection searchCN;
        SqlDataAdapter DA_Condition;
        SqlDataAdapter DA_Top3;
        DataSet searchDS;
        public Search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String studentId = textBox6.Text;
            double remain = MyMessage.GetRemain(studentId);
            if(remain >0)
                MessageBox.Show("当前用户余额"+remain.ToString());
            else
                MessageBox.Show("未找到该用户!请输入正确卡号");
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            searchCN = new SqlConnection(MyMessage.link);
            DA_Condition = new SqlDataAdapter("select 上机记录.卡号,上机表.姓名 as 学生姓名,上机表.专业班级,管理员.姓名 as 管理员姓名,开始时刻,上机用时,状态 from (上机记录 inner join 上机表 on 上机表.卡号 = 上机记录.卡号)inner join 管理员 on 上机记录.管理员 = 管理员.编号", searchCN);
            searchDS = new DataSet();
            DA_Condition.Fill(searchDS, "条件查询");

            String studentId = textBox6.Text;
            String studentName = textBox1.Text;
            String className = textBox2.Text;
            DateTime start_time = DateTime.MinValue;
            DateTime end_time = DateTime.MaxValue;
            String status = textBox5.Text;
            bool idFlag = studentId==""?true:false;
            bool nameFlag = studentName == "" ? true : false;
            bool classFlag = className == "" ? true : false;
            bool statusFlag = status == "" ? true : false;

            if (textBox3.Text =="") start_time = DateTime.MinValue;
            else start_time = Convert.ToDateTime(textBox3.Text);

            if (textBox4.Text =="") end_time = DateTime.MaxValue;
              else   end_time = Convert.ToDateTime(textBox4.Text);

            for (int i = 0; i < searchDS.Tables[0].Rows.Count; i++)
            {
                if (status == "") status = searchDS.Tables[0].Rows[i]["状态"].ToString();

                DateTime startTime = Convert.ToDateTime(searchDS.Tables[0].Rows[i]["开始时刻"].ToString());
                DateTime endTime = startTime.AddSeconds(double.Parse(searchDS.Tables[0].Rows[i]["上机用时"].ToString()));
                TimeSpan s = endTime - startTime;

                if ((studentId == searchDS.Tables[0].Rows[i]["卡号"].ToString() || studentId == "")
                    &&(studentName == searchDS.Tables[0].Rows[i]["学生姓名"].ToString() || studentName == "")
                    && (className == searchDS.Tables[0].Rows[i]["专业班级"].ToString() || className == "")
                    && (status == searchDS.Tables[0].Rows[i]["状态"].ToString() || status == "")
                    && (DateTime.Compare(start_time, startTime) < 0 || textBox3.Text == "")
                    && (DateTime.Compare(end_time, endTime) > 0 || textBox3.Text == "")
                  )
                {
                    int index =  this.dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells["id"].Value = searchDS.Tables[0].Rows[i]["卡号"].ToString();
                    dataGridView1.Rows[index].Cells["st_name"].Value = searchDS.Tables[0].Rows[i]["学生姓名"].ToString();
                    dataGridView1.Rows[index].Cells["admin_name"].Value = searchDS.Tables[0].Rows[i]["管理员姓名"].ToString();
                    dataGridView1.Rows[index].Cells["start"].Value = searchDS.Tables[0].Rows[i]["开始时刻"].ToString();
                    dataGridView1.Rows[index].Cells["time"].Value = s.ToString();
                    dataGridView1.Rows[index].Cells["status"].Value = searchDS.Tables[0].Rows[i]["状态"].ToString();
                }  
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id", "卡号");
            dataGridView1.Columns.Add("st_name", "学生姓名");
            dataGridView1.Columns.Add("admin_name", "管理员姓名");
            dataGridView1.Columns.Add("start", "开始时刻");
            dataGridView1.Columns.Add("time", "上机用时");
            dataGridView1.Columns.Add("status", "状态");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchCN = new SqlConnection(MyMessage.link);
            DA_Top3 = new SqlDataAdapter("select  上机表.专业班级,sum(上机记录.上机用时) as 总时长 from 上机记录 inner join 上机表 on 上机表.卡号 = 上机记录.卡号 group by 上机表.专业班级", searchCN);
            searchDS = new DataSet();
            DA_Top3.Fill(searchDS, "Top3查询");

            double network = double.Parse(searchDS.Tables[0].Rows[2]["总时长"].ToString());
            double database = double.Parse(searchDS.Tables[0].Rows[1]["总时长"].ToString());
            double multimedia = double.Parse(searchDS.Tables[0].Rows[0]["总时长"].ToString());

            database = Math.Round((database / network), 2);
            multimedia = Math.Round((multimedia / network), 2);

            MessageBox.Show("网络:数据库:多媒体 = " 
                + (network / network).ToString()
                + " : " + database.ToString()
                + " : " + multimedia.ToString()
                );
        }


        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}

