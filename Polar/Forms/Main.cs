using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MinimalLibrary;
using MinimalLibrary.Controls;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.External;
using Polar.Data;
using Polar.ListItems;
using MinimalLibrary.Themes;
using System.Drawing;

namespace Polar.Forms
{
    public partial class Main : MForm
    {
        /// <summary>
        /// Logged user
        /// </summary>
        User User;

        /// <summary>
        /// Drawer item - Home
        /// </summary>
        public DrawerItem diHome;

        /// <summary>
        /// Drawer item - Passwords
        /// </summary>
        public DrawerItem diPasswords;

        /// <summary>
        /// Drawer item - Passwords
        /// </summary>
        public DrawerItem diSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        public Main(User loggedUser)
        {
            // Initialize component
            InitializeComponent();

            // Variables
            User = loggedUser;

            // Load user config file
            Config config = User.GetConfig();

            // Apply settings from config
            Minimal.SetTheme(this, config.AppTheme);
            drawer.UsedTheme = config.DrawerTheme;
            Minimal.SetTint(this, config.Tint);
            lblPasswordCount.Tint = lblGroupsCount.Tint = lblWarningsCount.Tint = Color.White;
            lblPasswords.Tint = lblGroups.Tint = lblWarnings.Tint = Color.White;
            btnAddPasswordShortcut.Tint = Color.FromArgb(76, 175, 80);
            btnAddGroupShortcut.Tint = Color.FromArgb(255, 152, 0);
            btnShowWarningsShortcut.Tint = Color.FromArgb(211, 47, 47);

            // Initialize drawer
            InitializeDrawer();

            // Turn on double buffer
            DoubleBuffered = true;

            // Hide all error labels
            lblErrorMessagePassword.Visible = false;
            lblErrorMessagePasswordEdit.Visible = false;

            // User name and last name
            lblUser.Text = User.Name + " " + User.Lastname;

            // Dashboard
            int passwordsCount = 0;
            int groupsCount = 0;

            foreach(Group group in User.GetGroups())
            {
                int count = 0;

                // Get group items
                foreach (Password password in User.GetPasswords())
                {
                    if (password.Group == group.Name)
                    {
                        count++;
                    }
                }

                passwordsCount += count;
                groupsCount++;
            }

            lblPasswordCount.Text = passwordsCount.ToString();
            lblGroupsCount.Text = groupsCount.ToString();

            // Settings
            if (config.AppTheme == Minimal.Light)
            {
                rbAppThemeLight.Checked = true;
            }

            if (config.AppTheme == Minimal.Dark)
            {
                rbAppThemeDark.Checked = true;
            }

            if (config.DrawerTheme == Minimal.Light)
            {
                rbDrawerThemeLight.Checked = true;
            }

            if (config.DrawerTheme == Minimal.Dark)
            {
                rbDrawerThemeDark.Checked = true;
            }
        }

        /// <summary>
        /// Runs on startup
        /// </summary>
        private void InitializeDrawer()
        {
            // Set up drawer items
            // Home
            diHome = new DrawerItem("Home");
            diHome.Icon = MIcon.Home;
            diHome.Click += new EventHandler(HomeClick);

            // Passwords
            diPasswords = new DrawerItem("Passwords");
            diPasswords.Icon = MIcon.Key;
            diPasswords.Click += new EventHandler(PasswordsClick);

            // Settings
            diSettings = new DrawerItem("Settings");
            diSettings.Icon = MIcon.Slider;
            diSettings.Click += new EventHandler(SettingsClick);

            // Add items to drawer
            drawer.Items.Add(diHome);
            drawer.Items.Add(diPasswords);
            drawer.Items.Add(diSettings);

            // Set drawer's default selected item
            drawer.SelectedItem = diHome;
        }


