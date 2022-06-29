using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TrackTraceProject.PresentationLayer;

namespace TrackTraceProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* private field to store the business controller for the presentation layer
        * _BusinessController is used for all interactions with the  business layer for all windows
        *
        *  Added by Eoin K 11/12/20
        */
        private static BusinessController _BusinessController;

        /* public constructor used by MainWindow.xaml
        *
        *  Added by Eoin K 11/12/20
        */
        public MainWindow()
        {            
            InitializeComponent();
            Loaded += Start;
            Closed += Finish;
        }

        /* public property for windows to access the business controller
        *
        *  Added by Eoin K 11/12/20
        */
        public static BusinessController BusinessController { get => _BusinessController; set { } }

        /* public method for windows to generate a success message
        *
        *  Added by Eoin K 11/12/20
        */
        public static SuccessMessage SuccessMessage(string l_String)
        {
            return new SuccessMessage(l_String);
        }

        /* private method to run after the window has loaded
        * _BusinessController is set the the instance of BusinessController
        * _BusinessController loads persisted data 
        *
        *  Added by Eoin K 11/12/20
        */
        private void Start(object sender, RoutedEventArgs e)
        {
            _BusinessController = BusinessController.Instance;
            _BusinessController.Load();
        }

        /* private method to run when the window was closed
        * _BusinessController saves the data to the persistant storage
        *
        *  Added by Eoin K 11/12/20
        */
        private void Finish(object sender, EventArgs e)
        {
            _BusinessController.Save();
        }

        /* private method used to open the new individual window
        *
        *  Added by Eoin K 11/12/20
        */
        private void Btn_NewIndividual_Click(object sender, RoutedEventArgs e)
        {
            new PresentationLayer.NewIndividual.NewIndividualWindow().ShowDialog();
        }

        /* private method used to open the new location window
        *
        *  Added by Eoin K 11/12/20
        */
        private void Btn_NewLocation_Click(object sender, RoutedEventArgs e)
        {
            new PresentationLayer.NewLocation.NewLocationWindow().ShowDialog();
        }

        /* private method used to open the record contact window
        *
        *  Added by Eoin K 11/12/20
        */
        private void Btn_RecordContact_Click(object sender, RoutedEventArgs e)
        {
            if(_BusinessController.EnoughContactData())
            {
                new PresentationLayer.RecordContact.RecordContactWindow().ShowDialog();
            }
            else
            {
                MessageBox.Show("Please create more individuals.\nContact recording requires at least 2 individuals.");
            }
        }

        /* private method used to open the record visit window
        *
        *  Added by Eoin K 11/12/20
        */
        private void Btn_RecordVisit_Click(object sender, RoutedEventArgs e)
        {
            if (_BusinessController.EnoughVisitData())
            {
                new PresentationLayer.RecordVisit.RecordVisitWindow().ShowDialog();
            }
            else
            {
                MessageBox.Show("Please record enough data.\nVisit recording requires at least 1 individuals and 1 location.");
            }
        }

        /* private method used to open the generate contacts window
        *
        *  Added by Eoin K 13/12/20
        */
        private void Btn_GenerateContacts_Click(object sender, RoutedEventArgs e)
        {
            if (_BusinessController.EnoughContactListData())
            {
                new PresentationLayer.GenerateContacts.GenerateContactsWindow().ShowDialog();
            }
            else
            {
                MessageBox.Show("Please record enough data.\nContact generation requires at least 1 contact.");
            }
        }

        /* private method used to open the generate visits window
        *
        *  Added by Eoin K 13/12/20
        */
        private void Btn_GenerateVisits_Click(object sender, RoutedEventArgs e)
        {
            if (_BusinessController.EnoughVisitListData())
            {
                new PresentationLayer.GenerateVisits.GenerateVisitsWindow().ShowDialog();
            }
            else
            {
                MessageBox.Show("Please record enough data.\nVisit generation requires at least 1 visit.");
            }
        }
    }
}
