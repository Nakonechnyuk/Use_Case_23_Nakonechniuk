using Bogus;
using UseCase_23.Entities;
using System.Text;

namespace UseCase_23
{
    /// <summary>
    /// Class for creating fake data
    /// </summary>
    public static class DataGenerator
    {
        public static readonly string separator = ",";

        public static readonly string pathTofolder = Path.GetFullPath(@"..\..\..\");

        private static List<Credit> CreditGeneratorForOneTitle(int titleId)
        {

            var creditIds = 1;
            var fakedTitleIds = new[] {1, 2, 3};
            var roles = new[] {
                "Director", "Producer", "Screenwriter",
                "Actor", "Actress", "Cinematographer",
                "Film Editor", "Production Designer",
                "Costume Designer", "Music Composer"
            };

            var fakeCredits = new Faker<Credit>()
                .RuleFor(x => x.Id, y => creditIds++)
                .RuleFor(x => x.TitlesId, y => titleId)
                .RuleFor(x => x.RealName, y => y.Name.FullName())
                .RuleFor(x => x.CharacterName, y => y.Name.LastName())
                .RuleFor(x => x.Role, y => y.PickRandom(roles));

            return fakeCredits.Generate(new Random().Next(1, 6));
        }

        public static List<Titles> TitlesGenerator(int titlesCount)
        {
            var titlesIds = 1;
            var ageCertificationsList = new[] {
                "G", "PG", "PG-13", "R", "NC-17", "U", "U/A", "A", "S", "AL", "6", "9", "12", "12A",
                "15", "18", "18R", "R18", "R21", "M", "MA15+", "R16", "R18+", "X18", "T", "E", "E10+",
                "EC", "C", "CA", "GP", "M/PG", "TV-Y", "TV-Y7", "TV-G", "TV-PG", "TV-14", "TV-MA" };

            var genresList = new[] {
                "Horor", "Action","Western","Gangster", "Detective", "Drama",
                "Historical", "Comedy", "Melodrama", "Musical",
                "Noir", "Political", "Adventure", "Fairy tale", "Tragedy", "Tragicomedy" };

            var fakerTitles = new Faker<Titles>()
               .RuleFor(x => x.Id, y => titlesIds++)
               .RuleFor(x => x.Title, f => f.Lorem.Word())
               .RuleFor(x => x.Description, f => f.Lorem.Sentence())
               .RuleFor(u => u.ReleaseYear, f => f.Random.Int(1900, 2016))
               .RuleFor(x => x.AgeCertification, y => y.PickRandom(ageCertificationsList))
               .RuleFor(x => x.Runtime, y => y.Random.Int(20, 300))
               .RuleFor(x => x.Genres, y => y.Make(3, () => y.PickRandom(genresList)))
               .RuleFor(x => x.ProductionCountry, y => y.Address.CountryCode())
               .RuleFor(x => x.Seasons, y => y.Random.Int(1, 10).OrNull(y, 0.15f));

            return fakerTitles.Generate(titlesCount);
        }

        public static List<Credit> FullListCreditsGenerator(IEnumerable<int> listTitletIds)
        {
            var listCredits = new List<Credit>();

            foreach (var titleId in listTitletIds)
            {
                listCredits.AddRange(CreditGeneratorForOneTitle(titleId));
            }

            return listCredits;
        }

        public static void WriteToCSVFileCredits (List<Credit> list)
        {
            var pathFolder = $"Files\\CreditList.csv";
            string rootPath = pathTofolder + pathFolder;
            string[] headings = { "id", "title_id", "real_name", "character_name", "role" };
            var output = new StringBuilder();
            
            if (!File.Exists(rootPath))
            {
                output.AppendLine(string.Join(separator, headings));
            }
           
            foreach (var item in list)
            {
                string[] newLine = {
                    item.Id.ToString(),
                    item.TitlesId.ToString(),
                    item.RealName?.ToString(),
                    item.CharacterName.ToString(),
                    item.Role?.ToString(),
                };

                output.AppendLine(string.Join(separator, newLine));
            }

            WriteToFile(rootPath, output);
        }

        public static void WriteToCSVFileTitles(List<Titles> listTitles)
        {
            var pathFolder = $"Files\\TitlesList.csv";
            string rootPath = pathTofolder + pathFolder;
            string[] headings = { "id", "title", "description", "release_year", "age_certification", "runtime", "genres", "production_country", "seasons" };
            var output = new StringBuilder();
            
            if (!File.Exists(rootPath))
            {
                output.AppendLine(string.Join(separator, headings));
            }
            
            foreach (var item in listTitles)
            {
                    string[] newLine = {
                    item.Id.ToString(),
                    item.Title?.ToString(),
                    item.Description?.ToString(),
                    item.ReleaseYear.ToString(),
                    item.AgeCertification?.ToString(),
                    item.Runtime.ToString(),
                    string.Join(";", item.Genres),
                    item.ProductionCountry.ToString(),
                    item.Seasons?.ToString(),
                };

                output.AppendLine(string.Join(separator, newLine));
            }

            WriteToFile(rootPath, output);
        }

        private static void WriteToFile(string rootPath, StringBuilder output)
        {
            try
            {
                File.AppendAllText(rootPath, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.", ex.Message);
                return;
            }
        }
    }
}
