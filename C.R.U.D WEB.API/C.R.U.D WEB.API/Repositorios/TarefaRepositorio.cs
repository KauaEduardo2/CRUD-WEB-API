using C.R.U.D_WEB.API.Data;
using C.R.U.D_WEB.API.Models;
using C.R.U.D_WEB.API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace C.R.U.D_WEB.API.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext){
            _dbContext = sistemaTarefasDBContext;
        }
        //Buscar usuario por ID
        public async Task<TarefaModel> BuscarUsuarioPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        //Buscar todos usuarios
        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }
        
        //Adicionar
        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }
        //Atualizar
        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarUsuarioPorId(id);
            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} nao foi encontrada no banco de dados");
            }
            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }
        //Apagar
        public async Task<bool> Apagar(int id)
        {
            //Buscar usuario e validar

            TarefaModel tarefaPorId = await BuscarUsuarioPorId(id);
            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} nao foi encontrado no banco de dados");
            }
            //Logica para remover usuario
            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }


    }
}
