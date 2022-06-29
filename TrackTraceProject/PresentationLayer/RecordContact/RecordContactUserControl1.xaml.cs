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

namespace TrackTraceProject.PresentationLayer.RecordContact
{
    /// <summary>
    /// Interaction logic for RecordContactUserControl1.xaml
    /// </summary>
    public partial class RecordContactUserControl1 : UserControl
    {
        /* private field to store the first selected individual ID
        *  is modified through ListBox_Individual1_SelectionChanged
        *  is accessed through SelectedIndividualID1
        *
        *  Added by Eoin K 10/12/20
        */
        private int _SelectedIndividualID1;

        /* private field to store the first selected individual ID
        *  is modified through ListBox_Individual2_SelectionChanged
        *  is accessed through SelectedIndividualID2
        *
        *  Added by Eoin K 10/12/20
        */
        private int _SelectedIndividualID2;


        /* public constructor used by RecordContactWindow.xaml.cs
        *
        *  Added by Eoin K 11/12/20
        */
        public RecordContactUserControl1(List<int> l_IDs)
        {
            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;

            // set the individual ids as if they has not been selected yet
            _SelectedIndividualID1 = -1;
            _SelectedIndividualID2 = -1;

            // intialise the date time picker
            DateTimePicker_DateTime.Value = DateTime.Now;

            // add the each single entry from users to the individual list boxes
            for (int i = 0; i < l_IDs.Count; i++)
            {
                ListBox_Individual1.Items.Add($"User {l_IDs[i]}");
                ListBox_Individual2.Items.Add($"User {l_IDs[i]}");
            };
        }

        /* private method called when the Selected Individual ID 1 list box changes value
        *  sets a store of the value
        *
        *  Added by Eoin K 11/12/20
        */
        private void ListBox_Individual1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore selections made when the list box loses focus
            if (ListBox_Individual1.SelectedIndex == -1) return;

            // the id is set to the selected index plus one as the list box uses a zero-based index
            _SelectedIndividualID1 = ListBox_Individual1.SelectedIndex + 1;
        }

        /* private method called when the Selected Individual ID 2 list box changes value
        *  sets a store of the value
        *
        *  Added by Eoin K 11/12/20
        */
        private void ListBox_Individual2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore selections made when the list box loses focus
            if (ListBox_Individual1.SelectedIndex == -1) return;

            // the id is set to the selected index plus one as the list box uses a zero-based index
            _SelectedIndividualID2 = ListBox_Individual2.SelectedIndex + 1;
        }

        /* public property DateAndTime to hold the selected date and time
        *  Since this property is built on a getter the date time picker does not need a value changed handler
        *
        *  Added by Eoin K 11/12/20
        */
        public DateTime DateAndTime { 
            get 
            {
                return (DateTime)DateTimePicker_DateTime.Value;
            }
            set { }
        }

        /* public property SelectedIndividualID1 to hold the id of the first selected user
        *  this property accesses the stored first selected user ID
        *
        *  Added by Eoin K 11/12/20
        */
        public int SelectedIndividualID1 { get => _SelectedIndividualID1; set { } }

        /* public property SelectedIndividualID1 to hold the id of the first selected user
        *  this property accesses the stored first selected user ID
        *
        *  Added by Eoin K 11/12/20
        */
        public int SelectedIndividualID2 { get => _SelectedIndividualID2; set { } }

        /* public method to check if all selections have been made
        *  Checks if a user has been selected in both list boxes
        *
        *  Added by Eoin K 11/12/20
        */
        public bool HasMadeSelection()
        {
            return ((_SelectedIndividualID1 != -1) && (_SelectedIndividualID2 != -1));
        }
    }
}
