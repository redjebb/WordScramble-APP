namespace WordScramble;

partial class IndexForm
{
    private System.ComponentModel.IContainer components = null;
    private Label labelTitle;
    private Label labelAttempts;
    private Label labelAttemptsCount;
    private Label labelGuessedWords;
    private Label labelGuessedWordsCount;
    private Label labelScrambledWordText;
    private Label labelScrambledWord;
    private Label labelFailedAttempts;
    private TextBox textBoxInput;
    private TextBox textBoxFailedAttempts;
    private Button buttonCheck;
    private Button buttonSkip;
    
    private Label labelTimer;
    private Label labelScore;
    private Button buttonHint;
    private TextBox textBoxNewWord;
    private Button buttonAddWord;
    private Label labelAddWord;

    private ComboBox comboBoxCategory;
    private Label labelCategory;
    private Label labelDifficulty;
    private ListBox listBoxAchievements;
    private Label labelAchievements;
    private Button buttonToggleTheme;
    private Button buttonRestart;
    private Button buttonBonus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        labelTitle = new Label();
        labelAttempts = new Label();
        labelAttemptsCount = new Label();
        labelGuessedWords = new Label();
        labelGuessedWordsCount = new Label();
        labelScrambledWordText = new Label();
        labelScrambledWord = new Label();
        labelFailedAttempts = new Label();
        textBoxInput = new TextBox();
        textBoxFailedAttempts = new TextBox();
        buttonCheck = new Button();
        buttonSkip = new Button();
        labelTimer = new Label();
        labelScore = new Label();
        buttonHint = new Button();
        textBoxNewWord = new TextBox();
        buttonAddWord = new Button();
        labelAddWord = new Label();
        comboBoxCategory = new ComboBox();
        labelCategory = new Label();
        labelDifficulty = new Label();
        listBoxAchievements = new ListBox();
        labelAchievements = new Label();
        buttonToggleTheme = new Button();
        buttonRestart = new Button();
        buttonBonus = new Button();
        SuspendLayout();
        
        // Timer
        labelTimer.AutoSize = true;
        labelTimer.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        labelTimer.ForeColor = Color.Crimson;
        labelTimer.Location = new Point(25, 25);
        labelTimer.Size = new Size(100, 25);
        labelTimer.Text = "Време: 30";
        
        // Score
        labelScore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        labelScore.AutoSize = true;
        labelScore.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        labelScore.ForeColor = Color.ForestGreen;
        labelScore.Location = new Point(640, 25);
        labelScore.Size = new Size(95, 25);
        labelScore.Text = "Точки: 0";

        // Title
        labelTitle.Anchor = AnchorStyles.Top;
        labelTitle.AutoSize = true;
        labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        labelTitle.Location = new Point(260, 15);
        labelTitle.Size = new Size(245, 45);
        labelTitle.Text = "Word Scramble";
        
        // Button Toggle Theme (Dark Mode)
        buttonToggleTheme.Location = new Point(25, 65);
        buttonToggleTheme.Size = new Size(110, 30);
        buttonToggleTheme.Text = "🌙 Dark Mode";
        buttonToggleTheme.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        buttonToggleTheme.Click += ButtonToggleThemeClick;

        // Button Restart
        buttonRestart.Location = new Point(145, 65);
        buttonRestart.Size = new Size(110, 30);
        buttonRestart.Text = "🔄 Рестарт";
        buttonRestart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        buttonRestart.BackColor = Color.LightBlue;
        buttonRestart.Click += ButtonRestartClick;

        // Button Bonus Wheel
        buttonBonus.Location = new Point(265, 65);
        buttonBonus.Size = new Size(120, 30);
        buttonBonus.Text = "🎁 Бонус Колело";
        buttonBonus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        buttonBonus.BackColor = Color.Gold;
        buttonBonus.Click += ButtonBonusClick;

        // Category Selection
        labelCategory.Location = new Point(510, 65);
        labelCategory.Size = new Size(90, 25);
        labelCategory.Text = "Категория:";
        labelCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

        comboBoxCategory.Location = new Point(605, 62);
        comboBoxCategory.Size = new Size(130, 25);
        comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBoxCategory.Items.AddRange(new object[] { "Всички (All)", "Животни/Природа", "Държави/Градове", "Общи (General)" });
        comboBoxCategory.SelectedIndex = 0;
        comboBoxCategory.SelectedIndexChanged += ComboBoxCategorySelectedIndexChanged;

        // Difficulty Label
        labelDifficulty.Anchor = AnchorStyles.Top;
        labelDifficulty.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
        labelDifficulty.ForeColor = Color.DarkOrange;
        labelDifficulty.Location = new Point(45, 145);
        labelDifficulty.Size = new Size(690, 25);
        labelDifficulty.Text = "Трудност: Начисляване...";
        labelDifficulty.TextAlign = ContentAlignment.MiddleCenter;

        // Attempts & Guessed
        labelAttempts.Location = new Point(45, 110);
        labelAttempts.Size = new Size(79, 21);
        labelAttempts.Text = "Attempts:";
        labelAttempts.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        
        labelAttemptsCount.Location = new Point(130, 110);
        labelAttemptsCount.Size = new Size(19, 21);
        labelAttemptsCount.Text = "0";
        labelAttemptsCount.Font = new Font("Segoe UI", 11F);
        
