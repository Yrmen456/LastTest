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

namespace LastTest.Control
{
    public partial class TovarItemsControl : UserControl
    {
        public Tovar tovar;
        public TovarItemsControl(Tovar tovar)
        {
            InitializeComponent();
            this.tovar = tovar;
            ShowTovar();
            ShowBtn();
            Program.main.UpdateForm += () => { ShowBtn(); };
        }

        public void ShowTovar()
        {
            label1.Text = "Название: "+tovar.Title;
            label2.Text = "Категория: " + tovar.CategoryName;
            label3.Text = "Цена: " + Math.Round(tovar.Price,2);
        }
        public void ShowBtn()
        {
            switch (ThisUser.User.Role)
            {
                case ProgramRole.Admin:
                    button1.Visible = true;
                    break;
                case ProgramRole.Menedger:
                    button1.Visible = true;
                    break;
                default:
                    button1.Visible = false;
                    break;
            }
        }
    }
}
