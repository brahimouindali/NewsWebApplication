using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class RatingService
    {
        private readonly IUnitOfWork<Rating> _rating;

        public RatingService(IUnitOfWork<Rating> rating)
        {
            _rating = rating;
        }

        public IEnumerable<Rating> GetRatings()
        {
            return _rating.Entity.GetAll();
        }
        public void Update(Rating rating)
        {
            _rating.Entity.Update(rating);
            _rating.Save();
        }

        public Rating GetRating(object id)
        {
            var rating = _rating.Entity.GetById(id);
            return rating;
        }

        public void Insert(Rating rating)
        {
            _rating.Entity.Insert(rating);
            _rating.Save();
        }

        public bool countRatings(string articleId)
        {
            var ratings = _rating.Entity.GetAll();
            var ratingsCount = ratings.Where(r => r.ArticleId == articleId).Count();

            return ratingsCount == 1 ? true : false;
        }
    }
}
