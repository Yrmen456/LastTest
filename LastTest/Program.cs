using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LastTest.Data;
using LastTest.Forms;
namespace LastTest
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static Main main;
        [STAThread]
        static void Main()
        {
            SQL.Conect = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BookShop;Integrated Security=True";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyApp.Open(new Authorization());
            Application.Run();
        }
        private static int FormCount;

        public class MyApp : ApplicationContext
        {
            public static void Open(Form form)
            {
                FormCount++;
                form.Closed += OnClosed;
                form.Show();
                form.Focus();
            }

            private static void OnClosed(object sender, EventArgs e)
            {
                FormCount--;
                if (FormCount==0)
                {
                    Application.Exit();
                }
            }
        }
    }
}
