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

namespace TrackTraceProject.PresentationLayer.GenerateVisits
{
    /// <summary>
    /// Interaction logic for GenerateVisitsWindow.xaml
    /// </summary>
    public partial class GenerateVisitsWindow : Window
    {
        /* private field to store presentational position
        *  the window is built with a counter system that decides what logic to run
        *
        *  Added by Eoin K 13/12/20
        */
        private int _Position;

        /* private field to store the user control
        *  this user control is used to get the input for generating a list of visits
        *
        *  Added by Eoin K 13/12/20
        */
        private GenerateVisitsUserControl1 _UserControl1;

        /* private method to run after the window has loaded
        *  Sets the starting position counter
        *  Sets the content of the content are to the generate visits user control
        *
        *  Added by Eoin K 13/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _Position = 1;
            _UserControl1 = new GenerateVisitsUserControl1(MainWindow.BusinessController.ListLocationIDs());
            ContentArea.Content = _UserControl1;
        }

        /* public constructor used by MainWindow.xaml.cs
        *
        *  Added by Eoin K 13/12/20
        */
        public GenerateVisitsWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        /* private method to handle a click
        *  if the position is 1 then it checks a selection has been made before searching for visits with the selected info
        *  if the position is 2 then it closes the generate visits window
        *  
        *  Added by Eoin K 13/12/20
        */
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {

            switch (_Position)
            {
                case 1:
                    // Only create a new user if all selections in GenerateVisitsUserControl1 have been chosen
                    if (_UserControl1.HasMadeValidSelection())
                    {
                        // call business controller generate visits list
                        List<string> visits = MainWindow.BusinessController.GenerateVisits(
                            _UserControl1.StartDateAndTime,
                            _UserControl1.EndDateAndTime,
                            _UserControl1.SelectedLocationID
                        );

                        if (visits.Count == 0)
                        {
                            MessageBox.Show(
                                $"No Visits were found with Location {_UserControl1.SelectedLocationID} between {_UserControl1.StartDateAndTime} and {_UserControl1.EndDateAndTime}" +
                                "\nYou can try again by selecting different location or different dates"
                            );
                            return;
                        }

                        ContentArea.Content = new GenerateVisitsUserControl2(_UserControl1.SelectedLocationID, _UserControl1.StartDateAndTime, _UserControl1.EndDateAndTime, visits);

                        Btn_Next.Content = "Close";
                        _Position++;
                    }
                    else
                    {
                        MessageBox.Show("Please select a individual, a location, a starting Date & Time and a finishing Date & Time.\nThis data is needed to generate a list of visits.");
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
