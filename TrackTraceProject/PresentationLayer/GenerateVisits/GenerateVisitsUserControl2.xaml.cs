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
    /// Interaction logic for GenerateVisitsUserControl2.xaml
    /// </summary>
    public partial class GenerateVisitsUserControl2 : UserControl
    {
        /* private field to store the generated visits as a text string
        *  this text is used when the user copys the generated visits to the clipboard 
        *
        *  Added by Eoin K 13/12/20
        */
        private string _ClipboardText = "";

        /* public constructor used by GenerateVisitsWindow.xaml.cs
        *
        *  Added by Eoin K 13/12/20
        */
        public GenerateVisitsUserControl2(int l_SelectedLocationID, DateTime l_SelectedStartDate, DateTime l_SelectedEndDate, List<string> l_GeneratedVisits)
        {
            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;

            // set label to display the selected location 
            Lbl_GeneratedVisits.Content = $"Generated Contacts at Location {l_SelectedLocationID} between {l_SelectedStartDate} and {l_SelectedStartDate}";

            // set the list box to hold the generated visits
            ListBox_GeneratedVisits.ItemsSource = l_GeneratedVisits;

            // turn the generated visits into clipboard text for the Copy To Clipboard Click function
            for (int i = 0; i < l_GeneratedVisits.Count; i++)
            {
                _ClipboardText += (l_GeneratedVisits[i] + "\r\n");
            }
        }

        /* private method called when the user clicks the copy to clipboard button
        *  method adds the content of the list box to the program user's computer clipboard
        *
        *  Added by Eoin K 13/12/20
        */
        private void Btn_CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_ClipboardText);
        }
    }
}
