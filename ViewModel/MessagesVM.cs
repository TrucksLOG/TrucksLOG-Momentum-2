using TrucksLOG.Model;

namespace TrucksLOG.ViewModel
{
    internal class MessagesVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public MessagesVM()
        {
            _pageModel = new PageModel();
        }
    }
}
