using System.ComponentModel.Composition;
using RearrangeItemsInListboxViaDragAndDrop.Applications.ViewModels;

namespace RearrangeItemsInListboxViaDragAndDrop.Applications.Controllers
{
    [Export]
    internal class ApplicationController
    {
        private readonly ShellViewModel shellViewModel;


        [ImportingConstructor]
        public ApplicationController(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }



        public void Initialize()
        {
        }

        public void Run()
        {
            shellViewModel.Show();
        }

        public void Shutdown()
        {
        }
    }
}
