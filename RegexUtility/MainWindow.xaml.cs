using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.IO;


namespace RegexUtility
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            buscadorArchivo.ShowDialog();
            tbArchivoContenido.Text = buscadorArchivo.FileName;
        }

        private void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            if (tbArchivoContenido.Text == "")
            {
                lbEncontrados.Content = "Debe especificar el archivo";
                return;
            }
            string p = tbPatron.Text;
            Regex er;
            using (StreamReader sr= File.OpenText(buscadorArchivo.FileName))
            {
                tbResultado.Text = "";
                string texto = sr.ReadToEnd();
                tbResultado.AppendText(texto);
                if ((bool)cbMultilinea.IsChecked)
                {
                     er = new Regex(@p,RegexOptions.Multiline);
                }
                else
                {
                    er = new Regex(@p);
                }
                var x=er.Matches(texto);
                tbResultado.AppendText("\n---------------------------------------------------\n");
                foreach (var y in x)
                {
                    tbResultado.AppendText(String.Format("[{0}]\n",y));
                }
                lbEncontrados.Content=string.Format("Encontrados={0}", x.Count);
            }
            
        }

        private OpenFileDialog buscadorArchivo = new OpenFileDialog();
    }
}
