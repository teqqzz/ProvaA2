using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProvaA2.Data;
using ProvaA2.Models;
using ProvaA2.Repositories;
using ProvaA2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ProvaA2.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;
    public UsuarioController(IUsuarioRepository usuarioRepository,
        IConfiguration configuration, TokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        _usuarioRepository.Cadastrar(usuario);
        return Created("", usuario);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        Usuario? usuarioExistente = _usuarioRepository
            .BuscarUsuarioPorEmailSenha(usuario.Email, usuario.Senha);

        if (usuarioExistente == null)
        {
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos!" });
        }

        string token = _tokenService.GerarToken(usuarioExistente);
        return Ok(token);
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        return Ok(_usuarioRepository.Listar());
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Permissao.ToString())
        };

        var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);
        
        var assinatura = new SigningCredentials(
            new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(30),
            signingCredentials: assinatura
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}