using System;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Microsoft.Extensions.DependencyInjection;

namespace Anjoz.Identity.Domain.Validadores.Base
{
    public class DomainServiceValidator<TEntidade> : IDomainServiceValidator<TEntidade>
        where TEntidade : class
    {
        public DomainServiceValidator(IServiceProvider serviceProvider)
        {
            Salvar = serviceProvider.GetService<ISaveServiceValidator<TEntidade>>();
            Atualizar = serviceProvider.GetService<IUpdateServiceValidator<TEntidade>>();
            Deletar = serviceProvider.GetService<IDeleteServiceValidator<TEntidade>>();
            Customizado = serviceProvider.GetService<ICustomServiceValidator<TEntidade>>();
        }

        public ISaveServiceValidator<TEntidade> Salvar { get; set; }
        public IUpdateServiceValidator<TEntidade> Atualizar { get; set; }
        public IDeleteServiceValidator<TEntidade> Deletar { get; set; }
        public ICustomServiceValidator<TEntidade> Customizado { get; set; }
    }
}