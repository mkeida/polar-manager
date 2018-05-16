using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polar.Data
{
    /// <summary>
    /// Group object
    /// </summary>
    public class Group : IComparable
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of items in group
        /// </summary>
        public int ItemsCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Group(string name)
        {
            Name = name;
            ItemsCount = 0;
        }

        /// <summary>
        /// IComparable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // Parse object to group
            Group group = (Group) obj;

            // Compare firts letters of names
            return string.Compare(Name[0].ToString(), group.Name[0].ToString());
        }
    }
}
