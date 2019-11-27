using System;
using System.Data.SqlServerCe;
using System.IO;
//using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using JVUtils;

namespace JVSQL
{
	/// <summary>
	/// Summary description for BancoDeDados.
	/// </summary>
	public class SQLServerCEDataBase
	{
        private SqlCeConnection conn = null;

        public SqlCeConnection SqlCeConnection 
		{
			get 
			{
				return conn;
			}
		}

		public SQLServerCEDataBase()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Boolean DatabaseExists (String dataBaseName) 
		{
			return File.Exists (dataBaseName);
		}

		public Boolean OpenDatabase (string databaseName, string password) 
		{   // Open database 
            Debug.AddLog("OpenDatabase");

            if (conn == null) 
			{   // Create database
				if (!DatabaseExists (databaseName)) 
				{
					return CreateDatabaseFile (databaseName, password);
				} 
				else 
				{ // Create connection 
					try 
					{   // Try open database
                        string connStr = "Data Source = '" + databaseName + "'; LCID=1033; Password = " + password + "; Encrypt = FALSE; ";
						conn = new SqlCeConnection(connStr);
						conn.Open();
						return true;
					}
					catch (Exception e)
					{
                        Debug.AddLog("OpenDatabase: Error " + e.Message);

						return false;
					}
				}
			} 
			else 
			{
				return true;
			}
		}

		public void CloseDatabase () 
		{
			if (conn != null) 
			{
                Debug.AddLog("CloseDatabase");
                conn.Close();
			}
		}

		public Boolean CreateDatabaseFile (string databaseName, string password) 
		{
			return CreateDatabaseFile (databaseName, password, false);
		}

		public Boolean CreateDatabaseFile (string databaseName, string password, bool recreateIfExists) 
		{
            Debug.AddLog("CreateDatabaseFile");
            
            // Verifica se o database existe. 
			if (DatabaseExists (databaseName))
			{   // Se existir e for para recriar, então deleta o arquivo
				if (recreateIfExists) 
				{
                    Debug.AddLog("CreateDatabaseFile: recreate. First delete file.");
                    File.Delete(databaseName);
				} 
				else 
				{
					return true;
				}
			} 

			// Cria a "engine" de conexão
            string connStr = "Data Source = '" + databaseName + "'; LCID=1033; Password = " + password + "; Encrypt = FALSE; ";
 
			SqlCeEngine engine = new SqlCeEngine(connStr);

			try 
			{   // Cria o banco de dados
                Debug.AddLog("CreateDatabaseFile: creating new database.");

                string dir = databaseName.Substring(0, databaseName.LastIndexOf(@"\") + 1);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                engine.CreateDatabase();
				engine.Dispose();
			}
            catch (Exception e)
			{   // Se der erro retorna false e finish
                Debug.AddLog("CreateDatabaseFile: error: " + e.Message);
				return false;
			}

			// Tenta abrir o banco de dados, se falhar retorna false
            Debug.AddLog("CreateDatabaseFile: opening");
            if (!OpenDatabase(databaseName, password)) 
			{
                Debug.AddLog("CreateDatabaseFile: opening fail");
                CloseDatabase();
				return false;
			}

			return true;
		}

        // Executa SQLs do tipo Insert, Delete, Update. O retorno é a quantidade
        // de linhas afetadas. Retornará -1 se houver erro ou não estiver conectado
        // ou 0 (zero) se o sql for do tipo Create ou Alter qualquer coisa.
        public int ExecSQL(String sql)
        {
            if (conn != null)
            {
                try
                {
                    Debug.AddLog("ExecSQL: sql=" + sql);
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    return 0;
                }
                catch (Exception e)
                {   // Se der erro retorna false e finish
                    Debug.AddLog("ExecSQL: Exception caught=" + e.Message);
                    if (e.Message != null && !e.Message.Equals(""))
                        MessageBox.Show("SQL Error. The message is: " + e.Message,
                            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 
                            MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            else
            {
                Debug.AddLog("ExecSQL: not connected");
                MessageBox.Show("The database connection does not exist.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return -2;
            }
        }

        // Executa SQLs do tipo Insert, Delete, Update. O retorno é a quantidade
        // de linhas afetadas. Retornará -1 se houver erro ou não estiver conectado
        // ou 0 (zero) se o sql for do tipo Create ou Alter qualquer coisa.
        public int ExecSQL(SqlCeCommand command)
        {
            if (conn != null)
            {
                try
                {
                    Debug.AddLog("ExecSQL: sql = " + command.CommandText);
                    command.Prepare();
                    command.ExecuteNonQuery();

                    return 0;
                }
                catch (Exception e)
                {   // Se der erro retorna false e finish
                    Debug.AddLog("ExecSQL: Exception caught:" + e.Message);
                    if (e.Message != null && !e.Message.Equals(""))
                        MessageBox.Show("SQL Error. The original message is: " + e.Message,
                            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 
                            MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            else
            {
                Debug.AddLog("ExecSQL: Not connected");
                MessageBox.Show("The database connection does not exist.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return -2;
            }
        }

        // Executa SQLs do tipo Select. Retorna a lista de linhas selecionadas.
		// Lembre-se! Depois de usar um Open sempre FECHE o DataReader. Ou 
		// teremos memory leak!
		// Retornará NULL se a conexão não existir ou se o Select falhar.
        public void OpenSQL(String sql, out SqlCeDataReader myReader) 
		{
			myReader = null;

            if (conn != null)
            {
                try
                {
                    Debug.AddLog("OpenSQL: sql=" + sql);
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;

                    myReader = cmd.ExecuteReader();
                    cmd.Dispose();
                }
                catch (Exception e)
                {
                    Debug.AddLog("OpenSQL: Exception caught:" + e.Message);
                }
            }
            else
            {
                Debug.AddLog("OpenSQL: not connected");
            }
		}

		public void OpenSQL (String sql, out DataSet ds) 
		{
			ds = null;

			if (conn != null) 
			{
				try
				{
                    Debug.AddLog("OpenSQL: sql=" + sql);
                    SqlCeCommand cmd = conn.CreateCommand();
					cmd.CommandText = sql;

					//Create a SqlCeDataAdapter object
					SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
					//Fill the DataSet
					ds = new DataSet();
					da.Fill(ds);
                    cmd.Dispose();
				}
				catch (Exception ex) 
				{
                    Debug.AddLog("OpenSQL: Exception caught:" + ex.Message);
                    if (ex.Message != null && !ex.Message.Equals(""))
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 
                            MessageBoxDefaultButton.Button1);
				}
			}
            else
            {
                Debug.AddLog("OpenSQL: not connected");
            }
        } 

		public Boolean TableExists (string tableName) 
		{
			SqlCeDataReader myReader = null;
			OpenSQL ("select * from " + tableName + " where 1=2", out myReader);

            Boolean result = myReader != null;

            if (result) { myReader.Close(); }

			return (result);
		}

		public Boolean CreateTableIfNotExists (string tableName, string sqlCreateTable) 
		{
			if (!TableExists (tableName)) 
			{
				return (ExecSQL (sqlCreateTable) > -1);
			}

			return true;
		}
	}
}
