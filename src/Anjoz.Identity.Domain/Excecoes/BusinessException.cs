using System;
using System.Collections.Generic;
using System.Linq;

namespace Anjoz.Identity.Domain.Excecoes
{
    public class BusinessException : Exception
    {
        public BusinessException(string mensagens)
            : base(mensagens)
        {
            Errors = new List<string>
            {
                mensagens
            };
        }
        
        public BusinessException(string mensagens, Exception excecao)
            : base(mensagens, excecao)
        {
            Errors = new List<string>
            {
                mensagens
            };
        }
        
        public BusinessException(ICollection<string> messages)
             : base(messages.Aggregate((a, b) => $"{a}\n{b}"))
        {
            Errors = messages;
        }

        public BusinessException(ICollection<string> messages, Exception excecao)
            : base(messages.Aggregate((a, b) => $"{a}\n{b}"), excecao)
        {
            Errors = messages;
        }

        public ICollection<string> Errors { get; }
    }
}