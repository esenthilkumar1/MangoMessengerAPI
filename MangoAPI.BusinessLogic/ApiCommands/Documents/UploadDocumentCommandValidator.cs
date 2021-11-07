﻿using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace MangoAPI.BusinessLogic.ApiCommands.Documents
{
    public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
    {
        public UploadDocumentCommandValidator()
        {
            var allowedExtensions = new List<string>
            {
                "jpg", "JPG", "txt", "TXT", "pdf", "PDF", "gif", "GIF", "png", "PNG"
            };

            RuleFor(x => x.FormFile).NotEmpty();
            RuleFor(x => x.FormFile.Length).LessThanOrEqualTo(10 * 1024 * 1024);

            RuleFor(x => x.FormFile.FileName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(t =>
                {
                    var validExtension = t.Split('.').Last();
                    return allowedExtensions.Contains(validExtension);
                })
                .Length(1, 20);
        }
    }
}