        labelGuessedWords.Location = new Point(220, 110);
        labelGuessedWords.Size = new Size(129, 21);
        labelGuessedWords.Text = "Guessed:";
        labelGuessedWords.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        
        labelGuessedWordsCount.Location = new Point(305, 110);
        labelGuessedWordsCount.Size = new Size(19, 21);
        labelGuessedWordsCount.Text = "0";
        labelGuessedWordsCount.Font = new Font("Segoe UI", 11F);

        // Scrambled Word Display
        labelScrambledWord.Anchor = AnchorStyles.Top;
        labelScrambledWord.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
        labelScrambledWord.ForeColor = Color.DarkBlue;
        labelScrambledWord.Location = new Point(45, 175);
        labelScrambledWord.Size = new Size(690, 65);
        labelScrambledWord.Text = "word";
        labelScrambledWord.TextAlign = ContentAlignment.MiddleCenter;
        
        // TextBox Input
        textBoxInput.Font = new Font("Segoe UI", 14F);
        textBoxInput.Location = new Point(45, 260);
        textBoxInput.PlaceholderText = "Въведи 5-буквената дума...";
        textBoxInput.Size = new Size(240, 32);
        
        // Buttons
        buttonCheck.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        buttonCheck.Location = new Point(295, 258);
        buttonCheck.Size = new Size(110, 36);
        buttonCheck.Text = "Провери";
        buttonCheck.Click += ButtonCheckClick;
        
        buttonHint.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        buttonHint.Location = new Point(415, 258);
        buttonHint.Size = new Size(110, 36);
        buttonHint.Text = "Подсказка";
        buttonHint.Click += ButtonHintClick;
        
        buttonSkip.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        buttonSkip.Location = new Point(535, 258);
        buttonSkip.Size = new Size(200, 36);
        buttonSkip.Text = "Пропусни думата";
        buttonSkip.Click += ButtonSkipClick;
        
        // History of failed attempts
        labelFailedAttempts.Location = new Point(45, 320);
        labelFailedAttempts.Size = new Size(150, 21);
        labelFailedAttempts.Text = "Сгрешени опити:";
        labelFailedAttempts.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        
        textBoxFailedAttempts.Location = new Point(45, 350);
        textBoxFailedAttempts.Multiline = true;
        textBoxFailedAttempts.ReadOnly = true;
        textBoxFailedAttempts.ScrollBars = ScrollBars.Vertical;
        textBoxFailedAttempts.Size = new Size(200, 150);
        textBoxFailedAttempts.Font = new Font("Segoe UI", 10F);

        // Achievements Panel
        labelAchievements.Location = new Point(270, 320);
        labelAchievements.Size = new Size(200, 21);
        labelAchievements.Text = "Постижения (Badges):";
        labelAchievements.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

        listBoxAchievements.Location = new Point(270, 350);
        listBoxAchievements.Size = new Size(220, 150);
        listBoxAchievements.Font = new Font("Segoe UI", 9F, FontStyle.Italic);

        // Add Word Section
        labelAddWord.Location = new Point(515, 320);
        labelAddWord.Size = new Size(150, 21);
        labelAddWord.Text = "Добави нова дума:";
        labelAddWord.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        labelAddWord.Name = "labelAddWord";

        textBoxNewWord.Location = new Point(515, 350);
        textBoxNewWord.Size = new Size(220, 29);
        textBoxNewWord.Font = new Font("Segoe UI", 11F);

        buttonAddWord.Location = new Point(515, 390);
        buttonAddWord.Size = new Size(220, 35);
        buttonAddWord.Text = "Запиши в words.txt";
        buttonAddWord.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        buttonAddWord.Click += ButtonAddWordClick;

        // Form setup
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(780, 535);
        Controls.Add(labelAddWord); Controls.Add(textBoxNewWord); Controls.Add(buttonAddWord);
        Controls.Add(buttonHint); Controls.Add(labelTimer); Controls.Add(labelScore);
        Controls.Add(textBoxFailedAttempts); Controls.Add(labelFailedAttempts);
        Controls.Add(buttonSkip); Controls.Add(buttonCheck); Controls.Add(textBoxInput);
        Controls.Add(labelScrambledWord); Controls.Add(labelTitle);
        Controls.Add(buttonToggleTheme); Controls.Add(buttonRestart); Controls.Add(buttonBonus);
        Controls.Add(labelCategory); Controls.Add(comboBoxCategory);
        Controls.Add(labelDifficulty); Controls.Add(labelAttempts); Controls.Add(labelAttemptsCount);
        Controls.Add(labelGuessedWords); Controls.Add(labelGuessedWordsCount);
        Controls.Add(listBoxAchievements); Controls.Add(labelAchievements);
        MinimumSize = new Size(796, 574);
        Name = "IndexForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Word Scramble – Ultimate Edition";
        Load += IndexFormLoad;
        ResumeLayout(false);
        PerformLayout();
    }
}