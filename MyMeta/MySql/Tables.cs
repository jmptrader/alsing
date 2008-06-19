using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.MySql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(ITables))]

	public class MySqlTables : Tables
	{
		public MySqlTables()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				string type = this.dbRoot.ShowSystemData ? "SYSTEM TABLE" : "TABLE";
				DataTable metaData = this.LoadData(OleDbSchemaGuid.Tables, new Object[] {this.Database.Name, null, null, type});

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
