using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Models;

public  class Usuario
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string NombreDeUsuario { get; set; }
    public string Correo { get; set; }
    public string Contrasena { get; set; }
    public string PreferenciasAlimentarias {  get; set; }
    public string AlimentosAlergicos { get; set; }
    public string RestriccionesAlimentarias { get; set; }
}
