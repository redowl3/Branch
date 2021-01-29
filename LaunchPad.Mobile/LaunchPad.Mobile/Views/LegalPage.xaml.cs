﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LegalPage : ContentPage
    {
        public LegalPage()
        {
            InitializeComponent();
            LegalTextLabel.Text = Constants.SampleText;
        }
    }
}