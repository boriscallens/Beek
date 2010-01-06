using System.Collections.Generic;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Model.Beek
{
    public class GenreFactory
    {
        private readonly IBeekRepository repository;

        public GenreFactory(IBeekRepository beekRepository)
        {
            repository = beekRepository;    
        }

        public IEnumerable<BaseGenre> RebuildGenreTree()
        {
            var autoBiography = new AutoBiographyGenre();
            var memoire = new MemoirGenre();
            var biography = new BiographyGenre();

            var alternateHistory = new AlternateHistoryGenre();
            var periodPiece = new PeriodPieceGenre();
            var costumeDrama = new CostumeDramaGenre();
            var jidaigeki = new JidaigekiGenre();
            var historicalFiction = new HistoricalFictionGenre();

            var historical = new HistoricalGenre();
            var genres = new List<BaseGenre>();
            
            biography.AddSubGenre(autoBiography);
            biography.AddSubGenre(memoire);
            historicalFiction.AddSubGenre(alternateHistory);
            historicalFiction.AddSubGenre(periodPiece);
            historicalFiction.AddSubGenre(costumeDrama);
            historicalFiction.AddSubGenre(jidaigeki);
            historical.AddSubGenre(biography);
            historical.AddSubGenre(historicalFiction);

            genres.Add(historical);
            return genres;
        }
    }
}