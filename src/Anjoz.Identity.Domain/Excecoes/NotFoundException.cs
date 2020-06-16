using System;
using System.Collections.Generic;

namespace Anjoz.Identity.Domain.Excecoes
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string mensagens) : base(mensagens)
        {
        }

        public NotFoundException(string mensagens, Exception excecao) : base(mensagens, excecao)
        {
        }

        public NotFoundException(ICollection<string> messages) : base(messages)
        {
        }

        public NotFoundException(ICollection<string> messages, Exception excecao) : base(messages, excecao)
        {
        }
    }
}