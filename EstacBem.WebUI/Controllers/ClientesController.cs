using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Infra.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.WebUI.Controllers
{
    public class ClientesController : Controller
    {
        // Service Injection
        private readonly IClienteService _clienteService;
        private readonly IUnitOfWork _uow;

        public ClientesController(IClienteService clienteService, IUnitOfWork unitOfWork = null)
        {
            _clienteService = clienteService;
            _uow = unitOfWork;
        }


        // Pagina inicial
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = _clienteService.GetAllAsync().Result.OrderBy(x => x.Nome);

            return await Task.FromResult(View(items));
        }


        // Cadastro de clientes
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            //Tratando o cliente
            var clienteFormatado = new Cliente();
            clienteFormatado.Nome = cliente.Nome.Trim().ToUpper();
            clienteFormatado.SobreNome = cliente.SobreNome.Trim().ToUpper();
            clienteFormatado.CPF = cliente.CPF.Trim();

            await _clienteService.CreateAsync(clienteFormatado);

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


        // Editar clientes
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var item = await _clienteService.GetByIdAsync(id);
                return View(item);
            }
            catch
            {
                return RedirectToAction("Index", "Clientes");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            //Tratando o cliente
            var clienteFormatado = new Cliente();
            clienteFormatado.Id = cliente.Id;
            clienteFormatado.Nome = cliente.Nome.Trim().ToUpper();
            clienteFormatado.SobreNome = cliente.SobreNome.Trim().ToUpper();
            clienteFormatado.CPF = cliente.CPF.Trim();

            await _clienteService.UpdateAsync(clienteFormatado);

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

        //Remover clientes
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var item = await _clienteService.GetByIdAsync(id);
                return View(item);
            }
            catch
            {
                return RedirectToAction("Index", "Clientes");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemovePost(int id)
        {
            await _clienteService.DeleteAsync(_clienteService.GetByIdAsync(id).Result);

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

        //Retorna se existe algum cliente cadastrado
        public Task<JsonResult> GetClienteByCPF(string cpf)
        {
            var cliente = _clienteService.GetAllAsync().Result.Where(x => x.CPF == cpf).Count() > 0;

            return Task.FromResult(Json(cliente));
        }
    }
}

