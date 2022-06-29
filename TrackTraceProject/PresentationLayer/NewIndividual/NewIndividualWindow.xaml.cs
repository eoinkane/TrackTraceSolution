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

namespace TrackTraceProject.PresentationLayer.NewIndividual
{
    /// <summary>
    /// Interaction logic for NewIndividualWindow.xaml
    /// </summary>
    public partial class NewIndividualWindow : Window
    {
        /* private field to store presentational position
        *  the window is built with a counter system that decides what logic to run
        *
        *  Added by Eoin K 10/12/20
        */
        private int _Position;

        /* private field to store the user control
        *  this user control is used to get the input for the new user
        *
        *  Added by Eoin K 10/12/20
        */
        private NewIndividualUserControl1 _UserControl1;

        /* private method to run after the window has loaded
        *  Sets the starting position counter
        *  Sets the content of the content are to the new individual user control
        *
        *  Added by Eoin K 10/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _Position = 1;
            _UserControl1 = new NewIndividualUserControl1();
            ContentArea.Content = _UserControl1;
        }

        /* public constructor used by MainWindow.xaml.cs
        *
        *  Added by Eoin K 10/12/20
        */
        public NewIndividualWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        /* private method to handle a click
        *  if the position is 1 then it runs validation before creating a new user
        *  if the position is 2 then it closes the new individual window
        *  
        *  Added by Eoin K 11/12/20
        */
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            switch (_Position)
            {
                case 1:
                    if (MainWindow.BusinessController.ValidPhoneNumber(_UserControl1.PhoneNumber))
                    {
                        MainWindow.BusinessController.CreateUser(_UserControl1.PhoneNumber);
                        ContentArea.Content = MainWindow.SuccessMessage("User Created");
                        Btn_Next.Content = "Close";
                        _Position++;
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
