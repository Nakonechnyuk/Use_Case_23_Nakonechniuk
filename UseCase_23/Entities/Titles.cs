namespace UseCase_23.Entities
{
    public class Titles
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Movie (series) name
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Title description
        /// </summary>
        public string? Description { get; set; } 

        /// <summary>
        /// The release Year
        /// </summary>
        public int ReleaseYear { get; set; }   
        
        /// <summary>
        /// Category of people who can watch film
        /// </summary>
        public string? AgeCertification { get; set; }
        
        /// <summary>
        /// Duration in a minutes 
        /// </summary>
        public int Runtime { get; set; }

        /// <summary>
        /// Genres of the movie
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// The production country in ISO 3166-1 Alpha-3 code
        /// </summary>
        public string ProductionCountry { get; set; }

        /// <summary>
        /// The number of seasons for series or be empty for movies
        /// </summary>
        public int? Seasons { get; set; }
    }
}
