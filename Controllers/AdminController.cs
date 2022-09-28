using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AvaliacaoLP3.Models;
using AvaliacaoLP3.ViewModels;

namespace AvaliacaoLP3.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public static List<LojaViewModel> lojas = new List<LojaViewModel>();

    public AdminController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult GerenciamentoLojas(){
        return View(lojas);
    }

    public IActionResult DetalheLoja(int id){       
        return View(lojas[id]);
    }

    public IActionResult CadastrarLoja(){   
        return View();
    }

    public IActionResult Cadastramento([FromForm] string nome, [FromForm] string email, [FromForm] string categoria, [FromForm] string descricao, [FromForm] int piso){
        
        foreach (var loja in lojas)
        {
            if(nome == loja.Nome){
                ViewData["Nome"] = nome;
                return View();
            }
        }        

        LojaViewModel estabelecimento = new LojaViewModel(lojas.Count(), piso, nome, descricao, categoria, email);
        lojas.Add(estabelecimento);
        return View("CadastrarLoja");
    }

    public IActionResult Deletar(int id){
        
       for(int i = 0; i < lojas.Count(); i++){
         if(lojas[i].Id == id){
            lojas.RemoveAt(i);
         }
       }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}