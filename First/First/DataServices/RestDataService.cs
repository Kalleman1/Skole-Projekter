﻿using First.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Debug = System.Diagnostics.Debug;

namespace First.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5298" : "https://localholst:7298";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }; 
        }

        public async Task AddToDoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine($"Damn, no internet connection!");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Du har lavet et Todo!!!"); 
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Damn, exception {ex.Message}");
            }
        }

        public async Task DeleteToDoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine($"Damn, no internet connection!");
                return;
            }

            try
            {
                HttpResponseMessage respone = await _httpClient.DeleteAsync($"{_url}/todo{id}"); 
            }

            catch(Exception ex)
            {
                Debug.WriteLine($"Damn, exception {ex.Message}");
            }

            return; 
        }

        public async Task<List<ToDo>> GetAllToDosAsync()
        {
            List<ToDo> todos = new List<ToDo>(); 

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine($"Damn, no internet connection!");
            }

            try
            {
                HttpResponseMessage respone = await _httpClient.GetAsync($"{_url}/todo");

                if (respone.IsSuccessStatusCode)
                {
                    string content = await respone.Content.ReadAsStringAsync();

                    todos = JsonSerializer.Deserialize<List<ToDo>>(content);
                }
                else
                {
                    Debug.WriteLine($"Damn, non 2xx http respone message.");
                }
               
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Damn, exception {ex.Message}");
            }
            return todos;
        }

        public async Task UpdateToDoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine($"Damn, no internet connection!");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{toDo.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Du har lavet et Todo!!!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Damn, exception {ex.Message}");
            }
        }
    }
}
