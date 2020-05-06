using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The current search terms
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; }
        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public HashSet<string> Genres { get; set; }

        /// <summary>
        /// The min IMDB rating
        /// </summary>
        [BindProperty]
        public double IMDBMin { get; set; }

        /// <summary>
        /// The max IMDB rating 
        /// </summary>
        [BindProperty]
        public double IMDBMax { get; set; }
        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }
        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax)
        {
            //this.IMDBMin = IMDBMin;
            //this.IMDBMax = IMDBMax;
            if(SearchTerms != null)
                Movies = MovieDatabase.All.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
            if(MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }
            if(Genres != null)
            {
                Movies = Movies.Where(movie => Genres.Contains(movie.MajorGenre));
            }
            if(IMDBMax != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
            }
            if (IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin);
            }

        }


    }
}
