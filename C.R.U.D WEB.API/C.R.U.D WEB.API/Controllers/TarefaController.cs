﻿using C.R.U.D_WEB.API.Models;
using C.R.U.D_WEB.API.Repositorios;
using C.R.U.D_WEB.API.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace C.R.U.D_WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodasTarefas() {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarUsuarioPorId(int id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarUsuarioPorId(id);
            return Ok(tarefa);
        }
        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody]TarefaModel tarefaModel)
        {
            TarefaModel tarefa =  await _tarefaRepositorio.Adicionar(tarefaModel);

            return Ok(tarefa);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaRepositorio.Atualizar(tarefaModel, id);

            return Ok(tarefa);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
           
            bool apagado = await _tarefaRepositorio.Apagar(id);

            return Ok(apagado);
        }


    }
}
