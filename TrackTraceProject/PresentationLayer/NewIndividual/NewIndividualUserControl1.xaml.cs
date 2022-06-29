using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrackTraceProject.PresentationLayer.NewIndividual
{
    /// <summary>
    /// Interaction logic for NewIndividualUserControl1.xaml
    /// </summary>
    public partial class NewIndividualUserControl1 : UserControl
    {
        /* public constructor used by NewIndividualWindow.xaml.cs
        *
        *  Added by Eoin K 11/12/20
        */
        public NewIndividualUserControl1()
        {
            // this string should be a valid phone number
            PhoneNumber = "+440000 000000";
            InitializeComponent();
            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;
        }

        /* public property PhoneNumber to hold the phone number
        *  A text box is binded to the value of PhoneNumber in the xaml file
        *  So any changes to from the WPF window will change the value of the public property
        *
        *  Added by Eoin K 11/12/20
        */
        public string PhoneNumber { get; set; }

        /* private method to control the visibility of the invalid phone number label
        *
        *  Added by Eoin K 11/12/20
        */
        private void ChangeInvalidMessageVisibilty(Visibility l_Visibility)
        {
            Lbl_InvalidPhoneNumberFormat.Visibility = l_Visibility;
        }

        /* private method called when the PhoneNumber textbox changes value
        *  Checks if the value is valid and displays a invalid message if validation returns false
        *
        *  Added by Eoin K 11/12/20
        */
        private void TxtBox_PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainWindow.BusinessController.ValidPhoneNumber(TxtBox_PhoneNumber.Text))
            {
                ChangeInvalidMessageVisibilty(Visibility.Hidden);
            }
            else
            {
                ChangeInvalidMessageVisibilty(Visibility.Visible);
            }
        }
    }
}
