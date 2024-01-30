using ExamenCORE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenCORE.Repositories
{
    public class RepositoryCliente
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader rdr;

        public RepositoryCliente()
        {
            string connection = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=NETCORE;Persist Security Info=True;User ID=sa;Password=MCSD2023;";
            this.cn = new SqlConnection(connection);
            this.cmd = new SqlCommand();
            this.cmd.Connection = this.cn;
        }
        
        public List<string> GetClientes()
        {
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = "SP_CLIENTES";
            this.cn.Open();

            this.rdr = this.cmd.ExecuteReader();

            List<string> result = new List<string>();

            while (this.rdr.Read())
            {
                result.Add(this.rdr["CodigoCliente"].ToString());
            }
            this.rdr.Close();
            this.cn.Close();

            return result;
        }

        public Cliente GetDatosCliente(string codCliente)
        {
            SqlParameter parameter = new SqlParameter("@CODCLIENTE", codCliente);
            this.cmd.Parameters.Add(parameter);

            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = "SP_DATOS_CLIENTE";
            this.cn.Open();

            this.rdr = this.cmd.ExecuteReader();
            Cliente cl = new Cliente();

            while (this.rdr.Read())
            {

                

                cl.CodCliente = this.rdr["CodigoCliente"].ToString();
                cl.Telefono = this.rdr["Telefono"].ToString();
                cl.Empresa = this.rdr["Empresa"].ToString();
                cl.Ciudad = this.rdr["Telefono"].ToString();
                cl.Contacto = this.rdr["Contacto"].ToString();
                cl.Cargo = this.rdr["Cargo"].ToString();

                

                
            }

            this.rdr.Close();
            this.cn.Close();
            this.cmd.Parameters.Clear();

            return cl;
        }

        public Pedido GetPedidos(string codCliente)
        {
            SqlParameter parameter = new SqlParameter("@CODCLIENTE", codCliente);
            this.cmd.Parameters.Add(parameter);

            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = "SP_PEDIDOS_CLIENTE";
            this.cn.Open();

            this.rdr = this.cmd.ExecuteReader();
            Pedido pd = new Pedido();

            while (this.rdr.Read())
            {
                pd.CodigoPedido = this.rdr["CodigoPedido"].ToString();
                pd.CodigoCliente = this.rdr["CodigoCliente"].ToString();
                pd.FormaEnvio = this.rdr["FormaEnvio"].ToString();
                pd.FechaEntrega = this.rdr["FechaEntrega"].ToString();
                pd.Importe = int.Parse(this.rdr["Importe"].ToString());
            }


            this.rdr.Close();
            this.cn.Close();
            this.cmd.Parameters.Clear();

            return pd;
        }

        public Pedido GetPedidoById(string codPedido)
        {
            SqlParameter parameter = new SqlParameter("@CODPEDIDO", codPedido);
            this.cmd.Parameters.Add(parameter);

            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = "SP_DATOS_PEDIDOS_CLIENTE";
            this.cn.Open();

            this.rdr = this.cmd.ExecuteReader();
            Pedido pd = new Pedido();

            while (this.rdr.Read())
            {
                pd.CodigoPedido = this.rdr["CodigoPedido"].ToString();
                pd.CodigoCliente = this.rdr["CodigoCliente"].ToString();
                pd.FormaEnvio = this.rdr["FormaEnvio"].ToString();
                pd.FechaEntrega = this.rdr["FechaEntrega"].ToString();
                pd.Importe = int.Parse(this.rdr["Importe"].ToString());
            }

            this.rdr.Close();
            this.cn.Close();
            this.cmd.Parameters.Clear();
            return pd;
        }

        public int InsertarPedido(string CodigoPedido, string fecha, string forma, int importe)
        {
            string sql = "insert into pedidos values (@codigopedido, @fecha, @foma, @importe)";
            SqlParameter parameter1 = new SqlParameter("@codigopedido", CodigoPedido);
            SqlParameter parameter2 = new SqlParameter("@fecha", fecha);
            SqlParameter parameter3 = new SqlParameter("@foma", forma);
            SqlParameter parameter4 = new SqlParameter("@importe", importe);

            this.cmd.Parameters.Add(parameter1);
            this.cmd.Parameters.Add(parameter2);
            this.cmd.Parameters.Add(parameter3);
            this.cmd.Parameters.Add(parameter4);

            this.cmd.CommandType= CommandType.Text;
            this.cmd.CommandText = sql;

            int result = this.cmd.ExecuteNonQuery();

            this.cn.Open();
            this.cn.Close();
            this.cmd.Parameters.Clear();


            

            return result;
        }

    }
}
