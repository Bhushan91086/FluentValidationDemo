using FluentValidation;
using System.ComponentModel;

namespace FluentValidationDemo
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            Include(new UserSimpleValidator());
            Include(new UserComplexValidator());
        }
    }

    public class UserSimpleValidator : AbstractValidator<User>
    {
        public UserSimpleValidator()
        {
            RuleFor(n => n.Name).NotNull().NotEmpty();
            RuleFor(e => e.Email).EmailAddress();
            RuleFor(a => a.Address).NotNull().MaximumLength(10);
            RuleFor(a => a.Address).Must(x => x?.ToLower().Contains("street") == true).WithMessage("The address should contain street");
        }
    }

    public class UserComplexValidator : AbstractValidator<User>
    {
        public UserComplexValidator()
        {
            RuleForEach(m => m.Memberships).SetValidator(new MembershipValidator());
        }
    }

    public class MembershipValidator : AbstractValidator<Membership>
    {
        public MembershipValidator()
        {
            RuleFor(m => m.name).NotNull().NotEmpty();
        }
    }
}
