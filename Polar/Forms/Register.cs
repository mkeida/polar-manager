using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Net.Mail;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using SecurityLibrary;
using MinimalLibrary;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.External;
using Polar.Data;
using MinimalLibrary.Controls;
using System.IO;
using MinimalLibrary.Themes;

namespace Polar.Forms
{
    /// <summary>
    /// Register form
    /// </summary>
    public partial class Register : MForm
    {
        /// <summary>
        /// Global tint color
        /// </summary>
        private Color tintColor = Hex.Blue.ToColor();

        /// <summary>
        /// New registered user
        /// </summary>
        private User registeredUser;

        /// <summary>
        /// Constructor
        /// </summary>
        public Register()
        {
            InitializeComponent();

            // Start timer (update)
            main.Start();

            // Hide error label
            lblErrorMessage.Visible = false;

            // Add themes
            cbTheme.Items.Add(new SingleLineItem("Light"));
            cbTheme.Items.Add(new SingleLineItem("Dark"));
            cbTheme.SelectedItem = cbTheme.Items[0];

            // Get countries
            string countries = Properties.Resources.countries;

            // Fill combo box with countries
            foreach (string country in countries.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                cbCountry.Items.Add(new SingleLineItem(country));
            }
        }

        /// <summary>
        /// Update method
        /// </summary>
        private void Update(object sender, EventArgs e)
        {
            // Redraw side panel
            sidePanel.Invalidate();
        }

        /// <summary>
        /// SidePanel on paint method
        /// </summary>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(tintColor);

            // Variables
            Point mouse = PointToClient(Cursor.Position);
            int distanceToCenterX = (sidePanel.Width / 2) - mouse.X;
            int distanceToCenterY = (sidePanel.Height / 2) - mouse.Y;
            int incX = (distanceToCenterX / 50);
            int incY = (distanceToCenterY / 30);
            int bWidth = 350;
            int bHeight = 300;
            int bX = (sidePanel.Width / 2) - (bWidth / 2);
            int bY = (sidePanel.Height / 2) - (bHeight / 2);

            // Draw ears
            DrawCircle(g, Brushes.White, new Point(bX + 30 + incX, bY + 65 + incY), 50 - (distanceToCenterX / 120));
            DrawCircle(g, Brushes.White, new Point(bX + bWidth - 30 + incX, bY + 65 + incY), 50 + (distanceToCenterX / 120));

            // Draw head
            g.FillEllipse(Brushes.White, new Rectangle(bX, bY, bWidth, bHeight));
            g.FillRectangle(Brushes.White, new Rectangle(bX, bY + (bHeight / 2), bWidth, (this.Height / 2)));

            // Draw eyes
            DrawCircle(g, new SolidBrush(new Hex("#383838").ToColor()), new Point(bX + bWidth / 2 - 25 - incX, bY + 60 - incY), 8 - (distanceToCenterX / 650));
            DrawCircle(g, new SolidBrush(new Hex("#383838").ToColor()), new Point(bX + bWidth / 2 + 25 - incX, bY + 60 - incY), 8 + (distanceToCenterX / 650));

            // Draw muzzle
            SolidBrush brush = new SolidBrush(MColor.Lighten(210, tintColor, Color.White));
            DrawCircle(g, brush, new Point(bX + bWidth / 2 - (distanceToCenterX / 20), bY + 150 - incY), 65);
            g.FillRectangle(brush, new Rectangle(bX + bWidth / 2 - 65 - (distanceToCenterX / 20), bY + 150 - incY, 130, 50));
            DrawCircle(g, brush, new Point(bX + bWidth / 2 - (distanceToCenterX / 20), bY + 50 + 150 - incY), 65);

            // Draw mouth
            Pen pen = new Pen(new Hex("#383838").ToColor(), 4);
            g.DrawLine(pen, new Point(bX + bWidth / 2 - 10 - (distanceToCenterX / 15), bY + 225 - (distanceToCenterY / 15)), new Point(bX + bWidth / 2 + 10 - (distanceToCenterX / 15), bY + 225 - (distanceToCenterY / 15)));

