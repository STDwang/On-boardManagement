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
 * 管理模块
 * 
 * 完成功能:
 *  1. 实时监控学生上机信息,状态
 *  2. 上机下机管理
 *  3. 余额不足报警
 *  4. 定时自动下机
 *
 */
namespace code
{
    public partial class Manage : Form
    {

        SqlConnection freshCN;
        SqlDataAdapter freshDA;
        DataSet freshDS;
        SqlCommandBuilder freshCB;

        SqlConnection updateCN;
        SqlDataAdapter updateDA;
        DataSet updateDS;
        SqlCommandBuilder updateCB;

        SqlConnection recordCN;
        SqlDataAdapter recordDA;
        SqlDataAdapter stDA;
        SqlDataAdapter payDA;
        DataSet DS;
        SqlCommandBuilder stCB;


        String stId;
        double reMoney;
        double alreadypay;
        int index;
        bool isOK = false;
        bool exit = false;


        public Manage()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            recordCN = new SqlConnection(MyMessage.link);
            recordDA = new SqlDataAdapter("select 上机记录.卡号,上机表.姓名 as 学生姓名,上机表.专业班级,开始时刻,上机用时,正上机,余额 from (上机记录 inner join 上机表 on 上机表.卡号 = 上机记录.卡号)", recordCN);
            DS = new DataSet();
            stDA = new SqlDataAdapter("select * from 上机表", recordCN);
            DS = new DataSet();
            payDA = new SqlDataAdapter("select * from 小时收费", recordCN);
            DS = new DataSet();
            recordDA.Fill(DS, "上机查询");
            stDA.Fill(DS, "上机表");
            payDA.Fill(DS, "小时收费");

            stCB = new SqlCommandBuilder(stDA);
            stId = textBox1.Text;
            isready();

