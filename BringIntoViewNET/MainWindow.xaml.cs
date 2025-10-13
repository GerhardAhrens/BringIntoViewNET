//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Lifeprojects.de">
//     Class: MainWindow
//     Copyright © Lifeprojects.de yyyy
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>dd.MM.yyyy</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BringIntoViewNET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            this.InitializeComponent();
            this.WindowTitel = "Minimal WPF Template";
            WeakEventManager<Window, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);

            List.RequestBringIntoView += ListRequestBringToView;

            DataContext = new[]
            {
                Tuple.Create("John", new SolidColorBrush(Colors.Fuchsia)),
                Tuple.Create("Ryan", new SolidColorBrush(Colors.LawnGreen)),
                Tuple.Create("Marlon", new SolidColorBrush(Colors.Gold)),
                Tuple.Create("Charlie", new SolidColorBrush(Colors.DeepSkyBlue)),
            };
        }

        private string _WindowTitel;

        public string WindowTitel
        {
            get { return _WindowTitel; }
            set
            {
                if (this._WindowTitel != value)
                {
                    this._WindowTitel = value;
                    this.OnPropertyChanged();
                }
            }
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void ListRequestBringToView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = DisableAutoScroll.IsChecked.GetValueOrDefault();
        }

        private void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = List.ItemContainerGenerator.ContainerFromItem(SelectionList.SelectedItem) as FrameworkElement;
            if (item == null)
            {
                return;
            }

            item.BringIntoView();
        }

        #region INotifyPropertyChanged implementierung
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }

            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
        #endregion INotifyPropertyChanged implementierung
    }
}