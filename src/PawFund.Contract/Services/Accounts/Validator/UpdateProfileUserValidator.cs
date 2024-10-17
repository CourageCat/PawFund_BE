using FluentValidation;
using Microsoft.AspNetCore.Http;
using PawFund.Contract.Services.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Accounts.Validator
{
    public class UpdateProfileUserValidator : AbstractValidator<Command.UpdateUserCommand>
    {
        public UpdateProfileUserValidator()
        {
            // Kiểm tra định dạng file ảnh nếu có truyền vào
            RuleFor(x => x.AvatarFile)
                .Must(BeAValidImageFormat)
                .When(x => x.AvatarFile != null) // Chỉ kiểm tra nếu file không null
                .WithMessage("File must be in .png or .jpg format.");
        }

        // Hàm kiểm tra định dạng file
        private bool BeAValidImageFormat(IFormFile? file)
        {
            if (file == null) return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(fileExtension);
        }
    }
} 
