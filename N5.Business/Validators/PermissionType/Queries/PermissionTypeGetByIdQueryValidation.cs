namespace N5.Business.Validators.PermissionType.Queries
{
    using FluentValidation;
    using N5.Business.Features.PermissionType.Queries;
    using N5.Core.Messages;

    public class PermissionTypeGetByIdQueryValidation : AbstractValidator<PermissionTypeGetByIdQuery>
    {
		public PermissionTypeGetByIdQueryValidation()
		{
            RuleFor(entity => entity.Id)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .WithName("Id");
        }
	}
}