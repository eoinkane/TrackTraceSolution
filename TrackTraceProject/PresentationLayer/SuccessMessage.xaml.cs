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

namespace TrackTraceProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for SuccessMessage.xaml
    /// </summary>
    public partial class SuccessMessage : UserControl
    {
        /* public constructor to initialise the Success Message
        *
        *  Added by Eoin K 11/12/20
        */
        public SuccessMessage(string l_Message)
        {
            Message = l_Message;
            InitializeComponent();
            // This line allows for any properties to be passed to the User Control from it parent object
            DataContext = this;
        }

        /* public property Message to hold the message to display
        *  A label is binded to the value of PhoneNumber in the xaml file
        *
        *  Added by Eoin K 11/12/20
        */
        public string Message { get; set; }
    }
}
