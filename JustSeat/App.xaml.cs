using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JustSeat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<IDialogService>(() => new DialogService());
        }
    }
}
