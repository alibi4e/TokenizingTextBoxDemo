// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml.Markup;
using Windows.UI.Text;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenizingTextBoxDemo
{
    /// <summary>
    /// An abstract <see cref="MarkupExtension"/> which to produce text-based icons.
    /// </summary>
    public abstract class TextIconExtension : MarkupExtension
    {
        /// <summary>
        /// Gets or sets the size of the icon to display.
        /// </summary>
        public double FontSize { get; set; }

        [ThreadStatic]
        private static FontFamily symbolThemeFontFamily;

        /// <summary>
        /// Gets the reusable "Segoe Fluent Icons,Segoe MDL2 Assets" <see cref="FontFamily"/> instance.
        /// </summary>
        protected static FontFamily SymbolThemeFontFamily
        {
            get => symbolThemeFontFamily ??= new FontFamily("Segoe Fluent Icons,Segoe MDL2 Assets");
        }

        /// <summary>
        /// Gets or sets the thickness of the icon glyph.
        /// </summary>
        public FontWeight FontWeight { get; set; } = FontWeights.Normal;

        /// <summary>
        /// Gets or sets the font style for the icon glyph.
        /// </summary>
        public FontStyle FontStyle { get; set; } = FontStyle.Normal;

        /// <summary>
        /// Gets or sets the foreground <see cref="Brush"/> for the icon.
        /// </summary>
        public Brush Foreground { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether automatic text enlargement, to reflect the system text size setting, is enabled.
        /// </summary>
        public bool IsTextScaleFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the icon is mirrored when the flow direction is right to left.
        /// </summary>
        public bool MirroredWhenRightToLeft { get; set; }
    }
}
