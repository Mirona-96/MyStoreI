using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MyStore.Pages.Clientes
{
    public class EditModel : PageModel
    {

        public ClienteInfo clienteInfo = new ClienteInfo();
        public string errorMessage = string.Empty;
        public string sucessMessage = string.Empty;
        public void OnGet()
        {
            String idCliente = Request.Query["idCliente"];

            string connString = "Server = localhost; Database = mystorebd; userId = root; password = junho996;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string query = "Select * FROM Clientes WHERE idCliente = @idCliente;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clienteInfo.idCliente = " " + reader.GetInt32(0);
                                clienteInfo.nome = reader.GetString(1);
                                clienteInfo.email = reader.GetString(2);
                                clienteInfo.telefone = reader.GetString(3);
                                clienteInfo.endereco = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                errorMessage = "Erro de MySQL: " + ex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = "Erro ocorrido: " + ex.Message;
            }
        }

        public void OnPost()
        {
            // Retrieve form data
            clienteInfo.idCliente = Request.Form["idCliente"];
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


            string connString = "Server=localhost;Database=mystorebd;UserId=root;Password=junho996;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string updateQuery = "UPDATE Clientes " +
                                        "SET nome = @nome, email = @email, telefone = @telefone, endereco = @endereco " +
                                        "WHERE idCliente = @idCliente;";

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@nome", clienteInfo.nome);
                        updateCmd.Parameters.AddWithValue("@email", clienteInfo.email);
                        updateCmd.Parameters.AddWithValue("@telefone", clienteInfo.telefone);
                        updateCmd.Parameters.AddWithValue("@endereco", clienteInfo.endereco);
                        updateCmd.Parameters.AddWithValue("@idCliente", clienteInfo.idCliente);

                        updateCmd.ExecuteNonQuery();
/*                        Console.WriteLine("Update Query: " + updateCmd.CommandText);
                        foreach (MySqlParameter param in updateCmd.Parameters)
                        {
                            errorMessage = ($"{param.ParameterName}: {param.Value}");
                           // Console.WriteLine($"{param.ParameterName}: {param.Value}");
                        }
*/
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            sucessMessage = "Dados do cliente atualizados com sucesso";
                        }
                        else
                        {
                            errorMessage = "Falha ao atualizar os dados do cliente";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                errorMessage = "Erro do MySQL: " + ex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = "Erro ocorrido: " + ex.Message;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}