            // Draw nose
            DrawCircle(g, new SolidBrush(new Hex("#383838").ToColor()), new Point(bX + bWidth / 2 - (distanceToCenterX / 15), bY + 150 - (distanceToCenterY / 15)), 45);
            DrawCircle(g, Brushes.White, new Point(bX + bWidth / 2 - (distanceToCenterX / 15) - 20, bY + 150 - (distanceToCenterY / 15) - 20), 10);

            // Draw slogan
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString("Bear strong protection!", new Font("Segoe UI Light", 48, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new Point(bX - 80, 50));
        }

        /// <summary>
        /// Draw circle using center point instead of top left right
        /// </summary>
        private void DrawCircle(Graphics g, Brush brush, Point center, int radius)
        {
            Rectangle rect = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            g.FillEllipse(brush, rect);
        }

        /// <summary>
        /// Sign up button click
        /// </summary>
        private void SignUpClick(object sender, EventArgs e)
        {
            // Validation
            if (!FormValidation())
            {
                return;
            }

            // Values
            string name = tbName.Text;
            string lastname = tbLastname.Text;
            string email = tbEmail.Text;
            string country = cbCountry.SelectedItem.PrimaryText;
            string password = tbPasswordOne.Text;

            // Values modified
            string passwordHash = SecurePasswordHasher.Hash(password);

            // Save hashed password to registry
            // Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Polar", email, passwordHash);
                
            // AES
            AES aes = new AES(AES.GetKey(passwordHash), AES.GetInitVec(passwordHash));

            // Document path
            string path = Path.Combine(Environment.CurrentDirectory, "Data", "users.xml");

            // Save user in XML storage file
            // Load document
            XDocument doc = XDocument.Load(path);

            // Create user element
            XElement user = new XElement("User");
            user.Add(new XAttribute("name", aes.Encrypt(name)));
            user.Add(new XAttribute("lastname", aes.Encrypt(lastname)));
            user.Add(new XAttribute("email", email)); 
            user.Add(new XAttribute("country", aes.Encrypt(country)));
            user.Add(new XAttribute("password", passwordHash));

            // Add user element to .xml root
            doc.Root.Add(user);

            // Save document
            doc.Save(path);

            // Login user
            registeredUser = User.Login(email, password);

            // Redirect
            tabMain.SelectedTab = tabSetup;
        }

        /// <summary>
        /// Form validation
        /// </summary>
        private bool FormValidation()
        {
            // Name
            // Verify if name is longer than 3 chars
            if (tbName.Text.Length < 3)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Name must be at least 3 characters long!";
                tbName.Tint = new Hex("#d32f2f").ToColor();
                tbName.Focus();
                return false;
            }

            tbName.Tint = tintColor;

            // Last name
            // Verify if last name is longer than 3 chars
            if (tbLastname.Text.Length < 3)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Last name must be at least 3 characters long!";
                tbLastname.Tint = new Hex("#d32f2f").ToColor();
                tbLastname.Focus();
                return false;
            }

            tbLastname.Tint = tintColor;

