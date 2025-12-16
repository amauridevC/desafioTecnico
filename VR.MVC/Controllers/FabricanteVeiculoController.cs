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
    public class FabricanteVeiculoController : Controller
    {
        private readonly IFabricanteVeiculoAppService _fabricanteVeiculoAppService;
        public FabricanteVeiculoController(IFabricanteVeiculoAppService fabricanteVeiculoAppService)
        {
            _fabricanteVeiculoAppService = fabricanteVeiculoAppService;
        }

        public ActionResult Index()
        {
            var fabricantes = _fabricanteVeiculoAppService.GetAllComEndereco(); 
            var viewModels = Mapper.Map<IEnumerable<FabricanteVeiculoViewModel>>(fabricantes);
            return View(viewModels);
        
        }

        
        public ActionResult Details(int id)
        {
            var fabricante = _fabricanteVeiculoAppService.GetByIdComEndereco(id); 
            if (fabricante == null) return HttpNotFound();
            var viewModel = Mapper.Map<FabricanteVeiculoViewModel>(fabricante);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FabricanteVeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var fabricante = Mapper.Map<FabricanteVeiculo>(viewModel);
                fabricante.Endereco = Mapper.Map<Endereco>(viewModel.Endereco); 

                _fabricanteVeiculoAppService.Add(fabricante);
                TempData["SuccessMessage"] = "Fabricante criado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao cadastrar: " + ex.Message);
                return View(viewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            var fabricante = _fabricanteVeiculoAppService.GetByIdComEndereco(id); 
            if (fabricante == null) return HttpNotFound();
            var viewModel = Mapper.Map<FabricanteVeiculoViewModel>(fabricante);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FabricanteVeiculoViewModel viewModel)
        {
            if (id != viewModel.Id) return new HttpStatusCodeResult(400);
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var fabricante = _fabricanteVeiculoAppService.GetByIdComEndereco(id);
                if (fabricante == null) return HttpNotFound();

                fabricante.Nome = viewModel.Nome;
                fabricante.Pais = viewModel.Pais;
                fabricante.Telefone = viewModel.Telefone;
                fabricante.Email = viewModel.Email;
                fabricante.AnoFundacao = viewModel.AnoFundacao;
                fabricante.Website = viewModel.Website;

                if (viewModel.Endereco != null)
                {
                    if (fabricante.Endereco == null)
                    {
                        fabricante.Endereco = new Endereco();
                    }

                    fabricante.Endereco.Cep = viewModel.Endereco.Cep;
                    fabricante.Endereco.Rua = viewModel.Endereco.Rua;
                    fabricante.Endereco.Cidade = viewModel.Endereco.Cidade;
                    fabricante.Endereco.Estado = viewModel.Endereco.Estado;

                    fabricante.Endereco.FabricanteVeiculoId = fabricante.Id;
                }

                _fabricanteVeiculoAppService.Update(fabricante);

                TempData["SuccessMessage"] = "Fabricante atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro: " + ex.Message);
                return View(viewModel);
            }
        }




        public ActionResult Delete(int id)
        {
            var fabricante = _fabricanteVeiculoAppService.GetByIdComEndereco(id);
            if (fabricante == null) return HttpNotFound();
            var viewModel = Mapper.Map<FabricanteVeiculoViewModel>(fabricante);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var fabricante = _fabricanteVeiculoAppService.GetById(id);
            if (fabricante != null)
            {
                _fabricanteVeiculoAppService.Remove(fabricante);
                TempData["SuccessMessage"] = "Fabricante de veículo excluído com sucesso!";
            }

            return RedirectToAction("Index");
        }
    }
}
