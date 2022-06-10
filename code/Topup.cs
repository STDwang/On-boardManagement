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
 *  功能:
 *  根据用户卡号充值,更新数据库
 *  表单校验
 */

namespace code
{
    public partial class Topup : Form
    {
        SqlConnection topUpCN;
        SqlDataAdapter topUpDA;
        DataSet topUpDS;
        SqlCommandBuilder topUpCB;
        public Topup()
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            topUpCN = new SqlConnection(MyMessage.link);
            topUpDA = new SqlDataAdapter("select * from 上机表", topUpCN);
            topUpDS = new DataSet();
            topUpDA.Fill(topUpDS, "上机表");

            topUpCB = new SqlCommandBuilder(topUpDA);
            try
            {
                String student_id = textBox1.Text;
                double topup = double.Parse(textBox2.Text);
                int Rowscount = topUpDS.Tables[0].Rows.Count;

                for (int i = 0; i < Rowscount; i++)
                {
                    double remains = double.Parse(topUpDS.Tables[0].Rows[i]["余额"].ToString());
                    if (student_id == topUpDS.Tables[0].Rows[i]["卡号"].ToString())
                    {
                        remains += topup;
                        topUpDS.Tables[0].Rows[i]["余额"] = remains.ToString();
                        topUpDA.Update(topUpDS, "上机表");
                        for (int j = 0; j < Rowscount; j++)
                        {
                            if (student_id == topUpDS.Tables[0].Rows[i]["卡号"].ToString()
                                && remains.ToString() == topUpDS.Tables[0].Rows[i]["余额"].ToString())
                            {
                                MessageBox.Show("充值成功！当前账户余额：" + remains.ToString());
                                break;
                            }
                        }
                        break;
                    }
                }
                
            }
            catch (System.FormatException e1)
            {
                MessageBox.Show("请输入正确的卡号和充值金额");
                System.Diagnostics.Debug.WriteLine(e1);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            topUpCN = new SqlConnection(MyMessage.link);
            topUpDA = new SqlDataAdapter("select * from 上机表", topUpCN);
            topUpDS = new DataSet();
            topUpDA.Fill(topUpDS, "上机表");

            topUpCB = new SqlCommandBuilder(topUpDA);

            String student_id = textBox1.Text;
            int Rowscount = topUpDS.Tables[0].Rows.Count;

            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < Rowscount; i++)
                {
                    if (student_id == topUpDS.Tables[0].Rows[i]["卡号"].ToString())
                    {
                        topUpDS.Tables[0].Rows[i]["状态"] = "正常";
                        topUpDA.Update(topUpDS, "上机表");
                        for (int j = 0; j < Rowscount; j++)
                        {
                            if (student_id == topUpDS.Tables[0].Rows[i]["卡号"].ToString()
                                && "正常" == topUpDS.Tables[0].Rows[i]["状态"].ToString())
                            {
                                MessageBox.Show("修改成功！当前账户状态：正常");
                                break;
                            }
                        }
                        break;
                    }
                }

            }

            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < Rowscount; i++)
                {
                    if (student_id == topUpDS.Tables[0].Rows[i]["卡号"].ToString())
                    {
                        topUpDS.Tables[0].Rows[i]["状态"] = "挂失";
                        topUpDA.Update(topUpDS, "上机表");
                        for (int j = 0; j < Rowscount; j++)
                        {
                            if (student_id == topUpDS.Tables[0].Rows[i]["卡号"].ToString()
                                && "挂失" == topUpDS.Tables[0].Rows[i]["状态"].ToString())
                            {
                                MessageBox.Show("修改成功！当前账户状态：挂失");
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
