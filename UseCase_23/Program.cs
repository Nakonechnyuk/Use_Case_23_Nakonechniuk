using UseCase_23;

internal class Program
{
    private static void Main(string[] args)
    {
        var fakedTitlesList = DataGenerator.TitlesGenerator(100);
        var fakedCredits = DataGenerator.FullListCreditsGenerator(fakedTitlesList.Select(x => x.Id));
        DataGenerator.WriteToCSVFileCredits(fakedCredits);
        DataGenerator.WriteToCSVFileTitles(fakedTitlesList);
    }
}