using FluentValidation;
using karavancidan.Model.ViewModel.Helper;
using karavancidan.Model.ViewModel.Login;
using karavancidan.Model.ViewModel.Newsletter;
using karavancidan.Model.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karavancidan.Model.FluentValidation.Newsletter
{
    public class NewsletterFluentValidator : AbstractValidator<NewsletterViewModel>
    {
        public NewsletterFluentValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu alan boş bırakılamaz!").NotNull().WithMessage("Bu alan boş bırakılamaz!").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz!");

        }

    }
}
