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
using LastTest.Data;
using System.Reflection;

namespace LastTest.Forms
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        public Authorization(TypeForm type)
        {
            InitializeComponent();
            EnabledBtn(type);
        }
        bool Next = true;
        public Authorization(TypeForm type, bool Next)
        {
            InitializeComponent();
            this.Next = Next;
            EnabledBtn(type);
        }

        void EnabledBtn(TypeForm type)
        {
            switch (type)
            {
                case TypeForm.AutoRegGuest:
                    break;
                case TypeForm.AutoGuest:
                    buttonRegistration.Enabled = false;
                    break;
                case TypeForm.AutoReg:
                    button3.Enabled = false;
                    break;
                case TypeForm.RegGuest:
                    buttonAuthorization.Enabled = false;
                    break;
                case TypeForm.Guest:
                    buttonRegistration.Enabled = false;
                    buttonAuthorization.Enabled = false;
                    break;
                case TypeForm.Reg:
                    button3.Enabled = false;
                    buttonAuthorization.Enabled = false;
                    break;
                case TypeForm.Auto:
                    buttonRegistration.Enabled = false;
                    button3.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar)
            {
                textBox2.UseSystemPasswordChar = false;
                button4.Text = "☺";
            }
            else{
                textBox2.UseSystemPasswordChar = true;
                button4.Text = "☻";
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter{ ParameterName = "Login", SqlDbType = SqlDbType.NVarChar, Value=textBox1.Text},
                new SqlParameter{ ParameterName = "Password", SqlDbType = SqlDbType.NVarChar, Value=textBox2.Text},
            };
            Output Output = new Output();
            await Task.Run(() => {
                Output = SQL.Output($@"Select * from Users 
                Inner Join Role on Role.RoleID = Users.Role
                where Login = @Login and Password = @Password", sqlParameters);
            });

            if (!Output.Result)
            {
                MessageBox.Show(Output.Message, "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Output.DataSet.Tables.Count <=0)
            {
                MessageBox.Show("Что то пошло не так", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Output.DataSet.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("Логин или Пароль введен не верно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            ThisUser.User = MyMetods.GetItem<User>(Output.DataSet.Tables[0].Rows[0]);

            MessageBox.Show($"Добро пожаловать {ThisUser.User.Surname} {ThisUser.User.Name} {ThisUser.User.Patronymic} \nВы авторизировалтсь под Ролью({ThisUser.User.RoleName})","Информация",MessageBoxButtons.OK, MessageBoxIcon.Information) ;
            if (!Next)
            {
                this.Close();
                return;
            }
            Program.MyApp.Open(new Main());
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Вы авторизировалтсь под Ролью({ThisUser.User.RoleName})", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Program.MyApp.Open(new Main());
            this.Close();
        }
    }
    public enum TypeForm
    {
        AutoRegGuest,
        AutoGuest,
        AutoReg,
        RegGuest,
        Guest,
        Reg,
        Auto,
    }
}
