using System;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace WsClientes
{
    public abstract class clsPessoaFis : clsPessoa
    {
        private string _cpf;
        private DateTime _dtnasc;
        private string _rg;
        private double _salario; //adicionado

        public string cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public DateTime dtnasc
        {
            get { return _dtnasc; }
            set { _dtnasc = value; }
        }

        public string rg
        {
            get { return _rg; }
            set { _rg = value; }
        }
      

        public clsPessoaFis()
        {
            this.cod = 0;
            this.email = "";
            this.telefone = "";
            this.login = "";
            this.senha = "";
            this.cpf = "";
            this.rg = "";
            this.salario = salario; //adicionado
        }

        public clsPessoaFis(long cod, string nome, string email, string telefone, string login, string senha, string cpf, DateTime dtnasc, string rg, double salario) //adicionado
        {
            this.cod = cod;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.login = login;
            this.senha = senha;
            this.cpf = cpf;
            this.dtnasc = dtnasc;
            this.rg = rg;
            this.salario = salario; //adicionado
        }

        public override void incluir()
        {
            string strConn = System.Configuration.ConfigurationSettings.AppSettings.Get("connectionstring").ToString();
            StringBuilder strSql = new StringBuilder("");
            strSql.Append("INSERT INTO clientes (nome, email, telefone, login, senha, cpf, dtnasc, rg, salario)"); //adicionado
            strSql.Append("VALUES(@nome, @email, @telefone, @login, @senha, @cpf, @dtnasc, @rg, @salario)"); //adicionado

            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@nome", this.nome);
            param[1] = new OleDbParameter("@email", this.email);
            param[2] = new OleDbParameter("@telefone", this.telefone);
            param[3] = new OleDbParameter("@login", this.login);
            param[4] = new OleDbParameter("@senha", this.senha);
            param[5] = new OleDbParameter("@cpf", this.cpf);
            param[6] = new OleDbParameter("@dtnasc", this.dtnasc);
            param[7] = new OleDbParameter("@rg", this.rg);
            param[8] = new OleDbParameter("@salario", this.salario); //adicionado
            param[6].OleDbType = OleDbType.Date;
            OleDbCommand cmd = new OleDbCommand();
            for (byte i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSql.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO RUNTIME: " + ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public override void alterar()
        {
            string connString = System.Configuration.ConfigurationSettings.AppSettings.Get("connectionstring").ToString();

            StringBuilder strSql = new StringBuilder("");
            strSql.Append("UPDATE clientes SET ");
            strSql.Append("  nome = @nome, ");
            strSql.Append("  email = @email, ");
            strSql.Append("  telefone = @telefone, ");
            strSql.Append("  login = @login, ");
            strSql.Append("  senha = @senha, ");
            strSql.Append("  cpf = @cpf, ");
            strSql.Append("  dtnasc = @dtnasc, ");
            strSql.Append("  rg = @rg, ");
            strSql.Append("  salario = @salario "); //adicionado
            strSql.Append("WHERE cod = @cod,");

            OleDbConnection conn = new OleDbConnection(connString);
            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@nome", this.nome);
            param[1] = new OleDbParameter("@email", this.email);
            param[2] = new OleDbParameter("@telefone", this.telefone);
            param[3] = new OleDbParameter("@login", this.login);
            param[4] = new OleDbParameter("@senha", this.senha);
            param[5] = new OleDbParameter("@cpf", this.cpf);
            param[6] = new OleDbParameter("@dtnasc", this.dtnasc);
            param[7] = new OleDbParameter("@rg", this.rg);
            param[8] = new OleDbParameter("@salario", this.salario); //adicionado
            param[9] = new OleDbParameter("@cod", this.cod);
            param[6].OleDbType = OleDbType.Date;
            OleDbCommand cmd = new OleDbCommand();
            for (byte i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSql.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO RUNTIME: " + ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public override DataSet obter()
        {
            string connString = System.Configuration.ConfigurationSettings.AppSettings.Get("connectionstring").ToString();

            StringBuilder strSql = new StringBuilder("");
            strSql.Append("SELECT cod, nome, email, telefone, login, senha, cpf, dtnasc, rg, salario "); //adicionado
            strSql.Append("FROM clientes WHERE cod = @cod");

            OleDbConnection conn = new OleDbConnection(connString);
            OleDbParameter param = new OleDbParameter("@cod", this.cod);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Parameters.Add(param);

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = strSql.ToString();
                cmd.CommandType = CommandType.Text;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (OleDbException ex)
            {
                throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO RUNTIME: " + ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
        }

    }
}
