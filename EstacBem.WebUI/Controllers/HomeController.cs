using EstacBem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //Service Injection
        private readonly IBolsaoService _bolsaoService;
        private readonly IVeiculoService _veiculoService;
        private readonly IEstadiaService _estadiaService;

        public HomeController(IBolsaoService bolsaoService, IVeiculoService veiculoService, IEstadiaService estadiaService = null)
        {
            _bolsaoService = bolsaoService;
            _veiculoService = veiculoService;
            _estadiaService = estadiaService;
        }

        //Página Inicial
        public async Task<IActionResult> Index()
        {
            //Recupera todos os bolsões
            ViewBag.Bolsoes = _bolsaoService.GetAllAsync().Result.OrderBy(x => x.Nome);

            return await Task.FromResult(View());
        }

        public Task<JsonResult> HasVeiculoEstacionado(string placa)
        {
            string placaToUpper = placa.Trim().ToUpper();

            var veiculoEstacionado = _estadiaService.GetAllAsync().Result
                .Where(x => x.Veiculo.Placa == placa && x.Saida == null).Count() > 0;

            return Task.FromResult(Json(veiculoEstacionado));
        }

        public Task<JsonResult> HasVeiculoCadastrado(string placa)
        {
            string placaToUpper = placa.Trim().ToUpper();

            var veiculoCadastrado = _veiculoService.GetAllAsync().Result
                .Where(x => x.Placa == placaToUpper)
                .Count() > 0;

            return Task.FromResult(Json(veiculoCadastrado));
        }
    }
}
