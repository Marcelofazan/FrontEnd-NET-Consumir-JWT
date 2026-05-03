using consumirAPI.DTO;
using consumirAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace consumirAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IWebHostEnvironment _env;

        public HomeController(IHttpClientFactory clientFactory, IWebHostEnvironment env)
        {
            _clientFactory = clientFactory;
            _env = env;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Faz o login e obtém o token JWT
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            // 1. Transforma o objeto em uma string JSON manualmente
            var json = JsonSerializer.Serialize(new
            {
                email = loginViewModel.username,
                senha = loginViewModel.password
            });

            var client = _clientFactory.CreateClient("APIClient");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                var token = loginResponse?.Token;

                // Armazenar o token JWT em uma sessão ou cookie para ser usado nas requisições subsequentes
                HttpContext.Session.SetString("JWToken", token);

                // Redirecionar para a página de listagem de produtos
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                // Leia o conteúdo para entender o erro
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro: {response.StatusCode} - {errorContent}");

                ModelState.AddModelError(string.Empty, "Login falhou. Verifique suas credenciais.");
                return View(loginViewModel);
            }
        }

        public async Task<IActionResult> Registrar()
        {
            var client = _clientFactory.CreateClient("APIClient");

            // Adicionar o token JWT no cabeçalho das requisições
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var path = Path.Combine(_env.ContentRootPath, "dadosadmin.json");
            var jsonString = System.IO.File.ReadAllText(path);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/registro", content);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Registro = "Registrado com sucesso.";
            }
            else
            {
                ViewBag.Registro = "Erro ao Registrar.";
            }

            return View("Index");
        }
    }
}
