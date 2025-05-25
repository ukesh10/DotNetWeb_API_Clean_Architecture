
using eCommerce.Application.DTOs;
using eCommerce.Application.Validations.Authentication;
using FluentValidation;

namespace eCommerce.Application.Validations
{
    public interface IValidationService
    {
        Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
    }
}
