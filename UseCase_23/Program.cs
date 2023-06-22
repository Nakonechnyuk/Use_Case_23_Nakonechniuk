namespace UseCase_23;

internal class Program
{
    private static void Main(string[] args)
    {
        var fakedTitlesList = DataGenerator.TitlesGenerator(120);
        var fakedCredits = DataGenerator.FullListCreditsGenerator(fakedTitlesList.Select(x => x.Id));
        DataGenerator.FormStringForCsvFileForTitles(fakedCredits);
        DataGenerator.FormStringForCsvFileForTitles(fakedTitlesList);
    }
}