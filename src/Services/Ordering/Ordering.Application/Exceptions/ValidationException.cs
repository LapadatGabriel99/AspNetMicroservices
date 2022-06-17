using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
            : base("One or more validation failures have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> validationFailures)
            : this()
        {
            Errors = validationFailures
                .GroupBy(x => x.PropertyName, e => e.ErrorMessage)
                .ToDictionary(x => x.Key, x => x.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }

        private void ConvertValidationFailures(IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (var failure in validationFailures)
            {
                if (!Errors.ContainsKey(failure.PropertyName))
                {
                    var temp = validationFailures
                        .Where(x => x.PropertyName == failure.PropertyName)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    Errors.Add(failure.PropertyName, temp);
                }
            }
        }
    }
}
