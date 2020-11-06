using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace STN.Cobrancas.Data.Models
{
    public class Cobranca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("DataVencimento")]
        public DateTime DataVencimento { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public string Cpf { get; set; }
    }
}
