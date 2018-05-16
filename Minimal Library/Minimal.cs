using System;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using MinimalLibrary.Themes;
using MinimalLibrary.Internal;
using MinimalLibrary.External;
using MinimalLibrary.Properties;
using System.Collections.Generic;

namespace MinimalLibrary
{
    /// <summary>
    /// Available types of the button
    /// </summary>
    public enum ButtonType { Raised, Flat, Outline }

    /// <summary>
    /// Available types of the label
    /// </summary>y
    public enum LabelType { Standard, Alternate, Tint }

    /// <summary>
    /// Available types of click effect
    /// </summary>
    public enum ClickEffect { None, Ink, Square, SquareRotate }

    /// <summary>
    /// Simplifies working with the library
    /// </summary>
    public class Minimal
    {
        /// <summary>
        /// Default light theme
        /// </summary>
        public static Theme Light = LoadTheme(Resources.light_theme);

        /// <summary>
        /// Default dark theme
        /// </summary>
        public static Theme Dark = LoadTheme(Resources.dark_theme);

        /// <summary>
        /// Application wide theme
        /// </summary>
        public static Theme UsedTheme = Light;

        /// <summary>
        /// Load XML theme file and pass data to new Theme instance
        /// </summary>
        public static Theme LoadTheme(string path)
        {
            // Initialize new Theme instance
            Theme theme = new Theme();

            try
            {
                // Initialize new .xml document
                XmlDocument xmlDoc = new XmlDocument();

                // Try load from path
                try { xmlDoc.Load(path); }

                // Try load from XML file
                catch { xmlDoc.LoadXml(path); }

                // Iterate through all possible ThemeColors
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    // Iterate through all ThemeColor's parameters and pass them to themeColor instance
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        // Theme color properties
                        try
                        {
                            // New ThemeColor instance
                            ThemeColor tc = new ThemeColor();

                            // Fills tc
                            tc.Normal = new Hex(childNode.Attributes["Normal"].Value);
                            tc.Hover = new Hex(childNode.Attributes["Hover"].Value);
                            tc.Focus = new Hex(childNode.Attributes["Focus"].Value);
                            tc.Disabled = new Hex(childNode.Attributes["Disabled"].Value);

                            // Pass filled tc instance to current theme property (node)
                            theme.GetType().GetProperty(node.Name).SetValue(theme, tc);
                        }
                        // Last theme property wich is not ThemeColor child
                        catch
                        {
                            // DARK BASED property
                            theme.GetType().GetProperty(node.Name).SetValue(theme, Convert.ToBoolean(childNode.Attributes["Value"].Value));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Error
                // Probably bad XML theme file formating
                MessageBox.Show("Error in reading XML theme file. " + e.Message);
            }

            // Return initialized theme instance
            return theme;
        }

        /// <summary>
        /// Set theme to given form
        /// </summary>
        public static void SetTheme(MForm form, Theme theme)
        {
            // Set window background color from theme
            form.BackColor = theme.FORM_BACKGROUND.Normal.ToColor();

            // Set theme
            form.UsedTheme = theme;

            // Invalidate
            form.Invalidate(true);

            // Refresh form
            form.Refresh();
        }

        /// <summary>
        /// Set tint globally to given form
        /// </summary>
        public static void SetTint(Form form, Color color)
        {
           // Iterate through all controls on form
           foreach (Control control in form.Controls)
           {
                ControlExtensions.SetProperty(control, "Tint", color);
                control.Invalidate(true);
           }
        }

        /// <summary>
        /// Set tint globally to given form
        /// </summary>
        public static void SetTint(Form form, Hex hex)
        {
            // Iterate through all controls on form
            foreach (Control control in form.Controls)
            {
                ControlExtensions.SetProperty(control, "Tint", hex.ToColor());
                control.Invalidate(true);
            }
        }

        /// <summary>
        /// Set tint globally to given form
        /// </summary>
        public static void SetTint(List<Control> controls, Hex hex)
        {
            // Iterate through all controls on form
            foreach (Control control in controls)
            {
                ControlExtensions.SetProperty(control, "Tint", hex.ToColor());
                control.Invalidate(true);
            }
        }

        /// <summary>
        /// Set tint globally to given form
        /// </summary>
        public static void SetTint(List<Control> controls, Color color)
        {
            // Iterate through all controls on form
            foreach (Control control in controls)
            {
                ControlExtensions.SetProperty(control, "Tint", color);
                control.Invalidate(true);
            }
        }
    }
}
