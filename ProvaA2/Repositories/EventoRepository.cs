using ProvaA2.Data;
using ProvaA2.Models;
using ProvaA2.Repositories;
using Microsoft.EntityFrameworkCore;



namespace ProvaA2.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly AppDbContext _context;

    public EventoRepository(AppDbContext context)
    {
        _context = context;
    }
        public IEnumerable<Evento> ListarEventos()
    {
        return _context.Eventos
            .Include(e => e.Usuario)  
            .ToList();
    }

    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public List<Evento> Listar()
    {
        return _context.Eventos.ToList();
    }

    public List<Evento> ListarPorUsuario(int usuarioId)
    {
        return _context.Eventos.Where(e => e.UsuarioId == usuarioId).ToList();
    }
}