using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VR.Application.Interfaces;
using VR.Domain.Entities;
using VR.MVC.Filters;
using VR.MVC.ViewModels;

namespace VR.MVC.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendaAppService _vendaAppService;
        private readonly IVeiculoAppService _veiculoAppService;
        private readonly IClienteAppService _clienteAppService;
        private readonly IFabricanteVeiculoAppService _fabricanteVeiculoAppService;

        public VendaController(
            IVendaAppService vendaAppService,
            IVeiculoAppService veiculoAppService,
            IClienteAppService clienteAppService,
            IFabricanteVeiculoAppService fabricanteVeiculoAppService)
        {
            _vendaAppService = vendaAppService;
            _veiculoAppService = veiculoAppService;
            _clienteAppService = clienteAppService;
            _fabricanteVeiculoAppService = fabricanteVeiculoAppService;
        }

        [AutorizacaoCustomizada(Roles = "Administrador, Gerente")]
        public ActionResult RelatorioMensal(int? mes = null, int? ano = null)
        {
            int mesAtual = mes ?? DateTime.Today.Month;
            int anoAtual = ano ?? DateTime.Today.Year;

            var relatorioDto = _vendaAppService.ObterRelatorioMensal(mesAtual, anoAtual);
            var viewModel = Mapper.Map<RelatorioVendasMensalViewModel>(relatorioDto);

            ViewBag.Mes = mesAtual;
            ViewBag.Ano = anoAtual;

            return View(viewModel);
        }

        public ActionResult Index()
        {
            var vendas = _vendaAppService.ObterTodasParaListagem();
            var viewModels = Mapper.Map<IEnumerable<VendaListaViewModel>>(vendas);
            return View(viewModels);
        }

        public ActionResult Details(int id)
        {
            var vendaDto = _vendaAppService.ObterTodasParaListagem()
                                   .FirstOrDefault(v => v.Id == id);

            if (vendaDto == null) return HttpNotFound();

            var viewModel = Mapper.Map<VendaListaViewModel>(vendaDto);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            CarregarViewBags();
            return View(new VendaViewModel { DataVenda = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarViewBags(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }

            try
            {
                var venda = Mapper.Map<Venda>(viewModel);
                _vendaAppService.Add(venda);

                TempData["SuccessMessage"] = "Venda criada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                string mensagem;

                if (ex.Message == "DATA_VENDA_NO_FUTURO")
                    mensagem = "Não é permitido cadastrar venda com data futura.";
                else if (ex.Message == "PRECO_VENDA_INVALIDO")
                    mensagem = "O preço de venda não pode ser menor que o preço do veículo.";
                else
                    mensagem = "Erro ao realizar a venda.";

                ModelState.AddModelError("", mensagem);
                CarregarViewBags(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao realizar venda: " + ex.Message);
                CarregarViewBags(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }
        }

       public ActionResult Edit(int id)
        {
            var venda = _vendaAppService.ObterPorId(id);

            if (venda == null)
                return HttpNotFound();

            var viewModel = Mapper.Map<VendaViewModel>(venda);

            ViewBag.CurrentClienteNome = venda.Cliente != null ? venda.Cliente.Nome : "(Cliente não disponível)";
            ViewBag.CurrentVeiculoModelo = venda.Veiculo != null ? venda.Veiculo.Modelo : "(Veículo não disponível)";
            ViewBag.CurrentFabricanteNome = venda.Veiculo?.Fabricante != null ? venda.Veiculo.Fabricante.Nome : "(Fabricante não disponível)";
            ViewBag.CurrentDataVenda = venda.DataVenda.ToString("dd/MM/yyyy");
  

            CarregarViewBags(
                viewModel.FabricanteVeiculoId,
                viewModel.VeiculoId,
                viewModel.ClienteId
            );

            return View(viewModel);
        }


        [HttpPost]
        [AutorizacaoCustomizada(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarViewBags(viewModel.FabricanteVeiculoId, viewModel.VeiculoId, viewModel.ClienteId);
                return View(viewModel);
            }

            try
            {
                var venda = Mapper.Map<Venda>(viewModel);
                _vendaAppService.Update(venda);
                TempData["SuccessMessage"] = "Venda atualizada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                string mensagem;
                if (ex.Message == "DATA_VENDA_NO_FUTURO")
                    mensagem = "Não é permitido cadastrar venda com data futura.";
                else if (ex.Message == "PRECO_VENDA_INVALIDO")
                    mensagem = "O preço de venda não pode ser menor que o preço do veículo.";
                else
                    mensagem = "Erro ao atualizar a venda.";

                ModelState.AddModelError("", mensagem);
                CarregarViewBags(viewModel.FabricanteVeiculoId, viewModel.VeiculoId, viewModel.ClienteId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao atualizar venda: " + ex.Message);
                CarregarViewBags(viewModel.FabricanteVeiculoId, viewModel.VeiculoId, viewModel.ClienteId);
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var vendaDto = _vendaAppService.ObterTodasParaListagem()
                               .FirstOrDefault(v => v.Id == id);

            if (vendaDto == null)
            {
                TempData["ErrorMessage"] = "Venda não encontrada.";
                return RedirectToAction("Index");
            }

            var viewModel = Mapper.Map<VendaListaViewModel>(vendaDto); 
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutorizacaoCustomizada(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var venda = _vendaAppService.ObterPorId(id);
                if (venda == null)
                {
                    TempData["ErrorMessage"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }
                _vendaAppService.Remove(venda);

                TempData["SuccessMessage"] = "Venda excluída com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir venda: " + ex.Message;
                return RedirectToAction("Index");
            }
        }




        private void CarregarViewBags(int? fabricanteId = null, int? veiculoId = null, int? clienteId = null)
        {
            var fabricantes = _fabricanteVeiculoAppService.GetAll();
            ViewBag.FabricanteVeiculoId = new SelectList(fabricantes, "Id", "Nome", fabricanteId);

            if (fabricanteId.HasValue)
            {
                var veiculos = _veiculoAppService.BuscarPorFabricante(fabricanteId.Value);
                ViewBag.VeiculoId = new SelectList(veiculos, "Id", "Modelo", veiculoId);
            }
            else
            {
                ViewBag.VeiculoId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Modelo");
            }

            var clientes = _clienteAppService.GetAll();
            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nome", clienteId);
        }

        public JsonResult VeiculosPorFabricante(int fabricanteId)
        {
            var veiculos = _veiculoAppService.BuscarPorFabricante(fabricanteId)
                .Select(v => new { id = v.Id, text = v.Modelo + " - " + v.AnoFabricacao.Year })
                .ToList();

            return Json(veiculos, JsonRequestBehavior.AllowGet);
        }

        private void TempDataSuccess(string mensagem)
        {
            TempData["SuccessMessage"] = mensagem;
        }
    }
}
