using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VR.Application.Interfaces;
using VR.Domain.Entities;
using VR.MVC.ViewModels;

namespace VR.MVC.Controllers
{

    public class ClienteController : Controller
    {
        private readonly IClienteAppService _clienteAppService;
        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }
        public ActionResult Index()
        {
            var clientes = _clienteAppService.GetAll();
            var viewModel = Mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
            return View(viewModel);
        }


        public ActionResult Details(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            if (cliente == null) return HttpNotFound();
            var viewModel = Mapper.Map<ClienteViewModel>(cliente);
            return View(viewModel);
        }


        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var cliente = Mapper.Map<Cliente>(viewModel);
                _clienteAppService.Add(cliente);

                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
                return RedirectToAction("Index");
            }

            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ClienteViewModel>(cliente);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var clienteExistente = _clienteAppService.GetById(viewModel.Id);
                if (clienteExistente == null)
                    return HttpNotFound();

                Mapper.Map(viewModel, clienteExistente);

                _clienteAppService.Update(clienteExistente);

                TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

 
        public ActionResult Delete(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            if (cliente == null)
                return HttpNotFound();

            var viewModel = Mapper.Map<ClienteViewModel>(cliente);
            return View(viewModel);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleConfirmed(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            if (cliente != null)
            {
                _clienteAppService.Remove(cliente);
                TempData["Sucesso"] = "Cliente excluído com sucesso!";
            }
            return RedirectToAction("Index");
        }
    }
}