        /// <summary>
        /// Fills group list box
        /// </summary>
        private void FillGroups()
        {
            // Clears previous items
            listLeft.ItemsClear();

            // Get list
            List<Group> list = User.GetGroups();

            // Sort list 
            list.Sort();

            // Return if list is empty
            if (list.Count == 0)
            {
                return;
            }

            // Previous group name
            string prevName = "";

            // Add first item's fist letter divider
            listLeft.Items.Add(new GroupDivider(list[0].Name[0].ToString()));

            // Add groups to list box
            foreach (Group group in list)
            {
                // If first letter of previous group don't match first letter of current group
                if (prevName != "")
                {
                    if (prevName[0] != group.Name[0])
                    {
                        // Adds new divider
                        listLeft.Items.Add(new GroupDivider(group.Name[0].ToString()));
                    }
                }

                // Temporary variable for items count
                int count = 0;

                // Get group items
                foreach (Password password in User.GetPasswords())
                {
                    if (password.Group == group.Name)
                    {
                        count++;
                    }
                }

                // Temporary variable for items count text
                string text = "";

                // Fill text
                if (count == 0)
                {
                    text = "Contains no items";
                }
                else if (count == 1)
                {
                    text = "Contains 1 item";
                }
                else if (count > 1)
                {
                    text = "Contains " + count + " items";
                }

                // Add list item
                listLeft.Items.Add(new GroupItem(group.Name, text));

                // Previous item name
                prevName = group.Name;
            }
        }

        /// <summary>
        /// Triggers when user click at drawer's settings item
        /// </summary>
        private void SettingsClick(object sender, EventArgs e)
        {
            content.SelectedTab = tabSettings;
        }

        /// <summary>
        /// Triggers when user click at drawer's passwords item
        /// </summary>
        private void PasswordsClick(object sender, EventArgs e)
        {
            lblEditGroup.Type = LabelType.Alternate;
            // Selects tab
            content.SelectedTab = tabPasswords;

            // Select side content tab
            sideContent.SelectedTab = tabList;

            // Add groups to list view
            FillGroups();
        }

        /// <summary>
        /// Triggers when user click at drawer's home item
        /// </summary>
        private void HomeClick(object sender, EventArgs e)
        {
            // Selects home tab
            content.SelectedTab = tabHome;

            // Dashboard
            int passwordsCount = 0;
            int groupsCount = 0;

            foreach (Group group in User.GetGroups())
            {
                int count = 0;

                // Get group items
                foreach (Password password in User.GetPasswords())
                {
                    if (password.Group == group.Name)
                    {
                        count++;
                    }
                }

                passwordsCount += count;
                groupsCount++;
            }

            lblPasswordCount.Text = passwordsCount.ToString();
            lblGroupsCount.Text = groupsCount.ToString();
        }

