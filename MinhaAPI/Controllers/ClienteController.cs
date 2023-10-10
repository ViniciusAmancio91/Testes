using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MinhaAPI.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private static List<Cliente> _clientes = new List<Cliente>
    {
        new Cliente { Id = 1, Name = "Cliente 1", Email = "cliente1@example.com" },
        new Cliente { Id = 2, Name = "Cliente 2", Email = "cliente2@example.com" },
    };

        // GET: api/<ClienteController>
        [HttpGet]
        public IActionResult GetClientes()
        {
            return Ok(_clientes);
        }

        // GET api/clientes/{id}
        [HttpGet("{id}")]
        public IActionResult GetClienteById(int id)
        {
            var cliente = _clientes.Find(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
       
        // POST api/<ClienteController>
        [HttpPost]
        public IActionResult CriarCliente([FromBody] Cliente newCliente)
        {
            if (newCliente == null)
            {
                return BadRequest("Dados do cliente inválidos.");
            }

            // Gere um ID único para o novo cliente (por exemplo, com base no último ID usado).
            int novoClienteId = _clientes.Max(c => c.Id) + 1;
            newCliente.Id = novoClienteId;

            _clientes.Add(newCliente);

            // Retorne o novo cliente criado com o status HTTP 201 (Created).
            return CreatedAtAction(nameof(GetClienteById), new { id = novoClienteId }, newCliente);
        }


        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public IActionResult AtualizaCliente(int id, [FromBody] Cliente clienteAtualizado)
        {
            if (clienteAtualizado == null || id != clienteAtualizado.Id)
            {
                return BadRequest("Dados do cliente inválidos.");
            }

            var clienteExistente = _clientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
            {
                return NotFound();
            }

            // Atualize os campos do cliente existente com os dados fornecidos.
            clienteExistente.Name = clienteAtualizado.Name;
            clienteExistente.Email = clienteAtualizado.Email;
            // Atualize outros campos conforme necessário.

            return NoContent(); // Retorna o status HTTP 204 (No Content) para indicar sucesso na atualização.
        }


        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletaCliente(int id)
        {
            var removerCliente = _clientes.FirstOrDefault(c => c.Id == id);

            if (removerCliente == null)
            {
                return NotFound();
            }

            _clientes.Remove(removerCliente);

            return NoContent(); // Retorna o status HTTP 204 (No Content) para indicar sucesso na exclusão.
        }

    }
}

