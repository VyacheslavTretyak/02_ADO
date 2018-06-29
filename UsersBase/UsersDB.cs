using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace UsersBase
{
	public class UsersDB
	{
		private SqlConnection sqlConnection = null;
		public DataSet DataSet { get; private set; } = null;
		private SqlDataAdapter adapter = null;
		public void Connect()
		{
			try
			{
				string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				sqlConnection = new SqlConnection(cs);
				string selectSql = "select * from Users;";
				adapter = new SqlDataAdapter(selectSql, sqlConnection);
				SqlCommandBuilder cmdBldr = new SqlCommandBuilder(adapter);
				DataSet = new DataSet();
				adapter.Fill(DataSet);
			}
			catch (Exception)
			{
				throw;
			}			
		}
		internal bool LoginIsFree(string login)
		{
			DataTable table = DataSet.Tables[0];
			DataRow[] rows = table.Select($"login='{login}'");
			if (rows.Length > 0)
			{
				return false;
			}
			return true;
		}		
		internal void Update() => adapter.Update(DataSet);
	}
	

}