        /// <summary>
        /// Click on add group
        /// </summary>
        private void GroupAddClick(object sender, EventArgs e)
        {
            // Validate
            // Blank name
            if (tbGroupName.Text == "")
            {
                lblErrorMessageGroup.Visible = true;
                lblErrorMessageGroup.Text = "Name cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessageGroup.Visible = false;

            // Group already exist
            if (User.GetGroupByName(tbGroupName.Text) != null)
            {
                lblErrorMessageGroup.Visible = true;
                lblErrorMessageGroup.Text = "Group with this name already exist!";

                return;
            }

            // Hide error message
            lblErrorMessageGroup.Visible = false;

            // Adds mew group
            User.AddGroup(new Group(tbGroupName.Text));

            // Reset text
            tbGroupName.Text = "";

            // Reset groups list
            FillGroups();
        }

        /// <summary>
        /// Click on add password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordAddClick(object sender, EventArgs e)
        {
            // Validate
            // Password name
            if (tbPasswordName.Text == "")
            {
                lblErrorMessagePassword.Visible = true;
                lblErrorMessagePassword.Text = "Name cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessagePassword.Visible = false;

            // Password web
            if (tbPasswordWeb.Text == "")
            {
                lblErrorMessagePassword.Visible = true;
                lblErrorMessagePassword.Text = "Web cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessagePassword.Visible = false;

            // Password secret
            if (tbPasswordSecret.Text == "")
            {
                lblErrorMessagePassword.Visible = true;
                lblErrorMessagePassword.Text = "Password cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessagePassword.Visible = false;

            // Selected group
            if (listLeft.SelectedItem == null)
            {
                lblErrorMessagePassword.Visible = true;
                lblErrorMessagePassword.Text = "You must choose group!";

                return;
            }

            lblErrorMessagePassword.Visible = false;

            // Validation successful
            // Add new password
            User.AddPassword(new Password(tbPasswordName.Text, tbPasswordWeb.Text, tbPasswordSecret.Text, listLeft.SelectedItem.PrimaryText));

            // Reset form
            tbPasswordName.Text = "";
            tbPasswordWeb.Text = "";
            tbPasswordSecret.Text = "";

            // Reset groups list
            FillGroups();

            // Selects previous group
            foreach(MItem item in listLeft.Items)
            {
                if (item.PrimaryText == tbPasswordGroup.Text)
                {
                    listLeft.SelectedItem = item;
                    listLeft.Invalidate(true);
                }
            }

            // Reset right list
            // Clear previous passwords
            listRight.ItemsClear();

            // Add passwords to list view
            foreach (Password item in User.GetPasswords())
            {
                Group selectedGroup = User.GetGroupByName(listLeft.SelectedItem.PrimaryText);

                if (item.Group == selectedGroup.Name)
                {
                    listRight.Items.Add(new PasswordItem(item.Name, item.Web, item.Secret));
                }
            }

            listRight.Invalidate(true);
        }

        /// <summary>
        /// Search
        /// </summary>
        private void SearchTextChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            listLeft.Filter = tbSearch.Text;
        }

        /// <summary>
        /// List groups selected item changed
        /// </summary>
        private void GroupsSelectedItemChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sideContent.SelectedTab == tabEditGroup)
            {
                sideContent.SelectedTab = tabList;
            }

            // View all passwords
            if (sideContent.SelectedTab == tabList)
            {
                // Clear previous passwords
                listRight.ItemsClear();

                // Add passwords to list view
                foreach (Password item in User.GetPasswords())
                {
                    Group selectedGroup = User.GetGroupByName(listLeft.SelectedItem.PrimaryText);

                    if (item.Group == selectedGroup.Name)
                    {
                        listRight.Items.Add(new PasswordItem(item.Name, item.Web, item.Secret));
                    }
                }

                listRight.Invalidate(true);
            }

            // Add password
            if (sideContent.SelectedTab == tabAddPassword)
            {
                tbPasswordGroup.Text = listLeft.SelectedItem.PrimaryText;
            }

            // View single password
            if (sideContent.SelectedTab == tabViewPassword)
            {
                // Selected password
                Password selectedPassword = User.GetPassword(listLeft.SelectedItem.PrimaryText);

                // Fill form basic data
                viewName.Text = selectedPassword.Name;
                viewWeb.Text = selectedPassword.Web;
                viewSecret.Text = new string('*', selectedPassword.Secret.Length);
                viewGroup.Text = selectedPassword.Group;
            }

            // Edit password group change
            if (sideContent.SelectedTab == tabEditPassword)
            {
                tbEditGroup.Text = listLeft.SelectedItem.PrimaryText;
            }

            if (listLeft.SelectedItem != null)
            {
                lblEditGroup.Type = LabelType.Tint;
            }
            else
            {
                lblEditGroup.Type = LabelType.Alternate;
            }
        }

        /// <summary>
        /// List passwords selected item changed
        /// </summary>
        private void PasswordsSelectedItemChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Select content tab
            sideContent.SelectedTab = tabViewPassword;

            // Selected password
            Password selectedPassword = User.GetPassword(listRight.SelectedItem.PrimaryText);

            // Fill form basic data
            viewName.Text = selectedPassword.Name;
            viewWeb.Text = selectedPassword.Web;
            viewSecret.Text = new string('*', selectedPassword.Secret.Length);
            viewGroup.Text = selectedPassword.Group;

            // Reset group list
            listLeft.ItemsClear();

            // Add divider
            listLeft.Items.Add(new GroupDivider(selectedPassword.Group));

            // Get passwords which belongs to selected group
            foreach(Password password in User.GetPasswords())
            {
                if (password.Group == selectedPassword.Group)
                {
                    // Add passwords from same group
                    listLeft.Items.Add(new PasswordItemMin(password.Name, password.Web));
                }
            }

            // Selected item
            foreach(MItem item in listLeft.Items)
            {
                if (item is MDividerItem)
                {
                    continue;
                }

                if (item.PrimaryText == selectedPassword.Name)
                {
                    listLeft.SelectedItem = item;
                }
            }

            listLeft.Invalidate(true);
        }

