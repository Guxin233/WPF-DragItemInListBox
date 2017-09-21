using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Waf.Foundation;
using System.Windows;
using RearrangeItemsInListboxViaDragAndDrop.Applications.Controllers;

namespace RearrangeItemsInListboxViaDragAndDrop
{
    public partial class App : Application
    {
        private AggregateCatalog catalog;
        private CompositionContainer container;
        private ApplicationController controller;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Model).Assembly));
            // Add the WafApplication assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);

            controller = container.GetExportedValue<ApplicationController>();
            controller.Initialize();
            controller.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            controller.Shutdown();
            container.Dispose();
            catalog.Dispose();

            base.OnExit(e);
        }
    }
}
