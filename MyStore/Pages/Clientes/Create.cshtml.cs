using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;

namespace MyStore.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String sucessMessage = "";

        public void OnGet()
        {

        }


        public void OnPost() 
        {
            clienteInfo.nome = Request.Form["name"];
            clienteInfo.email = Request.Form["email"];
            clienteInfo.telefone = Request.Form["telephone"];
            clienteInfo.endereco = Request.Form["endereco"];

            // Check for null or empty values
            if (string.IsNullOrEmpty(clienteInfo.nome) ||
                string.IsNullOrEmpty(clienteInfo.email) ||
                string.IsNullOrEmpty(clienteInfo.telefone) ||
                string.IsNullOrEmpty(clienteInfo.endereco))
            {
                errorMessage = "Todos os campos são necessários";
                return;
            }


            //gravar novo cliente na Base de dados
            string connString = "Server = localhost; Database=mystorebd; UserId=root; Password=junho996;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string insertQuery = "INSERT INTO Clientes(nome,email, telefone, endereco)" +
                        "VALUES (@nome,@email, @telefone, @endereco);";

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@nome", clienteInfo.nome);
                        insertCmd.Parameters.AddWithValue("@email", clienteInfo.email);
                        insertCmd.Parameters.AddWithValue("@telefone", clienteInfo.telefone);
                        insertCmd.Parameters.AddWithValue("@endereco", clienteInfo.endereco);

                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            sucessMessage = "Novo cliente adicionado correctamente";
                        }
                        else
                        {
                            errorMessage = "Ocorreu um erro no registo do novo cliente";
                        }
                    }
                }
            } 
            catch (MySqlException ex)
            {
                errorMessage = "Erro do MySql: " + ex.Message;
            } 
            catch (Exception ex)
            {
                errorMessage = "Erro ocorrido: " + ex.Message;
            }
        }
    }
}
