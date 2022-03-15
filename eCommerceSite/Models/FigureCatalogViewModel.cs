namespace eCommerceSite.Models
{
    public class FigureCatalogViewModel
    {

        public FigureCatalogViewModel(List<Figure> figures, int lastPage, int currPage)
        {
            Figures = figures;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        public List<Figure> Figures { get; private set; }

        /// <summary>
        /// The last page of the catalog,
        /// calculated by having the total number of products
        /// divided by products per page.
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// Current page the user
        /// is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
