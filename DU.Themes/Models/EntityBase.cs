using System;

namespace DU.Themes.Models
{
    /// <summary>
    /// Base Entity class
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        public DateTime TouchTime { get; set; }
    }
}