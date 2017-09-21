using System.Waf.Applications;

namespace DragItemInListBox.Applications.Views
{
    internal interface IShellView : IView
    {
        void Show();

        void Close();
    }
}
