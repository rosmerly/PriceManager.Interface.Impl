using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface IArticleRepository
    {
        Article GetArticleByName(string name);
        IEnumerable<Article> GetArticles();


        void InsertArticle(Article article);
        bool AddDiscount(Article article, double discount, DateTime startDate, DateTime endDate);
    }
}