        /// <summary>
        /// Add password shortcut click
        /// </summary>
        private void AddPasswordShortcutClick(object sender, EventArgs e)
        {
            // Select content tab
            content.SelectedTab = tabPasswords;

            // Select drawer tab
            drawer.SelectedItem = diPasswords;

            // Add groups to list view
            FillGroups();
            sideContent.SelectedTab = tabAddPassword;
        }

        /// <summary>
        /// Add group shortcut click
        /// </summary>
        private void AddGroupShortcutClick(object sender, EventArgs e)
        {
            // Select content tab
            content.SelectedTab = tabPasswords;

            // Select drawer tab
            drawer.SelectedItem = diPasswords;

            // Add groups to list box
            FillGroups();
            sideContent.SelectedTab = tabAddGroup;
        }

        /// <summary>
        /// View passwords back button click
        /// </summary>
        private void ViewPasswordBackClick(object sender, EventArgs e)
        {
            // Switch side content
            sideContent.SelectedTab = tabList;

            // Fill groups list with passwords from opened group
            FillGroups();

            foreach(MItem item in listLeft.Items)
            {
                if (item.PrimaryText == viewGroup.Text)
                {
                    listLeft.SelectedItem = item;
                }
            }

            listLeft.Invalidate(true);

            // Refresh
            // Reset right list
            // Clear previous passwords
            listRight.ItemsClear();

            // Add passwords to list view
            foreach (Password item in User.GetPasswords())
            {
                Group selectedGroup = User.GetGroupByName(listLeft.SelectedItem.PrimaryText);

                if (item.Group == selectedGroup.Name)
                {
                    listRight.Items.Add(new PasswordItem(item.Name, item.Web, item.Secret));
                }
            }

            listRight.Invalidate(true);
        }

        /// <summary>
        /// Add passwords back button click
        /// </summary>
        private void AddPasswordBackClick(object sender, EventArgs e)
        {
            drawer.SelectedItem = diHome;
            content.SelectedTab = tabHome;
        }

        /// <summary>
        /// Add group back button click
        /// </summary>
        private void AddGroupBackClick(object sender, EventArgs e)
        {
            drawer.SelectedItem = diHome;
            content.SelectedTab = tabHome;
        }

        /// <summary>
        /// Show more from selected password
        /// </summary>
        private void ShowMore(object sender, EventArgs e)
        {
            // Create new random
            Random r = new Random();

            // Get selected password
            Password password = User.GetPassword(viewName.Text);

            // Get current secret text
            string secret = viewSecret.Text;

            // Change random hidden char to readable char
            int character = r.Next(0, secret.Length);

            // Number of '*' in secret
            int count = secret.Where(c => c == '*').Count();

            // Check if selected character is '*'
            while (secret[character] != '*')
            {
                // If there are no any characters left to reveal
                if (count == 0)
                {
                    break;
                }

                character = r.Next(0, secret.Length);
            }

            // Change * with password char
            StringBuilder sb = new StringBuilder(secret);
            sb[character] = password.Secret[character];

            // Change viewName
            viewSecret.Text = sb.ToString();
        }

        /// <summary>
        /// Show less from selected password
        /// </summary>
        private void ShowLess(object sender, EventArgs e)
        {
            // Create new random
            Random r = new Random();

            // Get selected password
            Password password = User.GetPassword(viewName.Text);

            // Get current secret text
            string secret = viewSecret.Text;

            // Change random hidden char to readable char
            int character = r.Next(0, secret.Length);

            // Number of '*' in secret
            int count = secret.Where(c => c == '*').Count();

            // Check if selected character is '*'
            while (secret[character] == '*')
            {
                // If there are no any characters left to hide
                if (count == secret.Length)
                {
                    break;
                }

                character = r.Next(0, secret.Length);
            }

            // Change * with password char
            StringBuilder sb = new StringBuilder(secret);
            sb[character] = '*';

            // Change viewName
            viewSecret.Text = sb.ToString();
        }

