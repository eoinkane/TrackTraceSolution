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
using System.Windows.Shapes;

namespace TrackTraceProject.PresentationLayer.GenerateContacts
{
    /// <summary>
    /// Interaction logic for GenerateContactsWindow.xaml
    /// </summary>
    public partial class GenerateContactsWindow : Window
    {
        /* private field to store presentational position
        *  the window is built with a counter system that decides what logic to run
        *
        *  Added by Eoin K 13/12/20
        */
        private int _Position;

        /* private field to store the user control
        *  this user control is used to get the input for generating a list of contacts
        *
        *  Added by Eoin K 13/12/20
        */
        private GenerateContactsUserControl1 _UserControl1;

        /* private method to run after the window has loaded
        *  Sets the starting position counter
        *  Sets the content of the content are to the generate contacts user control
        *
        *  Added by Eoin K 13/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _Position = 1;
            _UserControl1 = new GenerateContactsUserControl1(MainWindow.BusinessController.ListUserIDs());
            ContentArea.Content = _UserControl1;
        }

        /* public constructor used by MainWindow.xaml.cs
        *
        *  Added by Eoin K 13/12/20
        */
        public GenerateContactsWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        /* private method to handle a click
        *  if the position is 1 then it checks a selection has been made before searching for contacts with the selected info
        *  if the position is 2 then it closes the generate contacts window
        *  
        *  Added by Eoin K 13/12/20
        */
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {

            switch (_Position)
            {
                case 1:
                    // Only create a new user if all selections in GenerateContactsUserControl1 have been chosen
                    if (_UserControl1.HasMadeSelection())
                    {
                        // call business controller generate contacts list
                        List<string> contacts = MainWindow.BusinessController.GenerateContacts(
                            _UserControl1.DateAndTime,
                            _UserControl1.SelectedIndividualID
                        );

                        if (contacts.Count == 0)
                        {
                            MessageBox.Show(
                                $"No Contacts were found with User {_UserControl1.SelectedIndividualID} after {_UserControl1.DateAndTime}" +
                                "\nYou can try again by selecting different individual or a different date"
                            );
                            return;
                        }

                        ContentArea.Content = new GenerateContactsUserControl2(_UserControl1.SelectedIndividualID, _UserControl1.DateAndTime, contacts);

                        Btn_Next.Content = "Close";
                        _Position++;
                    }
                    else
                    {
                        MessageBox.Show("Please select a individual and Date & Time.\nOne individual ID and one Date & Time need to be selected.");
                    }
                    break;
                case 2:
                    Close();
                    break;
                default:
                    break;
            }
        }
    }
}
