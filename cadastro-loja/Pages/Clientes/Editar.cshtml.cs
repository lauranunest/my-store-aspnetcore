using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace cadastro_loja.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        public InformacoesCliente informacoesCliente = new InformacoesCliente();
        public String mensagemErro = "";
        public String mensagemSucesso = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String stringConexao = "Data Source=.\\SQLEXPRESS2022;Initial Catalog=loja;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    String sql = "SELECT * FROM clientes WHERE id=@id";

                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {

                            if (leitor.Read())
                            {
                                informacoesCliente.id = "" + leitor.GetInt32(0);
                                informacoesCliente.nome = leitor.GetString(1);
                                informacoesCliente.email = leitor.GetString(2);
                                informacoesCliente.celular = leitor.GetString(3);
                                informacoesCliente.endereco = leitor.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
            }
        }



        public void OnPost()
        {
            informacoesCliente.id = Request.Form["id"];
            informacoesCliente.nome = Request.Form["nome"];
            informacoesCliente.email = Request.Form["email"];
            informacoesCliente.celular = Request.Form["celular"];
            informacoesCliente.endereco = Request.Form["endereco"];

            if (informacoesCliente.id.Length == 0 || informacoesCliente.nome.Length == 0
                || informacoesCliente.email.Length == 0 || informacoesCliente.celular.Length == 0
                || informacoesCliente.endereco.Length == 0)
            {
                mensagemErro = "Todos os campos são necessários";
                return;
            }

            try
            {
                String stringConexao = "Data Source=.\\SQLEXPRESS2022;Initial Catalog=loja;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    String sql = "UPDATE clientes " +
                        "SET nome=@nome, email=@email, celular=@celular, endereco=@endereco " +
                        "WHERE id=@id";

                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", informacoesCliente.nome);
                        comando.Parameters.AddWithValue("@email", informacoesCliente.email);
                        comando.Parameters.AddWithValue("@celular", informacoesCliente.celular);
                        comando.Parameters.AddWithValue("@endereco", informacoesCliente.endereco);
                        comando.Parameters.AddWithValue("@id", informacoesCliente.id);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}
