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

namespace wpp_Taller.View
{
    /// <summary>
    /// Lógica de interacción para ReportProfromPDF.xaml
    /// </summary>
    public partial class ReportProfromPDF : Window
    {
        int idProform;
        public ReportProfromPDF(int idP)
        {
            InitializeComponent();
            idProform = idP;
        }



    }
}
