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

namespace TrackTraceProject.PresentationLayer.NewLocation
{
    /// <summary>
    /// Interaction logic for NewLocationUserControl1.xaml
    /// </summary>
    public partial class NewLocationUserControl1 : UserControl
    {
        /* public constructor used by NewIndividualWindow.xaml.cs
        *
        *  Added by Eoin K 11/12/20
        */
        public NewLocationUserControl1()
        {            
            LocationName = "";
            Address = "";
            // this string should be a valid postal code
            PostalCode = "";
            Country = "";

            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;
        }

        /* public property Name to hold the name of the new location
        *  A text box is binded to the value of Name in the xaml file
        *  So any changes to from the WPF window will change the value of the public property
        *  
        *  Is name LocationName instead of Name as there is a conflict with an existing wpf property
        *
        *  Added by Eoin K 11/12/20
        */
        public string LocationName { get; set; }

        /* public property Address to hold the address of the new location
        *  A text box is binded to the value of Address in the xaml file
        *  So any changes to from the WPF window will change the value of the public property
        *
        *  Added by Eoin K 11/12/20
        */
        public string Address { get; set; }

        /* public property PostalCode to hold the postal code of the new location
        *  A text box is binded to the value of PostalCode in the xaml file
        *  So any changes to from the WPF window will change the value of the public property
        *
        *  Added by Eoin K 11/12/20
        */
        public string PostalCode { get; set; }

        /* public property Country to hold the country of the new location
        *  A text box is binded to the value of Country in the xaml file
        *  So any changes to from the WPF window will change the value of the public property
        *
        *  Added by Eoin K 11/12/20
        */
        public string Country { get; set; }

        /* private method to control the visibility of the invalid postal code label
        *
        *  Added by Eoin K 11/12/20
        */
        private void ChangeInvalidMessageVisibilty(Visibility l_Visibility)
        {
            Lbl_InvalidPostalCodeFormat.Visibility = l_Visibility;
        }

        /* private method called when the PostalCode textbox changes value
        *  Checks if the value is valid and displays a invalid message if validation returns false
        *
        *  Added by Eoin K 11/12/20
        */
        private void TxtBox_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainWindow.BusinessController.ValidPostalCode(TxtBox_PostalCode.Text))
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
