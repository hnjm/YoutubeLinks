﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using YoutubeLinks.Shared.Localization;
using static YoutubeLinks.Shared.Features.Playlists.Commands.ExportPlaylist;

namespace YoutubeLinks.Shared.Features.Playlists.Commands
{
    public class ImportPlaylistFromJson
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
            public bool Public { get; set; }
            public List<LinkModel> ExportedLinks { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IStringLocalizer<ValidationMessage> localizer)
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage(x => localizer[nameof(ValidationMessageString.NameNotEmpty)])
                    .MaximumLength(ValidationConsts.MaximumStringLength)
                    .WithMessage(x => localizer[nameof(ValidationMessageString.NameMaximumLength)]);
            }
        }

        public class FormModel : Command
        {
            public IBrowserFile File { get; set; }
        }

        public class FormModelValidator : AbstractValidator<FormModel>
        {
            public FormModelValidator(IStringLocalizer<ValidationMessage> localizer)
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage(x => localizer[nameof(ValidationMessageString.NameNotEmpty)])
                    .MaximumLength(ValidationConsts.MaximumStringLength)
                    .WithMessage(x => localizer[nameof(ValidationMessageString.NameMaximumLength)]);

                RuleFor(x => x.File)
                    .NotEmpty()
                    .WithMessage(x => localizer[nameof(ValidationMessageString.FileNotEmpty)])
                    .SetValidator(new FileValidator(localizer));
            }
        }

        public class FileValidator : AbstractValidator<IBrowserFile>
        {
            private readonly int _maxFileSize = 5242880;
            private readonly List<string> _allowedFileTypes = ["application/json"];

            public FileValidator(IStringLocalizer<ValidationMessage> localizer)
            {
                RuleFor(x => x.Size)
                    .LessThanOrEqualTo(_maxFileSize)
                    .WithMessage(x => localizer[nameof(ValidationMessageString.FileMaxFileSize)]);

                RuleFor(x => x.ContentType)
                    .Must(x => _allowedFileTypes.Contains(x))
                    .WithMessage(x => localizer[nameof(ValidationMessageString.FileContentTypeShouldBeJson)]);
            }
        }
    }
}
