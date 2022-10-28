using System;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace WsClientes
{
    public abstract class clsPessoa
    {

        private long _cod;
        private string _nome;
        private string _email;
        private string _telefone;
        private string _login;
        private string _senha;
        private double _salario; //adicionado

        public long cod
        {
            get { return _cod; }
            set { _cod = value; }
        }

        public string nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        public string login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public double salario //adicionado
        {
            get { return _salario; }
            set { _salario = value; }
        }

        public clsPessoa()
        {
        }
        public abstract void incluir();
        public abstract void alterar();
        public abstract DataSet obter();
        public abstract void PagarImposto(); //adicionado 
        public virtual void excluir()
        {
            string strConn = System.Configuration.ConfigurationSettings.AppSettings.Get("connectionstring").ToString();

            StringBuilder strSql = new StringBuilder("");
            strSql.Append("DELETE FROM clientes ");
            strSql.Append("WHERE cod = @cod;");

            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbParameter param = new OleDbParameter("@cod", this.cod);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Parameters.Add(param);

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
        public virtual long autenticar()
        {
            string strConn = System.Configuration.ConfigurationSettings.AppSettings.Get("connectionstring").ToString();

            StringBuilder strSql = new StringBuilder("");
            strSql.Append("SELECT cod ");
            strSql.Append("FROM clientes WHERE login = @login AND senha = @senha;");

            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@login", this.login);
            param[1] = new OleDbParameter("@senha", this.senha);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Parameters.Add(param[0]);
            cmd.Parameters.Add(param[1]);

            long ret = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSql.ToString();
                ret = Convert.ToInt64(cmd.ExecuteScalar());
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
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return ret;

          
            double salario = 4800.00;
            double salarioFinal = 0;

            if (salario >= 606.00 && salario <= 1212.00)
            {

                salarioFinal = salario - (7.5 * (salario / 100));
                Console.WriteLine(salarioFinal);
            }
            else
            {
                if (salario > 1212.00 && salario <= 2427.35)
                {

                    salarioFinal = salario - (9.00 * (salario / 100));
                    Console.WriteLine(salarioFinal);
                }
                if (salario > 2427.35 && salario <= 3641.03)
                {
                    salarioFinal = salario - (12.00 * (salario / 100));
                    Console.WriteLine(salarioFinal);

              
              if (salario > 3641.03)
              {
                salarioFinal = salario - (14.00 * (salario / 100));
                Console.WriteLine(salarioFinal);
              } 
                }
              
                return ret;
         
            }
        }
    }
}
