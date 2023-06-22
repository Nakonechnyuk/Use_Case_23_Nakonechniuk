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
        private const string Separator = ",";

        private static readonly string PathToFolder = Path.GetFullPath(@"..\..\..\");

        private static int _creditIds = 1;
        
        /// <summary>
        /// Generate random amount(from 1 to 6) of credits per one title
        /// </summary>
        /// <param name="titleId">The unique identifier of the title</param>
        /// <returns>List of credits</returns>
        private static List<Credit> CreditGeneratorForOneTitle(int titleId)
        {
            var roles = new[] {
                "Director", "Producer", "Screenwriter",
                "Actor", "Actress", "Cinematographer",
                "Film Editor", "Production Designer",
                "Costume Designer", "Music Composer"
            };

            var fakeCredits = new Faker<Credit>()
                .RuleFor(x => x.Id, y => _creditIds++)
                .RuleFor(x => x.TitlesId, y => titleId)
                .RuleFor(x => x.RealName, y => y.Name.FullName().OrNull(y, 0.2f))
                .RuleFor(x => x.CharacterName, y => y.Name.LastName().OrNull(y, 0.2f))
                .RuleFor(x => x.Role, y => y.PickRandom(roles).OrNull(y, 0.15f));

            return fakeCredits.Generate(new Random().Next(1, 6));
        }

        /// <summary>
        /// Generate list of titles
        /// </summary>
        /// <param name="titlesCount">The amount of title that needed to generate</param>
        /// <returns>List of titles</returns>
        public static List<Titles> TitlesGenerator(int titlesCount)
        {
            var titlesIds = 1;
            var lorem = new Bogus.DataSets.Lorem(locale: "en");
            var ageCertificationsList = new[] {
                "G", "PG", "PG-13", "R", "NC-17", "U", "U/A", "A", "S", "AL", "6", "9", "12", "12A",
                "15", "18", "18R", "R18", "R21", "M", "MA15+", "R16", "R18+", "X18", "T", "E", "E10+",
                "EC", "C", "CA", "GP", "M/PG", "TV-Y", "TV-Y7", "TV-G", "TV-PG", "TV-14", "TV-MA" };

            var genresList = new[] {
                "Horror", "Action","Western","Gangster", "Detective", "Drama",
                "Historical", "Comedy", "Melodrama", "Musical",
                "Noir", "Political", "Adventure", "Fairy tale", "Tragedy", "Tragicomedy" };

            var fakerTitles = new Faker<Titles>()
               .RuleFor(x => x.Id, y => titlesIds++)
               .RuleFor(x => x.Title, y => lorem.Word().OrNull(y, 0.15f))
               .RuleFor(x => x.Description, y => lorem.Sentence(6).OrNull(y, 0.15f))
               .RuleFor(x => x.ReleaseYear, y => y.Random.Int(1600, 2029))
               .RuleFor(x => x.AgeCertification, y => y.PickRandom(ageCertificationsList))
               .RuleFor(x => x.Runtime, y => y.Random.Int(-20, 300))
               .RuleFor(x => x.Genres, y => y.Make(3, () => y.PickRandom(genresList)).OrNull(y, 0.15f))
               .RuleFor(x => x.ProductionCountry, y => y.Address.CountryCode().OrNull(y, 0.15f))
               .RuleFor(x => x.Seasons, y => y.Random.Int(-1, 10).OrNull(y));
            
            return fakerTitles.Generate(titlesCount);
        }

        /// <summary>
        /// Generates list of credits for all generated titles
        /// </summary>
        /// <param name="listTitleIds">List of title ids</param>
        /// <returns>Full list of credits</returns>
        public static List<Credit> FullListCreditsGenerator(IEnumerable<int> listTitleIds)
        {
            var listCredits = new List<Credit>();

            foreach (var titleId in listTitleIds)
            {
                listCredits.AddRange(CreditGeneratorForOneTitle(titleId));
            }

            return listCredits;
        }

        /// <summary>
        /// Form and write credits to file
        /// </summary>
        /// <param name="list">the list of credits</param>
        public static void FormStringForCsvFileForTitles(List<Credit> list)
        {
            var output = new StringBuilder();
            foreach (var item in list)
            {
                string[] newLine = {
                    item.Id.ToString(),
                    item.TitlesId.ToString(),
                    item.RealName,
                    item.CharacterName,
                    item.Role,
                };

                output.AppendLine(string.Join(Separator, newLine));
            }

            WriteToFile(output, false);
        }

        /// <summary>
        /// Form and write titles to file
        /// </summary>
        /// <param name="listTitles">the list of titles</param>
        public static void FormStringForCsvFileForTitles(List<Titles> listTitles)
        {
            var output = new StringBuilder();
            
            foreach (var item in listTitles)
            {
                    string?[] newLine = {
                    item.Id.ToString(),
                    item.Title,
                    item.Description,
                    item.ReleaseYear.ToString(),
                    item.AgeCertification,
                    item.Runtime.ToString(),
                    item.Genres == null? null: string.Join(";", item.Genres),
                    item.ProductionCountry,
                    item.Seasons?.ToString(),
                };

                output.AppendLine(string.Join(Separator, newLine));
            }

            WriteToFile(output, true);
        }

        /// <summary>
        /// Write to file
        /// </summary>
        /// <param name="isTitle">Whether the entity is title</param>
        /// <param name="data">Data for writing</param>
        private static void WriteToFile(StringBuilder data, bool isTitle)
        {
            string pathFolder;
            var output = new StringBuilder();
            if (isTitle)
            {
                pathFolder = "Files\\title_list.csv";
                string [] headings = { "id", "title", "description", "release_year", "age_certification", "runtime", "genres", "production_country", "seasons" };
                output.AppendLine(string.Join(Separator, headings));
            }
            else
            {
                pathFolder = "Files\\credit_list.csv";
                string[] headings = { "id", "title_id", "real_name", "character_name", "role" };
                output.AppendLine(string.Join(Separator, headings));
            }

            output.Append(data);
            var rootPath = PathToFolder + pathFolder;

            try
            {
                File.WriteAllText(rootPath, output.ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
            }
        }
    }
}
