using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacity.CRUD.Model;
using Newtonsoft.Json;

namespace Farmacity.CRUD
{
    public partial class Form1 : Form
    {
        private int? _id;
        public Form1(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string respuesta = await ObtenerListado();
            List<Articulo> articulos = JsonConvert.DeserializeObject<List<Articulo>>(respuesta);
            dataGridView1.DataSource = articulos;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Refrescar(false);
        }

        private async Task Refrescar(bool busquedaFiltrada)
        {
            string respuesta;
            if (busquedaFiltrada)
            {
                respuesta = await ObtenerListadoFiltrado(txtBusqueda.Text);
            }
            else
            {
                respuesta = await ObtenerListado();
            }
             
            List<Articulo> articulos = JsonConvert.DeserializeObject<List<Articulo>>(respuesta);
            dataGridView1.DataSource = articulos;
        }
        
        private async void button3_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55350/api/Articulo/AgregarArticulo");

                var obArticulo = new Articulo
                {
                    Descripcion = txtDescripcion.Text,
                    Activo = (int)txtActivo.Value,
                    Precio = (int)txtPrecio.Value,
                    Stock = (int)txtStock.Value
                };
                var postTask = client.PostAsJsonAsync("AgregarArticulo", obArticulo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    await Refrescar(false);
                }
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            int? id = Getid();
            if (id != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:55350/api/Articulo/EditarArticulo");

                    var obArticulo = new Articulo
                    {
                        IdArticulo = (int)id,
                        Descripcion = txtDescripcion.Text,
                        Precio = (int)txtPrecio.Value,
                        Stock = (int)txtStock.Value,
                        Activo = (int)txtActivo.Value
                    };
                    var postTask = client.PutAsJsonAsync("Articulo", obArticulo);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        await Refrescar(false);
                    }
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = Getid();
            if (id != null)
            {
                using (var client = new HttpClient())
                {
                    var obArticulo = new Articulo
                    {
                        IdArticulo = (int)id,
                        Descripcion = txtDescripcion.Text,
                        Activo = (int)txtActivo.Value,
                        Precio = (int)txtPrecio.Value,
                        Stock = (int)txtStock.Value
                    };

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri("http://localhost:55350/api/Articulo/BorrarArticulo"),
                        Content = new StringContent(JsonConvert.SerializeObject(obArticulo), Encoding.UTF8, "application/json")
                    };
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        await Refrescar(false);
                    }
                }
            }
        }

        private int? Getid()
        {
            try
            {
                return int.Parse(
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()
                );
            }
            catch
            {
                return null;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtDescripcion.Text = dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].FormattedValue.ToString();
                txtActivo.Text = dataGridView1.Rows[e.RowIndex].Cells["Activo"].FormattedValue.ToString();
                txtPrecio.Text = dataGridView1.Rows[e.RowIndex].Cells["Precio"].FormattedValue.ToString();
                txtStock.Text = dataGridView1.Rows[e.RowIndex].Cells["Stock"].FormattedValue.ToString();
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await Refrescar(true);
        }

        private async Task<string> ObtenerListado()
        {
            WebRequest oRequest = WebRequest.Create("http://localhost:55350/api/Articulo/ObtenerListado");
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

        private async Task<string> ObtenerListadoFiltrado(string articulo)
        {
            WebRequest oRequest = WebRequest.Create("http://localhost:55350/obtenerPorFiltro?articulo=" + articulo);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }
    }
}
