using consumirAPI.DTO;
using consumirAPI.Models;
using consumirAPI.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace consumirAPI.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IApiService _apiService;

        // Injeção de dependência
        public ProdutoController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produto = await _apiService.GetAll();

            if (produto == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var produto = await _apiService.GetById(id);

            if (produto == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(produto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto novoproduto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos. Verifique os campos.";
                return View(novoproduto);
            }
            try
            {
                var produto = await _apiService.Create(novoproduto);

                if (produto == null)
                {
                    ViewBag.Erro = "Registro não encontrado.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Não foi possível inserir o produto.";
                Console.WriteLine($"Exceção: {ex.Message}");
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var produto = await _apiService.GetById(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Produto produto)
        {
            if (id != produto.Id)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            try
            {
                var produtoaltera = await _apiService.Update(id, produto);

                if (produtoaltera == false)
                {
                    ViewBag.Erro = "Registro não encontrado.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Não foi possível alterar produto.";
                Console.WriteLine($"Exceção: {ex.Message}");
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var produto = await _apiService.GetById(id);

            if (produto == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buscaproduto = await _apiService.GetById(id);

            if (buscaproduto == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            try
            {
                var produtodeleta = await _apiService.Delete(id);

                if (produtodeleta == false)
                {
                    ViewBag.Erro = "Registro não encontrado.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Não foi possível deletar produto.";
                Console.WriteLine($"Exceção: {ex.Message}");
                return View("Error");
            }
 
            return RedirectToAction(nameof(Index));
        }
    }
}
