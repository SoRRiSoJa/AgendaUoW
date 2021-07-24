using AgendaUoW.Domain.Models;
using AgendaUoW.Domain.Services;
using AgendaUoW.Resources;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public ContatoController(IContatoService _contatoService, IMapper _mapper)
        {

            this._contatoService = _contatoService ?? throw new ArgumentNullException(nameof(_contatoService));
            this._mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
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
        public async Task<ActionResult<bool>> Excluir(int idContato)
        {
            return Ok(await _contatoService.Excluir(idContato));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoResource>>> Listar()
        {
            return Ok( _mapper.Map<IEnumerable<ContatoResource>>(await _contatoService.Listar()));
        }
        [HttpGet("numero/{numeroTelefone}")]
        public async Task<ActionResult<IEnumerable<ContatoResource>>> ConsultarPorNumero(string numeroTelefone)
        {
            return Ok(_mapper.Map<IEnumerable<ContatoResource>>(await _contatoService.ObterPorNumero(numeroTelefone)));
        }
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<ContatoResource>>> ConsultarPorNome(string nome)
        {
            return Ok(_mapper.Map<IEnumerable<ContatoResource>>(await _contatoService.ObterPorNome(nome)));
        }
    }
}
