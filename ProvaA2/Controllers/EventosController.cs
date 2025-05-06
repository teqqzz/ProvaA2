using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaA2.Models;
using ProvaA2.Repositories;
using ProvaA2.Data;


using System.Security.Claims;

namespace ProvaA2.Controllers;

[ApiController]
[Route("api/evento")]
public class EventosController : ControllerBase
{
    private readonly IEventoRepository _eventoRepo;
    private readonly IUsuarioRepository _usuarioRepo;

    public EventosController(IEventoRepository eventoRepo, IUsuarioRepository usuarioRepo)
    {
        _eventoRepo = eventoRepo;
        _usuarioRepo = usuarioRepo;
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        return Ok(_eventoRepo.Listar());
    }

    [HttpGet("usuario")]
    [Authorize]
    public IActionResult ListarEventosDoUsuario()
    {
        var id = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
        return Ok(_eventoRepo.ListarPorUsuario(id));
    }

    [HttpPost("cadastrar")]
    [Authorize]
    public IActionResult Cadastrar([FromBody] Evento evento)
    {
        var id = int.Parse(User.Claims.First(c => c.Type == "UsuarioId").Value);
        evento.UsuarioId = id;
        _eventoRepo.Cadastrar(evento);
        return Created("", evento);
    }
}