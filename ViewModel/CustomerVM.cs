using TrucksLOG.Model;

namespace TrucksLOG.ViewModel
{
    class CustomerVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public CustomerVM()
        {
            _pageModel = new PageModel();
        }
    }
}
