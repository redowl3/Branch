using LaunchPad.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.CustomLayouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConcernsQuestionContainerLayout : ContentView
    {
        private List<SurveySummary> SurveySummaries = new List<SurveySummary>();
        public string QuestionGuid { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string SubAnswerText { get; set; }
        public ConcernsQuestionContainerLayout()
        {
            InitializeComponent();
            this.BindingContext = App.ConcernsAndSkinCareSurveyViewModel;
        }

        private void ToggleContainer(object sender, EventArgs e)
        {
            var toggleContainerParent = ((StackLayout)((StackLayout)((Frame)((Grid)((Grid)sender).Parent).Parent).Parent).Parent);
            var senderParent = (Grid)sender;
            var parentOfSenderParent = (Grid)senderParent.Parent;
            (parentOfSenderParent.Children[1] as StackLayout).IsVisible = !(parentOfSenderParent.Children[1] as StackLayout).IsVisible;
            if((parentOfSenderParent.Children[1] as StackLayout).IsVisible)
            {
                senderParent.BackgroundColor = Color.FromHex("#000");
                ((Label)senderParent.Children[0]).TextColor = Color.FromHex("#fff");
                ((Image)senderParent.Children[1]).Source = "icon_arrow_top";
            }
            else
            {
                ((Image)senderParent.Children[1]).Source = "icon_down_arrow";
            }
            QuestionText = (e as TappedEventArgs)?.Parameter as string;
            QuestionGuid = Guid.NewGuid().ToString();
            foreach (var item in toggleContainerParent.Children)
            {
                var child1 = item as StackLayout;
                var child2 = child1.Children[2] as Frame;
                var child3 = child2.Content as Grid;
                var grid = child3.Children[0] as Grid;
                var label = grid.Children[0] as Label;
                if (label?.Text?.ToLower() != QuestionText.ToLower())
                {
                    ((StackLayout)child3.Children[1]).IsVisible = false ;
                }
               
            }
        }

        private void editor_unfocused(object sender, FocusEventArgs e)
        {
            var topParent = (StackLayout)((Grid)((Frame)((Grid)((Editor)sender).Parent).Parent).Parent).Parent;
            if (topParent != null)
            {
                QuestionText = ((topParent.Children[0] as Grid)?.Children[0] as Label)?.Text;
                SubAnswerText = ((Editor)sender).Text;
            }
        }

        private void Finish(object sender, EventArgs e)
        {

        }

        private void SaveAndContinue(object sender, EventArgs e)
        {
            (this.BindingContext as ConcernsAndSkinCareSurveyViewModel)?.ContinueCommand.Execute(null);
            //SurveySummaries.Add(new SurveySummary
            //{
            //    QuestionGuid = QuestionGuid,
            //    QuestionText = QuestionText
            //});
        }

        private void OptionTapped(object sender, EventArgs e)
        {
            try
            {
                var senderParent = ((Grid)sender).Parent as StackLayout;
                SubAnswerText = (((Grid)sender).Children[0] as Label).Text;
                if (((Grid)sender).BackgroundColor != Color.FromHex("#000"))
                {
                    ((Grid)sender).BackgroundColor = Color.FromHex("#000");
                    (((Grid)sender).Children[0] as Label).TextColor = Color.FromHex("#fff");

                    SubAnswerText = (((Grid)sender).Children[0] as Label).Text;
                }
                else
                {
                    ((Grid)sender).BackgroundColor = Color.FromHex("#fff");
                    (((Grid)sender).Children[0] as Label).TextColor = Color.FromHex("#000");
                    SubAnswerText = "";
                }

                foreach (var item in senderParent.Children)
                {
                    var grid = item as Grid;
                    var label = grid.Children[0] as Label;
                    if (label?.Text?.ToLower() != SubAnswerText.ToLower())
                    {
                        grid.BackgroundColor = Color.FromHex("#fff");
                        label.TextColor = Color.FromHex("#000");
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void YesNoButtonClicked(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundColor = Color.FromHex("#000");
                ((Button)sender).TextColor = Color.FromHex("#fff");
                var container = ((StackLayout)((Button)sender).Parent);
                foreach (var item in container.Children)
                {
                    var button = item as Button;
                    if (button?.Text?.ToLower() != ((Button)sender).Text.ToLower())
                    {
                        button.BackgroundColor = Color.FromHex("#fff");
                        button.TextColor = Color.FromHex("#000");
                    }
                }
            }
            catch (Exception)
            {

            }
         
        }

        private void OptionSelected(object sender, EventArgs e)
        {
            try
            {
                var senderParent = ((StackLayout)((Grid)((Button)sender).Parent).Parent);
                var topParent = ((StackLayout)(senderParent.Parent));
                ((Button)sender).BackgroundColor = Color.FromHex("#000");
                ((Button)sender).TextColor = Color.FromHex("#fff");
                foreach (var item in senderParent.Children)
                {
                    var grid = item as Grid;
                    var button = grid.Children[0] as Button;

                    if (button?.Text?.ToLower() != ((Button)sender).Text.ToLower())
                    {
                        button.BackgroundColor = Color.FromHex("#fff");
                        button.TextColor = Color.FromHex("#000");
                    }
                }
            }
            catch (Exception)
            {

            }
           
        }

        private void UnselectThis(object sender, EventArgs e)
        {
            try
            {
                var toggleContainerParent = ((StackLayout)((StackLayout)((Frame)((Grid)((Grid)sender).Parent).Parent).Parent).Parent);
                var senderParent = (Grid)sender;
                var parentOfSenderParent = (Grid)senderParent.Parent;
                (parentOfSenderParent.Children[1] as StackLayout).IsVisible = false;
                senderParent.BackgroundColor = Color.FromHex("#000");
                ((Label)senderParent.Children[0]).TextColor = Color.FromHex("#fff");
                ((Image)senderParent.Children[1]).Source = "icon_down_arrow";
                //QuestionText = (e as TappedEventArgs)?.Parameter as string;
                //QuestionGuid = Guid.NewGuid().ToString();
                //foreach (var item in toggleContainerParent.Children)
                //{
                //    var child1 = item as StackLayout;
                //    var child2 = child1.Children[2] as Frame;
                //    var child3 = child2.Content as Grid;
                //    var grid = child3.Children[0] as Grid;
                //    var label = grid.Children[0] as Label;
                //    if (label?.Text?.ToLower() != QuestionText.ToLower())
                //    {
                //        ((StackLayout)child3.Children[1]).IsVisible = false;
                //    }

                //}
            }
            catch (Exception)
            {

            }
        }
    }
}