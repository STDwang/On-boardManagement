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
 * 登录模块
 * 
 * 完成功能:
 * 用户登录,用户名仅能从列表框中读取(账户来自数据库)
 *  
 */

namespace code
{
    public partial class Login : Form
    {
        SqlConnection loginCN; 
        SqlDataAdapter loginDA;
        DataSet loginDS; 
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loginCN = new SqlConnection(MyMessage.link);
            loginDA = new SqlDataAdapter("select * from 管理员", loginCN);
            loginDS = new DataSet();
            loginDA.Fill(loginDS, "admin");
            textBox2.PasswordChar = '*';

            for (int i = 0; i < loginDS.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(loginDS.Tables[0].Rows[i]["编号"].ToString());
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
           textBox1.Text = listBox1.Text;
            MyMessage.adminID = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool successFlag = false; 
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择右侧列表栏用户名");
                return;
            }
            for (int i = 0; i < loginDS.Tables[0].Rows.Count; i++)
            {
                if (textBox2.Text == loginDS.Tables[0].Rows[i]["密码"].ToString())
                {
                    MessageBox.Show("用户名："+ loginDS.Tables[0].Rows[i]["姓名"].ToString()+"，欢迎登录！");
                    successFlag = true;
                    Main f = new Main();
                    f.Show();
                    this.Hide();

                    loginCN.Dispose();
                    loginDA.Dispose();
                    loginDS.Dispose();
                    break;
                }
            }
            if(successFlag == false) MessageBox.Show("请输入正确的密码！");
            return;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }
    }
}
