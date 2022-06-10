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
 * 功能:
 * 根据用户输入信息(学生姓名,专业班级,首充金额),注册用户,写入数据表: 收费管理.上机表
 * 限制用户输入,弹窗交互
 */
namespace code
{

    public partial class Register : Form
    {
        SqlConnection registerCN;
        SqlDataAdapter registerDA;
        DataSet registerDS;
        SqlCommandBuilder registerCB;


        public Register()
        {
            InitializeComponent();
        }

        private void st_register_Click(object sender, EventArgs e)
        {
            registerCN = new SqlConnection(MyMessage.link);
            registerDA = new SqlDataAdapter("select * from 上机表", registerCN);
            registerDS = new DataSet();
            registerDA.Fill(registerDS, "上机表");

            int Rowcount = registerDS.Tables[0].Rows.Count - 1;
            try
            {
                String student_name = st_name.Text;
                String student_class = st_class.Text;
                Double student_pay = int.Parse(st_pay.Text);
                String status = "正常";
                String student_id = (int.Parse(registerDS.Tables[0].Rows[Rowcount]["卡号"].ToString()) + 1).ToString();

                registerCB = new SqlCommandBuilder(registerDA);
                DataRow row = NewMethod(student_name, student_class, student_pay, status, student_id);
                registerDS.Tables[0].Rows.Add(row);
                registerDA.Update(registerDS, "上机表");

                MessageBox.Show(
                    "用户卡号：" + registerDS.Tables[0].Rows[registerDS.Tables[0].Rows.Count - 1]["卡号"].ToString()
                    + "\n姓名：" + registerDS.Tables[0].Rows[registerDS.Tables[0].Rows.Count - 1]["姓名"].ToString()
                    + "\n班级：" + registerDS.Tables[0].Rows[registerDS.Tables[0].Rows.Count - 1]["专业班级"].ToString()
                    + "\n当前余额：" + registerDS.Tables[0].Rows[registerDS.Tables[0].Rows.Count - 1]["余额"].ToString()
                   + "\n当前状态：" + registerDS.Tables[0].Rows[registerDS.Tables[0].Rows.Count - 1]["状态"].ToString()
                    + "\n已成功注册!"
                    );
                registerCN.Close();
            }
            catch (System.FormatException e1)
            {
                MessageBox.Show("请输入正确的信息！");
                System.Diagnostics.Debug.WriteLine(e1);
            }

           
        }

        private DataRow NewMethod(string student_name, string student_class, double student_pay, string status, string student_id)
        {
            DataRow row = registerDS.Tables[0].NewRow();
            row["卡号"] = student_id;
            row["姓名"] = student_name;
            row["专业班级"] = student_class;
            row["余额"] = student_pay;
            row["状态"] = status;
            return row;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        //表单校验
        private void st_pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') )
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
    }
}
