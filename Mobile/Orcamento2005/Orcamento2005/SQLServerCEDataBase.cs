using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Orcamento2005
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

		public Boolean OpenDatabase (
			String databaseName, 
			String password) 
		{   // Só abre o banco se a conexão ainda não existir
			if (conn == null) 
			{   // Se o banco não existe, então cria e faz a conexão
				if (!DatabaseExists (databaseName)) 
				{
					return CreateDatabaseFile (databaseName, password);
				} 
				else 
				{ // Se o banco já existir, então faz a conexão
					try 
					{   // Tenta abrir o banco de dados
                        string connStr = "Data Source = '" + databaseName + "'; LCID=1033; Password = " + password + "; Encrypt = FALSE; ";
						conn = new SqlCeConnection(connStr);
						conn.Open();
						return true;
					}
					catch 
					{
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
				conn.Close();
			}
		}

		public Boolean CreateDatabaseFile (
			String databaseName, 
			String password) 
		{
			return CreateDatabaseFile (databaseName, password, false);
		}

		public Boolean CreateDatabaseFile (
			String databaseName, 
			String password,
			Boolean recreateIfExists) 
		{
			// Verifica se o database existe. 
			if (DatabaseExists (databaseName))
			{   // Se existir e for para recriar, então deleta o arquivo
				if (recreateIfExists) 
				{
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
				engine.CreateDatabase();
				engine.Dispose();
			}
            catch (Exception e)
			{   // Se der erro retorna false e finish
                Console.WriteLine("{0} Exception caught.", e);
				return false;
			}

			// Tenta abrir o banco de dados, se falhar retorna false
			if (!OpenDatabase (databaseName, password)) 
			{
				CloseDatabase ();
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
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    return 0;
                }
                catch (Exception e)
                {   // Se der erro retorna false e finish
                    Console.WriteLine("{0} Exception caught.", e);
                    MessageBox.Show("Erro na execução de SQL. Chame o suporte. A mensagem original foi: " + e.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("A conexão com o banco de dados não existe. Chame o suporte.",
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
                    command.Prepare();
                    command.ExecuteNonQuery();

                    return 0;
                }
                catch (Exception e)
                {   // Se der erro retorna false e finish
                    Console.WriteLine("{0} Exception caught.", e);
                    MessageBox.Show("Erro na execução de SQL. Chame o suporte. A mensagem original foi: " + e.Message,
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("A conexão com o banco de dados não existe. Chame o suporte.",
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
					SqlCeCommand cmd = conn.CreateCommand();
					cmd.CommandText = sql;

					myReader = cmd.ExecuteReader();
                    cmd.Dispose();
				}
				catch {}
			} 
		}

		public void OpenSQL (String sql, out DataSet ds) 
		{
			ds = null;

			if (conn != null) 
			{
				try
				{
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
					MessageBox.Show (ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				}
			} 
		} 

		public Boolean TableExists (String tableName) 
		{
			SqlCeDataReader myReader = null;
			OpenSQL ("select * from " + tableName + " where 1=2", out myReader);

            Boolean result = myReader != null;

            if (result) { myReader.Close(); }

			return (result);
		}

		public Boolean CreateTableIfNotExists (
			String tableName, 
			String sqlCreateTable) 
		{
			if (!TableExists (tableName)) 
			{
				return (ExecSQL (sqlCreateTable) > -1);
			}

			return true;
		}
	}
}
