using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class PriceManager : IPriceManager
    {
        #region Fields
        IArticleRepository _articleRepository = new ArticleRepository();
        #endregion

        public void AddArticle(string name, double price, DateTime insertionDateTime)
        {
            if (_articleRepository.GetArticleByName(name) == null)
                throw new ArgumentException();

            var newArticle = new Article()
            {
                Name = name,
                Price = price,
                InsertionDateTime = DateTime.Now
            };

            _articleRepository.InsertArticle(newArticle);
        }

       public bool AddPromotion(string articleName, double discount, DateTime startDate, int durationDays)
       {
           var article = _articleRepository.GetArticleByName(articleName);
           if (article == null) return false;

           return _articleRepository.AddDiscount(article, discount, startDate, startDate.AddDays(durationDays));
       }

       public double GetPrice(string name, DateTime date)
       {
           var article = _articleRepository.GetArticleByName(name);
           if (article == null) new Exception(string.Format("Article {0} doesn't exist", name));

           var discount = article.Discounts.FirstOrDefault(t => t.StartDate >= date && t.EndDate <= date);

           return discount == null ? article.Price : article.Price + article.Price * discount.Value;
       }
       public IEnumerable<string> GetArticleNames()
       {
           return _articleRepository.GetArticles().Select(t => t.Name);
       }

       public void SetChristmasPeriod(DateTime startDate, DateTime endDate)
       {
           var articles = _articleRepository.GetArticles();

           foreach (var article in articles)
               AddPromotion(article.Name, 30, startDate, endDate.Day - startDate.Day);
           
       }
       public void SetClearancePeriod(DateTime startDate, int DaysDuration)
       {
           var articles = _articleRepository.GetArticles();

           foreach (var article in articles)
               AddPromotion(article.Name, 50, startDate, DaysDuration);
       }
    }
}
