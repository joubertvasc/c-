using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;
using System.IO;
using System.Globalization;

namespace Orcamento2005
{
	/// <summary>
	/// Summary description for TipoMovimDataSet.
	/// </summary>
	public class TipoMovimDataSet
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

		public TipoMovimDataSet(OrcamentoDataBase orcamentoDataBase)
		{
			db = orcamentoDataBase;
			geraCodigo = new SQLCEGeraCodigo (db.SQLServerCEDataBase);
		}

		public void AddTipoMovim(string codigo, 
			string descricao, 
			string natureza, 
			string foraUso) 
		{			
			DataRow myRow;
			myRow = dataTable.NewRow();
			myRow["cdtipomovim"] = codigo;
			myRow["detipomovim"] = descricao;
			myRow["flnatureza"]  = natureza;
			myRow["flforauso"]   = foraUso;

			dataSet.Tables [0].Rows.Add (myRow);
			dataTable.AcceptChanges();

			String sql = 
				"insert into tipomovim (cdtipomovim, detipomovim, flnatureza, " +
				"                       flforauso, floperacao, dtoperacao) " +
				" values ('" + codigo + "', '" + descricao + "', '" + natureza +
				"', '" + foraUso + "', 'I', GETDATE() )";
				
			db.SQLServerCEDataBase.ExecSQL (sql);
		}	

		public void AltTipoMovim(int linha,
			string codigo, 
			string descricao, 
			string natureza, 
			string foraUso) 
		{			
			DataRow myRow;
			myRow = dataTable.Rows [linha];
			myRow["cdtipomovim"] = codigo;
			myRow["detipomovim"] = descricao;
			myRow["flnatureza"]  = natureza;
			myRow["flforauso"]   = foraUso;
			dataTable.AcceptChanges();

			String sql = 
				"update tipomovim set " + 
				" detipomovim='" + descricao + "', " +
				" flnatureza='" + natureza + "', " +
				" flforauso='" + foraUso + "', " +
				" floperacao='A', dtoperacao=getdate()" +
				" where cdtipomovim = '" + codigo + "'";

			db.SQLServerCEDataBase.ExecSQL (sql);
		}	

		public void DelTipoMovim(int linha) 
		{			
			DataRow myRow;
			myRow = dataTable.Rows [linha];

			String sql = 
				"update tipomovim set flOperacao = 'D', dtoperacao=GETDATE() " +
				" where cdtipomovim = '" + myRow["cdtipomovim"] + "'";

			db.SQLServerCEDataBase.ExecSQL (sql);

			myRow.Delete();
			dataTable.AcceptChanges();
		}	

		public String GeraCodigo () 
		{
			return geraCodigo.GeraCodigo ("TipoMovim", "cdtipomovim");
		}

		public void SelectAll (
			Boolean filtrarForaUso, 
			Boolean filtrarDeletados, 
			String campoOrdenacao,
			Boolean ordemCrescente) 
		{
			String sql = "select cdTipoMovim, deTipoMovim, flNatureza, flForaUso, flOperacao, " +
						 "       dtOperacao from tipomovim";

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
  
    public String Natureza(String cdTipoMovim)
    {
      String sql = "select flnatureza from tipomovim where cdtipomovim = '" + cdTipoMovim + "'";
      SqlCeDataReader myReader = null;
      String result = "";

      db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

      if (myReader != null)
      {
        if (myReader.Read())
        {
          result = System.Convert.ToString(myReader["flnatureza"]);
          myReader.Close();
        }
      }

      return result;
    }
  }
}
