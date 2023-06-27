// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TokenizingTextBoxDemo
{
    public sealed partial class MainWindow : Window
    {
        private readonly List<EmailInfo> _samples = new List<EmailInfo>()
        {
            new EmailInfo() { EmailAddress = "bilal.ali@bluejeans.com" },
            new EmailInfo() { EmailAddress = "parvez@bluejeans.com" },
            new EmailInfo() { EmailAddress = "steven.praveen@bluejeans.com" },
            new EmailInfo() { EmailAddress = "ashoka.n@bluejeans.com" },
            new EmailInfo() { EmailAddress = "ajey@bluejeans.com" },
            new EmailInfo() { EmailAddress = "atheeq@bluejeans.com" },
            new EmailInfo() { EmailAddress = "kateryna.novak@bluejeans.com" },
            new EmailInfo() { EmailAddress = "nanda.doddapaneni@bluejeans.com" }
        };

        private TokenizingTextBox _ttb;        

        private AdvancedCollectionView _acv;

        public ObservableCollection<EmailInfo> SelectedEmails { get; set; }

        public MainWindow()        
        {
            InitializeComponent();

            _acv = new AdvancedCollectionView(_samples, false);

            _acv.SortDescriptions.Add(new SortDescription(nameof(EmailInfo.EmailAddress), SortDirection.Ascending));

            MainGrid.Loaded += (sender, e) => { this.OnXamlRendered(this.MainGrid); };            
        }

        public void OnXamlRendered(FrameworkElement control)
        {
            SelectedEmails = new();

            control.DataContext = this;

            if (_ttb != null)
            {

                _ttb.TextChanged -= TextChanged;
                _ttb.TokenItemAdding -= TokenItemCreating;
            }

            if (control.FindChild("TokenBox") is TokenizingTextBox ttb)
            {
                _ttb = ttb;
                _ttb.TextChanged += TextChanged;
                _ttb.TokenItemAdding += TokenItemCreating;

                _acv.Filter = item => !_ttb.Items.Contains(item) && (item as EmailInfo).EmailAddress.Contains(_ttb.Text, System.StringComparison.CurrentCultureIgnoreCase);

                _ttb.ItemsSource = SelectedEmails;
                _ttb.SuggestedItemsSource = _acv;
            }
        }

        private void TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                _acv.RefreshFilter();
            }
        }

        private void TokenItemCreating(object sender, TokenItemAddingEventArgs e)
        {
            e.Item = _samples.FirstOrDefault((item) => item.EmailAddress.Contains(e.TokenText, System.StringComparison.CurrentCultureIgnoreCase));

            if (e.Item == null)
            {
                e.Item = new EmailInfo()
                {
                    EmailAddress = e.TokenText                    
                };
            }
        }

        private async void ClearButtonClick(object sender, RoutedEventArgs e)
        {            
            _acv.RefreshFilter();
            await _ttb.ClearAsync();
        }
    }
}
