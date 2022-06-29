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

namespace TrackTraceProject.PresentationLayer.RecordVisit
{
    /// <summary>
    /// Interaction logic for RecordVisitWindow.xaml
    /// </summary>
    public partial class RecordVisitWindow : Window
    {
        /* private field to store presentational position
        *  the window is built with a counter system that decides what logic to run
        *
        *  Added by Eoin K 11/12/20
        */
        private int _Position;

        /* private field to store the user control
        *  this user control is used to get the input for the new visit
        *
        *  Added by Eoin K 11/12/20
        */
        private RecordVisitUserControl1 _UserControl1;

        /* private method to run after the window has loaded
        *  Sets the starting position counter
        *  Sets the content of the content are to the record visit user control
        *
        *  Added by Eoin K 11/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _Position = 1;
            _UserControl1 = new RecordVisitUserControl1(MainWindow.BusinessController.ListUserIDs(), MainWindow.BusinessController.ListLocationIDs());
            ContentArea.Content = _UserControl1;
        }

        /* public constructor used by MainWindow.xaml.cs
        *
        *  Added by Eoin K 11/12/20
        */
        public RecordVisitWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        /* private method to handle a click
        *  if the position is 1 then it checks a selection has been made before recording a new visit
        *  if the position is 2 then it closes the record visit window
        *  
        *  Added by Eoin K 11/12/20
        */
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {

            switch (_Position)
            {
                case 1:
                    // Only create a new user if all selections in RecordVisitUserControl1 have been chosen
                    if (_UserControl1.HasMadeSelection())
                    {
                        // call business controller record visit
                        MainWindow.BusinessController.RecordVisit(
                            _UserControl1.SelectedIndividualID,
                            _UserControl1.SelectedLocationID,
                            _UserControl1.DateAndTime
                        );

                        ContentArea.Content = MainWindow.SuccessMessage("Visit Created");
                        Btn_Next.Content = "Close";
                        _Position++;
                    }
                    else
                    {
                        MessageBox.Show("Please make a selection in both the individual and location list.\nOne individual ID and one location ID need to be selected.");
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
