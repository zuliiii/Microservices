using FluentValidation;
using Ticket.Web.Models.Catalog;

namespace Ticket.Web.Validators
{
	public class EventUpdateInputValidator: AbstractValidator<EventUpdateInput>
	{
        public EventUpdateInputValidator()
        {
			RuleFor(x => x.Title).NotEmpty().WithMessage("Title field cannot be empty");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description field cannot be empty");
			RuleFor(x => x.Price).NotEmpty().WithMessage("Price field cannot be empty");
			RuleFor(x => x.Location).NotEmpty().WithMessage("Select a location");
			RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Select a category");
		}
    }
}
