using LastTest.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LastTest.Control;

namespace LastTest.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Program.main = this;
            Show();
            Show2();
            panel5.AutoScroll = false;
            panel5.VerticalScroll.Maximum = 0;
            panel5.AutoScroll = true;
        }
        public delegate void Update();
        public Update UpdateForm = () => { };
        void Show2()
        {
            panel11.Controls.Clear();
            TovarControl TovarControl = new TovarControl();
            TovarControl.Dock = DockStyle.Fill;
            panel11.Controls.Add(TovarControl);
        }
        void Show()
        {
            label1.Text = $"{ThisUser.User.Surname} {ThisUser.User.Name} {ThisUser.User.Patronymic} \nРоль({ThisUser.User.RoleName})";
            foreach (var Panel in panel5.Controls.OfType<Panel>())
            {
                //Panel.Visible = false;
            }
            switch (ThisUser.User.Role)
            {
                case ProgramRole.Guest:
                    panel12.Visible = true;
                    break;
                case ProgramRole.Admin:
                    break;
                case ProgramRole.Menedger:
                    break;
                case ProgramRole.Client:
                    break;
                default:
                    
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formClosed = false;
            switch (ThisUser.User.Role)
            {
                case ProgramRole.Guest:
                    if (1 == 2)
                    {
                        return;
                    }
                    var dr = MessageBox.Show("Хотите закончить сеанс?\nЕсли завершите сеанс не авторизировшись данные будут утерены", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    formClosed = false;
                    break;
            }
            ThisUser.User = new User();
            Program.MyApp.Open(new Authorization());
            this.Close();
            formClosed = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ThisUser.User.Role == ProgramRole.Guest)
            {
                ThisUser.User = new User();
                Authorization authorization = new Authorization(TypeForm.AutoReg, false);
                authorization.ShowDialog();
                Show();
                UpdateForm();
            }
        }
        bool formClosed = true;
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (formClosed)
            {
                switch (ThisUser.User.Role)
                {
                    case ProgramRole.Guest:
                        if (1 == 2)
                        {
                            return;
                        }
                        var dr = MessageBox.Show("Хотите закончить сеанс?\nЕсли завершите сеанс не авторизировшись данные будут утерены", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                        break;
                }
            }
        }

        private void panelBtn1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelBtn1_Load(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
