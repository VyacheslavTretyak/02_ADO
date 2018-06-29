using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersBase
{
    class Password
    {
		private string value;
		public Password(string password)
		{
			value = password;
		}
		
		public override int GetHashCode()
		{
			int code = 0;
			foreach (char ch in value)
			{
				code += ch >> 2;
			}
			return code;
		}
	}
}
