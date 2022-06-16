using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Internal.Contracts
{
    public interface IEmailBuilder
    {
        public IEmailBuilder To(string to);

        public IEmailBuilder Body(string body);

        public IEmailBuilder Subject(string subject);

        public Email Create();
    }
}
