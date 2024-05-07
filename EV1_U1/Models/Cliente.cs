﻿using System;
using System.Collections.Generic;

namespace EV1_U1.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Servicio> ServiciosIdServicios { get; set; } = new List<Servicio>();
}