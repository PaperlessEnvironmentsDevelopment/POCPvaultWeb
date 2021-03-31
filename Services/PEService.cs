using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using POCPvaultWeb.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace POCPvaultWeb.Services
{
    public class PEService
    {
        public readonly IConfiguration _configuration;
        private string _webServiceUrl;
        private string _dbConnection;

        public PEService(IConfiguration configuration)
        {
            _configuration = configuration;
            _webServiceUrl = _configuration.GetSection("WebServiceUrl").Value;
            _dbConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        //----------------------//
        //--Public              //
        //----------------------//

        //Get User Client List
        public List<ClientContract> GetClients(string accessToken, int webUserId)
        {
            try
            {
                var client = new RestClient(_webServiceUrl);
                var request = new RestRequest("PESYS/GetClientList", Method.GET);
                //var apiKey = GetAPIKey(webUserId);
                //request.AddHeader("apikey", apiKey);
                request.AddHeader("provisionkey", accessToken);
                request.AddQueryParameter("WebUserId", webUserId.ToString());

                var response = client.Execute(request).Content;
                var list = JsonConvert.DeserializeObject<List<ClientContract>>(response);

                if (list == null)
                {
                    list = new List<ClientContract>();
                }
                return list;
            }
            catch(Exception ex)
            {
                return new List<ClientContract>();
            }
        }

        //Get User Vaults
        public List<VaultContract> GetWebUserVault(string accessToken, int clientId, int webUserId)
        {
            try
            {
                var client = new RestClient(_webServiceUrl);
                var request = new RestRequest("PESYS/GetWebUserVault", Method.GET);
                //var apiKey = GetAPIKey(webUserId);
                //request.AddHeader("apikey", apiKey);
                request.AddHeader("provisionkey", accessToken);
                request.AddQueryParameter("ClientID", clientId.ToString());
                request.AddQueryParameter("WebUserId", webUserId.ToString());

                var response = client.Execute(request).Content;
                var list = JsonConvert.DeserializeObject<List<VaultContract>>(response);

                if (list == null)
                {
                    list = new List<VaultContract>();
                }

                return list;
            }
            catch(Exception ex)
            {
                return new List<VaultContract>();
            }
        }

        //Get Web User
        public WebUserContract GetWebUser(string accessToken, int clientId, int webUserId)
        {
            try
            {
                var client = new RestClient(_webServiceUrl);
                var request = new RestRequest("PESYS/GetWebUser", Method.GET);
                //var apiKey = GetAPIKey(webUserId);
                //request.AddHeader("apikey", apiKey);
                request.AddHeader("provisionkey", accessToken);
                request.AddQueryParameter("ClientID", clientId.ToString());
                request.AddQueryParameter("WebUserId", webUserId.ToString());

                var response = client.Execute(request).Content;
                var user = JsonConvert.DeserializeObject<List<WebUserContract>>(response);

                if (user == null)
                {
                    user = new List<WebUserContract>();
                }

                return user.FirstOrDefault();
            }
            catch(Exception ex)
            {
                return new WebUserContract();
            }
            
        }

        //Get User Web Applications
        public List<WebUserApplicationContract> GetWebUserApplications(string accessToken, int clientId, int webUserId)
        {
            try
            {
                var client = new RestClient(_webServiceUrl);
                var request = new RestRequest("PESYS/GetWebUserApplication", Method.GET);
                //var apiKey = GetAPIKey(webUserId);
                //request.AddHeader("apikey", apiKey);
                request.AddHeader("provisionkey", accessToken);
                request.AddQueryParameter("ClientID", clientId.ToString());
                request.AddQueryParameter("WebUserId", webUserId.ToString());

                var response = client.Execute(request).Content;
                var list = JsonConvert.DeserializeObject<List<WebUserApplicationContract>>(response);

                if (list == null)
                {
                    list = new List<WebUserApplicationContract>();
                }

                return list;
            }
            catch(Exception ex)
            {
                return new List<WebUserApplicationContract>();
            }
        }

        //----------------------//
        //--Private             //
        //----------------------//

        private string GetAPIKey(int webUserId)
        {
            string apiKey = string.Empty;
            string query = "SELECT APIKey FROM WebUser WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(_dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@ID", webUserId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    apiKey = reader.GetString(0);
                                }
                            }
                        }
                    }
                }
            }

            return apiKey;
        }
    }
}
