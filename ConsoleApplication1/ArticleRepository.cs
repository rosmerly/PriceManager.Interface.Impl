using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ArticleRepository : IArticleRepository
    {
        #region Fields
        IList<Article> _articles = new List<Article>();
        #endregion

        public Article GetArticleByName(string name)
        {
            return _articles.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerable<Article> GetArticles()
        {
            return _articles;
        }

        public void InsertArticle(Article article)
        {
            _articles.Add(article);
        }

        public bool AddDiscount(Article article, double discount, DateTime startDate, DateTime endDate)
        {
            //overlaping?
            var discountOverlaping = article.Discounts.FirstOrDefault(t => t.StartDate >= startDate || t.EndDate <= endDate);

            if (discountOverlaping != null)
            {
                if (discount < discountOverlaping.Value) return false;

                //is not the same period
                if (startDate != discountOverlaping.StartDate && endDate != discountOverlaping.EndDate)
                {
                    var atBegining = (endDate <= discountOverlaping.EndDate);
                    var atEnd = (startDate >= discountOverlaping.StartDate);

                    if (atEnd)
                    {
                        article.Discounts.Add(new Discount()
                            {
                                Value = discountOverlaping.Value,
                                StartDate = discountOverlaping.StartDate,
                                EndDate = startDate.AddDays(-1)
                            });
                    }
                    if (atBegining)
                    {
                        article.Discounts.Add(new Discount()
                        {
                            Value = discountOverlaping.Value,
                            StartDate = endDate.AddDays(1),
                            EndDate = discountOverlaping.EndDate
                        });
                    }
                }
                article.Discounts.Remove(discountOverlaping);
            }

            article.Discounts.Add(new Discount()
            {
                Value = discount,
                StartDate = startDate,
                EndDate = endDate
            });

            return true;
        }
    }
}
