using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

namespace cadastro_loja.Pages.Clientes
{
    public class CriarModel : PageModel
    {
        public InformacoesCliente informacoesCliente = new InformacoesCliente();
        public String mensagemErro = "";
        public String mensagemSucesso = "";


        public void OnGet()
        {

        }

        public void OnPost()
        {
            informacoesCliente.nome = Request.Form["nome"];
            informacoesCliente.email = Request.Form["email"];
            informacoesCliente.celular = Request.Form["celular"];
            informacoesCliente.endereco = Request.Form["endereco"];

            if (informacoesCliente.nome.Length == 0 || informacoesCliente.email.Length == 0
                || informacoesCliente.celular.Length == 0 || informacoesCliente.endereco.Length == 0)
            {
                mensagemErro = "Todos os campos são necessários";
                return;
            }

            //salva o cliente novo dentro do banco de dados
            try
            {
                String stringConexao = "Data Source=.\\SQLEXPRESS2022;Initial Catalog=loja;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    String sql = "INSERT INTO clientes " +
                        "(nome, email, celular, endereco) VALUES " +
                        "(@nome, @email, @celular, @endereco);";

                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", informacoesCliente.nome);
                        comando.Parameters.AddWithValue("@email", informacoesCliente.email);
                        comando.Parameters.AddWithValue("@celular", informacoesCliente.celular);
                        comando.Parameters.AddWithValue("@endereco", informacoesCliente.endereco);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return;
            }

            informacoesCliente.nome = ""; informacoesCliente.email = ""; informacoesCliente.celular = ""; informacoesCliente.endereco = "";
            mensagemSucesso = "Novo cliente adicionado com sucesso!";

            Response.Redirect("/Clientes/Index");
        }
    }
}
