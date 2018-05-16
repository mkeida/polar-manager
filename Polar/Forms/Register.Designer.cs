namespace Polar.Forms
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sidePanel = new MinimalLibrary.Controls.MBufferedPanel();
            this.main = new System.Windows.Forms.Timer(this.components);
            this.tabMain = new MinimalLibrary.Controls.MStackPanel();
            this.tabRegister = new System.Windows.Forms.TabPage();
            this.tbEmail = new MinimalLibrary.Controls.MTextBox();
            this.chbConditions = new MinimalLibrary.Controls.MCheckBox();
            this.lblErrorMessage = new MinimalLibrary.Controls.MLabel();
            this.mButton2 = new MinimalLibrary.Controls.MButton();
            this.btnSignUp = new MinimalLibrary.Controls.MButton();
            this.mLabel6 = new MinimalLibrary.Controls.MLabel();
            this.tbPasswordTwo = new MinimalLibrary.Controls.MTextBox();
            this.mLabel5 = new MinimalLibrary.Controls.MLabel();
            this.tbPasswordOne = new MinimalLibrary.Controls.MTextBox();
            this.mLabel4 = new MinimalLibrary.Controls.MLabel();
            this.cbCountry = new MinimalLibrary.Controls.MComboBox();
            this.mLabel3 = new MinimalLibrary.Controls.MLabel();
            this.mLabel2 = new MinimalLibrary.Controls.MLabel();
            this.tbLastname = new MinimalLibrary.Controls.MTextBox();
            this.mLabel1 = new MinimalLibrary.Controls.MLabel();
            this.tbName = new MinimalLibrary.Controls.MTextBox();
            this.tabLogin = new System.Windows.Forms.TabPage();
            this.lblErrorMessageLogin = new MinimalLibrary.Controls.MLabel();
            this.mLabel7 = new MinimalLibrary.Controls.MLabel();
            this.tbPasswordLogin = new MinimalLibrary.Controls.MTextBox();
            this.mLabel8 = new MinimalLibrary.Controls.MLabel();
            this.tbEmailLogin = new MinimalLibrary.Controls.MTextBox();
            this.mButton1 = new MinimalLibrary.Controls.MButton();
            this.mButton3 = new MinimalLibrary.Controls.MButton();
            this.tabSetup = new System.Windows.Forms.TabPage();
            this.Next = new MinimalLibrary.Controls.MButton();
            this.mColorBox9 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox10 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox11 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox12 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox13 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox14 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox15 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox16 = new MinimalLibrary.Controls.MColorBox();
            this.mLabel11 = new MinimalLibrary.Controls.MLabel();
            this.mColorBox7 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox8 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox5 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox6 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox3 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox4 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox2 = new MinimalLibrary.Controls.MColorBox();
            this.mColorBox1 = new MinimalLibrary.Controls.MColorBox();
            this.lblBlue = new MinimalLibrary.Controls.MLabel();
            this.lblBlueName = new MinimalLibrary.Controls.MLabel();
            this.trbBlue = new MinimalLibrary.Controls.MTrackbar();
            this.lblGreen = new MinimalLibrary.Controls.MLabel();
            this.lblGreenName = new MinimalLibrary.Controls.MLabel();
            this.trbGreen = new MinimalLibrary.Controls.MTrackbar();
            this.lblRed = new MinimalLibrary.Controls.MLabel();
            this.lblRedName = new MinimalLibrary.Controls.MLabel();
            this.trbRed = new MinimalLibrary.Controls.MTrackbar();
            this.mLabel9 = new MinimalLibrary.Controls.MLabel();
            this.cbTheme = new MinimalLibrary.Controls.MComboBox();
            this.mLabel10 = new MinimalLibrary.Controls.MLabel();
            this.tabMain.SuspendLayout();
            this.tabRegister.SuspendLayout();
            this.tabLogin.SuspendLayout();
            this.tabSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidePanel
            // 
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(720, 673);
            this.sidePanel.TabIndex = 0;
            this.sidePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // main
            // 
            this.main.Enabled = true;
            this.main.Interval = 1;
            this.main.Tick += new System.EventHandler(this.Update);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabRegister);
            this.tabMain.Controls.Add(this.tabLogin);
            this.tabMain.Controls.Add(this.tabSetup);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(720, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(542, 673);
            this.tabMain.TabIndex = 1;
            this.tabMain.UseControlBackground = false;
            this.tabMain.UsedTheme = null;
            // 
            // tabRegister
            // 
            this.tabRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabRegister.Controls.Add(this.tbEmail);
            this.tabRegister.Controls.Add(this.chbConditions);
            this.tabRegister.Controls.Add(this.lblErrorMessage);
            this.tabRegister.Controls.Add(this.mButton2);
            this.tabRegister.Controls.Add(this.btnSignUp);
            this.tabRegister.Controls.Add(this.mLabel6);
            this.tabRegister.Controls.Add(this.tbPasswordTwo);
            this.tabRegister.Controls.Add(this.mLabel5);
            this.tabRegister.Controls.Add(this.tbPasswordOne);
            this.tabRegister.Controls.Add(this.mLabel4);
            this.tabRegister.Controls.Add(this.cbCountry);
            this.tabRegister.Controls.Add(this.mLabel3);
            this.tabRegister.Controls.Add(this.mLabel2);
            this.tabRegister.Controls.Add(this.tbLastname);
            this.tabRegister.Controls.Add(this.mLabel1);
            this.tabRegister.Controls.Add(this.tbName);
            this.tabRegister.Location = new System.Drawing.Point(4, 4);
            this.tabRegister.Name = "tabRegister";
            this.tabRegister.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegister.Size = new System.Drawing.Size(534, 644);
            this.tabRegister.TabIndex = 0;
            this.tabRegister.Text = "Register";
            // 
            // tbEmail
            // 
            this.tbEmail.AcceptsReturn = false;
            this.tbEmail.AcceptsTab = false;
            this.tbEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbEmail.HideSelection = true;
            this.tbEmail.Location = new System.Drawing.Point(12, 194);
            this.tbEmail.Margin = new System.Windows.Forms.Padding(6);
            this.tbEmail.MaxLength = 32767;
            this.tbEmail.Multiline = false;
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.PasswordChar = '\0';
            this.tbEmail.Placeholder = "";
            this.tbEmail.ReadOnly = false;
            this.tbEmail.ShortcutsEnabled = true;
            this.tbEmail.Size = new System.Drawing.Size(518, 40);
            this.tbEmail.TabIndex = 33;
            this.tbEmail.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbEmail.UsedTheme = null;
            this.tbEmail.UseSystemPasswordChar = false;
            this.tbEmail.WordWrap = true;
            this.tbEmail.TextChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TbTextChanged);
            // 
            // chbConditions
            // 
            this.chbConditions.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.chbConditions.Location = new System.Drawing.Point(12, 564);
            this.chbConditions.Name = "chbConditions";
            this.chbConditions.Size = new System.Drawing.Size(215, 36);
            this.chbConditions.TabIndex = 31;
            this.chbConditions.Text = "I agree with terms and conditions";
            this.chbConditions.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.chbConditions.UsedTheme = null;
            this.chbConditions.UseVisualStyleBackColor = true;
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblErrorMessage.Location = new System.Drawing.Point(7, 541);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(103, 20);
            this.lblErrorMessage.TabIndex = 30;
            this.lblErrorMessage.Text = "Error message";
            this.lblErrorMessage.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblErrorMessage.Type = MinimalLibrary.LabelType.Tint;
            this.lblErrorMessage.UsedTheme = null;
            // 
            // mButton2
            // 
            this.mButton2.CapitalText = true;
            this.mButton2.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.mButton2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.mButton2.FullColored = true;
            this.mButton2.Location = new System.Drawing.Point(12, 625);
            this.mButton2.Name = "mButton2";
            this.mButton2.Size = new System.Drawing.Size(251, 36);
            this.mButton2.TabIndex = 29;
            this.mButton2.Text = "I already have an account";
            this.mButton2.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mButton2.Type = MinimalLibrary.ButtonType.Outline;
            this.mButton2.UsedTheme = null;
            this.mButton2.Click += new System.EventHandler(this.ExistingAccountClick);
            // 
            // btnSignUp
            // 
            this.btnSignUp.CapitalText = true;
            this.btnSignUp.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.btnSignUp.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSignUp.ForeColor = System.Drawing.Color.White;
            this.btnSignUp.FullColored = true;
            this.btnSignUp.Location = new System.Drawing.Point(279, 625);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(251, 36);
            this.btnSignUp.TabIndex = 28;
            this.btnSignUp.Text = "Sign up";
            this.btnSignUp.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnSignUp.Type = MinimalLibrary.ButtonType.Raised;
            this.btnSignUp.UsedTheme = null;
            this.btnSignUp.Click += new System.EventHandler(this.SignUpClick);
            // 
            // mLabel6
            // 
            this.mLabel6.AutoSize = true;
            this.mLabel6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel6.Location = new System.Drawing.Point(12, 399);
            this.mLabel6.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.mLabel6.Name = "mLabel6";
            this.mLabel6.Size = new System.Drawing.Size(111, 20);
            this.mLabel6.TabIndex = 27;
            this.mLabel6.Text = "Password again";
            this.mLabel6.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel6.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel6.UsedTheme = null;
            // 
            // tbPasswordTwo
            // 
            this.tbPasswordTwo.AcceptsReturn = false;
            this.tbPasswordTwo.AcceptsTab = false;
            this.tbPasswordTwo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbPasswordTwo.HideSelection = true;
            this.tbPasswordTwo.Location = new System.Drawing.Point(12, 425);
            this.tbPasswordTwo.Margin = new System.Windows.Forms.Padding(6);
            this.tbPasswordTwo.MaxLength = 32767;
            this.tbPasswordTwo.Multiline = false;
            this.tbPasswordTwo.Name = "tbPasswordTwo";
            this.tbPasswordTwo.PasswordChar = '*';
            this.tbPasswordTwo.Placeholder = "";
            this.tbPasswordTwo.ReadOnly = false;
            this.tbPasswordTwo.ShortcutsEnabled = true;
            this.tbPasswordTwo.Size = new System.Drawing.Size(518, 40);
            this.tbPasswordTwo.TabIndex = 26;
            this.tbPasswordTwo.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbPasswordTwo.UsedTheme = null;
            this.tbPasswordTwo.UseSystemPasswordChar = false;
            this.tbPasswordTwo.WordWrap = true;
            this.tbPasswordTwo.TextChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TbTextChanged);
            // 
            // mLabel5
            // 
            this.mLabel5.AutoSize = true;
            this.mLabel5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel5.Location = new System.Drawing.Point(12, 321);
            this.mLabel5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.mLabel5.Name = "mLabel5";
            this.mLabel5.Size = new System.Drawing.Size(70, 20);
            this.mLabel5.TabIndex = 25;
            this.mLabel5.Text = "Password";
            this.mLabel5.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel5.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel5.UsedTheme = null;
            // 
            // tbPasswordOne
            // 
            this.tbPasswordOne.AcceptsReturn = false;
            this.tbPasswordOne.AcceptsTab = false;
            this.tbPasswordOne.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbPasswordOne.HideSelection = true;
            this.tbPasswordOne.Location = new System.Drawing.Point(12, 347);
            this.tbPasswordOne.Margin = new System.Windows.Forms.Padding(6);
            this.tbPasswordOne.MaxLength = 32767;
            this.tbPasswordOne.Multiline = false;
            this.tbPasswordOne.Name = "tbPasswordOne";
            this.tbPasswordOne.PasswordChar = '*';
            this.tbPasswordOne.Placeholder = "";
            this.tbPasswordOne.ReadOnly = false;
            this.tbPasswordOne.ShortcutsEnabled = true;
            this.tbPasswordOne.Size = new System.Drawing.Size(518, 40);
            this.tbPasswordOne.TabIndex = 24;
            this.tbPasswordOne.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbPasswordOne.UsedTheme = null;
            this.tbPasswordOne.UseSystemPasswordChar = false;
            this.tbPasswordOne.WordWrap = true;
            this.tbPasswordOne.TextChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TbTextChanged);
            // 
            // mLabel4
            // 
            this.mLabel4.AutoSize = true;
            this.mLabel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel4.Location = new System.Drawing.Point(12, 246);
            this.mLabel4.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.mLabel4.Name = "mLabel4";
            this.mLabel4.Size = new System.Drawing.Size(60, 20);
            this.mLabel4.TabIndex = 23;
            this.mLabel4.Text = "Country";
            this.mLabel4.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel4.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel4.UsedTheme = null;
            // 
            // cbCountry
            // 
            this.cbCountry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(210)))), ((int)(((byte)(213)))));
            this.cbCountry.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.cbCountry.DefaultText = "Select your country";
            this.cbCountry.DisplaySearch = true;
            this.cbCountry.Location = new System.Drawing.Point(12, 272);
            this.cbCountry.Margin = new System.Windows.Forms.Padding(6);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Padding = new System.Windows.Forms.Padding(1);
            this.cbCountry.Size = new System.Drawing.Size(518, 37);
            this.cbCountry.TabIndex = 22;
            this.cbCountry.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.cbCountry.UsedTheme = null;
            this.cbCountry.SelectedItemChanged += new System.ComponentModel.PropertyChangedEventHandler(this.SelectedCountryChanged);
            // 
            // mLabel3
            // 
            this.mLabel3.AutoSize = true;
            this.mLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel3.Location = new System.Drawing.Point(12, 168);
            this.mLabel3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.mLabel3.Name = "mLabel3";
            this.mLabel3.Size = new System.Drawing.Size(52, 20);
            this.mLabel3.TabIndex = 21;
            this.mLabel3.Text = "E-Mail";
            this.mLabel3.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel3.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel3.UsedTheme = null;
            // 
            // mLabel2
            // 
            this.mLabel2.AutoSize = true;
            this.mLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel2.Location = new System.Drawing.Point(12, 90);
            this.mLabel2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.mLabel2.Name = "mLabel2";
            this.mLabel2.Size = new System.Drawing.Size(72, 20);
            this.mLabel2.TabIndex = 19;
            this.mLabel2.Text = "Lastname";
            this.mLabel2.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel2.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel2.UsedTheme = null;
            // 
            // tbLastname
            // 
            this.tbLastname.AcceptsReturn = false;
            this.tbLastname.AcceptsTab = false;
            this.tbLastname.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbLastname.HideSelection = true;
            this.tbLastname.Location = new System.Drawing.Point(12, 116);
            this.tbLastname.Margin = new System.Windows.Forms.Padding(6);
            this.tbLastname.MaxLength = 32767;
            this.tbLastname.Multiline = false;
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.PasswordChar = '\0';
            this.tbLastname.Placeholder = "";
            this.tbLastname.ReadOnly = false;
            this.tbLastname.ShortcutsEnabled = true;
            this.tbLastname.Size = new System.Drawing.Size(518, 40);
            this.tbLastname.TabIndex = 18;
            this.tbLastname.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbLastname.UsedTheme = null;
            this.tbLastname.UseSystemPasswordChar = false;
            this.tbLastname.WordWrap = true;
            this.tbLastname.TextChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TbTextChanged);
            // 
            // mLabel1
            // 
            this.mLabel1.AutoSize = true;
            this.mLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel1.Location = new System.Drawing.Point(9, 12);
            this.mLabel1.Name = "mLabel1";
            this.mLabel1.Size = new System.Drawing.Size(49, 20);
            this.mLabel1.TabIndex = 17;
            this.mLabel1.Text = "Name";
            this.mLabel1.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel1.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel1.UsedTheme = null;
            // 
            // tbName
            // 
            this.tbName.AcceptsReturn = false;
            this.tbName.AcceptsTab = false;
            this.tbName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbName.HideSelection = true;
            this.tbName.Location = new System.Drawing.Point(12, 38);
            this.tbName.Margin = new System.Windows.Forms.Padding(6);
            this.tbName.MaxLength = 32767;
            this.tbName.Multiline = false;
            this.tbName.Name = "tbName";
            this.tbName.PasswordChar = '\0';
            this.tbName.Placeholder = "";
            this.tbName.ReadOnly = false;
            this.tbName.ShortcutsEnabled = true;
            this.tbName.Size = new System.Drawing.Size(518, 40);
            this.tbName.TabIndex = 16;
            this.tbName.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbName.UsedTheme = null;
            this.tbName.UseSystemPasswordChar = false;
            this.tbName.WordWrap = true;
            this.tbName.TextChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TbTextChanged);
            // 
            // tabLogin
            // 
            this.tabLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabLogin.Controls.Add(this.lblErrorMessageLogin);
            this.tabLogin.Controls.Add(this.mLabel7);
            this.tabLogin.Controls.Add(this.tbPasswordLogin);
            this.tabLogin.Controls.Add(this.mLabel8);
            this.tabLogin.Controls.Add(this.tbEmailLogin);
            this.tabLogin.Controls.Add(this.mButton1);
            this.tabLogin.Controls.Add(this.mButton3);
            this.tabLogin.Location = new System.Drawing.Point(4, 4);
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogin.Size = new System.Drawing.Size(534, 644);
            this.tabLogin.TabIndex = 1;
            this.tabLogin.Text = "Login";
            // 
            // lblErrorMessageLogin
            // 
            this.lblErrorMessageLogin.AutoSize = true;
            this.lblErrorMessageLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblErrorMessageLogin.Location = new System.Drawing.Point(8, 593);
            this.lblErrorMessageLogin.Name = "lblErrorMessageLogin";
            this.lblErrorMessageLogin.Size = new System.Drawing.Size(103, 20);
            this.lblErrorMessageLogin.TabIndex = 36;
            this.lblErrorMessageLogin.Text = "Error message";
            this.lblErrorMessageLogin.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblErrorMessageLogin.Type = MinimalLibrary.LabelType.Tint;
            this.lblErrorMessageLogin.UsedTheme = null;
            this.lblErrorMessageLogin.Visible = false;
            // 
            // mLabel7
            // 
            this.mLabel7.AutoSize = true;
            this.mLabel7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel7.Location = new System.Drawing.Point(9, 87);
            this.mLabel7.Name = "mLabel7";
            this.mLabel7.Size = new System.Drawing.Size(70, 20);
            this.mLabel7.TabIndex = 35;
            this.mLabel7.Text = "Password";
            this.mLabel7.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel7.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel7.UsedTheme = null;
            // 
            // tbPasswordLogin
            // 
            this.tbPasswordLogin.AcceptsReturn = false;
            this.tbPasswordLogin.AcceptsTab = false;
            this.tbPasswordLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbPasswordLogin.HideSelection = true;
            this.tbPasswordLogin.Location = new System.Drawing.Point(12, 113);
            this.tbPasswordLogin.Margin = new System.Windows.Forms.Padding(6);
            this.tbPasswordLogin.MaxLength = 32767;
            this.tbPasswordLogin.Multiline = false;
            this.tbPasswordLogin.Name = "tbPasswordLogin";
            this.tbPasswordLogin.PasswordChar = '*';
            this.tbPasswordLogin.Placeholder = "";
            this.tbPasswordLogin.ReadOnly = false;
            this.tbPasswordLogin.ShortcutsEnabled = true;
            this.tbPasswordLogin.Size = new System.Drawing.Size(518, 40);
            this.tbPasswordLogin.TabIndex = 34;
            this.tbPasswordLogin.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbPasswordLogin.UsedTheme = null;
            this.tbPasswordLogin.UseSystemPasswordChar = false;
            this.tbPasswordLogin.WordWrap = true;
            // 
            // mLabel8
            // 
            this.mLabel8.AutoSize = true;
            this.mLabel8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel8.Location = new System.Drawing.Point(9, 12);
            this.mLabel8.Name = "mLabel8";
            this.mLabel8.Size = new System.Drawing.Size(52, 20);
            this.mLabel8.TabIndex = 33;
            this.mLabel8.Text = "E-Mail";
            this.mLabel8.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel8.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel8.UsedTheme = null;
            // 
            // tbEmailLogin
            // 
            this.tbEmailLogin.AcceptsReturn = false;
            this.tbEmailLogin.AcceptsTab = false;
            this.tbEmailLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbEmailLogin.HideSelection = true;
            this.tbEmailLogin.Location = new System.Drawing.Point(12, 38);
            this.tbEmailLogin.Margin = new System.Windows.Forms.Padding(6);
            this.tbEmailLogin.MaxLength = 32767;
            this.tbEmailLogin.Multiline = false;
            this.tbEmailLogin.Name = "tbEmailLogin";
            this.tbEmailLogin.PasswordChar = '\0';
            this.tbEmailLogin.Placeholder = "";
            this.tbEmailLogin.ReadOnly = false;
            this.tbEmailLogin.ShortcutsEnabled = true;
            this.tbEmailLogin.Size = new System.Drawing.Size(518, 40);
            this.tbEmailLogin.TabIndex = 32;
            this.tbEmailLogin.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.tbEmailLogin.UsedTheme = null;
            this.tbEmailLogin.UseSystemPasswordChar = false;
            this.tbEmailLogin.WordWrap = true;
            // 
            // mButton1
            // 
            this.mButton1.CapitalText = true;
            this.mButton1.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.mButton1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.mButton1.FullColored = true;
            this.mButton1.Location = new System.Drawing.Point(12, 625);
            this.mButton1.Name = "mButton1";
            this.mButton1.Size = new System.Drawing.Size(251, 36);
            this.mButton1.TabIndex = 31;
            this.mButton1.Text = "I don\'t have an account";
            this.mButton1.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mButton1.Type = MinimalLibrary.ButtonType.Outline;
            this.mButton1.UsedTheme = null;
            this.mButton1.Click += new System.EventHandler(this.NewAccountClick);
            // 
            // mButton3
            // 
            this.mButton3.CapitalText = true;
            this.mButton3.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.mButton3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.mButton3.ForeColor = System.Drawing.Color.White;
            this.mButton3.FullColored = true;
            this.mButton3.Location = new System.Drawing.Point(279, 625);
            this.mButton3.Name = "mButton3";
            this.mButton3.Size = new System.Drawing.Size(251, 36);
            this.mButton3.TabIndex = 30;
            this.mButton3.Text = "Sign in";
            this.mButton3.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mButton3.Type = MinimalLibrary.ButtonType.Raised;
            this.mButton3.UsedTheme = null;
            this.mButton3.Click += new System.EventHandler(this.SignInClick);
            // 
            // tabSetup
            // 
            this.tabSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabSetup.Controls.Add(this.Next);
            this.tabSetup.Controls.Add(this.mColorBox9);
            this.tabSetup.Controls.Add(this.mColorBox10);
            this.tabSetup.Controls.Add(this.mColorBox11);
            this.tabSetup.Controls.Add(this.mColorBox12);
            this.tabSetup.Controls.Add(this.mColorBox13);
            this.tabSetup.Controls.Add(this.mColorBox14);
            this.tabSetup.Controls.Add(this.mColorBox15);
            this.tabSetup.Controls.Add(this.mColorBox16);
            this.tabSetup.Controls.Add(this.mLabel11);
            this.tabSetup.Controls.Add(this.mColorBox7);
            this.tabSetup.Controls.Add(this.mColorBox8);
            this.tabSetup.Controls.Add(this.mColorBox5);
            this.tabSetup.Controls.Add(this.mColorBox6);
            this.tabSetup.Controls.Add(this.mColorBox3);
            this.tabSetup.Controls.Add(this.mColorBox4);
            this.tabSetup.Controls.Add(this.mColorBox2);
            this.tabSetup.Controls.Add(this.mColorBox1);
            this.tabSetup.Controls.Add(this.lblBlue);
            this.tabSetup.Controls.Add(this.lblBlueName);
            this.tabSetup.Controls.Add(this.trbBlue);
            this.tabSetup.Controls.Add(this.lblGreen);
            this.tabSetup.Controls.Add(this.lblGreenName);
            this.tabSetup.Controls.Add(this.trbGreen);
            this.tabSetup.Controls.Add(this.lblRed);
            this.tabSetup.Controls.Add(this.lblRedName);
            this.tabSetup.Controls.Add(this.trbRed);
            this.tabSetup.Controls.Add(this.mLabel9);
            this.tabSetup.Controls.Add(this.cbTheme);
            this.tabSetup.Controls.Add(this.mLabel10);
            this.tabSetup.Location = new System.Drawing.Point(4, 4);
            this.tabSetup.Name = "tabSetup";
            this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetup.Size = new System.Drawing.Size(534, 644);
            this.tabSetup.TabIndex = 2;
            this.tabSetup.Text = "Setup";
            // 
            // Next
            // 
            this.Next.CapitalText = true;
            this.Next.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.Next.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Next.ForeColor = System.Drawing.Color.White;
            this.Next.FullColored = true;
            this.Next.Location = new System.Drawing.Point(435, 625);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(95, 36);
            this.Next.TabIndex = 71;
            this.Next.Text = "Confirm";
            this.Next.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.Next.Type = MinimalLibrary.ButtonType.Raised;
            this.Next.UsedTheme = null;
            this.Next.Click += new System.EventHandler(this.ConfirmClick);
            // 
            // mColorBox9
            // 
            this.mColorBox9.Color = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(72)))));
            this.mColorBox9.DrawBorder = false;
            this.mColorBox9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox9.ForeColor = System.Drawing.Color.White;
            this.mColorBox9.Location = new System.Drawing.Point(410, 268);
            this.mColorBox9.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox9.Name = "mColorBox9";
            this.mColorBox9.Size = new System.Drawing.Size(120, 44);
            this.mColorBox9.TabIndex = 70;
            this.mColorBox9.Text = "Ahoj";
            this.mColorBox9.Title = "#8D6E63";
            this.mColorBox9.UsedTheme = null;
            this.mColorBox9.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox10
            // 
            this.mColorBox10.Color = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(110)))), ((int)(((byte)(99)))));
            this.mColorBox10.DrawBorder = false;
            this.mColorBox10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox10.ForeColor = System.Drawing.Color.White;
            this.mColorBox10.Location = new System.Drawing.Point(410, 224);
            this.mColorBox10.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox10.Name = "mColorBox10";
            this.mColorBox10.Size = new System.Drawing.Size(120, 44);
            this.mColorBox10.TabIndex = 69;
            this.mColorBox10.Text = "Ahoj";
            this.mColorBox10.Title = "Brown";
            this.mColorBox10.UsedTheme = null;
            this.mColorBox10.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox11
            // 
            this.mColorBox11.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this.mColorBox11.DrawBorder = false;
            this.mColorBox11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox11.ForeColor = System.Drawing.Color.White;
            this.mColorBox11.Location = new System.Drawing.Point(277, 268);
            this.mColorBox11.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox11.Name = "mColorBox11";
            this.mColorBox11.Size = new System.Drawing.Size(120, 44);
            this.mColorBox11.TabIndex = 68;
            this.mColorBox11.Text = "Ahoj";
            this.mColorBox11.Title = "#FFB300";
            this.mColorBox11.UsedTheme = null;
            this.mColorBox11.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox12
            // 
            this.mColorBox12.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(0)))));
            this.mColorBox12.DrawBorder = false;
            this.mColorBox12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox12.ForeColor = System.Drawing.Color.White;
            this.mColorBox12.Location = new System.Drawing.Point(277, 224);
            this.mColorBox12.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox12.Name = "mColorBox12";
            this.mColorBox12.Size = new System.Drawing.Size(120, 44);
            this.mColorBox12.TabIndex = 67;
            this.mColorBox12.Text = "Ahoj";
            this.mColorBox12.Title = "Amber";
            this.mColorBox12.UsedTheme = null;
            this.mColorBox12.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox13
            // 
            this.mColorBox13.Color = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.mColorBox13.DrawBorder = false;
            this.mColorBox13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox13.ForeColor = System.Drawing.Color.White;
            this.mColorBox13.Location = new System.Drawing.Point(145, 268);
            this.mColorBox13.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox13.Name = "mColorBox13";
            this.mColorBox13.Size = new System.Drawing.Size(120, 44);
            this.mColorBox13.TabIndex = 66;
            this.mColorBox13.Text = "Ahoj";
            this.mColorBox13.Title = "#43A047";
            this.mColorBox13.UsedTheme = null;
            this.mColorBox13.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox14
            // 
            this.mColorBox14.Color = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(160)))), ((int)(((byte)(71)))));
            this.mColorBox14.DrawBorder = false;
            this.mColorBox14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox14.ForeColor = System.Drawing.Color.White;
            this.mColorBox14.Location = new System.Drawing.Point(145, 224);
            this.mColorBox14.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox14.Name = "mColorBox14";
            this.mColorBox14.Size = new System.Drawing.Size(120, 44);
            this.mColorBox14.TabIndex = 65;
            this.mColorBox14.Text = "Ahoj";
            this.mColorBox14.Title = "Green";
            this.mColorBox14.UsedTheme = null;
            this.mColorBox14.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox15
            // 
            this.mColorBox15.Color = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.mColorBox15.DrawBorder = false;
            this.mColorBox15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox15.ForeColor = System.Drawing.Color.White;
            this.mColorBox15.Location = new System.Drawing.Point(13, 268);
            this.mColorBox15.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox15.Name = "mColorBox15";
            this.mColorBox15.Size = new System.Drawing.Size(120, 44);
            this.mColorBox15.TabIndex = 64;
            this.mColorBox15.Text = "Ahoj";
            this.mColorBox15.Title = "#34495E";
            this.mColorBox15.UsedTheme = null;
            this.mColorBox15.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox16
            // 
            this.mColorBox16.Color = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.mColorBox16.DrawBorder = false;
            this.mColorBox16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox16.ForeColor = System.Drawing.Color.White;
            this.mColorBox16.Location = new System.Drawing.Point(13, 224);
            this.mColorBox16.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox16.Name = "mColorBox16";
            this.mColorBox16.Size = new System.Drawing.Size(120, 44);
            this.mColorBox16.TabIndex = 63;
            this.mColorBox16.Text = "Ahoj";
            this.mColorBox16.Title = "Dark gray";
            this.mColorBox16.UsedTheme = null;
            this.mColorBox16.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mLabel11
            // 
            this.mLabel11.AutoSize = true;
            this.mLabel11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel11.Location = new System.Drawing.Point(13, 93);
            this.mLabel11.Name = "mLabel11";
            this.mLabel11.Size = new System.Drawing.Size(96, 20);
            this.mLabel11.TabIndex = 62;
            this.mLabel11.Text = "Color palette";
            this.mLabel11.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel11.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel11.UsedTheme = null;
            // 
            // mColorBox7
            // 
            this.mColorBox7.Color = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.mColorBox7.DrawBorder = false;
            this.mColorBox7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox7.ForeColor = System.Drawing.Color.White;
            this.mColorBox7.Location = new System.Drawing.Point(410, 167);
            this.mColorBox7.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox7.Name = "mColorBox7";
            this.mColorBox7.Size = new System.Drawing.Size(120, 44);
            this.mColorBox7.TabIndex = 61;
            this.mColorBox7.Text = "Ahoj";
            this.mColorBox7.Title = "#3498DB";
            this.mColorBox7.UsedTheme = null;
            this.mColorBox7.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox8
            // 
            this.mColorBox8.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mColorBox8.DrawBorder = false;
            this.mColorBox8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox8.ForeColor = System.Drawing.Color.White;
            this.mColorBox8.Location = new System.Drawing.Point(410, 123);
            this.mColorBox8.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox8.Name = "mColorBox8";
            this.mColorBox8.Size = new System.Drawing.Size(120, 44);
            this.mColorBox8.TabIndex = 60;
            this.mColorBox8.Text = "Ahoj";
            this.mColorBox8.Title = "Blue";
            this.mColorBox8.UsedTheme = null;
            this.mColorBox8.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox5
            // 
            this.mColorBox5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(31)))), ((int)(((byte)(162)))));
            this.mColorBox5.DrawBorder = false;
            this.mColorBox5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox5.ForeColor = System.Drawing.Color.White;
            this.mColorBox5.Location = new System.Drawing.Point(277, 167);
            this.mColorBox5.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox5.Name = "mColorBox5";
            this.mColorBox5.Size = new System.Drawing.Size(120, 44);
            this.mColorBox5.TabIndex = 59;
            this.mColorBox5.Text = "Ahoj";
            this.mColorBox5.Title = "#8E24AA";
            this.mColorBox5.UsedTheme = null;
            this.mColorBox5.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox6
            // 
            this.mColorBox6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(36)))), ((int)(((byte)(170)))));
            this.mColorBox6.DrawBorder = false;
            this.mColorBox6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox6.ForeColor = System.Drawing.Color.White;
            this.mColorBox6.Location = new System.Drawing.Point(277, 123);
            this.mColorBox6.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox6.Name = "mColorBox6";
            this.mColorBox6.Size = new System.Drawing.Size(120, 44);
            this.mColorBox6.TabIndex = 58;
            this.mColorBox6.Text = "Ahoj";
            this.mColorBox6.Title = "Purple";
            this.mColorBox6.UsedTheme = null;
            this.mColorBox6.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox3
            // 
            this.mColorBox3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(24)))), ((int)(((byte)(91)))));
            this.mColorBox3.DrawBorder = false;
            this.mColorBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox3.ForeColor = System.Drawing.Color.White;
            this.mColorBox3.Location = new System.Drawing.Point(145, 167);
            this.mColorBox3.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox3.Name = "mColorBox3";
            this.mColorBox3.Size = new System.Drawing.Size(120, 44);
            this.mColorBox3.TabIndex = 57;
            this.mColorBox3.Text = "Ahoj";
            this.mColorBox3.Title = "#D81B60";
            this.mColorBox3.UsedTheme = null;
            this.mColorBox3.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox4
            // 
            this.mColorBox4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(27)))), ((int)(((byte)(96)))));
            this.mColorBox4.DrawBorder = false;
            this.mColorBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox4.ForeColor = System.Drawing.Color.White;
            this.mColorBox4.Location = new System.Drawing.Point(145, 123);
            this.mColorBox4.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox4.Name = "mColorBox4";
            this.mColorBox4.Size = new System.Drawing.Size(120, 44);
            this.mColorBox4.TabIndex = 56;
            this.mColorBox4.Text = "Ahoj";
            this.mColorBox4.Title = "Pink";
            this.mColorBox4.UsedTheme = null;
            this.mColorBox4.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox2
            // 
            this.mColorBox2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.mColorBox2.DrawBorder = false;
            this.mColorBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox2.ForeColor = System.Drawing.Color.White;
            this.mColorBox2.Location = new System.Drawing.Point(13, 167);
            this.mColorBox2.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox2.Name = "mColorBox2";
            this.mColorBox2.Size = new System.Drawing.Size(120, 44);
            this.mColorBox2.TabIndex = 55;
            this.mColorBox2.Text = "Ahoj";
            this.mColorBox2.Title = "#E53935";
            this.mColorBox2.UsedTheme = null;
            this.mColorBox2.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // mColorBox1
            // 
            this.mColorBox1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.mColorBox1.DrawBorder = false;
            this.mColorBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mColorBox1.ForeColor = System.Drawing.Color.White;
            this.mColorBox1.Location = new System.Drawing.Point(13, 123);
            this.mColorBox1.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.mColorBox1.Name = "mColorBox1";
            this.mColorBox1.Size = new System.Drawing.Size(120, 44);
            this.mColorBox1.TabIndex = 54;
            this.mColorBox1.Text = "Ahoj";
            this.mColorBox1.Title = "Red";
            this.mColorBox1.UsedTheme = null;
            this.mColorBox1.Click += new System.EventHandler(this.ColorBoxClick);
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBlue.Location = new System.Drawing.Point(498, 454);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(33, 20);
            this.lblBlue.TabIndex = 48;
            this.lblBlue.Text = "229";
            this.lblBlue.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblBlue.Type = MinimalLibrary.LabelType.Tint;
            this.lblBlue.UsedTheme = null;
            // 
            // lblBlueName
            // 
            this.lblBlueName.AutoSize = true;
            this.lblBlueName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBlueName.Location = new System.Drawing.Point(13, 454);
            this.lblBlueName.Name = "lblBlueName";
            this.lblBlueName.Size = new System.Drawing.Size(18, 20);
            this.lblBlueName.TabIndex = 47;
            this.lblBlueName.Text = "B";
            this.lblBlueName.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblBlueName.Type = MinimalLibrary.LabelType.Tint;
            this.lblBlueName.UsedTheme = null;
            // 
            // trbBlue
            // 
            this.trbBlue.AutoHide = true;
            this.trbBlue.IncreaseValueGradually = true;
            this.trbBlue.Location = new System.Drawing.Point(30, 447);
            this.trbBlue.Maximum = 255D;
            this.trbBlue.Minimum = 0D;
            this.trbBlue.Name = "trbBlue";
            this.trbBlue.Size = new System.Drawing.Size(462, 35);
            this.trbBlue.TabIndex = 46;
            this.trbBlue.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.trbBlue.UsedTheme = null;
            this.trbBlue.Value = 229D;
            this.trbBlue.ValueChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TrbBlueChanged);
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGreen.Location = new System.Drawing.Point(498, 411);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(33, 20);
            this.lblGreen.TabIndex = 45;
            this.lblGreen.Text = "136";
            this.lblGreen.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblGreen.Type = MinimalLibrary.LabelType.Tint;
            this.lblGreen.UsedTheme = null;
            // 
            // lblGreenName
            // 
            this.lblGreenName.AutoSize = true;
            this.lblGreenName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGreenName.Location = new System.Drawing.Point(13, 411);
            this.lblGreenName.Name = "lblGreenName";
            this.lblGreenName.Size = new System.Drawing.Size(19, 20);
            this.lblGreenName.TabIndex = 44;
            this.lblGreenName.Text = "G";
            this.lblGreenName.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblGreenName.Type = MinimalLibrary.LabelType.Tint;
            this.lblGreenName.UsedTheme = null;
            // 
            // trbGreen
            // 
            this.trbGreen.AutoHide = true;
            this.trbGreen.IncreaseValueGradually = true;
            this.trbGreen.Location = new System.Drawing.Point(30, 404);
            this.trbGreen.Maximum = 255D;
            this.trbGreen.Minimum = 0D;
            this.trbGreen.Name = "trbGreen";
            this.trbGreen.Size = new System.Drawing.Size(462, 35);
            this.trbGreen.TabIndex = 43;
            this.trbGreen.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.trbGreen.UsedTheme = null;
            this.trbGreen.Value = 136D;
            this.trbGreen.ValueChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TrbGreenChanged);
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRed.Location = new System.Drawing.Point(498, 367);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(25, 20);
            this.lblRed.TabIndex = 42;
            this.lblRed.Text = "30";
            this.lblRed.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblRed.Type = MinimalLibrary.LabelType.Tint;
            this.lblRed.UsedTheme = null;
            // 
            // lblRedName
            // 
            this.lblRedName.AutoSize = true;
            this.lblRedName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRedName.Location = new System.Drawing.Point(13, 367);
            this.lblRedName.Name = "lblRedName";
            this.lblRedName.Size = new System.Drawing.Size(18, 20);
            this.lblRedName.TabIndex = 41;
            this.lblRedName.Text = "R";
            this.lblRedName.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblRedName.Type = MinimalLibrary.LabelType.Tint;
            this.lblRedName.UsedTheme = null;
            // 
            // trbRed
            // 
            this.trbRed.AutoHide = true;
            this.trbRed.IncreaseValueGradually = true;
            this.trbRed.Location = new System.Drawing.Point(30, 360);
            this.trbRed.Maximum = 255D;
            this.trbRed.Minimum = 0D;
            this.trbRed.Name = "trbRed";
            this.trbRed.Size = new System.Drawing.Size(462, 35);
            this.trbRed.TabIndex = 40;
            this.trbRed.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.trbRed.UsedTheme = null;
            this.trbRed.Value = 30D;
            this.trbRed.ValueChanged += new System.ComponentModel.PropertyChangedEventHandler(this.TrbRedChanged);
            // 
            // mLabel9
            // 
            this.mLabel9.AutoSize = true;
            this.mLabel9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel9.Location = new System.Drawing.Point(13, 333);
            this.mLabel9.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.mLabel9.Name = "mLabel9";
            this.mLabel9.Size = new System.Drawing.Size(97, 20);
            this.mLabel9.TabIndex = 39;
            this.mLabel9.Text = "Custom color";
            this.mLabel9.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel9.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel9.UsedTheme = null;
            // 
            // cbTheme
            // 
            this.cbTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(210)))), ((int)(((byte)(213)))));
            this.cbTheme.ClickEffect = MinimalLibrary.ClickEffect.Ink;
            this.cbTheme.DefaultText = "Light";
            this.cbTheme.DisplaySearch = false;
            this.cbTheme.Location = new System.Drawing.Point(12, 38);
            this.cbTheme.Margin = new System.Windows.Forms.Padding(6);
            this.cbTheme.Name = "cbTheme";
            this.cbTheme.Padding = new System.Windows.Forms.Padding(1);
            this.cbTheme.Size = new System.Drawing.Size(518, 37);
            this.cbTheme.TabIndex = 38;
            this.cbTheme.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.cbTheme.UsedTheme = null;
            this.cbTheme.SelectedItemChanged += new System.ComponentModel.PropertyChangedEventHandler(this.ThemeChanged);
            // 
            // mLabel10
            // 
            this.mLabel10.AutoSize = true;
            this.mLabel10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mLabel10.Location = new System.Drawing.Point(9, 12);
            this.mLabel10.Name = "mLabel10";
            this.mLabel10.Size = new System.Drawing.Size(132, 20);
            this.mLabel10.TabIndex = 37;
            this.mLabel10.Text = "Application theme";
            this.mLabel10.Tint = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.mLabel10.Type = MinimalLibrary.LabelType.Alternate;
            this.mLabel10.UsedTheme = null;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.sidePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Register";
            this.ShowIcon = false;
            this.Text = "Register";
            this.tabMain.ResumeLayout(false);
            this.tabRegister.ResumeLayout(false);
            this.tabRegister.PerformLayout();
            this.tabLogin.ResumeLayout(false);
            this.tabLogin.PerformLayout();
            this.tabSetup.ResumeLayout(false);
            this.tabSetup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MinimalLibrary.Controls.MBufferedPanel sidePanel;
        private System.Windows.Forms.Timer main;
        private MinimalLibrary.Controls.MStackPanel tabMain;
        private System.Windows.Forms.TabPage tabRegister;
        private MinimalLibrary.Controls.MLabel lblErrorMessage;
        private MinimalLibrary.Controls.MButton btnSignUp;
        private MinimalLibrary.Controls.MLabel mLabel6;
        private MinimalLibrary.Controls.MTextBox tbPasswordTwo;
        private MinimalLibrary.Controls.MLabel mLabel5;
        private MinimalLibrary.Controls.MTextBox tbPasswordOne;
        private MinimalLibrary.Controls.MLabel mLabel4;
        private MinimalLibrary.Controls.MComboBox cbCountry;
        private MinimalLibrary.Controls.MLabel mLabel3;
        private MinimalLibrary.Controls.MLabel mLabel2;
        private MinimalLibrary.Controls.MTextBox tbLastname;
        private MinimalLibrary.Controls.MLabel mLabel1;
        private MinimalLibrary.Controls.MTextBox tbName;
        private System.Windows.Forms.TabPage tabLogin;
        private MinimalLibrary.Controls.MButton mButton2;
        private MinimalLibrary.Controls.MCheckBox chbConditions;
        private MinimalLibrary.Controls.MButton mButton1;
        private MinimalLibrary.Controls.MButton mButton3;
        private MinimalLibrary.Controls.MLabel mLabel7;
        private MinimalLibrary.Controls.MTextBox tbPasswordLogin;
        private MinimalLibrary.Controls.MLabel mLabel8;
        private MinimalLibrary.Controls.MTextBox tbEmailLogin;
        private System.Windows.Forms.TabPage tabSetup;
        private MinimalLibrary.Controls.MLabel mLabel10;
        private MinimalLibrary.Controls.MComboBox cbTheme;
        private MinimalLibrary.Controls.MLabel mLabel9;
        private MinimalLibrary.Controls.MTrackbar trbRed;
        private MinimalLibrary.Controls.MLabel lblRedName;
        private MinimalLibrary.Controls.MLabel lblRed;
        private MinimalLibrary.Controls.MLabel lblBlue;
        private MinimalLibrary.Controls.MLabel lblBlueName;
        private MinimalLibrary.Controls.MTrackbar trbBlue;
        private MinimalLibrary.Controls.MLabel lblGreen;
        private MinimalLibrary.Controls.MLabel lblGreenName;
        private MinimalLibrary.Controls.MTrackbar trbGreen;
        private MinimalLibrary.Controls.MColorBox mColorBox1;
        private MinimalLibrary.Controls.MColorBox mColorBox2;
        private MinimalLibrary.Controls.MColorBox mColorBox7;
        private MinimalLibrary.Controls.MColorBox mColorBox8;
        private MinimalLibrary.Controls.MColorBox mColorBox5;
        private MinimalLibrary.Controls.MColorBox mColorBox6;
        private MinimalLibrary.Controls.MColorBox mColorBox3;
        private MinimalLibrary.Controls.MColorBox mColorBox4;
        private MinimalLibrary.Controls.MLabel mLabel11;
        private MinimalLibrary.Controls.MColorBox mColorBox9;
        private MinimalLibrary.Controls.MColorBox mColorBox10;
        private MinimalLibrary.Controls.MColorBox mColorBox11;
        private MinimalLibrary.Controls.MColorBox mColorBox12;
        private MinimalLibrary.Controls.MColorBox mColorBox13;
        private MinimalLibrary.Controls.MColorBox mColorBox14;
        private MinimalLibrary.Controls.MColorBox mColorBox15;
        private MinimalLibrary.Controls.MColorBox mColorBox16;
        private MinimalLibrary.Controls.MTextBox tbEmail;
        private MinimalLibrary.Controls.MButton Next;
        private MinimalLibrary.Controls.MLabel lblErrorMessageLogin;
    }
}