// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenizingTextBoxDemo
{
    /// <summary>
    /// Custom <see cref="MarkupExtension"/> which can provide symbol-baased <see cref="FontIconSource"/> values.
    /// </summary>
    [MarkupExtensionReturnType(ReturnType = typeof(FontIconSource))]
    public class SymbolIconSourceExtension : TextIconExtension
    {
        /// <summary>
        /// Gets or sets the <see cref="Xaml.Controls.Symbol"/> value representing the icon to display.
        /// </summary>
        public Symbol Symbol { get; set; }

        /// <inheritdoc/>
        protected override object ProvideValue()
        {
            var fontIcon = new FontIconSource
            {
                Glyph = unchecked((char)Symbol).ToString(),
                FontFamily = SymbolThemeFontFamily,
                FontWeight = FontWeight,
                FontStyle = FontStyle,
                IsTextScaleFactorEnabled = IsTextScaleFactorEnabled,
                MirroredWhenRightToLeft = MirroredWhenRightToLeft
            };

            if (FontSize > 0)
            {
                fontIcon.FontSize = FontSize;
            }

            if (Foreground != null)
            {
                fontIcon.Foreground = Foreground;
            }

            return fontIcon;
        }
    }
}
