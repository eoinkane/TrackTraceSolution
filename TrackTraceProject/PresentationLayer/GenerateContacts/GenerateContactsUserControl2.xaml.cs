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
    /// Interaction logic for GenerateContactsUserControl2.xaml
    /// </summary>
    public partial class GenerateContactsUserControl2 : UserControl
    {
        /* private field to store the generated contacts as a text string
        *  this text is used when the user copys the generated contacts to the clipboard 
        *
        *  Added by Eoin K 13/12/20
        */
        private string _ClipboardText = "";

        /* public constructor used by GenerateContactsWindow.xaml.cs
        *
        *  Added by Eoin K 13/12/20
        */
        public GenerateContactsUserControl2(int l_SelectedIndividualID, DateTime l_SelectedDate, List<string> l_GeneratedContacts)
        {
            InitializeComponent();

            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;

            // set label to display the selected user
            Lbl_GeneratedContacts.Content = $"Generated Contacts of User {l_SelectedIndividualID} after {l_SelectedDate}";

            // set the list box to hold the generated contacts
            ListBox_GeneratedContacts.ItemsSource = l_GeneratedContacts;

            // turn the generated contacts into clipboard text for the Copy To Clipboard Click function
            for (int i = 0; i < l_GeneratedContacts.Count; i++)
            {
                _ClipboardText += (l_GeneratedContacts[i] + "\r\n");
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
