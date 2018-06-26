using System;
using System.Collections.Generic;
using System.Text;

namespace Wiki.Shared
{
   public class WikiEntry
    {
        public int ID { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Semicolon separated list
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        public string Steps { get; set; }

        /// <summary>
        /// Comma separated list
        /// </summary>
        /// <value>
        /// The screenshots.
        /// </value>
        public string Screenshots { get; set; }

        /// <summary>
        /// Comma separated list
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string Tags { get; set; }

        /// <summary>
        /// Comma separated list
        /// </summary>
        /// <value>
        /// The references.
        /// </value>
        public string References { get; set; }
    }
}
