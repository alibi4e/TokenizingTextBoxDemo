// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenizingTextBoxDemo
{
    /// <summary>
    /// Sample of strongly-typed data for <see cref="Microsoft.Toolkit.Uwp.UI.Controls.TokenizingTextBox"/>.
    /// </summary>
    public class SampleDataType
    {
        /// <summary>
        /// Gets or sets symbol to display.
        /// </summary>
        public Symbol Icon { get; set; }

        /// <summary>
        /// Gets or sets text to display.
        /// </summary>
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
