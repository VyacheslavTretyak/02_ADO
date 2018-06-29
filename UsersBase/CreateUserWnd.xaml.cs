using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UsersBase
{
    /// <summary>
    /// Interaction logic for CreateUserWnd.xaml
    /// </summary>
    public partial class CreateUserWnd : Window
    {
		public UsersDB Users { get; set; }
		private DataRow dataRow = null;
		private int id = 0;
		public CreateUserWnd()
        {
            InitializeComponent();
        }
		public CreateUserWnd(DataRow row) : this()
		{			
			id = (int)row[0];
			tbLogin.Text = row[1].ToString();			
			tbAdress.Text = row[3].ToString();
			tbPhone.Text = row[4].ToString();
			chbAdmin.IsChecked = (bool)row[5];
			dataRow = row;
		}

		private void BtnCreate_Click(object sender, RoutedEventArgs e)
		{
			DataTable table = Users.DataSet.Tables[0];
			DataRow[] rows = table.Select($"login='{tbLogin.Text}'");
			
			if (!Users.LoginIsFree(tbLogin.Text) && id == 0)
			{
				MessageBox.Show($"Login {tbLogin.Text} is used");
				return;
			}
			try
			{
				DataRow row = table.NewRow();
				if (id != 0)
				{
					dataRow[1] = tbLogin.Text;
					Password pass = new Password(tbPassword.Password);
					dataRow[2] = pass.GetHashCode();
					dataRow[3] = tbAdress.Text;
					dataRow[4] = tbPhone.Text; 
					dataRow[5] = chbAdmin.IsChecked;
				}
				else
				{
					row[0] = id;
					row[1] = tbLogin.Text;
					row[0] = ((int)table.Rows.OfType<DataRow>().Max(r => r[0])) + 1;
					Password pass = new Password(tbPassword.Password);
					row[2] = pass.GetHashCode();
					row[3] = tbAdress.Text;
					row[4] = tbPhone.Text;
					row[5] = chbAdmin.IsChecked;
					table.Rows.Add(row);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				DialogResult = false;
			}
			DialogResult = true;
		}

		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
