using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.MySql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IResultColumns))]

	public class MySqlResultColumns : ResultColumns
	{
		public MySqlResultColumns()
		{

		}
	}
}
