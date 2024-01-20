using TrucksLOG.Model;

namespace TrucksLOG.ViewModel
{
    class ImpressumVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ImpressumVM()
        {
            _pageModel = new PageModel();
        }
    }
}
