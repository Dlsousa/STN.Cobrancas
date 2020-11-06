using Microsoft.AspNetCore.Mvc;
using STN.Cobrancas.Data.Models;
using STN.Cobrancas.Data.Services;
using System.Collections.Generic;

namespace STN.Cobrancas.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CobrancaController : Controller
    {
        private readonly CobrancaService _cobrancaService;

        public CobrancaController(CobrancaService cobrancaService)
        {
            _cobrancaService = cobrancaService;
        }

        [HttpGet]
        public ActionResult<List<Cobranca>> GetByCpfMes(string cpf, int mes)
        {
            bool buscaCpf = !string.IsNullOrEmpty(cpf);
            bool buscaMes = mes != 0;

            if (!buscaCpf && !buscaMes)
                return BadRequest("Defina um CPF ou mês");
            if (buscaCpf && !CpfService.IsCpf(cpf))
                return BadRequest("CPF inválido");
            if (buscaMes && (mes <= 0 || mes > 12))
                return BadRequest("Mês inválido");

            List<Cobranca> cobranca = null;

            if (buscaCpf && buscaMes)
                cobranca = _cobrancaService.GetByCpfMes(cpf, mes);
            else if (buscaCpf)
                cobranca = _cobrancaService.GetByCpf(cpf.Trim().Replace(".", "").Replace("-", ""));
            else if (buscaMes)
                cobranca = _cobrancaService.GetByMes(mes);

            return cobranca;
        }

        [HttpPost]
        public ActionResult<Cobranca> Create([FromBody]Cobranca cobranca)
        {
            if (ModelState.IsValid)
            {
                if (!CpfService.IsCpf(cobranca.Cpf))
                    return BadRequest("CPF inválido");

                cobranca.Cpf = cobranca.Cpf.Trim().Replace(".", "").Replace("-", "");
                _cobrancaService.Create(cobranca);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}