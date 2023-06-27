// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

namespace TokenizingTextBoxDemo
{
    public class TokenizingTextBox : CommunityToolkit.WinUI.UI.Controls.TokenizingTextBox
    {
        private AdvancedCollectionView advancedCollectionView;

        public object SuggestedItems
        {
            get { return GetValue(SuggestedItemsProperty); }
            set { SetValue(SuggestedItemsProperty, value); }
        }
        
        public static readonly DependencyProperty SuggestedItemsProperty =
            DependencyProperty.Register("SuggestedItemsProperty", typeof(object), typeof(TokenizingTextBox), new PropertyMetadata(default(object)));

        public object ChoosenItems
        {
            get { return GetValue(ChoosenItemsProperty); }
            set { SetValue(ChoosenItemsProperty, value); }
        }
        
        public static readonly DependencyProperty ChoosenItemsProperty =
            DependencyProperty.Register("ChoosenItems", typeof(object), typeof(TokenizingTextBox), new PropertyMetadata(default(object)));

        public Predicate<object> Filter
        {
            get { return (Predicate<object>)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(Predicate<object>), typeof(TokenizingTextBox), new PropertyMetadata(default(Predicate<object>)));

        public IList<SortDescription> SuggestedItemsSortDescriptions
        {
            get { return (IList<SortDescription>)GetValue(SuggestedItemsSortDescriptionsProperty); }
            set { SetValue(SuggestedItemsSortDescriptionsProperty, value); }
        }
        
        public static readonly DependencyProperty SuggestedItemsSortDescriptionsProperty =
            DependencyProperty.Register("SuggestedItemsSortDescriptions", typeof(IList<SortDescription>), typeof(TokenizingTextBox), new PropertyMetadata(default(IList<SortDescription>)));

        public TokenizingTextBox()
        {
            SuggestedItemsSortDescriptions = new List<SortDescription>();
            Loaded += (sender, e) => {
                advancedCollectionView = new AdvancedCollectionView((IList)SuggestedItems, false);      
                SuggestedItemsSortDescriptions.ToList().ForEach(sortDescription => advancedCollectionView.SortDescriptions.Add(sortDescription));                
                advancedCollectionView.Filter = Filter;
                TextChanged += OnTextChanged;
                ItemsSource = ChoosenItems;
                SuggestedItemsSource = advancedCollectionView;
            };
        }

        private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                advancedCollectionView.RefreshFilter();
            }
        }
    }

    public sealed partial class MainWindow : Window
    {
        public List<EmailInfo> Samples = new()
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
        
        public ObservableCollection<EmailInfo> SelectedEmails { get; set; }

        public MainWindow()        
        {
            InitializeComponent();                     
            SelectedEmails = new();
            ZionTokenizingTextBox.Filter = item => !ZionTokenizingTextBox.Items.Contains(item) && (item as EmailInfo).EmailAddress.Contains(ZionTokenizingTextBox.Text, System.StringComparison.CurrentCultureIgnoreCase);
            ZionTokenizingTextBox.SuggestedItemsSortDescriptions.Add(new SortDescription(nameof(EmailInfo.EmailAddress), SortDirection.Ascending));
        }

        private void TokenItemCreating(object sender, TokenItemAddingEventArgs e)
        {
            e.Item = Samples.FirstOrDefault((item) => item.EmailAddress.Contains(e.TokenText, StringComparison.CurrentCultureIgnoreCase));

            if (e.Item == null)
            {
                e.Item = new EmailInfo()
                {
                    EmailAddress = e.TokenText                    
                };
            }
        }
    }
}
