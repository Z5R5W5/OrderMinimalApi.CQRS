using FluentValidation;

namespace OrderMinimalApi.Commands
{
    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => new[] { "Pending", "Processing", "Completed", "Cancelled" }.Contains(status))
                .WithMessage("Status must be one of the following: Pending, Processing, Completed, Cancelled.");
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
        }
    }
}