        /// <summary>
        /// Copy password's secret
        /// </summary>
        private void CopySecretClick(object sender, EventArgs e)
        {
            Clipboard.SetText(User.GetPassword(viewName.Text).Secret);
        }

        /// <summary>
        /// Removes password
        /// </summary>
        private void RemovePassword(object sender, EventArgs e)
        {
            // Get current selected item
            MItem selectedItem = listLeft.SelectedItem;

            // Group name
            string groupName = viewGroup.Text;

            // Index
            int index = listLeft.Items.IndexOf(selectedItem);

            // Remove password from user's storage
            User.RemovePassword(viewName.Text);

            // Removes deleted item from list
            listLeft.Items.Remove(selectedItem);

            // If there are any non MDivider items
            // (first item is always divider with group name)
            if (listLeft.Items.Count > 1)
            {
                // Select previous item
                listLeft.SelectedItem = listLeft.Items[index - 1];

                // Invalidate list
                listLeft.Invalidate(true);

                // Selected password
                Password selectedPassword = User.GetPassword(listLeft.SelectedItem.PrimaryText);

                // Fill form basic data
                viewName.Text = selectedPassword.Name;
                viewWeb.Text = selectedPassword.Web;
                viewSecret.Text = new string('*', selectedPassword.Secret.Length);
                viewGroup.Text = selectedPassword.Group;
            }
            else
            {
                // Whole group is empty
                // Go back
                // Selects tab
                content.SelectedTab = tabPasswords;

                // Select side content tab
                sideContent.SelectedTab = tabList;

                // Add groups to list view
                FillGroups();

                // Select group
                foreach (MItem item in listLeft.Items)
                {
                    if (item.PrimaryText == groupName)
                    {
                        listLeft.SelectedItem = item;
                        listLeft.Invalidate(true);
                    }
                }

                // There are no items in selected group
                // Clear passwords
                listRight.ItemsClear();
            }
        }

