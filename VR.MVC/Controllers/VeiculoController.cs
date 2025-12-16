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
    [AutorizacaoCustomizada(Roles = "Administrador")]
    public class VeiculoController : Controller
    {
        private readonly IVeiculoAppService _veiculoAppService;
        private readonly IFabricanteVeiculoAppService _fabricanteVeiculoAppService;
        public VeiculoController(IVeiculoAppService veiculoAppService, IFabricanteVeiculoAppService fabricanteVeiculoAppService)
        {
            _veiculoAppService = veiculoAppService;
            _fabricanteVeiculoAppService = fabricanteVeiculoAppService;
        }

        public ActionResult Index()
        {
            var veiculos = _veiculoAppService.GetAllComFabricante(); 
            var viewModels = Mapper.Map<IEnumerable<VeiculoViewModel>>(veiculos);
            return View(viewModels);
        }

        public ActionResult Details(int id)
        {
            var veiculo = _veiculoAppService.GetByIdComFabricante(id);
            if (veiculo == null) return HttpNotFound();

            var viewModel = Mapper.Map<VeiculoViewModel>(veiculo);
            return View(viewModel);
        }


        public ActionResult Create()
        {
            ViewBag.FabricanteVeiculoId = new SelectList(
                _fabricanteVeiculoAppService.GetAll(), "Id", "Nome");

            return View(new VeiculoViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarViewBagFabricante(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }

            try
            {
                var veiculo = Mapper.Map<Veiculo>(viewModel);
                _veiculoAppService.Add(veiculo);

                TempData["SuccessMessage"] = "Veículo cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex) 
            {
                string mensagem;

                if (ex.Message == "PRECO_ZERADO_OU_NEGATIVO")
                    mensagem = "O preço do veículo não pode ser zero ou negativo.";

                else if (ex.Message == "PRECO_MAXIMO")
                    mensagem = "O preço do veículo não pode exceder R$ 10.000.000,00.";
                else
                    mensagem = "Erro ao atualizar veículo: " + ex.Message;

                ModelState.AddModelError("", mensagem);
                CarregarViewBagFabricante(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro inesperado ao cadastrar veículo: " + ex.Message);
                CarregarViewBagFabricante(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }
        }

        private void CarregarViewBagFabricante(int? selectedId = null)
        {
            ViewBag.FabricanteVeiculoId = new SelectList(
                _fabricanteVeiculoAppService.GetAll(), "Id", "Nome", selectedId);
        }

        public ActionResult Edit(int id)
        {
            var veiculo = _veiculoAppService.GetByIdComFabricante(id);
            if (veiculo == null) return HttpNotFound();

            var viewModel = Mapper.Map<VeiculoViewModel>(veiculo);
            ViewBag.FabricanteVeiculoId = new SelectList(
                _fabricanteVeiculoAppService.GetAll(), "Id", "Nome", viewModel.FabricanteVeiculoId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VeiculoViewModel viewModel)
        {
            if (id != viewModel.Id) return new HttpStatusCodeResult(400);

            if (!ModelState.IsValid)
            {
                CarregarViewBagFabricante(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }

            try
            {
                var veiculo = _veiculoAppService.GetById(id);
                Mapper.Map(viewModel, veiculo);
                _veiculoAppService.Update(veiculo);

                TempData["SuccessMessage"] = "Veículo atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex) 
            {
                string mensagem;

                if(ex.Message == "PRECO_ZERADO_OU_NEGATIVO")
                    mensagem = "O preço do veículo não pode ser zero ou negativo.";

                else if (ex.Message == "PRECO_MAXIMO")
                    mensagem = "O preço do veículo não pode exceder R$ 10.000.000,00.";
                else
                    mensagem = "Erro ao atualizar veículo: " + ex.Message;

                ModelState.AddModelError("", mensagem);
                CarregarViewBagFabricante(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao atualizar veículo: " + ex.Message);
                CarregarViewBagFabricante(viewModel.FabricanteVeiculoId);
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var veiculo = _veiculoAppService.GetByIdComFabricante(id);
            if (veiculo == null) return HttpNotFound();

            var viewModel = Mapper.Map<VeiculoViewModel>(veiculo);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var veiculo = _veiculoAppService.GetById(id);
                if (veiculo != null)
                {
                    _veiculoAppService.Remove(veiculo);
                    TempData["SuccessMessage"] = "Veículo excluído com sucesso!";
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
