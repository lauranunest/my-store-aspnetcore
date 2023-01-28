using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace cadastro_loja.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<InformacoesCliente> clientesLista = new List<InformacoesCliente>();

        public void OnGet()
        {
            try
            {
                String stringConexao = "Data Source=.\\SQLEXPRESS2022;Initial Catalog=loja;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    String sql = "SELECT * FROM clientes";
                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                InformacoesCliente informacoesCliente = new InformacoesCliente();
                                informacoesCliente.id = "" + leitor.GetInt32(0);
                                informacoesCliente.nome = leitor.GetString(1);
                                informacoesCliente.email = leitor.GetString(2);
                                informacoesCliente.celular = leitor.GetString(3);
                                informacoesCliente.endereco = leitor.GetString(4);
                                informacoesCliente.atualizacao = leitor.GetDateTime(5).ToString();

                                clientesLista.Add(informacoesCliente);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class InformacoesCliente
    {
        public String id;
        public String nome;
        public String email;
        public String celular;
        public String endereco;
        public String atualizacao;
    }
}
