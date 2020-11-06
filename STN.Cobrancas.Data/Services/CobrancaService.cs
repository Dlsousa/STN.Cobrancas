using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using STN.Cobrancas.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace STN.Cobrancas.Data.Services
{
    public class CobrancaService
    {
        private readonly IMongoCollection<Cobranca> _cobranca;

        public CobrancaService(IConfiguration settings)
        {
            // TODO: para os testes não consegue pegar as configurações do appsettings.json
            //var client = new MongoClient(settings.GetConnectionString("StoreDBCon"));
            var client = new MongoClient("mongodb+srv://dbUser:ATjuxsfuUGZZra9n@cobrancastore.rzg3n.mongodb.net/CobrancaStore?retryWrites=true&w=majority");

            var database = client.GetDatabase("CobrancaStore");

            _cobranca = database.GetCollection<Cobranca>("Cobranca");
        }

        public List<Cobranca> Get() =>
            _cobranca.Find(c => true).ToList();

        public Cobranca Get(string id) =>
            _cobranca.Find<Cobranca>(c => c.Id == id).FirstOrDefault();

        public List<Cobranca> GetByCpf(string cpf) => _cobranca.Find<Cobranca>(c => c.Cpf == cpf).ToList();

        public List<Cobranca> GetByMes(int mes)
        {
            var start = new DateTime(DateTime.Now.Year, mes, 1);
            var end = start.AddMonths(1);
            return _cobranca.Find<Cobranca>(c => c.DataVencimento >= start && c.DataVencimento < end).ToList();
        }

        public List<Cobranca> GetByCpfMes(string cpf, int mes)
        {
            var start = new DateTime(DateTime.Now.Year, mes, 1);
            var end = start.AddMonths(1);
            return _cobranca.Find<Cobranca>(c => c.Cpf == cpf && c.DataVencimento >= start && c.DataVencimento < end).ToList();
        }

        public Cobranca Create(Cobranca cobranca)
        {
            _cobranca.InsertOne(cobranca);
            return cobranca;
        }

        public void Update(string id, Cobranca cobrancaIn) =>
            _cobranca.ReplaceOne(c => c.Id == id, cobrancaIn);

        public void Remove(Cobranca cobrancaIn) =>
            _cobranca.DeleteOne(c => c.Id == cobrancaIn.Id);

        public void Remove(string id) =>
            _cobranca.DeleteOne(c => c.Id == id);
    }
}

