using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Infra.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EstacBem.WebUI.Controllers
{
    public class PrecificacoesController : Controller
    {
        //Service Injection
        private readonly IPrecificacaoService _precificacaoService;
        private readonly IUnitOfWork _uow;

        public PrecificacoesController(IPrecificacaoService precificacaoService, IUnitOfWork uow = null)
        {
            _precificacaoService = precificacaoService;
            _uow = uow;
        }


        // Pagina inicial
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _precificacaoService.GetAllAsync();
            return View(items);
        }


        // Cadastro de precificações
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Precificacao precificacao)
        {

            await _precificacaoService.CreateAsync(precificacao);

            try
            {
                await _uow.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                await _uow.RollBack();
                return RedirectToAction("Index");
            }
        }


        // Editar precificações
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _precificacaoService.GetByIdAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Precificacao precificacao)
        {
            await _precificacaoService.UpdateAsync(precificacao);

            try
            {
                await _uow.Commit();
                return RedirectToAction("Index", "Precificacoes");
            }
            catch
            {
                await _uow.RollBack();
                return RedirectToAction("Index", "Precificacoes");
            }
        }

        //Remover precificações
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _precificacaoService.GetByIdAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> RemovePost(int id)
        {
            await _precificacaoService.DeleteAsync(_precificacaoService.GetByIdAsync(id).Result);

            try
            {
                await _uow.Commit();
                return RedirectToAction("Index", "Precificacoes");
            }
            catch
            {
                await _uow.RollBack();
                return RedirectToAction("Index", "Precificacoes");
            }
        }
    }
}
