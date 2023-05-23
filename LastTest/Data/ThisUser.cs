using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LastTest.Data
{
    public class ThisUser 
    {
        public static User User = new User();
    }
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class User : Role
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public ProgramRole _Role { get; set; } = 0;
        public ProgramRole Role
        {
            get => _Role;
            set
            {
                if (value == ProgramRole.Guest)
                {
                    RoleName = "Гость";
                }
                if (value != _Role)
                {
                    if (!Enum.IsDefined(typeof(ProgramRole), value))
                    {
                        value = ProgramRole.Client;
                        //MessageBox.Show("Такая Роль не предусмотрена сисемой", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    _Role = value;

                }
            }
        }

        public User()
        {
            Role = ProgramRole.Guest;
        }
    }
    public enum ProgramRole
    {
        Guest = 0,
        Admin = 1,
        Menedger =2,
        Client = 3,
    }
    public enum ProgramCategory
    {
        Defolt = 0,
        Book = 1,
        Product = 2,
    }
    public class Tovar : Category 
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public ProgramCategory Category { get; set; }
        public int ProductID { get; set; }
        public int BookID { get; set; }
    }


    public class Category : BookParameters
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class BookParameters : Author
    {
        public int BookID { get; set; }
        public int Author { get; set; }
        public int Genre { get; set; }
    }
    public class Author : Genre
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
    }

    public class Genre : ProductParameters
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
    }


    public class ProductParameters : Type
    {
        public int ProductID { get; set; }
        public string TypeID { get; set; }
    }

    public class Type
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }
}
