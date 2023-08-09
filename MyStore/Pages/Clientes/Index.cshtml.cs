using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyStore.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClienteInfo> listaClientes = new List<ClienteInfo>();
        public String errorMessage = "";

        public void OnGet()
        {

            string connString = "Server = localhost; Database = mystorebd; userId = root; password = junho996;";
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string query = "Select * FROM Clientes;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClienteInfo clienteInfo = new ClienteInfo();
                                clienteInfo.idCliente = " " + reader.GetInt32(0);
                                clienteInfo.nome = reader.GetString(1);
                                clienteInfo.email = reader.GetString(2);
                                clienteInfo.telefone = reader.GetString(3);
                                clienteInfo.endereco = reader.GetString(4);
                                clienteInfo.dataCriacao = reader.GetDateTime(5).ToString();

                                listaClientes.Add(clienteInfo);
                               
                            }
                        }
                    }
                }
            } 
            catch (MySqlException ex) { 
                errorMessage = "Erro de MySQL: " + ex.Message;
            }
            catch(Exception ex)
            {
                errorMessage = "Erro ocorrdo: " + ex.Message;
            }
        }
    }

    public class ClienteInfo
    {
        public string idCliente;
        public string nome;
        public string email;
        public string telefone;
        public string endereco;
        public string dataCriacao;
    }
}
