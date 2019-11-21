using Blog.Core.IRepository;
using Blog.Core.Model.Models;
using Blog.Core.Repository.BASE;

namespace Blog.Core.Repository
{
    public class BlogArticleRepository: BaseRepository<BlogArticle>, IBlogArticleRepository
    {
        
    }
}