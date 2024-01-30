using ExamenCORE.Models;
using ExamenCORE.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ExamenCoreWerku
{
    #region PROCEDIMIENTO ALMACENAD
    /* CARGAR CLIENTES */
    //    CREATE PROCEDURE SP_CLIENTES
    //AS

    //    SELECT* FROM clientes
    //GO

    /* DETALLES DEL CLIENTE */

    //    CREATE PROCEDURE SP_DATOS_CLIENTE
    //(@CODCLIENTE NVARCHAR(50))
    //AS

    //    SELECT* FROM clientes
    //    WHERE CodigoCliente = @CODCLIENTE
    //GO

    /* PEDIDOS CLIENTE */

    //    CREATE PROCEDURE SP_PEDIDOS_CLIENTE
    //(@CODCLIENTE NVARCHAR(50))
    //AS


    //    SELECT* FROM pedidos
    //    WHERE CodigoCliente = @CODCLIENTE


    //GO

    //    create procedure SP_DATOS_PEDIDOS_CLIENTE
    //(@CODPEDIDO NVARCHAR(50))
    //AS

    //    SELECT* FROM pedidos
    //    WHERE CodigoPedido = @CODPEDIDO
    //GO
    #endregion
    public partial class FormPractica : Form
    {
        RepositoryCliente repo;
        List<string> idlista;

        public FormPractica()
        {
            InitializeComponent();

            this.repo = new RepositoryCliente();
            this.idlista = new List<string>();

            loadClientes();

        }

        private void loadClientes()
        {
            List<string> clientes = this.repo.GetClientes();

            foreach (string cliente in clientes)
            {
                this.cmbclientes.Items.Add(cliente);
            }
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codCliente = this.cmbclientes.SelectedItem.ToString();

            Cliente cliente = this.repo.GetDatosCliente(codCliente);

            this.txtcargo.Text = cliente.Cargo;
            this.txtciudad.Text = cliente.Ciudad;
            this.txtempresa.Text = cliente.Empresa;
            this.txtcontacto.Text = cliente.Contacto;
            this.txttelefono.Text = cliente.Telefono;

            this.lstpedidos.Items.Clear();

            Pedido pd = this.repo.GetPedidos(codCliente);
            this.idlista.Add(pd.CodigoPedido);
            this.lstpedidos.Items.Add(pd.CodigoPedido);

        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pedido = this.lstpedidos.SelectedItem.ToString();



            Pedido pd = this.repo.GetPedidoById(pedido);

            this.txtcodigopedido.Text = pd.CodigoPedido;
            this.txtfechaentrega.Text = pd.FechaEntrega;
            this.txtformaenvio.Text = pd.FormaEnvio;
            this.txtimporte.Text = pd.Importe.ToString();

        }

        private void btneliminarpedido_Click(object sender, EventArgs e)
        {

        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {
            string code = this.txtcodigopedido.Text;
           string fecha= this.txtfechaentrega.Text;
            string forma= this.txtformaenvio.Text;
            int importe = int.Parse( this.txtimporte.Text);

            int insertados =
                this.repo.InsertarPedido(code, fecha, forma, importe);

            MessageBox.Show("INSERTADO CORRECTAMENTE "+ insertados, "", MessageBoxButtons.OK, MessageBoxIcon.Information );

        }
    }
}
