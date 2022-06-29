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

namespace TrackTraceProject.PresentationLayer.NewLocation
{
    /// <summary>
    /// Interaction logic for NewLocationWindow.xaml
    /// </summary>
    public partial class NewLocationWindow : Window
    {
        /* private field to store presentational position
        *  the window is built with a counter system that decides what logic to run
        *
        *  Added by Eoin K 10/12/20
        */
        private int _Position;

        /* private field to store the user control
        *  this user control is used to get the input for the new location
        *
        *  Added by Eoin K 10/12/20
        */
        private NewLocationUserControl1 _UserControl1;

        /* private method to run after the window has loaded
        *  Sets the starting position counter and the business controller field
        *  Sets the content of the content are to the new location user control
        *
        *  Added by Eoin K 10/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _Position = 1;
            _UserControl1 = new NewLocationUserControl1();
            ContentArea.Content = _UserControl1;
        }

        /* public constructor used by NewLocationWindow.xaml.cs
        *
        *  Added by Eoin K 10/12/20
        */
        public NewLocationWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            switch (_Position)
            {
                case 1:
                    if (MainWindow.BusinessController.ValidPostalCode(_UserControl1.PostalCode))
                    {
                        MainWindow.BusinessController.CreateLocation(_UserControl1.LocationName, _UserControl1.Address.Replace(Environment.NewLine, ""), _UserControl1.PostalCode, _UserControl1.Country);
                        ContentArea.Content = MainWindow.SuccessMessage("Location Created");
                        Btn_Next.Content = "Close";
                    }
                    _Position++;
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
