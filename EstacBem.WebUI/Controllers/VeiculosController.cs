using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Infra.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.WebUI.Controllers
{
    public class VeiculosController : Controller
    {
        //Service Injection
        private readonly IVeiculoService _veiculoService;
        private readonly IClienteService _clienteService;
        private readonly IUnitOfWork _uow;

        public VeiculosController(IVeiculoService veiculoService, IClienteService clienteService, IUnitOfWork uow = null)
        {
            _veiculoService = veiculoService;
            _clienteService = clienteService;
            _uow = uow;
        }

        // Página inicial
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = _veiculoService.GetAllAsync().Result.OrderBy(x => x.Modelo).OrderBy(x => x.Marca);
            return await Task.FromResult(View(items));
        }

        // Página cadastrar
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = new SelectList(await _clienteService.GetAllAsync(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            // Tratando o veículo
            Veiculo veiculoFormatado = new Veiculo();
            veiculoFormatado.ClienteId = veiculo.ClienteId;
            veiculoFormatado.Marca = veiculo.Marca.ToUpper();
            veiculoFormatado.Modelo = veiculo.Modelo.ToUpper();
            veiculoFormatado.Cor = veiculo.Cor.ToUpper();
            veiculoFormatado.Placa = veiculo.Placa.ToUpper();

            await _veiculoService.CreateAsync(veiculoFormatado);

            try
            {
                await _uow.Commit();
                return RedirectToAction("Index", "Veiculos");
            }
            catch
            {
                await _uow.RollBack();
                return RedirectToAction("Index", "Veiculos");
            }
        }

        // Página editar
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ViewBag.Clientes = new SelectList(_clienteService.GetAllAsync().Result, "Id", "Nome");
                var entity = await _veiculoService.GetByIdAsync(id);
                return View(entity);
            }
            catch
            {
                return RedirectToAction("Index", "Veiculos");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Veiculo veiculo)
        {
            // Tratando o veículo
            Veiculo veiculoFormatado = new Veiculo();
            veiculoFormatado.Id = veiculo.Id;
            veiculoFormatado.ClienteId = veiculo.ClienteId;
            veiculoFormatado.Marca = veiculo.Marca.ToUpper();
            veiculoFormatado.Modelo = veiculo.Modelo.ToUpper();
            veiculoFormatado.Cor = veiculo.Cor.ToUpper();
            veiculoFormatado.Placa = veiculo.Placa.ToUpper();

            await _veiculoService.UpdateAsync(veiculoFormatado);

            try
            {
                await _uow.Commit();
                return RedirectToAction("Index", "Veiculos");
            }
            catch
            {
                await _uow.RollBack();
                return RedirectToAction("Index", "Veiculos");
            }
        }

        // Página remover
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var entity = await _veiculoService.GetByIdAsync(id);
                return View(entity);
            }
            catch
            {
                return RedirectToAction("Index", "Veiculos");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemovePost(int id)
        {
            await _veiculoService.DeleteAsync(_veiculoService.GetByIdAsync(id).Result);

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

        public Task<JsonResult> GetVeiculoByPlaca(string placa)
        {
            var veiculo = _veiculoService.GetAllAsync().Result.Where(x => x.Placa == placa.ToUpper()).Count() > 0;

            return Task.FromResult(Json(veiculo));
        }
    }
}
