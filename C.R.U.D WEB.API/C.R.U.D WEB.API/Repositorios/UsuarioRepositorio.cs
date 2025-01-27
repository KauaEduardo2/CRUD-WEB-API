using C.R.U.D_WEB.API.Data;
using C.R.U.D_WEB.API.Models;
using C.R.U.D_WEB.API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace C.R.U.D_WEB.API.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext){
            _dbContext = sistemaTarefasDBContext;
        }
        //Buscar usuario por ID
        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }
        //Buscar todos usuarios
        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        
        //Adicionar
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }
        //Atualizar
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} nao foi encontrado no banco de dados");
            }
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }
        //Apagar
        public async Task<bool> Apagar(int id)
        {
            //Buscar usuario e validar

            UsuarioModel usuarioPorId = await BuscarUsuarioPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} nao foi encontrado no banco de dados");
            }
            //Logica para remover usuario
            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }


    }
}
