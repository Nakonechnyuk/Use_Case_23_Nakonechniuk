namespace UseCase_23.Entities
{
    public class Credit
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier of the Title
        /// </summary>
        public int TitlesId { get; set; }

        /// <summary>
        /// Full credits member name
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// Corresponding movie (series) character nam
        /// </summary>
        public string CharacterName { get; set; }

        /// <summary>
        /// Represent corresponding role
        /// </summary>
        public string Role { get; set; }
    }
}