        /// <summary>
        /// Open web click
        /// </summary>
        private void OpenWebClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(viewWeb.Text);
            }
            catch
            {

            }
        }

        /// <summary>
        /// End app after form close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Application theme changed
        /// </summary>
        private void AppThemeChanged(object sender, EventArgs e)
        {
            if (rbAppThemeLight.Checked)
            {
                Minimal.SetTheme(this, Minimal.Light);
                User.UpdateConfig(new Config(Minimal.Light, User.GetConfig().DrawerTheme, User.GetConfig().Tint));
            }

            if (rbAppThemeDark.Checked)
            {
                Minimal.SetTheme(this, Minimal.Dark);
                User.UpdateConfig(new Config(Minimal.Dark, User.GetConfig().DrawerTheme, User.GetConfig().Tint));
            }
        }

        /// <summary>
        /// Drawer theme changed
        /// </summary>
        private void DrawerThemeChanged(object sender, EventArgs e)
        {
            if (rbDrawerThemeLight.Checked)
            {
                drawer.UsedTheme = Minimal.Light;
                User.UpdateConfig(new Config(User.GetConfig().AppTheme, Minimal.Light, User.GetConfig().Tint));
            }

            if (rbDrawerThemeDark.Checked)
            {
                drawer.UsedTheme = Minimal.Dark;
                User.UpdateConfig(new Config(User.GetConfig().AppTheme, Minimal.Dark, User.GetConfig().Tint));
            }
        }

        /// <summary>
        /// Edit password click
        /// </summary>
        private void EditPasswordClick(object sender, EventArgs e)
        {
            // Switch tab
            sideContent.SelectedTab = tabEditPassword;

            // Add groups to list view
            FillGroups();

            foreach (MItem item in listLeft.Items)
            {
                if (item.PrimaryText == viewGroup.Text)
                {
                    listLeft.SelectedItem = item;
                }
            }

            listLeft.Invalidate(true);

            // Fill form
            tbEditName.Text = viewName.Text;
            tbEditWeb.Text = viewWeb.Text;
            tbEditGroup.Text = viewGroup.Text;
            tbEditSecret.Text = User.GetPassword(viewName.Text).Secret;
        }

        /// <summary>
        /// Edit password back click
        /// </summary>
        private void EditPasswordBackClick(object sender, EventArgs e)
        {
            // Switch side content
            sideContent.SelectedTab = tabViewPassword;

            // Reset group list
            listLeft.ItemsClear();

            // Add divider
            listLeft.Items.Add(new GroupDivider(viewGroup.Text));

            // Get passwords which belongs to selected group
            foreach (Password password in User.GetPasswords())
            {
                if (password.Group == viewGroup.Text)
                {
                    // Add passwords from same group
                    listLeft.Items.Add(new PasswordItemMin(password.Name, password.Web));
                }
            }

            // Selected item
            foreach (MItem item in listLeft.Items)
            {
                if (item is MDividerItem)
                {
                    continue;
                }

                if (item.PrimaryText == viewName.Text)
                {
                    listLeft.SelectedItem = item;
                }
            }

            listLeft.Invalidate(true);
        }

        /// <summary>
        /// Update password
        /// </summary>
        private void UpdatePasswordClick(object sender, EventArgs e)
        {
            // Validate
            // Password name
            if (tbEditName.Text == "")
            {
                lblErrorMessagePasswordEdit.Visible = true;
                lblErrorMessagePasswordEdit.Text = "Name cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessagePasswordEdit.Visible = false;

            // Password web
            if (tbEditWeb.Text == "")
            {
                lblErrorMessagePasswordEdit.Visible = true;
                lblErrorMessagePasswordEdit.Text = "Web cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessagePasswordEdit.Visible = false;

            // Password secret
            if (tbEditSecret.Text == "")
            {
                lblErrorMessagePasswordEdit.Visible = true;
                lblErrorMessagePasswordEdit.Text = "Password cannot be left blank!";

                return;
            }

            // Hide error message
            lblErrorMessagePasswordEdit.Visible = false;

            // Selected group
            if (listLeft.SelectedItem == null)
            {
                lblErrorMessagePasswordEdit.Visible = true;
                lblErrorMessagePasswordEdit.Text = "You must choose group!";

                return;
            }

            lblErrorMessagePasswordEdit.Visible = false;

            // Validation successful
            User.UpdatePassword(viewName.Text, new Password(tbEditName.Text, tbEditWeb.Text, tbEditSecret.Text, tbEditGroup.Text));

            // Refresh
            // Reset right list
            // Clear previous passwords
            listRight.ItemsClear();

            // Add passwords to list view
            foreach (Password item in User.GetPasswords())
            {
                Group selectedGroup = User.GetGroupByName(listLeft.SelectedItem.PrimaryText);

                if (item.Group == selectedGroup.Name)
                {
                    listRight.Items.Add(new PasswordItem(item.Name, item.Web, item.Secret));
                }
            }

            listRight.Invalidate(true);

            // Go back
            // Selected password
            Password selectedPassword = User.GetPassword(tbEditName.Text);

            // Fill form basic data
            viewName.Text = selectedPassword.Name;
            viewWeb.Text = selectedPassword.Web;
            viewSecret.Text = new string('*', selectedPassword.Secret.Length);
            viewGroup.Text = selectedPassword.Group;

            // Switch tab
            sideContent.SelectedTab = tabViewPassword;

            // Reset group list
            listLeft.ItemsClear();

            // Add divider
            listLeft.Items.Add(new GroupDivider(selectedPassword.Group));

            // Get passwords which belongs to selected group
            foreach (Password password in User.GetPasswords())
            {
                if (password.Group == selectedPassword.Group)
                {
                    // Add passwords from same group
                    listLeft.Items.Add(new PasswordItemMin(password.Name, password.Web));
                }
            }

            // Selected item
            foreach (MItem item in listLeft.Items)
            {
                if (item is MDividerItem)
                {
                    continue;
                }

                if (item.PrimaryText == selectedPassword.Name)
                {
                    listLeft.SelectedItem = item;
                }
            }

            listLeft.Invalidate(true);
        }

        /// <summary>
        /// Update of tint color
        /// </summary>

        private void PaletteClick(object sender, EventArgs e)
        {
            // Get color
            MColorBox box = (MColorBox)sender;
            Minimal.SetTint(this, box.Color);

            // Update configuration
            Config c = User.GetConfig();
            User.UpdateConfig(new Config(c.AppTheme, c.DrawerTheme, box.Color));

            // Set tint color for homepage
            lblPasswordCount.Tint = lblGroupsCount.Tint = lblWarningsCount.Tint = Color.White;
            lblPasswords.Tint = lblGroups.Tint = lblWarnings.Tint = Color.White;
            btnAddPasswordShortcut.Tint = Color.FromArgb(76, 175, 80);
            btnAddGroupShortcut.Tint = Color.FromArgb(255, 152, 0);
            btnShowWarningsShortcut.Tint = Color.FromArgb(211, 47, 47);

            trbRed.Value = box.Color.R;
            trbGreen.Value = box.Color.G;
            trbBlue.Value = box.Color.B;
        }

        /// <summary>
        /// Edit group
        /// </summary>
        private void EditGroupLabelClick(object sender, EventArgs e)
        {
            if (listLeft.SelectedItem == null)
            {
                return;
            }

            sideContent.SelectedTab = tabEditGroup;
            groupEditName.Text = listLeft.SelectedItem.PrimaryText;
        }

        private void UpdateGroupClick(object sender, EventArgs e)
        {
            // User did not changed name
            if (groupEditName.Text == listLeft.SelectedItem.PrimaryText)
            {
                return;
            }

            // Check if group name is unique
            foreach(Group group in User.GetGroups())
            {
                if (group.Name == groupEditName.Text)
                {
                    lblEditGroupErrorMess.Visible = true;
                    lblEditGroupErrorMess.Text = "Group with this name already exists!";
                    return;
                }
            }

            lblEditGroupErrorMess.Visible = false;

            // Validation successful
            foreach (Password password in User.GetPasswords())
            {
                if (password.Group == listLeft.SelectedItem.PrimaryText)
                {
                    User.UpdatePassword(password.Name, new Password(password.Name, password.Web, password.Secret, groupEditName.Text));
                }
            }

            // Rename group
            User.UpdateGroup(listLeft.SelectedItem.PrimaryText, new Group(groupEditName.Text));

            // Refresh groups
            FillGroups();

            // Go back
            sideContent.SelectedTab = tabList;
        }

        private void editGroupBackClick(object sender, EventArgs e)
        {
            sideContent.SelectedTab = tabList;
        }

        private void SetCustomColorClick(object sender, EventArgs e)
        {
            Color color = Color.FromArgb((int)trbRed.Value, (int)trbGreen.Value, (int)trbBlue.Value);
            Minimal.SetTint(this, color);

            // Update configuration
            Config c = User.GetConfig();
            User.UpdateConfig(new Config(c.AppTheme, c.DrawerTheme, color));

            // Set tint color for homepage
            lblPasswordCount.Tint = lblGroupsCount.Tint = lblWarningsCount.Tint = Color.White;
            lblPasswords.Tint = lblGroups.Tint = lblWarnings.Tint = Color.White;
            btnAddPasswordShortcut.Tint = Color.FromArgb(76, 175, 80);
            btnAddGroupShortcut.Tint = Color.FromArgb(255, 152, 0);
            btnShowWarningsShortcut.Tint = Color.FromArgb(211, 47, 47);
        }

        private void TrbValueChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            trbRed.Tint = trbGreen.Tint = trbBlue.Tint = lblRedName.Tint = lblGreenName.Tint = lblBlueName.Tint = lblRed.Tint = lblGreen.Tint = lblBlue.Tint = Color.FromArgb((int)trbRed.Value, (int)trbGreen.Value, (int)trbBlue.Value);
        }
    }
}
