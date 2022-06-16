using Ordering.Application.Internal.Contracts;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Internal
{
    public class EmailBuilder : IEmailBuilder
    {
        private readonly Email _email;

        public EmailBuilder()
        {
            _email = new Email();
        }

        public IEmailBuilder Body(string body)
        {
            _email.Body = body;

            return this;
        }

        public IEmailBuilder Subject(string subject)
        {
            _email.Subject = subject;

            return this;
        }

        public IEmailBuilder To(string to)
        {
            _email.To = to;

            return this;
        }

        public Email Create()
        {
            return _email;
        }
    }
}
