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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UsersBase
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private UsersDB db;
		
		public MainWindow()
		{
			InitializeComponent();
			db = new UsersDB();
			try
			{
				db.Connect();				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}

			listView.ItemsSource = db.DataSet.Tables[0].DefaultView;
			listView.MouseDoubleClick += ListView_MouseDoubleClick;
		}

		private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			DataRowView sel = listView.SelectedValue as DataRowView;	
			CreateUserWnd wnd = new CreateUserWnd(sel.Row);
			wnd.Users = db;
			wnd.ShowDialog();
		}

		private void Update_Click(object sender, RoutedEventArgs e)
		{
			db.Update();
		}
		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			DataRowView sel = listView.SelectedValue as DataRowView;
			db.DataSet.Tables[0].Rows.Remove(sel.Row);
		}

		private void AddUser_Click(object sender, RoutedEventArgs e)
		{
			CreateUserWnd wnd = new CreateUserWnd();
			wnd.Users = db;
			wnd.ShowDialog();
		}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			if(db == null)
			{
				return;
			}
			RadioButton radio = sender as RadioButton;
			DataTable table = db.DataSet.Tables[0];		
			
			switch (radio.Content)
			{
				case "All":
					table.DefaultView.RowFilter = "";
					break;
				case "Admin":
					table.DefaultView.RowFilter = "Admin=true";
					break;
				case "No Admin":
					table.DefaultView.RowFilter = "Admin=false";
					break;
			}
		
			
		}
	}
}

