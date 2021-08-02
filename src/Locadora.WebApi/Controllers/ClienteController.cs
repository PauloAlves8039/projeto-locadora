using Locadora.Dados;
using Locadora.Dominio;
using Locadora.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly RepositorioCliente _repositorioCliente;
        private readonly UnitOfWork _unitOfWork;

        public ClienteController(ILogger<ClienteController> logger, RepositorioCliente repositorioCliente, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _repositorioCliente = repositorioCliente;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult CriarCliente(ClienteDto clienteDto) 
        {
            try
            {
                var cliente = new Cliente();
                cliente.Nome = clienteDto.Nome;
                cliente.DataNascimento = clienteDto.DataNascimento;
                cliente.Cpf = clienteDto.Cpf;

                _unitOfWork.IniciarTransacao();
                _repositorioCliente.Salvar(cliente);
                _unitOfWork.Commit();
                
                return CreatedAtAction(nameof(CriarCliente), cliente.Id);
            }
            catch (Exception ex) 
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao adicionar cliente: ");
            }
        }
    }
}
