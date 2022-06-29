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

namespace TrackTraceProject.PresentationLayer.RecordContact
{
    /// <summary>
    /// Interaction logic for RecordContactWindow.xaml
    /// </summary>
    public partial class RecordContactWindow : Window
    {
        /* private field to store presentational position
        *  the window is built with a counter system that decides what logic to run
        *
        *  Added by Eoin K 11/12/20
        */
        private int _Position;

        /* private field to store the user control
        *  this user control is used to get the input for the new contact
        *
        *  Added by Eoin K 11/12/20
        */
        private RecordContactUserControl1 _UserControl1;

        /* private method to run after the window has loaded
        *  Sets the starting position counter
        *  Sets the content of the content are to the record contact user control
        *
        *  Added by Eoin K 11/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _Position = 1;
            _UserControl1 = new RecordContactUserControl1(MainWindow.BusinessController.ListUserIDs());
            ContentArea.Content = _UserControl1;
        }

        /* public constructor used by MainWindow.xaml.cs
        *
        *  Added by Eoin K 11/12/20
        */
        public RecordContactWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        /* private method to handle a click
        *  if the position is 1 then it checks a selection has been made before recording a new contact
        *  if the position is 2 then it closes the record contact window
        *  
        *  Added by Eoin K 11/12/20
        */
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            
            switch (_Position)
            {
                case 1:
                    // Only create a new user if all selections in RecordContactUserControl1 have been chosen
                    if (_UserControl1.HasMadeSelection())
                    {
                        // call business controller create contact
                        // to ensure tidy data the lowest user id is passed as the first id and the highest is passed as the second
                        MainWindow.BusinessController.RecordContact(
                            Math.Min(_UserControl1.SelectedIndividualID1, _UserControl1.SelectedIndividualID2),
                            Math.Max(_UserControl1.SelectedIndividualID1, _UserControl1.SelectedIndividualID2),
                            _UserControl1.DateAndTime
                        );

                        ContentArea.Content = MainWindow.SuccessMessage("Contact Created");
                        Btn_Next.Content = "Close";
                        _Position++;
                    }
                    else
                    {
                        MessageBox.Show("Please make a selection in both individual lists.\nTwo individual IDs need to be selected.");
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
