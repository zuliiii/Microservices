using FluentValidation;
using Ticket.Web.Models.Discount;

namespace Ticket.Web.Validators
{
	public class DiscountApplyInputValidator: AbstractValidator<DiscountApplyInput>
	{
		public DiscountApplyInputValidator()
		{
			RuleFor(x => x.Code).NotEmpty().WithMessage("discount coupon field cannot be empty");
		}
	}
}