            // EMail
            // Check for email length
            if (tbEmail.Text.Length == 0)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please enter your e-mail!";
                tbEmail.Tint = new Hex("#d32f2f").ToColor();
                tbEmail.Focus();
                return false;
            }

            tbEmail.Tint = tintColor;

            // Check for email format
            if (!IsValidMail(tbEmail.Text))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid e-mail format!";
                tbEmail.Tint = new Hex("#d32f2f").ToColor();
                tbEmail.Focus();
                return false;
            }

            tbEmail.Tint = tintColor;

            // Country
            // Check if country was chosen
            if (cbCountry.SelectedItem == null)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Choose your country!";
                cbCountry.Tint = new Hex("#d32f2f").ToColor();
                cbCountry.Focus();
                return false;
            }

            cbCountry.Tint = tintColor;

            // Passwords
            // Verify if passwords match
            if (tbPasswordOne.Text != tbPasswordTwo.Text)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Passwords do not match!";
                tbPasswordOne.Tint = new Hex("#d32f2f").ToColor();
                tbPasswordTwo.Tint = new Hex("#d32f2f").ToColor();
                tbPasswordOne.Focus();
                return false;
            }

            tbPasswordOne.Tint = tintColor;
            tbPasswordTwo.Tint = tintColor;

            if (tbPasswordOne.Text.Length < 6)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Password must be at least 6 characters long!";
                tbPasswordOne.Tint = new Hex("#d32f2f").ToColor();
                tbPasswordOne.Focus();
                return false;
            }

            tbPasswordOne.Tint = tintColor;

            if (!chbConditions.Checked)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "You must agree with terms and conditions!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if email is valid
        /// </summary>
        public bool IsValidMail(string emailaddress)
        {
            try
            {
                MailAddress mail = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// I already have an account button click
        /// </summary>
        private void ExistingAccountClick(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabLogin;
        }

        /// <summary>
        /// I don't have account button click
        /// </summary>
        private void NewAccountClick(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabRegister;
        }

        private void mButton4_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabSetup;
        }

        /// <summary>
        /// Value of red track-bar is changed
        /// </summary>
        private void TrbRedChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Change label text
            lblRed.Text = trbRed.Value.ToString();

            // Get color tint
            Color c = Color.FromArgb(int.Parse(lblRed.Text), int.Parse(lblGreen.Text), int.Parse(lblBlue.Text));

            // Update tint
            Minimal.SetTint(this, c);
            tintColor = c;
        }

        /// <summary>
        /// Value of green track-bar is changed
        /// </summary>
        private void TrbGreenChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Change label text
            lblGreen.Text = trbGreen.Value.ToString();

            // Get color tint
            Color c = Color.FromArgb(int.Parse(lblRed.Text), int.Parse(lblGreen.Text), int.Parse(lblBlue.Text));

            // Update tint
            Minimal.SetTint(this, c);
            tintColor = c;
        }

        /// <summary>
        /// Value of blue track-bar is changed
        /// </summary>
        private void TrbBlueChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Change label text
            lblBlue.Text = trbBlue.Value.ToString();

            // Get color tint
            Color c = Color.FromArgb(int.Parse(lblRed.Text), int.Parse(lblGreen.Text), int.Parse(lblBlue.Text));

            // Update tint
            Minimal.SetTint(this, c);
            tintColor = c;
        }

        /// <summary>
        /// Theme changed
        /// </summary>
        private void ThemeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Light theme
            if (cbTheme.SelectedItem.PrimaryText == "Light")
            {
                Minimal.SetTheme(this, Minimal.Light);
            }

            // Dark theme
            if (cbTheme.SelectedItem.PrimaryText == "Dark")
            {
                Minimal.SetTheme(this, Minimal.Dark);
            }
        }

        /// <summary>
        /// Sign in
        /// </summary>
        private void SignInClick(object sender, EventArgs e)
        {
            User user = null;
            // Login user
            try
            {
                user = User.Login(tbEmailLogin.Text, tbPasswordLogin.Text);
            }
            catch
            {
                lblErrorMessageLogin.Visible = true;
                lblErrorMessageLogin.Text = "Wrong username or password!";
                return;
            }

            // Open main form
            Main main = new Main(user);
            main.Show();

            // Close this window
            Hide();
        }

        /// <summary>
        /// Click on color box from color palette
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorBoxClick(object sender, EventArgs e)
        {
            // Get color box from sender
            MColorBox box = (MColorBox)sender;

            // Set tint
            Minimal.SetTint(this, box.Color);
            tintColor = box.Color;

            // Change track-bars
            trbRed.Value = box.Color.R;
            trbGreen.Value = box.Color.G;
            trbBlue.Value = box.Color.B;
        }

        /// <summary>
        /// Resets tint on country select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedCountryChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            cbCountry.Tint = tintColor;
        }

        /// <summary>
        /// Text changed
        /// </summary>
        private void TbTextChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MTextBox textBox = (MTextBox)sender;
            textBox.Tint = tintColor;
        }

        /// <summary>
        /// Setup confirm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmClick(object sender, EventArgs e)
        {
            // Theme
            Theme theme = null;

            // Get theme from comboBox
            if (cbTheme.SelectedItem.PrimaryText == "Light") { theme = Minimal.Light; }
            if (cbTheme.SelectedItem.PrimaryText == "Dark") { theme = Minimal.Dark; }

            // Add configuration for registered user
            registeredUser.AddConfig(new Config(theme, theme, tintColor));

            // Open main form
            Main main = new Main(registeredUser);
            main.Show();

            // Close this window
            Hide();
        }
    }
}
