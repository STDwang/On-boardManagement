using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 完成功能:
 * Main窗口,各个模块的入口按钮
 * 结束Main即彻底退出整个程序
 */


namespace code
{
    public partial class Main : Form
    {
        int mainFlag = 0;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register f = new Register();
            f.Show();
        }

        private void topup_Click(object sender, EventArgs e)
        {
            Topup f = new Topup();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search f = new Search();
            f.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainFlag == 0)
            {
                DialogResult result = MessageBox.Show("确定关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    mainFlag = 1;
                    e.Cancel = false;
                }
                else e.Cancel = true;
            }
            if (mainFlag == 1) Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Manage f = new Manage();
            f.Show();
        }
    }
}
