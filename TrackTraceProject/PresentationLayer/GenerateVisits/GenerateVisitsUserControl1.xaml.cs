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

namespace TrackTraceProject.PresentationLayer.GenerateVisits
{
    /// <summary>
    /// Interaction logic for GenerateVisitsUserControl1.xaml
    /// </summary>
    public partial class GenerateVisitsUserControl1 : UserControl
    {
        /* private field to store the selected individual ID
        *  is modified through 
        *  is accessed through SelectedIndividualID
        *
        *  Added by Eoin K 13/12/20
        */
        private int _SelectedLocationID;

        /* public constructor used by GenerateVisitsWindow.xaml.cs
        *
        *  Added by Eoin K 13/12/20
        */
        public GenerateVisitsUserControl1(List<int> l_LocationIDs)
        {
            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;

            // set the location id as it has not been selected yet
            _SelectedLocationID = -1;

            // intialise the date time picker
            DateTimePicker_StartDateTime.Value = DateTime.Now;
            DateTimePicker_EndDateTime.Value = DateTime.Now;

            // add each single entry of locations ids to the individual list box
            for (int i = 0; i < l_LocationIDs.Count; i++)
            {
                ListBox_Location.Items.Add($"Location {l_LocationIDs[i]}");
            };
        }

        /* private method called when the Selected Location ID list box changes value
        *  sets a store of the value
        *
        *  Added by Eoin K 13/12/20
        */
        private void ListBox_Location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore selections made when the list box loses focus
            if (ListBox_Location.SelectedIndex == -1) return;

            // the id is set to the selected index plus one as the list box uses a zero-based index
            _SelectedLocationID = ListBox_Location.SelectedIndex + 1;
        }

        /* public property StartDateAndTime to hold the selected start date and time
        *  Since this property is built on a getter the date time picker does not need a value changed handler
        *
        *  Added by Eoin K 13/12/20
        */
        public DateTime StartDateAndTime
        {
            get
            {
                return (DateTime)DateTimePicker_StartDateTime.Value;
            }
            set { }
        }

        /* public property EndDateAndTime to hold the selected end date and time
        *  Since this property is built on a getter the date time picker does not need a value changed handler
        *
        *  Added by Eoin K 13/12/20
        */
        public DateTime EndDateAndTime
        {
            get
            {
                return (DateTime)DateTimePicker_EndDateTime.Value;
            }
            set { }
        }

        /* public property SelectedIndividualID to hold the id of the selected user
        *  this property accesses the stored selected user ID
        *
        *  Added by Eoin K 13/12/20
        */
        public int SelectedLocationID { get => _SelectedLocationID; set { } }

        /* public method to check if a selection has been made
        *  Checks if a user has been selected in the location list box
        *
        *  Added by Eoin K 13/12/20
        */
        public bool HasMadeValidSelection()
        {
            return (_SelectedLocationID != -1) && (StartDateAndTime < EndDateAndTime);
        }

        /* private method called when either of the date time pickers change
        *  Checks if the two values are valid and displays a invalid message if validation returns false
        *
        *  Added by Eoin K 11/12/20
        */
        private void DateTimePicker_DateTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DateTimePicker_StartDateTime.Value == null || DateTimePicker_EndDateTime.Value == null) return;

            if (StartDateAndTime >= EndDateAndTime)
            {
                ChangeInvalidMessageVisibilty(Visibility.Visible);
            } else
            {
                ChangeInvalidMessageVisibilty(Visibility.Hidden);
            }
        }

        /* private method to control the visibility of the invalid date labels
        *
        *  Added by Eoin K 13/12/20
        */
        private void ChangeInvalidMessageVisibilty(Visibility l_Visibility)
        {
            Lbl_InvalidStartDate.Visibility = l_Visibility;
            Lbl_InvalidEndDate.Visibility = l_Visibility;
        }
    }
}
