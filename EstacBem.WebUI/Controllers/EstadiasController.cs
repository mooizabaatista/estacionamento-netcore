using EstacBem.Application.Interfaces;
using EstacBem.Domain.Entities;
using EstacBem.Infra.UnitOfWork;
using EstacBem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstacBem.WebUI.Controllers
{
    public class EstadiasController : Controller
    {
        // Inject Services
        private readonly IEstadiaService _estadiaService;
        private readonly IVeiculoService _veiculoservice;
        private readonly IBolsaoService _bolsaoService;
        private readonly IStatusService _statusService;
        private readonly IPrecificacaoService _precificacaoService;
        private readonly IClienteService _clienteService;
        private readonly IUnitOfWork _uow;


        public EstadiasController(IEstadiaService estadiaService, IVeiculoService veiculoservice, IBolsaoService bolsaoService, IStatusService statusService = null, IPrecificacaoService precificacaoService = null, IClienteService clienteService = null, IUnitOfWork uow = null)
        {
            _estadiaService = estadiaService;
            _veiculoservice = veiculoservice;
            _bolsaoService = bolsaoService;
            _statusService = statusService;
            _precificacaoService = precificacaoService;
            _clienteService = clienteService;
            _uow = uow;
        }

        // Pagina Inicial
        public async Task<IActionResult> Index()
        {
            var estadias = await _estadiaService.GetAllAsync();
            List<EstadiaViewModel> estadiasViewModel = new List<EstadiaViewModel>();

            foreach (var estadia in estadias)
            {
                string tempoTotalFormatado = "";


                if (estadia.Saida != null)
                {
                    TimeSpan resultadoTempoTotal = estadia.Saida.Value.Subtract(estadia.Entrada);
                    tempoTotalFormatado = $"{resultadoTempoTotal.Hours}:{resultadoTempoTotal.Minutes}";
                }

                estadiasViewModel.Add(new EstadiaViewModel
                {
                    Bolsao = estadia.Bolsao,
                    BolsaoId = estadia.BolsaoId,
                    Data = estadia.Data,
                    Entrada = estadia.Entrada,
                    Id = estadia.Id,
                    Saida = estadia.Saida,
                    Status = estadia.Status,
                    StatusId = estadia.StatusId,
                    ValorDemais = estadia.ValorDemais,
                    ValorPrimeira = estadia.ValorPrimeira,
                    ValorTotal = estadia.ValorTotal,
                    Veiculo = estadia.Veiculo,
                    VeiculoId = estadia.VeiculoId,
                    TempoTotal = tempoTotalFormatado
                });
            }

            return View(estadiasViewModel);
        }

        // Página Cadastrar
        [HttpGet]
        public async Task<IActionResult> Create(string placa)
        {
            // Tratando a placa recebida
            var placaFormatada = placa.Trim().ToUpper();

            // Obtendo o veículo a partir da placa
            var veiculo = _veiculoservice.GetVeiculoByPlaca(placaFormatada);

            if (veiculo == null)
            {
                return await Task.FromResult(RedirectToAction("Index", "Home"));
            }

            //Informações do veículo ( APENAS PARA INFORMATIVO NA TELA )
            ViewBag.Veiculo = veiculo;

            //Obtendo o cliente
            veiculo.Cliente = _clienteService.GetByIdAsync(veiculo.ClienteId).Result;

            // Obtendo a precificação atual
            var precificacao = _precificacaoService.GetAllAsync().Result
                .Where(x => x.DataHora < DateTime.Now)
                .OrderByDescending(x => x.DataHora)
                .FirstOrDefault();

            //Criando uma estadia em branco e preenchendo com as informações agregadas
            var estadia = new Estadia();
            estadia.VeiculoId = veiculo.Id;
            estadia.ValorPrimeira = precificacao.ValorPrimeira;
            estadia.ValorDemais = precificacao.ValorDemais;


            //Select Lists para os bolsões e status do pagamento da estadia
            ViewBag.Bolsoes = new SelectList(_bolsaoService.GetAllAsync().Result, "Id", "Nome");
            ViewBag.Status = new SelectList(_statusService.GetAllAsync().Result.Where(x => x.Tipo == "Pagamento"), "Id", "Nome");


            //Retornando a estadia para a view
            return await Task.FromResult(View(estadia));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Estadia estadia)
        {
            //Recupera o veículo informado no cadastro
            var veiculo = await _veiculoservice.GetByIdAsync(estadia.VeiculoId);

            //Cadastra a estadia
            estadia.Entrada = DateTime.Now;
            await _estadiaService.CreateAsync(estadia);

            var bolsaoSelecionado = _bolsaoService.GetByIdAsync(estadia.BolsaoId).Result;
            bolsaoSelecionado.QtdVagas -= 1;

            try
            {
                await _uow.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                await _uow.RollBack();
                return RedirectToAction("Index", "Estadias");
            }
        }



        //Atulizando e fazendo resumo da estadia
        [HttpPost]
        public async Task<bool> FecharEstadia(int estadiaId)
        {
            var estadiaAberta = _estadiaService.GetByIdAsync(estadiaId).Result;
            estadiaAberta.Saida = DateTime.Now;
            await _estadiaService.UpdateAsync(estadiaAberta);
            await _uow.Commit();

            //Calculando o valor da estadia
            var valorPrimeiraHora = estadiaAberta.ValorPrimeira;
            var valorDemais = estadiaAberta.ValorDemais;
            var valorPorMinuto = Math.Round((valorDemais * 100) / 60, 2);
            var entrada = estadiaAberta.Entrada;
            var saida = estadiaAberta.Saida;

            TimeSpan estadiaTotal = saida.Value.Subtract(entrada);

            var QtdMinutos = estadiaTotal.TotalMinutes;
            var valorPagar = 0m;

            //Abater a primeira hora
            if (QtdMinutos > 60)
            {
                valorPagar += valorPrimeiraHora;
                QtdMinutos -= 60;

                //Calcular valor restante
                var valorRestante = Math.Round(((decimal)QtdMinutos * valorPorMinuto) / 100, 2);

                // Atribuindo o valor restante ao pagamento
                valorPagar += valorRestante;

                //Atribuindo o valor total
                estadiaAberta.ValorTotal = valorPagar;
            }
            else
            {
                valorPagar += valorPrimeiraHora;
                estadiaAberta.ValorTotal = valorPagar;
            }


            // Devolvendo a vaga para o bolsao
            var bolsaoSelecionado = _bolsaoService.GetByIdAsync(estadiaAberta.BolsaoId).Result;
            bolsaoSelecionado.QtdVagas += 1;

            try
            {
                await _estadiaService.UpdateAsync(estadiaAberta);
                await _bolsaoService.UpdateAsync(bolsaoSelecionado);
                await _uow.Commit();
            }
            catch
            {
                await _uow.RollBack();
            }

            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Total(int estadiaId)
        {
            string tempoTotalFormatado = "";

            var estadia = _estadiaService.GetByIdAsync(estadiaId).Result;

            if (estadia.Saida != null)
            {
                TimeSpan resultadoTempoTotal = estadia.Saida.Value.Subtract(estadia.Entrada);
                tempoTotalFormatado = $"{resultadoTempoTotal.Hours}:{resultadoTempoTotal.Minutes}";
            }

            EstadiaViewModel estadiaViewModel = new EstadiaViewModel
            {
                Bolsao = estadia.Bolsao,
                BolsaoId = estadia.BolsaoId,
                Data = estadia.Data,
                Status = estadia.Status,
                Entrada = estadia.Entrada,
                Id = estadia.Id,
                Saida = estadia.Saida,
                StatusId = estadia.StatusId,
                TempoTotal = tempoTotalFormatado,
                ValorDemais = estadia.ValorDemais,
                ValorPrimeira = estadia.ValorPrimeira,
                ValorTotal = estadia.ValorTotal,
                Veiculo = estadia.Veiculo,
                VeiculoId = estadia.VeiculoId
            };

            ViewBag.Status = new SelectList(_statusService.GetAllAsync().Result.Where(x => x.Tipo == "Pagamento"), "Id", "Nome");

            return View(estadiaViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Finalizar(int estadiaId, int statusId)
        {
            var estadia = _estadiaService.GetByIdAsync(estadiaId).Result;

            estadia.StatusId = statusId;

            await _estadiaService.UpdateAsync(estadia);

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



        public Task<JsonResult> VerificaSaida(string placa)
        {
            placa.Trim().ToUpper();
            var veiculo = _estadiaService.GetAllAsync().Result.Where(x => x.Veiculo.Placa == placa).Where(x => x.Saida != null).Count() > 0;

            return Task.FromResult(Json(veiculo));
        }


    }
}
