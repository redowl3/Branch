using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.CustomLayouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductLayoutView : ContentView
    {
        public ProductLayoutView()
        {
            InitializeComponent();
        }
        private void benefits_close_clicked(object sender, EventArgs e)
        {
            BenefitsDetailsGridView.IsVisible = false;
            BenefitBoxView.BackgroundColor = Color.Gray;
        }

        private void TapGestureRecognizer_Tappedbenefits(object sender, EventArgs e)
        {
            DirectionDetailsGridView.IsVisible = false;
            KeyDetailsGridView.IsVisible = false;
            DirectionBoxView.BackgroundColor = KeyBoxView.BackgroundColor = Color.Gray;
            BenefitsDetailsGridView.IsVisible = !BenefitsDetailsGridView.IsVisible;
            if (BenefitsDetailsGridView.IsVisible)
            {
                BenefitBoxView.BackgroundColor = Color.White;
            }
            else
            {
                BenefitBoxView.BackgroundColor = Color.Gray;
            }
        }

        private void TapGestureRecognizer_Tappeddirections(object sender, EventArgs e)
        {
            BenefitsDetailsGridView.IsVisible = false;
            KeyDetailsGridView.IsVisible = false;
            BenefitBoxView.BackgroundColor = KeyBoxView.BackgroundColor = Color.Gray;
            DirectionDetailsGridView.IsVisible = !DirectionDetailsGridView.IsVisible;
            if (DirectionDetailsGridView.IsVisible)
            {
                DirectionBoxView.BackgroundColor = Color.White;
            }
            else
            {
                DirectionBoxView.BackgroundColor = Color.Gray;
            }
        }

        private void TapGestureRecognizer_TappedKey(object sender, EventArgs e)
        {
            BenefitsDetailsGridView.IsVisible = false;
            DirectionDetailsGridView.IsVisible = false;
            BenefitBoxView.BackgroundColor = DirectionBoxView.BackgroundColor = Color.Gray;
            KeyDetailsGridView.IsVisible = !KeyDetailsGridView.IsVisible;
            if (KeyDetailsGridView.IsVisible)
            {
                KeyBoxView.BackgroundColor = Color.White;
            }
            else
            {
                KeyBoxView.BackgroundColor = Color.Gray;
            }


        }
    }
}