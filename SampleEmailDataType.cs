// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenizingTextBoxDemo
{
    /// <summary>
    /// Sample of strongly-typed email address simulated data for <see cref="Microsoft.Toolkit.Uwp.UI.Controls.TokenizingTextBox"/>.
    /// </summary>
    public class SampleEmailDataType
    {
        /// <summary>
        /// Gets the initials to Display
        /// </summary>
        public string Initials => string.Empty + FirstName[0] + FamilyName[0];

        /// <summary>
        /// Gets or sets the first name .
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the family name .
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets the display text.
        /// </summary>
        public string DisplayName => $"{FirstName} {FamilyName}";

        /// <summary>
        /// Gets the formatted email address
        /// </summary>
        public string EmailAddress => $"{DisplayName} <{FirstName}.{FamilyName}@contoso.com>";

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}
