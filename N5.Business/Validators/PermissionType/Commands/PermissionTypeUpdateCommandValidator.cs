namespace N5.Business.Validators.PermissionType.Commands
{
    using FluentValidation;
    using N5.Business.Features.PermissionType.Commands;
    using N5.Core.Messages;

    public class PermissionTypeUpdateCommandValidator : AbstractValidator<PermissionTypeUpdateCommand>
    {
		public PermissionTypeUpdateCommandValidator()
		{
            RuleFor(entity => entity.Id)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .WithName("Id");

            RuleFor(entity => entity.Description)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .WithName("Description");
        }
	}
}