using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LastTest.Data;

namespace LastTest.Control
{
    public partial class TovarControl : UserControl
    {
        public TovarControl()
        {
            InitializeComponent();
            Program.main.UpdateForm = () => { };
            SqlTovar();
        }
        public List<Tovar> tovars;
        public async void SqlTovar()
        {
            Output Output = new Output();
            await Task.Run(() =>
            {
                Output = SQL.Output($@"Select * from Tovar
                left join Category  on Category.CategoryID = Tovar.Category
                left join ProductParameters  on ProductParameters.ProductID = Tovar.ProductID
                left join ProductType  on ProductType.TypeID = ProductParameters.TypeID
                left join BookParameters  on BookParameters.BookID = Tovar.BookID
                left join Author  on Author.AuthorID = BookParameters.Author
                left join Genre  on Genre.GenreID = BookParameters.Genre ");
            });
            if (!Output.Result)
            {
                MessageBox.Show(Output.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Output.DataSet.Tables.Count <= 0)
            {
                MessageBox.Show("Что то пошло не так", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tovars = MyMetods.ConvertDataTable<Tovar>(Output.DataSet.Tables[0]);
            comboBox1.DataSource = tovars;
            comboBox1.DisplayMember = "Title";
            comboBox1.ValueMember = "ID";
            tovars = tovars.Where(x => x.ID != 2).Select(x => x).ToList();
            ShowTovar();
        }

        public async void ShowTovar()
        {
            panelControl.Visible = false;
            for (int i = 0; i < tovars.Count; i++)
            {
                UserControl control;
                switch (tovars[i].Category)
                {
                    case ProgramCategory.Book:
                        control = new BookItemsControl(tovars[i]);
                        break;
                    case ProgramCategory.Product:
                        control = new ProductItemsControl(tovars[i]);
                        break;
                    default:
                        control = new TovarItemsControl(tovars[i]);
                        break;
                }
                if (control !=null)
                {
                    control.Dock = DockStyle.Top;
                    InsertControl(control, panelControl);
                }

            }
            Task.Delay(10);
            panelControl.Visible = true;
        }
        public async void InsertControl(UserControl control, Panel panel)
        {
            panel.SuspendLayout();
            panel.Controls.Add(control);
            control.BringToFront();
            panel.ResumeLayout();
        }
    }
}
