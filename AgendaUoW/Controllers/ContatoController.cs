using AgendaUoW.Domain.Models;
using AgendaUoW.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaUoW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        public ContatoController(IContatoService _contatoService)
        {

            this._contatoService = _contatoService ?? throw new ArgumentNullException(nameof(_contatoService));
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> Salvar([FromBody] Contato contato)
        {
            return Ok(await _contatoService.Salvar(contato));
        }
        [HttpPut("{idContato}")]
        public async Task<ActionResult<Contato>> Editar(int idContato, [FromBody] Contato contato)
        {
            return Ok(await _contatoService.Editar(idContato, contato));
        }
        [HttpDelete("{idAgenda}")]
        public async Task<ActionResult<Contato>> Excluir(int idContato)
        {
            return Ok(await _contatoService.Excluir(idContato));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> Listar()
        {
            return Ok(await _contatoService.Listar());
        }
        [HttpGet("numero/{numeroTelefone}")]
        public async Task<ActionResult<IEnumerable<Contato>>> ConsultarPorNumero(string numeroTelefone)
        {
            return Ok(await _contatoService.ObterPorNumero(numeroTelefone));
        }
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Contato>>> ConsultarPorNome(string nome)
        {
            return Ok(await _contatoService.ObterPorNome(nome));
        }
    }
}
