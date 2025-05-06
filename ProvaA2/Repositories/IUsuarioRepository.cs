using System;
using ProvaA2.Models;
using ProvaA2.Data;
using ProvaA2.Repositories;

namespace ProvaA2.Repositories;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    List<Usuario> Listar();
    Usuario? BuscarUsuarioPorEmailSenha(string email, string senha);
}
