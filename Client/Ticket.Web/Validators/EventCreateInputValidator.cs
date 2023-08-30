using FluentValidation;
using Ticket.Web.Models.Catalog;

namespace Ticket.Web.Validators
{
	public class EventCreateInputValidator:AbstractValidator<EventCreateInput>
	{
		public EventCreateInputValidator() 
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Title field cannot be empty");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description field cannot be empty");
			RuleFor(x => x.Price).NotEmpty().WithMessage("Price field cannot be empty");
			RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Select a category");

		}
	}
}
