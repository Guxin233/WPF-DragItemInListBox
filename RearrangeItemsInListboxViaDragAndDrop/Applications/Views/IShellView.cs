using System.Waf.Applications;

namespace RearrangeItemsInListboxViaDragAndDrop.Applications.Views
{
    internal interface IShellView : IView
    {
        void Show();

        void Close();
    }
}
