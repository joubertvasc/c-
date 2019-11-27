using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Globalization;

namespace Orcamento2005
{
	/// <summary>
	/// Summary description for CentroCustoDataSet.
	/// </summary>
	public class CentroCustoDataSet
	{
		private SQLCEGeraCodigo geraCodigo;
		private DataTable dataTable;
		private DataSet dataSet;
		private OrcamentoDataBase db;

		public DataSet DataSet 
		{
			get 
			{
				return dataSet;
			}
		}

		public DataTable DataTable 
		{
			get 
			{
				return dataTable;
			}
		}

		public CentroCustoDataSet(OrcamentoDataBase orcamentoDataBase)
		{
			db = orcamentoDataBase;
			geraCodigo = new SQLCEGeraCodigo (db.SQLServerCEDataBase);
		}

		public void AddCentroCusto(string codigo, 
			string descricao, 
			string foraUso) 
		{			
			DataRow myRow;
			myRow = dataTable.NewRow();
			myRow["cdcentro"]  = codigo;
			myRow["decentro"]  = descricao;
			myRow["flforauso"] = foraUso;

			dataSet.Tables [0].Rows.Add (myRow);
			dataTable.AcceptChanges();

			String sql = 
				"insert into centrocusto (cdcentro, decentro, " +
				"                       flforauso, floperacao, dtoperacao) " +
				" values ('" + codigo + "', '" + descricao + "', '" + foraUso + 
				"', 'I', GETDATE() )";
				
			db.SQLServerCEDataBase.ExecSQL (sql);
		}	

		public void AltCentroCusto(int linha,
			string codigo, 
			string descricao, 
			string foraUso) 
		{			
			DataRow myRow;
			myRow = dataTable.Rows [linha];
			myRow["cdcentro"]  = codigo;
			myRow["decentro"]  = descricao;
			myRow["flforauso"] = foraUso;
			dataTable.AcceptChanges();

			String sql = 
				"update centrocusto set " + 
				" decentro='" + descricao + "', " +
				" flforauso='" + foraUso + "', " +
				" floperacao='A', dtoperacao=getdate()" +
				" where cdcentro = '" + codigo + "'";

			db.SQLServerCEDataBase.ExecSQL (sql);
		}	

		public void DelCentroCusto(int linha) 
		{			
			DataRow myRow;
			myRow = dataTable.Rows [linha];

			String sql = 
				"update centrocusto set flOperacao = 'D', dtoperacao=GETDATE() " +
				" where cdcentro = '" + myRow["cdcentro"] + "'";

			db.SQLServerCEDataBase.ExecSQL (sql);

			myRow.Delete();
			dataTable.AcceptChanges();
		}	

		public String GeraCodigo () 
		{
			return geraCodigo.GeraCodigo ("CentroCusto", "cdcentro");
		}

		public void SelectAll (
			Boolean filtrarForaUso, 
			Boolean filtrarDeletados, 
			String campoOrdenacao,
			Boolean ordemCrescente) 
		{
			String sql = "select cdcentro, decentro, flForaUso, flOperacao, " +
				"       dtOperacao from centrocusto";

			if (filtrarForaUso) 
			{
				sql += " where flForaUso = 'N' ";
			}

			if (filtrarDeletados) 
			{
				if (filtrarForaUso) { sql += " and "; } 
				else { sql += " where "; }

				sql += " flOperacao <> 'D'";
			}

			if (campoOrdenacao != "") 
			{
				sql += " order by " + campoOrdenacao;

				if (!ordemCrescente) 
				{
					sql += " desc";
				}
			}

			db.SQLServerCEDataBase.OpenSQL (sql, out dataSet);

			dataTable = dataSet.Tables [0];
		}
	}
}