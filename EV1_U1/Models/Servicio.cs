using System;
using System.Collections.Generic;

namespace EV1_U1.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string? NombreServicio { get; set; }

    public int? Precio { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Cliente> ClientesIdClientes { get; set; } = new List<Cliente>();
}
