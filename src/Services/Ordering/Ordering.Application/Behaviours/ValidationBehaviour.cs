using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse>
        where TRequest : IRequest<TReponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TReponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TReponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

                var validationFailures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (validationFailures.Count != 0)
                {
                    throw new Exceptions.ValidationException(validationFailures);
                }
            }

            return await next();
        }
    }
}
