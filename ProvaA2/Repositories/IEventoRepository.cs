using System;
using ProvaA2.Models;
using ProvaA2.Data;
using ProvaA2.Repositories;

namespace ProvaA2.Repositories;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    List<Evento> ListarPorUsuario(int usuarioId);
}