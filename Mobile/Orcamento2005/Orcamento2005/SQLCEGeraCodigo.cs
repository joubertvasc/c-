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
	/// Summary description for SQLCEGeraCodigo.
	/// </summary>
	public class SQLCEGeraCodigo
	{
		private SQLServerCEDataBase bd;

		public SQLServerCEDataBase SQLServerCEDataBase 
		{
			get 
			{
				return bd;
			}
		}

		public SQLCEGeraCodigo(SQLServerCEDataBase sqlServerCEDataBase)
		{
			bd = sqlServerCEDataBase;
		}

		public String GeraCodigo (
			String nomeTabela,
			String campoChave1,
			String campoChave2,
			String campoChave3,
			String campoChave4) 
		{
			return GeraCodigo (nomeTabela, campoChave1, campoChave2, campoChave3, campoChave4, "");
		}

		public String GeraCodigo (
			String nomeTabela,
			String campoChave1,
			String campoChave2,
			String campoChave3) 
		{
			return GeraCodigo (nomeTabela, campoChave1, campoChave2, campoChave3, "", "");
		}

		public String GeraCodigo (
			String nomeTabela,
			String campoChave1,
			String campoChave2) 
		{
			return GeraCodigo (nomeTabela, campoChave1, campoChave2, "", "", "");
		}

		public String GeraCodigo (
			String nomeTabela,
			String campoChave1) 
		{
			return GeraCodigo (nomeTabela, campoChave1, "", "", "", "");
		}

		public String GeraCodigo (
			String nomeTabela,
			String campoChave1,
			String campoChave2,
			String campoChave3,
			String campoChave4,
			String campoChave5) 
		{
			SqlCeDataReader myReader = null; 
			bd.OpenSQL ("select vlcodigo from geracodigo" +
			            " where nmtabela='" + nomeTabela  + "' " +
			            "   and vlchave1='" + campoChave1 + "' " +
			            "   and vlchave2='" + campoChave2 + "' " +
			            "   and vlchave3='" + campoChave3 + "' " +
			            "   and vlchave4='" + campoChave4 + "' " +
			            "   and vlchave5='" + campoChave5 + "' ", 
			            out myReader);

			if (myReader != null)
			{
				if (myReader.Read()) 
				{
					String chaveAnterior = myReader.GetString (0);
					String chaveNova = chaveAnterior.Substring (0, chaveAnterior.Length -1);
					chaveNova = System.Convert.ToString (System.Convert.ToInt32 (chaveNova) + 1) + "P";
                    myReader.Close();

					if (AtualizaRegistroGeraCodigo (
						nomeTabela,
						campoChave1,
						campoChave2,
						campoChave3,
						campoChave4,
						campoChave5, chaveNova)) 
					{
						return chaveNova;
					}
					else 
					{
						return ""; 
					}
				} 
				else 
				{
					if (AdicionaRegistroGeraCodigo (
						nomeTabela,
						campoChave1,
						campoChave2,
						campoChave3,
						campoChave4,
						campoChave5, "1P")) 
					{
						return "1P";
					} 
					else 
					{
						return "";
					}
				}
			}
			else
			{
				return "";
			}
		}
		
		public Boolean AdicionaRegistroGeraCodigo (
			String nomeTabela,
			String campoChave1,
			String campoChave2,
			String campoChave3,
			String campoChave4,
			String campoChave5,
			String valorChave) 
		{
			return (bd.ExecSQL (
						"insert into geracodigo (nmtabela, vlchave1, vlchave2, " +
						"      vlchave3, vlchave4, vlchave5, vlcodigo) values (" +
						"'" + nomeTabela  + "', " +
						"'" + campoChave1 + "', " +
						"'" + campoChave2 + "', " +				
						"'" + campoChave3 + "', " +
						"'" + campoChave4 + "', " +
						"'" + campoChave5 + "', " +
						"'" + valorChave  + "')") > -1);
		}

		public Boolean AtualizaRegistroGeraCodigo (
			String nomeTabela,
			String campoChave1,
			String campoChave2,
			String campoChave3,
			String campoChave4,
			String campoChave5,
			String valorChave) 
		{
			return (bd.ExecSQL (
				"update geracodigo set vlcodigo = '" + valorChave + "' " +
				" where " + 
				" vlchave1 = '" + campoChave1 + "' and " +
				" vlchave2 = '" + campoChave2 + "' and " +
				" vlchave3 = '" + campoChave3 + "' and " +
				" vlchave4 = '" + campoChave4 + "' and " +
				" vlchave5 = '" + campoChave5 + "'") >= 0);
		}
	}
}
