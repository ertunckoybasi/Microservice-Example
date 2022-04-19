using FluentValidation;

namespace SentezMicro.Services.Customer.Application.Application.Features.Commands
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateValidator()
        {
            RuleFor(I => I.CurrentAccountName).NotEmpty();

        }
    }
}
