using LaunchPad.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.CustomLayouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LifeStylesQuestionContainerLayout : ContentView
    {
        public LifeStylesQuestionContainerLayout()
        {
            InitializeComponent();
            this.BindingContext = App.LifestylesSurveyViewModel;
        }

        private void SaveAndContinue(object sender, EventArgs e)
        {
            (this.BindingContext as LifestylesSurveyViewModel).NextCommand.Execute(null);
        }

        private void OptionTapped(object sender, EventArgs e)
        {
            try
            {
                var senderGrid = ((Grid)sender);
                var label = senderGrid.Children[0] as Label;
                var boxView = senderGrid.Children[1] as BoxView;
                var optionParent =((StackLayout)((Grid)(senderGrid.Parent)).Parent);
                var topIIParent= ((StackLayout)((StackLayout)(optionParent.Parent)).Parent);
                var topParent= (StackLayout)(topIIParent.Parent);
                foreach (var item in optionParent.Children)
                {
                    var grid = item as Grid;
                    var gridII = grid.Children[0] as Grid;
                    var labelI = gridII.Children[0] as Label;
                    var boxViewI = gridII.Children[1] as BoxView;
                    if (labelI?.Text?.ToLower() == label?.Text?.ToLower())
                    {
                        gridII.BackgroundColor = Color.Black;
                        labelI.TextColor = Color.White;
                        boxViewI.BackgroundColor = Color.White;
                    }
                    else
                    {
                        gridII.BackgroundColor = Color.White;
                        labelI.TextColor = Color.Black;
                        boxViewI.BackgroundColor = Color.Black;
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        private void OptionSelected(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundColor = Color.Black;
            ((Button)sender).TextColor = Color.White;
        }
    }
}