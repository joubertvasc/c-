using System;
using System.Data;
using System.Globalization;
using System.Data.SqlTypes;
using System.IO;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Orcamento2005
{
	/// <summary>
	/// Summary description for ContaDataSet.
	/// </summary>
	public class ContaDataSet
	{
		private SQLCEGeraCodigo geraCodigo;
		private DataTable dataTable;
		private DataSet dataSet;
		private OrcamentoDataBase db;
		private DataSet tiposConta;

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

		public DataSet TiposConta
		{ 
			get
			{
				return tiposConta;
			}
		}

		public ContaDataSet(OrcamentoDataBase orcamentoDataBase)
		{
			db = orcamentoDataBase;
			geraCodigo = new SQLCEGeraCodigo (db.SQLServerCEDataBase);

			db.SQLServerCEDataBase.OpenSQL (
              "select cdtipoconta, detipoconta from tipoconta " +
			  " where flforauso = 'N' order by detipoconta", out tiposConta);
		}

		public void AddConta(string codigo, 
			string descricao, 
			decimal limite,
			decimal saldoinicial,
			string tipoconta,
			string foraUso,
            int nuDiaBom,
            int nuDiaVencimento) 
		{			
			DataRow myRow;
			myRow = dataTable.NewRow();
			myRow["cdconta"]         = codigo;
			myRow["deconta"]         = descricao;
			myRow["vllimite"]        = limite;
			myRow["vlsaldoinicial"]  = saldoinicial;
      myRow["vlsaldo"]         = saldoinicial;
			myRow["cdtipoconta"]     = tipoconta;
			myRow["flforauso"]       = foraUso;
			myRow["detipoconta"]     = EncontraDeTipoConta (tipoconta);
      myRow["nuDiaBomCartao"]  = nuDiaBom;
      myRow["nuDiaVencCartao"] = nuDiaVencimento;

			dataSet.Tables [0].Rows.Add (myRow);
			dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into conta (cdconta, deconta, vllimite, vlsaldoinicial, cdtipoconta, " +
                "                   flforauso, floperacao, dtoperacao, vlsaldo, nudiabomcartao, nudiavenccartao) " +
                " values (@codigo, @deconta, @limite, @saldoinicial, @tipoconta, @forauso, 'I', GETDATE(), @saldo, @nudiabomcartao, @nudiavenccartao)";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@deconta", SqlDbType.NVarChar, 20);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@limite", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@saldoinicial", SqlDbType.Float, 100);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@tipoconta", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@forauso", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nudiabomcartao", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nudiavenccartao", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@saldo", SqlDbType.Float, 100);
            command.Parameters.Add(param);

            command.Parameters["@codigo"].Size = 5;
            command.Parameters["@deconta"].Size = 20;
            command.Parameters["@tipoconta"].Size = 1;
            command.Parameters["@forauso"].Size = 1;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = limite;
            command.Parameters[3].Value = saldoinicial;
            command.Parameters[4].Value = tipoconta;
            command.Parameters[5].Value = foraUso;
            command.Parameters[6].Value = nuDiaBom;
            command.Parameters[7].Value = nuDiaVencimento;
            command.Parameters[8].Value = saldoinicial;

            db.SQLServerCEDataBase.ExecSQL(command);
        }	
        
		public void AltConta(int linha,
			string codigo, 
			string descricao, 
			decimal limite,
			decimal saldoinicial,
			string tipoconta,
      string foraUso,
      int nuDiaBom,
      int nuDiaVencimento) 
    {			
			DataRow myRow;
			myRow = dataTable.Rows [linha];
			myRow["cdconta"]        = codigo;
			myRow["deconta"]        = descricao;
			myRow["vllimite"]       = limite;
			myRow["vlsaldoinicial"] = saldoinicial;
			myRow["cdtipoconta"]    = tipoconta;
			myRow["flforauso"]      = foraUso;
			myRow["detipoconta"]    = EncontraDeTipoConta (tipoconta);
      myRow["nuDiaBomCartao"] = nuDiaBom;
      myRow["nuDiaVencCartao"] = nuDiaVencimento;

			dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "update conta set " +
                " deconta = @deconta, " +
                " vllimite = @limite, " +
                " vlsaldoinicial = @saldoinicial, " +
                " cdtipoconta = @tipoconta, " +
                " flforauso = @forauso, " +
                " floperacao = 'A', dtoperacao=getdate(), " +
                " nudiabomcartao = @nudiabomcartao, " +
                " nudiavenccartao = @nudiavenccartao " +
                " where cdconta = @codigo";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@deconta", SqlDbType.NVarChar, 20);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@limite", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@saldoinicial", SqlDbType.Float, 100);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@tipoconta", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@forauso", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nudiabomcartao", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nudiavenccartao", SqlDbType.Int);
            command.Parameters.Add(param);

            command.Parameters["@codigo"].Size = 5;
            command.Parameters["@deconta"  ].Size = 20;
            command.Parameters["@tipoconta"].Size = 1;
            command.Parameters["@forauso"  ].Size = 1;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = limite;
            command.Parameters[3].Value = saldoinicial;
            command.Parameters[4].Value = tipoconta;
            command.Parameters[5].Value = foraUso;
            command.Parameters[6].Value = nuDiaBom;
            command.Parameters[7].Value = nuDiaVencimento;

            db.SQLServerCEDataBase.ExecSQL(command);
		}	

		public void DelConta(int linha) 
		{			
			DataRow myRow;
			myRow = dataTable.Rows [linha];

			String sql = 
				"update conta set flOperacao = 'D', dtoperacao=GETDATE() " +
				" where cdconta = '" + myRow["cdconta"] + "'";

			db.SQLServerCEDataBase.ExecSQL (sql);

			myRow.Delete();
			dataTable.AcceptChanges();
		}	

		public String GeraCodigo () 
		{
			return geraCodigo.GeraCodigo ("Conta", "cdconta");
		}

    private string PreencheEspacos(string original, int tamanho, Boolean esquerda)
    {
      string x = original;

      for (int i = original.Length; i < tamanho; i++)
      {
        if (esquerda)
        {
          x = " " + x;
        }
        else
        {
          x += " ";
        }
      }

      return x;
    }
    private string ValorFormatado(Decimal valor)
    {
      if (valor == 0) {
        return "0,00";
      } else {
        return valor.ToString("###,000.00");
      }
    }
    
    public void SelectAll(
			Boolean filtrarForaUso, 
			Boolean filtrarDeletados, 
			String campoOrdenacao,
			Boolean ordemCrescente) 
		{
			String sql = "select c.cdconta, c.deconta, c.vllimite, c.vlsaldoinicial," +
				" c.cdtipoconta, c.flforauso, c.floperacao, c.dtoperacao, c.vlSaldo, " +
        " c.nuDiaBomCartao, c.nuDiaVencCartao, tc.detipoconta, '' as vlsaldoformatado " +
				" from conta c, tipoconta tc " +
				" where c.cdtipoconta = tc.cdtipoconta";

			if (filtrarForaUso) {
				sql += " and c.flForaUso = 'N' ";
			}

			if (filtrarDeletados) {
				sql += " and c.flOperacao <> 'D'";
			}

			if (campoOrdenacao != "") {
				sql += " order by " + campoOrdenacao;

				if (!ordemCrescente) {
					sql += " desc";
				}
			}

			db.SQLServerCEDataBase.OpenSQL (sql, out dataSet);
      dataTable = dataSet.Tables[0];

      // Formata os valores para apresentação
      DataRow myRow;
      for (int i = 0; i < dataTable.Rows.Count; i++)
      {
        myRow = dataTable.Rows[i];
        myRow["vlsaldoformatado"] = PreencheEspacos(
          (ValorFormatado(System.Convert.ToDecimal(myRow["vlsaldo"]))), 10, true);
        dataTable.AcceptChanges();
      }
		}

		public string EncontraDeTipoConta (string codigoTipoConta) 
		{
			for (int i=0; i < tiposConta.Tables[0].Rows.Count; i++) 
			{
				if ((String)tiposConta.Tables[0].Rows [i].ItemArray [0] == codigoTipoConta) 
				{
					return (String)tiposConta.Tables[0].Rows [i].ItemArray [1];
				}
			}

			return "";
		}

		public string EncontraCdTipoConta (string descricaoTipoConta) 
		{
			for (int i=0; i < tiposConta.Tables[0].Rows.Count; i++) 
			{
				if ((String)tiposConta.Tables[0].Rows [i].ItemArray [1] == descricaoTipoConta) 
				{
					return (String)tiposConta.Tables[0].Rows [i].ItemArray [0];
				}
			}

			return "";
		}

        public Decimal GetSaldoConta(string conta)
        {
            String sql = "select vlsaldo from conta " +
                           " where cdconta= '" + conta + "' ";
            SqlCeDataReader myReader = null;
            Decimal result = 0;

            db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

            if (myReader != null) {
                if (myReader.Read()) {
                    result = System.Convert.ToDecimal(myReader["vlsaldo"]);
                    myReader.Close();
                }
            }

            return result;
         }

         public void SetSaldoConta(String cdConta, Decimal vlSaldo)
         {
             SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

             command.CommandText =
               "update conta " +
               " set vlSaldo = @vlSaldo " +
               " where cdConta=@cdConta ";
             SqlCeParameter param = null;

             param = new SqlCeParameter("@cdConta", SqlDbType.NVarChar, 5);
             command.Parameters.Add(param);

             param = new SqlCeParameter("@vlSaldo", SqlDbType.Decimal);
             command.Parameters.Add(param);

             command.Parameters["@cdConta"].Size = 5;
             command.Parameters[0].Value = cdConta;
             command.Parameters[1].Value = vlSaldo;

             db.SQLServerCEDataBase.ExecSQL(command);
         }

         public void AdicionaSaldoConta(String cdConta, Decimal vlSaldo)
         {
             SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

             command.CommandText =
               "update conta " +
               " set vlSaldo = vlSaldo + @vlSaldo " +
               " where cdConta=@cdConta ";
             SqlCeParameter param = null;

             param = new SqlCeParameter("@cdConta", SqlDbType.NVarChar, 5);
             command.Parameters.Add(param);

             param = new SqlCeParameter("@vlSaldo", SqlDbType.Decimal);
             command.Parameters.Add(param);

             command.Parameters["@cdConta"].Size = 5;
             command.Parameters[0].Value = cdConta;
             command.Parameters[1].Value = vlSaldo;

             db.SQLServerCEDataBase.ExecSQL(command);
         }

        public void SubtraiSaldoConta(String cdConta, Decimal vlSaldo)
         {
             SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

             command.CommandText =
               "update conta " +
               " set vlSaldo = vlSaldo - @vlSaldo " +
               " where cdConta=@cdConta ";
             SqlCeParameter param = null;

             param = new SqlCeParameter("@cdConta", SqlDbType.NVarChar, 5);
             command.Parameters.Add(param);

             param = new SqlCeParameter("@vlSaldo", SqlDbType.Decimal);
             command.Parameters.Add(param);

             command.Parameters["@cdConta"].Size = 5;
             command.Parameters[0].Value = cdConta;
             command.Parameters[1].Value = vlSaldo;

             db.SQLServerCEDataBase.ExecSQL(command);
         }
     }
}