            if (isOK)
            {
                updateCN = new SqlConnection(MyMessage.link);
                updateDA = new SqlDataAdapter("select * from 上机记录", updateCN);
                updateDS = new DataSet();
                updateDA.Fill(updateDS, "update");

                updateCB = new SqlCommandBuilder(updateDA);

                updateDS.Tables[0].Rows.Add();
                index = updateDS.Tables[0].Rows.Count - 1;

                updateDS.Tables[0].Rows[index]["卡号"] = stId;
                updateDS.Tables[0].Rows[index]["正上机"] = "True";
                updateDS.Tables[0].Rows[index]["管理员"] = MyMessage.adminID;
                updateDS.Tables[0].Rows[index]["上机用时"] = 0;
                updateDS.Tables[0].Rows[index]["开始时刻"] = DateTime.Now;
                updateDA.Update(updateDS, "update");

                timer1.Enabled = true;
            }
            else
            {
                MessageBox.Show("帐号异常,请调整后重试!");
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            freshCN = new SqlConnection(MyMessage.link);
            freshDA = new SqlDataAdapter("select 上机记录.卡号,上机表.姓名 as 学生姓名,上机表.专业班级,开始时刻,上机用时,正上机,余额 from (上机记录 inner join 上机表 on 上机表.卡号 = 上机记录.卡号)", freshCN);
            freshDS = new DataSet();
            freshDA.Fill(freshDS, "刷新");
            freshCB = new SqlCommandBuilder(freshDA);
            freshDA.Update(freshDS, "刷新");
            for (int i = 0; i < freshDS.Tables[0].Rows.Count; i++)
            {
                if (freshDS.Tables[0].Rows[i]["正上机"].ToString() == "True")
                {
                    int index = this.dataGridView1.Rows.Add();

                    dataGridView1.Rows[index].Cells["id"].Value = freshDS.Tables[0].Rows[i]["卡号"].ToString();
                    dataGridView1.Rows[index].Cells["st_name"].Value = freshDS.Tables[0].Rows[i]["学生姓名"].ToString();
                    dataGridView1.Rows[index].Cells["class"].Value = freshDS.Tables[0].Rows[i]["专业班级"].ToString();
                    dataGridView1.Rows[index].Cells["start"].Value = freshDS.Tables[0].Rows[i]["开始时刻"].ToString();
                    dataGridView1.Rows[index].Cells["time"].Value = freshDS.Tables[0].Rows[i]["上机用时"].ToString();
                    dataGridView1.Rows[index].Cells["now"].Value = freshDS.Tables[0].Rows[i]["正上机"].ToString();
                    dataGridView1.Rows[index].Cells["remain"].Value = freshDS.Tables[0].Rows[i]["余额"].ToString();

                    if (Convert.ToDouble(dataGridView1.Rows[index].Cells["remain"].Value.ToString()) < 1.0)
                        dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;

                    DateTime exit_time = Convert.ToDateTime("22:30");
                    if (DateTime.Now > exit_time)
                    {
                        textBox1.Text = dataGridView1.Rows[0].Cells["id"].Value.ToString();
                        exit = true;
                        button2.PerformClick();
                    }
                }
            }
            if (isOK)
            {
                for (int i = 0; i < updateDS.Tables[0].Rows.Count; i++)
                {
                    if (updateDS.Tables[0].Rows[i]["正上机"].ToString() == "True")
                    {
                        String curr_id = updateDS.Tables[0].Rows[i]["卡号"].ToString();

                        for (int j = 0; j < DS.Tables[1].Rows.Count; j++)
                        {
                            double remain = Convert.ToDouble(DS.Tables[1].Rows[j]["余额"].ToString());
                            remain -= price() * (timer1.Interval / 1000);
                            DS.Tables[1].Rows[j]["余额"] = remain;
                            stDA.Update(DS, "上机表");
                        }

                        int times = Convert.ToInt32(updateDS.Tables[0].Rows[i]["上机用时"].ToString());
                        times += timer1.Interval / 1000;
                        updateDS.Tables[0].Rows[i]["上机用时"] = times;
                        updateDA.Update(updateDS, "update");
                    }
                }


            }
            else if (!isOK)
            {
                stId = textBox1.Text;
                for (int i = 0; i < updateDS.Tables[0].Rows.Count; i++)
                {
                    if (updateDS.Tables[0].Rows[i]["卡号"].ToString() == stId)
                    {
                        updateDS.Tables[0].Rows[i]["正上机"] = "False";
                    }
                }

                updateDA.Update(updateDS, "update");

                stId = "";
                isOK = true;
            }
        }
        private void Manage_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id", "卡号");
            dataGridView1.Columns.Add("st_name", "学生姓名");
            dataGridView1.Columns.Add("class", "专业班级");
            dataGridView1.Columns.Add("start", "开始时刻");
            dataGridView1.Columns.Add("time", "上机用时");
            dataGridView1.Columns.Add("now", "正上机");
            dataGridView1.Columns.Add("remain", "余额");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (exit == true)
                {
                    stId = textBox1.Text;
                    isOK = false;
                }
                else
                {
                    int times = Convert.ToInt32(updateDS.Tables[0].Rows[index]["上机用时"].ToString());
                    int hour = Convert.ToInt32(times / 3600);
                    int minitue = Convert.ToInt32((times - hour) / 60);
                    int sec = Convert.ToInt32(times - hour - minitue);

                    DialogResult result = MessageBox.Show(
                                     "用户卡号:" + textBox1.Text +
                                     "\t开始时刻" + updateDS.Tables[0].Rows[index]["开始时刻"].ToString() +
                                     "\t上机时间:" + hour.ToString() + ":" + minitue.ToString() + ":" + sec.ToString() +
                                     "\t卡内余额:" + MyMessage.GetRemain(textBox1.Text)
                    , "确认下机", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (DialogResult.Yes == result)
                    {
                        stId = textBox1.Text;
                        isOK = false;
                    }
                    else
                        return;
                }
            }
            catch
            {
                MessageBox.Show("帐号异常,请调整后重试!");
                return;
            }
        }

        public bool isready()
        {
            for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
            {
                if (stId == DS.Tables[1].Rows[i]["卡号"].ToString()
                    && DS.Tables[1].Rows[i]["状态"].ToString() != "挂失"
                    && double.Parse(DS.Tables[1].Rows[i]["余额"].ToString()) > 1
                    )
                {
                    isOK = true;
                    break;
                }
            }

            for (int j = 0; j < DS.Tables[0].Rows.Count; j++)
            {
                if (DS.Tables[0].Rows[j]["卡号"].ToString() == stId &&
                    DS.Tables[0].Rows[j]["正上机"].ToString() == "True")
                {
                    MessageBox.Show("同一卡号禁止同时上机!");
                    isOK = false;
                    break;
                }
            }
            return isOK;
        }

        public double price()
        {
            double needpay = 0;
            DateTime[] period_s = new DateTime[DS.Tables[2].Rows.Count];
            DateTime[] period_e = new DateTime[DS.Tables[2].Rows.Count];
            double[] period_pay = new double[DS.Tables[2].Rows.Count];

            for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
            {
                period_s[i] = Convert.ToDateTime(DS.Tables[2].Rows[i]["开始"].ToString());
                period_e[i] = Convert.ToDateTime(DS.Tables[2].Rows[i]["结束"].ToString());
                period_pay[i] = Convert.ToDouble(DS.Tables[2].Rows[i]["小时收费"].ToString());
            }
            for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
            {
                if (DateTime.Now >= period_s[i] && period_e[i] >= DateTime.Now)
                {
                    needpay = period_pay[i];
                    break;
                }
            }
            return needpay / 3600;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
        }
    }


}


