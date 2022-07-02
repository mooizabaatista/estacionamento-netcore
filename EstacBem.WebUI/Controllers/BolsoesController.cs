
using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Infra.UnitOfWork;
using EstacBem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.WebUI.Controllers
{
    public class BolsoesController : Controller
    {
        // Service Injection
        private readonly IBolsaoService _bolsaoService;
        private readonly IUnitOfWork _uow;

        public BolsoesController(IBolsaoService bolsaoService, IUnitOfWork unitOfWork = null)
        {
            _bolsaoService = bolsaoService;
            _uow = unitOfWork;
        }


        // Pagina inicial
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _bolsaoService.GetAllAsync();
            return View(items);
        }


        // Cadastro de bolsoes
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bolsao bolsao)
        {
            await _bolsaoService.CreateAsync(bolsao);

            try
            {
                await _uow.Commit();
            }
            catch
            {

                await _uow.RollBack();
            }

            return RedirectToAction("Index", "Bolsoes");
        }

        // Editar bolsoes
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var item = await _bolsaoService.GetByIdAsync(id);
                return View(item);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Bolsao bolsao)
        {
            await _bolsaoService.UpdateAsync(bolsao);

            try
            {
                await _uow.Commit();
            }
            catch
            {

                await _uow.RollBack();
            }

            return RedirectToAction("Index");
        }

        //Remover bolsoes
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var item = await _bolsaoService.GetByIdAsync(id);
                return View(item);
            }
            catch 
            {
                return RedirectToAction("Index", "Bolsoes");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RemovePost(int id)
        {
            await _bolsaoService.DeleteAsync(_bolsaoService.GetByIdAsync(id).Result);

            try
            {
                await _uow.Commit();
            }
            catch
            {
                await _uow.RollBack();
            }

            return RedirectToAction("Index", "Bolsoes");
        }

        public Task<JsonResult> GetBolsaoInfo(int id)
        {
            var options = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };


            var bolsaoSelecionado = _bolsaoService.GetAllAsync().Result.Where(x => x.Id == id).FirstOrDefault();

            var bolsao = new BolsaoViewModel();
            bolsao.Nome = bolsaoSelecionado.Nome;
            bolsao.Qtd = bolsaoSelecionado.QtdVagas;
            bolsao.Id = bolsaoSelecionado.Id;


            return Task.FromResult(Json(bolsao));
        }
    }
}
