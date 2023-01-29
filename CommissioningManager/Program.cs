using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommissioningManager.Interfaces;
using CommissioningManager.Models;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace CommissioningManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = Bootstrap();

            Application.Run(container.GetInstance<DashBoard>());
        }

        private static Container Bootstrap()
        {
            var container = new Container();

            container.RegisterSingleton<IModel<LuxDataModel>, LuxDataModel>();
            container.RegisterSingleton<IModel<ScannerDataModel>, ScannerDataModel>();
            container.RegisterSingleton<IModel<TeleControllerDataModel>, TeleControllerDataModel>();

            AutoRegisterWindowsForms(container);

            container.Verify();

            return container;
        }

        private static void AutoRegisterWindowsForms(Container container)
        {
            var types = container.GetTypesToRegister<Form>(typeof(Program).Assembly);

            foreach (var type in types)
            {
                var registration =
                    Lifestyle.Transient.CreateRegistration(type, container);

                registration.SuppressDiagnosticWarning(
                    DiagnosticType.DisposableTransientComponent,
                    "Forms should be disposed by app code; not by the container.");

                container.AddRegistration(type, registration);
            }
        }
    }
}
