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

namespace TrackTraceProject.PresentationLayer.RecordVisit
{
    /// <summary>
    /// Interaction logic for RecordVisitUserControl1.xaml
    /// </summary>
    public partial class RecordVisitUserControl1 : UserControl
    {
        /* private field to store the selected individual ID
        *  is modified through 
        *  is accessed through SelectedIndividualID
        *
        *  Added by Eoin K 10/12/20
        */
        private int _SelectedIndividualID;

        /* private field to store the selected location ID
        *  is modified through 
        *  is accessed through SelectedLocationID
        *
        *  Added by Eoin K 10/12/20
        */
        private int _SelectedLocationID;


        /* public constructor used by RecordContactWindow.xaml.cs
        *
        *  Added by Eoin K 11/12/20
        */
        public RecordVisitUserControl1(List<int> l_UserIDs, List<int> l_LocationIDs)
        {
            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;

            // set the individual ids as if they has not been selected yet
            _SelectedIndividualID = -1;
            _SelectedLocationID = -1;

            // intialise the date time picker
            DateTimePicker_DateTime.Value = DateTime.Now;

            // add each single entry of user to the individual list box
            for (int i = 0; i < l_UserIDs.Count; i++)
            {
                ListBox_Individual.Items.Add($"User {l_UserIDs[i]}");
            };

            // add each singel entry of location to the location list box
            for (int i = 0; i < l_LocationIDs.Count; i++)
            {
                ListBox_Location.Items.Add($"Location {l_LocationIDs[i]}");
            };
        }

        /* private method called when the Selected Individual ID list box changes value
        *  sets a store of the value
        *
        *  Added by Eoin K 11/12/20
        */
        private void ListBox_Individual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore selections made when the list box loses focus
            if (ListBox_Individual.SelectedIndex == -1) return;

            // the id is set to the selected index plus one as the list box uses a zero-based index
            _SelectedIndividualID = ListBox_Individual.SelectedIndex + 1;
        }

        /* private method called when the Selected Location ID list box changes value
        *  sets a store of the value
        *
        *  Added by Eoin K 11/12/20
        */
        private void ListBox_Location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore selections made when the list box loses focus
            if (ListBox_Location.SelectedIndex == -1) return;

            // the id is set to the selected index plus one as the list box uses a zero-based index
            _SelectedLocationID = ListBox_Location.SelectedIndex + 1;
        }

        /* public property DateAndTime to hold the selected date and time
        *  Since this property is built on a getter the date time picker does not need a value changed handler
        *
        *  Added by Eoin K 11/12/20
        */
        public DateTime DateAndTime
        {
            get
            {
                return (DateTime)DateTimePicker_DateTime.Value;
            }
            set { }
        }

        /* public property SelectedIndividualID to hold the id of the selected user
        *  this property accesses the stored selected user ID
        *
        *  Added by Eoin K 11/12/20
        */
        public int SelectedIndividualID { get => _SelectedIndividualID; set { } }

        /* public property _SelectedLocationID to hold the id of the selected location
        *  this property accesses the stored first selected location ID
        *
        *  Added by Eoin K 11/12/20
        */
        public int SelectedLocationID { get => _SelectedLocationID; set { } }

        /* public method to check if all selections have been made
        *  Checks if a user has been selected in both list boxes
        *
        *  Added by Eoin K 11/12/20
        */
        public bool HasMadeSelection()
        {
            return ((_SelectedIndividualID != -1) && (_SelectedLocationID != -1));
        }
    }
}
