using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polar.Data
{
    public class Password : IComparable
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Web
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Password(string name, string web, string secret, string group)
        {
            Name = name;
            Web = web;
            Secret = secret;
            Group = group;
        }

        /// <summary>
        /// IComparable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // Parse object to password
            Password password = (Password)obj;

            // Compare firts letters of names
            return string.Compare(Name[0].ToString(), password.Name[0].ToString());
        }
    }
}
