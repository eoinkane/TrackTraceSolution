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

namespace TrackTraceProject.PresentationLayer.GenerateContacts
{
    /// <summary>
    /// Interaction logic for GenerateContactsUserControl1.xaml
    /// </summary>
    public partial class GenerateContactsUserControl1 : UserControl
    {
        /* private field to store the selected individual ID
        *  is modified through 
        *  is accessed through SelectedIndividualID
        *
        *  Added by Eoin K 13/12/20
        */
        private int _SelectedIndividualID;

        /* public constructor used by GenerateContactsWindow.xaml.cs
        *
        *  Added by Eoin K 13/12/20
        */
        public GenerateContactsUserControl1(List<int> l_UserIDs)
        {
            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;

            // set the individual id as if they has not been selected yet
            _SelectedIndividualID = -1;

            // intialise the date time picker
            DateTimePicker_DateTime.Value = DateTime.Now;

            // add each single entry of user ids to the individual list box
            for (int i = 0; i < l_UserIDs.Count; i++)
            {
                ListBox_Individual.Items.Add($"User {l_UserIDs[i]}");
            };
        }

        /* private method called when the Selected Individual ID list box changes value
        *  sets a store of the value
        *
        *  Added by Eoin K 13/12/20
        */
        private void ListBox_Individual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore selections made when the list box loses focus
            if (ListBox_Individual.SelectedIndex == -1) return;

            // the id is set to the selected index plus one as the list box uses a zero-based index
            _SelectedIndividualID = ListBox_Individual.SelectedIndex + 1;
        }

        /* public property DateAndTime to hold the selected date and time
        *  Since this property is built on a getter the date time picker does not need a value changed handler
        *
        *  Added by Eoin K 13/12/20
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
        *  Added by Eoin K 13/12/20
        */
        public int SelectedIndividualID { get => _SelectedIndividualID; set { } }

        /* public method to check if a selection has been made
        *  Checks if a user has been selected in the individual list box
        *
        *  Added by Eoin K 13/12/20
        */
        public bool HasMadeSelection()
        {
            return (_SelectedIndividualID != -1);
        }
    }
}
