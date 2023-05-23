using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LastTest.Data
{
    public class ValidateTextBox : TextBox
    {
        public bool RegexValidate { get; set; } = false;
        public bool Check { get; set; } = false;
        public bool Space { get; set; } = true;
        public ErrorProvider ErrorProvider { get; set; } = new ErrorProvider();
        public string Regex { get; set; }
        public string Message { get; set; } = "Не соотвествует типу.";

        public delegate void MyValidate();
        public MyValidate CustomValidate = () => { };
        public CustomError[] CustomErrors { get; set; }


        public ValidateTextBox()
        {
            ErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            this.Validating += ValidateTextBox_Validating;
            this.TextChanged += ValidateTextBox_TextChanged;
        }

        private void ValidateTextBox_Validating(object sender, EventArgs e)
        {
            if (!Space)
            {
                this.Text = System.Text.RegularExpressions.Regex.Replace(this.Text, @"\s+", " ");
                if (this.Text.Length <= 0)
                {
                    return;
                }
                if (this.Text[0] == ' ')
                {
                    this.Text = this.Text.Remove(0, 1);
                }
                if (this.Text.Length >= 1 && this.Text[this.Text.Length - 1] == ' ')
                {
                    this.Text = this.Text.Remove(this.Text.Length - 1);
                }
            }
        }

        private void ValidateTextBox_TextChanged(object sender, EventArgs e)
        {
            ThisValidate();
        }

        public void ThisValidate()
        {
            if (RegexValidate)
            {
                string text = this.Text;
                Regex reg = new Regex(this.Regex);
                this.Check = reg.IsMatch(text);
                if (ErrorProvider !=null)
                {
                    if (!this.Check)
                    {
                        string Error = Message;
                        for (int i = 0; i < CustomErrors.Length; i++)
                        {
                            if (! new Regex(CustomErrors[i].Regex).IsMatch(text))
                            {
                                Error += "\n" + CustomErrors[i].Message;
                            }
                        }
                        ErrorProvider.SetError(this, Error);
                    }
                    else
                    {
                        ErrorProvider.Clear();
                    }
                }
            }
            CustomValidate();
        }
    }

    public class CustomError
    {
        public string Regex { get; set; }
        public string Message { get; set; }
    }
}
