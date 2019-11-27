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
	/// Summary description for OrcamentoDataBase.
	/// </summary>
	public class OrcamentoDataBase
	{
		private String db = "\\Application Data\\Orcamento\\orcamento.sdf";
		private String senha = "teste";
		private int versaoDoAplicativo;
		private int versaoNoBanco;
		private SQLServerCEDataBase bancoDeDados;
		
		public SQLServerCEDataBase SQLServerCEDataBase 
		{
			get 
			{
				return bancoDeDados;
			}
		}

		public OrcamentoDataBase(int numeroVersao)
		{
			//
			// TODO: Add constructor logic here
			//

            if (!Directory.Exists("\\Application Data"))
            {
                Directory.CreateDirectory("\\Application Data");
            }

            if (!Directory.Exists("\\Application Data\\Orcamento"))
            {
                Directory.CreateDirectory("\\Application Data\\Orcamento");
            }

            bancoDeDados = new SQLServerCEDataBase();
			versaoDoAplicativo = numeroVersao;
		}

		public void FechaBancoDados() 
		{
			if (bancoDeDados != null) 
			{
				bancoDeDados.CloseDatabase();
			}
		}

		public Boolean VerificaBancoDeDados () 
		{
			if (bancoDeDados.OpenDatabase (db, senha)) 
			{
				if (!VerificaTables()) 
				{
					MessageBox.Show ("Ocorreu um erro ao acessar o banco de dados. Contacte o administrador.", 
						"Aviso", MessageBoxButtons.OK, 
						MessageBoxIcon.Exclamation, 
						MessageBoxDefaultButton.Button1);

					return false;
				} 
				else 
				{
					return true;
				};
			} 
			else    
			{
				return false;
			}
		}

		private Boolean AtualizaVersao(int versaoAGravar) 
		{
			SqlCeDataReader  myReader = null; 
			bancoDeDados.OpenSQL ( "select * from configuracao", out myReader);

			if (myReader != null)
			{
				if (myReader.Read()) 
				{
                    myReader.Close();

					if (bancoDeDados.ExecSQL (
						"update configuracao set nuversao ='" + 
						System.Convert.ToString (versaoAGravar) + "'") == -1) 
					{
						return false; 
					}
				} 
				else 
				{
					if (bancoDeDados.ExecSQL (
						"insert into configuracao (nuversao) values ('" + 
						System.Convert.ToString (versaoAGravar) + "')") == -1) 
					{
						return false; 
					}
				}
			}
			else
			{
				return false;
			}

			versaoNoBanco = versaoAGravar;/**/
			return true;
   		}

		private Boolean VerificaTables() 
		{
			// Verifica se a tabela de configuração existe.
			// Se não existir então é porque o banco necessita ser criado.
			if (!bancoDeDados.TableExists ("configuracao")) 
			{
				// Cria a tabela de configuração
				if (bancoDeDados.ExecSQL (
					"create table configuracao (nuversao nchar (7) not null)") == -1) 
				{
					return false;
				} 

				// Coloca a versão do sistema como o (zero) para indicar que as
				// tabelas precisam ser criadas.
				if (!AtualizaVersao(0)) 
				{
					return false;
				}
			} 
			else 
			{   // Se a tabela de configuração já existir então busca qual a versão
				// que está no banco
				SqlCeDataReader myReader = null;
				bancoDeDados.OpenSQL ("select * from configuracao", out myReader);

				if (myReader != null) 
				{
                    if (myReader.Read()) 
					{
                        versaoNoBanco = System.Convert.ToInt32(myReader.GetString(0));
                        myReader.Close();
                    } 
					else 
					{ 
						return false; 
					}
				} 
				else 
				{
					return false;
				}
			}

			////////////////////////////////////////////////////////////
			// Verifica se a versão do sistema é maior que a do banco //
			// Se for então o banco necessita ser atualizado.         //
			////////////////////////////////////////////////////////////
			
			// Verifica se a versão é anterior a 1.0.0-0
			if (versaoNoBanco < 1000) 
			{
				if (!VerificaVersao1000()) 
				{
					return false;
				} 
				else
				{
					if (!AtualizaVersao (1000)) 
					{
						return false;
					}
				}
			}

			// Quando houver novas versões que modifiquem o banco de dados
			// incluir função que faça o upgrade neste ponto!

			return true;
		}

		// Cria as tabelas iniciais do sistema, se não existirem.
		private Boolean VerificaVersao1000() 
		{
			if (!bancoDeDados.CreateTableIfNotExists ("geracodigo", 
				"create table geracodigo (nmtabela nvarchar (20) not null, " +
				"                         vlchave1 nvarchar (20) not null, " +
				"                         vlchave2 nvarchar (20) not null, " +
				"                         vlchave3 nvarchar (20) not null, " +
				"                         vlchave4 nvarchar (20) not null, " +
				"                         vlchave5 nvarchar (20) not null, " +
				"                         vlcodigo nvarchar (20) not null, " +
				"       primary key (nmtabela, vlchave1, vlchave2, vlchave3,"+
				"                    vlchave4, vlchave5))")) 
			{
				return false;
			}

			if (!bancoDeDados.CreateTableIfNotExists ("tipomovim", 
				"create table tipomovim (cdtipomovim nvarchar (5) not null primary key, " +
				"                        detipomovim nvarchar (20) not null, " +
				"                        flnatureza nchar (1) not null, " +
				"                        flforauso nchar (1) not null, " +
				"                        floperacao nchar (1) not null, " +
				"                        dtoperacao datetime)")) 
			{
				return false;
			}

			if (!bancoDeDados.TableExists ("tipoconta")) 
			{
				// Cria a tabela de configuração
				if (bancoDeDados.ExecSQL (
					"create table tipoconta (cdtipoconta nchar (1) not null primary key, " +
					"                        detipoconta nvarchar (20) not null, " +
					"                        flforauso nchar (1) not null)") == -1) 
				{
					return false;
				} 
				else 
				{
					String sql = "Insert into tipoconta (cdtipoconta, detipoconta, flforauso)" +
						" values ('A', 'Conta corrente', 'N')";

					bancoDeDados.ExecSQL (sql);
					
					sql = "Insert into tipoconta (cdtipoconta, detipoconta, flforauso)" +
						" values ('B', 'Cartão de crédito', 'N')";

					bancoDeDados.ExecSQL (sql);

					sql = "Insert into tipoconta (cdtipoconta, detipoconta, flforauso)" +
						" values ('C', 'Poupança', 'N')";

					bancoDeDados.ExecSQL (sql);

					sql = "Insert into tipoconta (cdtipoconta, detipoconta, flforauso)" +
						" values ('D', 'Aplicação financeira', 'N')";

					bancoDeDados.ExecSQL (sql);

					sql = "Insert into tipoconta (cdtipoconta, detipoconta, flforauso)" +
						" values ('E', 'Caixa', 'N')";

					bancoDeDados.ExecSQL (sql);

					sql = "Insert into tipoconta (cdtipoconta, detipoconta, flforauso)" +
						" values ('Z', 'Outras contas', 'N')";

					bancoDeDados.ExecSQL (sql);
				}
			}

            if (!bancoDeDados.CreateTableIfNotExists("conta",
                "create table conta (cdconta nvarchar (5) not null primary key, " +
                "                    deconta nvarchar (20) not null, " +
                "                    vllimite float not null, " +
                "                    vlsaldoinicial float not null, " +
                "                    vlsaldo float not null, " +
                "                    cdtipoconta nchar (1) not null references tipoconta (cdtipoconta) on delete cascade, " +
                "                    flforauso nchar (1) not null, " +
                "                    floperacao nchar (1) not null, " +
                "                    dtoperacao datetime, " +
                "                    nuDiaVencCartao integer, " +
                "                    nuDiaBomCartao integer)"))
            {
                return false;
            }

            if (!bancoDeDados.CreateTableIfNotExists("controlemensal",
                "create table controlemensal (cdcontrole nvarchar (10) not null primary key, " +
                "                             numes integer not null, " +
                "                             nuano integer not null)")) {
                return false;
            }

            if (!bancoDeDados.CreateTableIfNotExists("centrocusto", 
				"create table centrocusto (cdcentro nvarchar (5) not null primary key, " +
				"                          decentro nvarchar (20) not null, " +
				"                          flforauso nchar (1) not null, " +
				"                          floperacao nchar (1) not null, " +
				"                          dtoperacao datetime)")) 
			{
				return false;
			}

            if (!bancoDeDados.CreateTableIfNotExists("contafixa",
                "create table contafixa (cdcontafixa nvarchar (10) not null primary key, " +
                "                        decontafixa nvarchar (30) not null, " +
                "                        cdtipomovim nvarchar (5) not null, " +
                "                        cdcentrocusto nvarchar (5) not null, " +
                "                        nudiavencimento integer not null, " +
                "                        vlconta float not null, " +
                "                        flforauso nchar (1) not null, " +
                "                        floperacao nchar (1) not null, " +
                "                        dtoperacao datetime not null)"))
            {
                return false;
            }

            if (!bancoDeDados.CreateTableIfNotExists("creditofixo",
                "create table creditofixo (cdcreditofixo nvarchar (10) not null primary key, " +
                "                          decreditofixo nvarchar (30) not null, " +
                "                          cdtipomovim nvarchar (5) not null, " +
                "                          nudiarecebimento integer not null, " +
                "                          vlcredito float not null, " +
                "                          flforauso nchar (1) not null, " +
                "                          floperacao nchar (1) not null, " +
                "                          dtoperacao datetime not null)"))
            {
                return false;
            }

            if (!bancoDeDados.CreateTableIfNotExists("prestacoes",
                "create table prestacoes (cdprestacao nvarchar (10) not null primary key, " +
                "                         deprestacao nvarchar (30) not null, " +
                "                         cdtipomovim nvarchar (5) not null, " +
                "                         cdcentrocusto nvarchar (5) not null, " +
                "                         nuparcelas integer not null, " +
                "                         vlparcela float not null, " +
                "                         vlprestacao float not null, " +
                "                         dtaquisicao datetime not null, " +
                "                         dtprimeira datetime not null, " +
                "                         nudiavenc integer not null, " +
                "                         floperacao nchar (1) not null, " +
                "                         dtoperacao datetime not null)"))
            {
                return false;
            }

            if (!bancoDeDados.CreateTableIfNotExists("lancamento", 
       				  "create table lancamento (cdlancamento nvarchar (10) not null primary key, " +
				        "                         delancamento nvarchar (30) not null, " +
				        "                         dtlancamento datetime not null, " + 
				        "                         cdtipomovim nvarchar (5) not null, " +
				        "                         cdcentrocusto nvarchar (5), " +
				        "                         vllancamento float not null, " +
				        "                         floperacao nchar (1) not null, " +
                "                         dtoperacao datetime not null, " +
                "                         cdconta nvarchar (5), " +
                "                         dtquitacao datetime, " +
                "                         cdcontafixa nvarchar (10), " +
                "                         cdcreditofixo nvarchar (10), " +
                "                         cdprestacao nvarchar (10), " +
                "                         nuparcela integer, " +
                "                         vljuros float, " +
                "                         vldesconto float, " +
                "                         nucheque nvarchar (10))")) 
			{
				return false;
			}

			return true;
		}
	}
}
