using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaA2.Models;

public class Evento{
public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Local { get; set; } = string.Empty;

    [Required]
    public DateTime Data { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.Now;

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }
}
