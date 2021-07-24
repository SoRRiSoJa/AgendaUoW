using FluentValidation;

namespace AgendaUoW.Validators
{
    using AgendaUoW.Domain.Models;
    public class ContatoValidator : AbstractValidator<Contato>
    {
        public ContatoValidator()
        {
            RuleFor((agenda) => agenda.Nome).NotNull().NotEmpty().WithMessage("Você deve informar um nome.");
            RuleFor((agenda) => agenda.Nome).MaximumLength(75).WithMessage("O nome deve ter até 75 caracteres.");
            RuleFor((agenda) => agenda.Numero).MaximumLength(25).WithMessage("O Numero do telefone deve ter até 25 caracteres.");
        }
    }
}
