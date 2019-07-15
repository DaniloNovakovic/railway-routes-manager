﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Client.Helpers
{
    public class ValidationBase : BindableBase
    {
        [XmlIgnore]
        public bool IsValid { get; private set; }

        public ValidationErrors ValidationErrors { get; set; } = new ValidationErrors();

        public void Validate()
        {
            ValidationErrors.Clear();
            ValidateSelf();
            IsValid = ValidationErrors.IsValid;
            OnPropertyChanged(nameof(IsValid));
            OnPropertyChanged(nameof(ValidationErrors));
        }

        protected virtual void ValidateSelf()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    foreach (string memberName in validationResult.MemberNames)
                    {
                        ValidationErrors[memberName] = validationResult.ErrorMessage;
                    }
                }
            }
        }
    }
}