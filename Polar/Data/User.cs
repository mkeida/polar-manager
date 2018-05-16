using System;
using System.Collections.Generic;
using System.Xml;
using SecurityLibrary;
using System.Security.Authentication;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Linq;
using MinimalLibrary.Themes;
using MinimalLibrary;
using System.Drawing;
using MinimalLibrary.External;

namespace Polar.Data
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's personal AES
        /// </summary>
        public AES AES;

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Login user
        /// </summary>
        public static User Login(string email, string password)
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "users.xml");

            // Load data to document
            doc.Load(path);

            // Valid password from register file
            // string passwordHash = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Polar", email, "NULL").ToString();

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // Get user node by email
                if (node.Attributes[2].InnerText == email)
                {
                    string passwordHash = node.Attributes[4].InnerText;

                    // Check if both passwords match
                    if (SecurePasswordHasher.Verify(password, passwordHash))
                    {
                        // Temporary AES
                        AES aes = new AES(AES.GetKey(passwordHash), AES.GetInitVec(passwordHash));

                        // Attributes
                        string attName = aes.Decrypt(node.Attributes[0].InnerText);
                        string attLastname = aes.Decrypt(node.Attributes[1].InnerText);
                        string attEmail = node.Attributes[2].InnerText;
                        string attCountry = aes.Decrypt(node.Attributes[3].InnerText);

                        // Return new user object
                        return new User(attName, attLastname, attEmail, attCountry, aes);
                    }
                    else
                    {
                        // Throw exception
                        throw new AuthenticationException("Passwords do not match!");
                    }
                }
            }

            // Throw exception
            throw new AuthenticationException("User not found!");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public User(string name, string lastname, string email, string country, AES AES)
        {
            // Initialization
            Name = name;
            Lastname = lastname;
            Email = email;
            Country = country;
            this.AES = AES;
        }

        /// <summary>
        /// Load all group objects from storage .xml file
        /// </summary>
        public List<Group> GetGroups()
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "groups.xml");

            // Load data to document
            doc.Load(path);

            // Crate new list
            List<Group> groups = new List<Group>();

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // If user email and group email from XML match
                if (Email == node.Attributes[0].InnerText)
                {
                    string name = AES.Decrypt(node.Attributes[1].InnerText);

                    // Add new group to collection
                    groups.Add(new Group(name));
                }
            }

            // Return list
            return groups;
        }

        /// <summary>
        /// Gets group object by name
        /// </summary>
        public Group GetGroupByName(string name)
        {
            foreach(Group item in GetGroups())
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            // No group found with given name
            return null;
        }

        /// <summary>
        /// Adds group for user
        /// </summary>
        /// <param name="group"></param>
        public void AddGroup(Group group)
        {
            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "groups.xml");

            // Save group in XML storage file
            // Load document
            XDocument doc = XDocument.Load(path);

            // Create group element
            XElement groupElement = new XElement("Group");
            groupElement.Add(new XAttribute("owner", Email));
            groupElement.Add(new XAttribute("name", AES.Encrypt(group.Name)));

            // Add user element to .xml root
            doc.Root.Add(groupElement);

            // Save document
            doc.Save(path);
        }

        public void UpdateGroup(string name, Group group)
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "groups.xml");

            // Load data to document
            doc.Load(path);

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // If user email and group email from XML match
                if (Email == node.Attributes[0].InnerText)
                {
                    if (name == AES.Decrypt(node.Attributes[1].InnerText))
                    {
                        // Removes old node
                        node.ParentNode.RemoveChild(node);
                    }
                }
            }

            // Save document
            doc.Save(path);

            // Add new password
            AddGroup(group);
        }

        /// <summary>
        /// Load all password objects from storage .xml file
        /// </summary>
        public List<Password> GetPasswords()
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "passwords.xml");

            // Load data to document
            doc.Load(path);

            // Crate new list
            List<Password> passwords = new List<Password>();

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // If user email and group email from XML match
                if (Email == node.Attributes[0].InnerText)
                {
                    string name = AES.Decrypt(node.Attributes[1].InnerText);
                    string web = AES.Decrypt(node.Attributes[2].InnerText);
                    string secret = AES.Decrypt(node.Attributes[3].InnerText);
                    string group = AES.Decrypt(node.Attributes[4].InnerText);

                    // Add new group to collection
                    passwords.Add(new Password(name, web, secret, group));
                }
            }

            // Return list
            return passwords;
        }

        /// <summary>
        /// Adds password for user
        /// </summary>
        /// <param name="password"></param>
        public void AddPassword(Password password)
        {
            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "passwords.xml");

            // Save password in XML storage file
            // Load document
            XDocument doc = XDocument.Load(path);

            // Create password element
            XElement passwordElement = new XElement("Password");
            passwordElement.Add(new XAttribute("owner", Email));
            passwordElement.Add(new XAttribute("name", AES.Encrypt(password.Name)));
            passwordElement.Add(new XAttribute("web", AES.Encrypt(password.Web)));
            passwordElement.Add(new XAttribute("secret", AES.Encrypt(password.Secret)));
            passwordElement.Add(new XAttribute("group", AES.Encrypt(password.Group)));

            // Add user element to .xml root
            doc.Root.Add(passwordElement);

            // Save document
            doc.Save(path);
        }

        /// <summary>
        /// Gets group object by name
        /// </summary>
        public Password GetPassword(string name)
        {
            foreach (Password item in GetPasswords())
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            // No group found with given name
            return null;
        }

        /// <summary>
        /// Remove password with given name
        /// </summary>
        public void RemovePassword(string name)
        {
            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "passwords.xml");

            // Load document
            XDocument doc = XDocument.Load(path);

            // Remove document
            doc.Descendants("Password").Where(x => (string)x.Attribute("name") == AES.Encrypt(name)).Where(x => (string)x.Attribute("owner") == Email).Remove();

            // Save document
            doc.Save(path);
        }

        /// <summary>
        /// Updates password
        /// </summary>
        public void UpdatePassword(string name, Password password)
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "passwords.xml");

            // Load data to document
            doc.Load(path);

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // If user email and group email from XML match
                if (Email == node.Attributes[0].InnerText)
                {
                    if (name == AES.Decrypt(node.Attributes[1].InnerText))
                    {
                        // Removes old node
                        node.ParentNode.RemoveChild(node);
                    }
                }
            }

            // Save document
            doc.Save(path);

            // Add new password
            AddPassword(password);
        }

        /// <summary>
        /// Return user's user configuration object
        /// </summary>
        /// <returns></returns>
        public Config GetConfig()
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "configs.xml");

            // Load data to document
            doc.Load(path);

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // If user email and node owner (email) match
                if (Email == node.Attributes[0].InnerText)
                {
                    Theme appTheme = null;
                    Theme drawerTheme = null;

                    // Attributes
                    string appThemeName = node.Attributes[1].InnerText;
                    string drawerThemeName = node.Attributes[2].InnerText;
                    Color tint = new Hex(node.Attributes[3].InnerText).ToColor();

                    // Get theme
                    if (appThemeName == "Light") { appTheme = Minimal.Light; }
                    if (appThemeName == "Dark") { appTheme = Minimal.Dark; }
                    if (drawerThemeName == "Light") { drawerTheme = Minimal.Light; }
                    if (drawerThemeName == "Dark") { drawerTheme = Minimal.Dark; }

                    // Return configuration object
                    return new Config(appTheme, drawerTheme, tint);
                }
            }

            // No configuration found
            return null;
        }

        /// <summary>
        /// Adds new configuration node
        /// </summary>
        public void AddConfig(Config config)
        {
            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "configs.xml");

            // Save password in XML storage file
            // Load document
            XDocument doc = XDocument.Load(path);

            // Theme name
            string appThemeName = "";
            string drawerThemeName = "";

            // Initialize theme name
            if (config.AppTheme == Minimal.Light) { appThemeName = "Light"; }
            if (config.AppTheme == Minimal.Dark) { appThemeName = "Dark"; }
            if (config.DrawerTheme == Minimal.Light) { drawerThemeName = "Light"; }
            if (config.DrawerTheme == Minimal.Dark) { drawerThemeName = "Dark"; }

            // Create password element
            XElement passwordElement = new XElement("Config");
            passwordElement.Add(new XAttribute("owner", Email));
            passwordElement.Add(new XAttribute("appTheme", appThemeName));
            passwordElement.Add(new XAttribute("drawerTheme", drawerThemeName));
            passwordElement.Add(new XAttribute("tint", new Hex(config.Tint).Value));

            // Add user element to .xml root
            doc.Root.Add(passwordElement);

            // Save document
            doc.Save(path);
        }

        /// <summary>
        /// Modifies user configuration
        /// </summary>
        public void UpdateConfig(Config config)
        {
            // Create new XML document
            XmlDocument doc = new XmlDocument();

            // Document path
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "configs.xml");

            // Load data to document
            doc.Load(path);

            // Get all nodes
            foreach (XmlNode node in doc.DocumentElement)
            {
                // If user email and node owner (email) match
                if (Email == node.Attributes[0].InnerText)
                {
                    // Removes old node
                    node.ParentNode.RemoveChild(node);
                }
            }

            // Save document
            doc.Save(path);

            // Add new node
            AddConfig(config);
        }
    }
}
