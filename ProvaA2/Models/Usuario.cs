using System;

namespace ProvaA2.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public Permissao Permissao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
