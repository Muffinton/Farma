using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Windows.Forms;
using Farmacity.CRUD.Model;

namespace Farmacity.CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:55350/api/Articulo/ObtenerListado")};
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            
        }
    }
}
