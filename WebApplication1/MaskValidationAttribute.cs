using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class MaskValidationAttribute : ValidationAttribute
    {
        string[] masks;
        public MaskValidationAttribute(string[] masks)
        {
            this.masks = masks;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                foreach(var mask in masks)
                {
                    if (ValidateName(mask, value.ToString()))
                        return true;
                }
                var error = "Name does not match the masks " + string.Join(' ',masks);
                ErrorMessage = error;
                return false;
            }
            return false;
        }

        private bool ValidateName(string mask, string name)
        {
            if (mask.Length == name.Length)
            {
                for (var i = 0; i != mask.Length; i++)
                {
                    if (mask[i] == 'X' && char.IsLetter(name[i]))
                        continue;
                    else if (mask[i] == '#' && char.IsDigit(name[i]))
                        continue;
                    else if (mask[i] == name[i])
                        continue;
                    else
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
