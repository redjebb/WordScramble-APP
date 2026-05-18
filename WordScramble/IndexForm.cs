using System.IO;

namespace WordScramble;

public partial class IndexForm : Form
{
    private const string WordsTextFile = "words.txt";

    private readonly List<string> failedAttempts = new();
    private readonly List<string> allWordsFromFile = new(); 
    private readonly List<string> activeWordList = new();   
    private readonly List<string> unlockedBadges = new();   
    private readonly Random random = new();
    private readonly System.Windows.Forms.Timer roundTimer = new();

    private int attempts = 0;
    private int guessedWords = 0;
    private int consecutiveWins = 0; 
    private string currentWord = string.Empty;

    private int timeLeft = 30;
    private int score = 0;
    private int hintsShown = 0;
    private bool isDarkMode = false; 
    private bool hasUsedBonusThisRound = false; 

    private readonly HashSet<string> animalKeywords = new() { "tiger", "zebra", "snake", "camel", "shark", "whale", "koala", "lemur", "panda", "puppy", "mouse", "horse", "sheep" };
    private readonly HashSet<string> countryKeywords = new() { "spain", "italy", "japan", "china", "egypt", "india", "paris", "tokyo", "texas", "miami", "malta", "greece", "chile" };

    public IndexForm()
    {
        InitializeComponent();
        SetupTimer();
    }

    private void SetupTimer()
    {
        roundTimer.Interval = 1000;
        roundTimer.Tick += RoundTimerTick;
    }

    private void IndexFormLoad(object sender, EventArgs e)
    {
        GetAllWords();
        FilterWordsByCategory(); 
    }

    private void GetAllWords()
    {
        if (!File.Exists(WordsTextFile))
        {
            string[] defaultWords = { "tiger", "zebra", "snake", "spain", "italy", "japan", "smart", "coder", "apple", "world", "mouse", "house" };
            File.WriteAllLines(WordsTextFile, defaultWords);
        }

        allWordsFromFile.Clear();
        using StreamReader reader = new(WordsTextFile);
        while (!reader.EndOfStream)
        {
            string? word = reader.ReadLine();
            if (!string.IsNullOrWhiteSpace(word))
            {
                allWordsFromFile.Add(word.Trim().ToLower());
            }
        }
    }

    private void FilterWordsByCategory()
    {
        activeWordList.Clear();
        int selectedIndex = comboBoxCategory.SelectedIndex; 

        foreach (string word in allWordsFromFile)
        {
            bool isAnimal = animalKeywords.Contains(word);
            bool isCountry = countryKeywords.Contains(word);

            if (selectedIndex == 0) activeWordList.Add(word);
            else if (selectedIndex == 1 && isAnimal) activeWordList.Add(word);
            else if (selectedIndex == 2 && isCountry) activeWordList.Add(word);
            else if (selectedIndex == 3 && !isAnimal && !isCountry) activeWordList.Add(word);
        }

        GenerateNewWord();
    }

    private void GenerateNewWord()
    {
        if (activeWordList.Count == 0)
        {
            roundTimer.Stop();
            labelScrambledWord.Text = "Няма думи";
            ToggleGameControls(false);
            MessageBox.Show($"Няма повече налични думи в тази категория. Общ резултат: {score} точки!", "Край", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        ToggleGameControls(true);
        int randomIndex = random.Next(activeWordList.Count);
        currentWord = activeWordList[randomIndex];
        
        int uniqueLettersCount = currentWord.Distinct().Count();
        string difficulty = "Easy";
        labelDifficulty.ForeColor = Color.DarkOrange;

        if (uniqueLettersCount < 5) 
        {
            difficulty = "Medium";
            labelDifficulty.ForeColor = Color.BlueViolet;
        }
        
        if (currentWord.Contains('x') || currentWord.Contains('z') || currentWord.Contains('q') || currentWord.Contains('j'))
        {
            difficulty = "Hard";
            labelDifficulty.ForeColor = Color.Crimson;
        }
        
        labelDifficulty.Text = $"Ниво на трудност: {difficulty} (Категоризирана дума)";
        ResetGameInfo();
    }

    private void ToggleGameControls(bool enabled)
    {
        buttonCheck.Enabled = enabled;
        buttonSkip.Enabled = enabled;
        buttonHint.Enabled = enabled;
        buttonBonus.Enabled = enabled;
        textBoxInput.Enabled = enabled;
    }

    private void ResetGameInfo()
    {
        attempts = 0;
        hintsShown = 0;
        hasUsedBonusThisRound = false; 
        failedAttempts.Clear();
        textBoxInput.Clear();
        textBoxFailedAttempts.Clear();
        labelScrambledWord.Text = ScrambleWord(currentWord);

        timeLeft = 30;
        UpdateUIStats();
        roundTimer.Start();
        textBoxInput.Focus();
    }

    private string ScrambleWord(string word)
    {
        char[] letters = word.ToCharArray();
        for (int i = letters.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);
            (letters[i], letters[randomIndex]) = (letters[randomIndex], letters[i]);
        }
        string scrambledWord = new(letters);
        if (scrambledWord == word && word.Length > 1) return ScrambleWord(word);
        return scrambledWord;
    }

    private void RoundTimerTick(object? sender, EventArgs e)
    {
        timeLeft--;
        UpdateUIStats();

        if (timeLeft <= 0)
        {
            roundTimer.Stop();
            consecutiveWins = 0; 
            MessageBox.Show($"Времето свърши! Думата беше: {currentWord.ToUpper()}", "Край на времето", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            GenerateNewWord();
            UpdateLabels();
        }
    }

    private void UpdateUIStats()
    {
        labelTimer.Text = $"Време: {timeLeft} сек.";
        labelScore.Text = $"Точки: {score}";
    }

    private void ButtonCheckClick(object sender, EventArgs e)
    {
        string input = textBoxInput.Text.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(input))
        {
            MessageBox.Show("Моля, въведете дума.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (input == currentWord)
        {
            SuccessfulAttempt();
        }
        else
        {
            UnsuccessfulAttempt(input);
        }
        UpdateLabels();
    }

    private void SuccessfulAttempt()
    {
        roundTimer.Stop();
        guessedWords++;
        consecutiveWins++;

        int multiplier = labelDifficulty.Text.Contains("Hard") ? 3 : (labelDifficulty.Text.Contains("Medium") ? 2 : 1);
        int pointsEarned = currentWord.Length * timeLeft * multiplier;
        score += pointsEarned;

        CheckForAchievements(); 

        activeWordList.Remove(currentWord);
        MessageBox.Show($"Вярно! Спечели {pointsEarned} точки! (Бонус за трудност: x{multiplier})", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        GenerateNewWord();
    }

    private void UnsuccessfulAttempt(string input)
    {
        attempts++;
        consecutiveWins = 0; 
        failedAttempts.Add(input);
        score = Math.Max(0, score - 5);
        UpdateUIStats();

        if (attempts > 9)
        {
            roundTimer.Stop();
            MessageBox.Show($"Твърде много грешки! Вярната дума беше: {currentWord.ToUpper()}", "Нова дума", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GenerateNewWord();
        }
    }

    private void CheckForAchievements()
    {
        if (timeLeft >= 25 && !unlockedBadges.Contains("⚡ Бързак (Позната под 5 сек.)"))
        {
            unlockedBadges.Add("⚡ Бързак (Позната под 5 сек.)");
            listBoxAchievements.Items.Add("⚡ Бързак (Позната под 5 сек.)");
        }
        if (consecutiveWins == 3 && !unlockedBadges.Contains("🔥 В Серия (3 верни подред)"))
        {
            unlockedBadges.Add("🔥 В Серия (3 верни подред)");
            listBoxAchievements.Items.Add("🔥 В Серия (3 верни подред)");
        }
        if (labelDifficulty.Text.Contains("Hard") && !unlockedBadges.Contains("🧠 Експерт (Позната Hard дума)"))
        {
            unlockedBadges.Add("🧠 Експерт (Позната Hard дума)");
            listBoxAchievements.Items.Add("🧠 Експерт (Позната Hard дума)");
        }
    }

    private void ButtonHintClick(object sender, EventArgs e)
    {
        if (hintsShown < currentWord.Length - 1)
        {
            hintsShown++;
            string hintText = currentWord.Substring(0, hintsShown);
            score = Math.Max(0, score - 15); 
            UpdateUIStats();
            MessageBox.Show($"Подсказка (първи {hintsShown} букви): {hintText.ToUpper()}", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Остава само една буква, не може повече!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void ButtonSkipClick(object sender, EventArgs e)
    {
        roundTimer.Stop();
        consecutiveWins = 0;
        score = Math.Max(0, score - 20); 
        GenerateNewWord();
        UpdateLabels();
    }

    private void ButtonAddWordClick(object sender, EventArgs e)
    {
        string newWord = textBoxNewWord.Text.Trim().ToLower();
        
        if (string.IsNullOrWhiteSpace(newWord) || newWord.Length != 5)
        {
            MessageBox.Show("Думата трябва да е ТОЧНО с 5 букви!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            File.AppendAllText(WordsTextFile, Environment.NewLine + newWord);
            MessageBox.Show($"Успешно добавихте '{newWord}' в речника words.txt!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxNewWord.Clear();
            
            GetAllWords();
            FilterWordsByCategory();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Грешка при запис: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ComboBoxCategorySelectedIndexChanged(object sender, EventArgs e)
    {
        FilterWordsByCategory();
    }

    private void ButtonRestartClick(object sender, EventArgs e)
    {
        roundTimer.Stop();
        var confirmResult = MessageBox.Show("Сигурни ли сте, че искате да рестартирате играта? Всички точки ще бъдат нулирани.", "Рестарт", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        
        if (confirmResult == DialogResult.Yes)
        {
            score = 0;
            guessedWords = 0;
            consecutiveWins = 0;
            unlockedBadges.Clear();
            listBoxAchievements.Items.Clear();
            
            GetAllWords();
            FilterWordsByCategory();
            UpdateLabels();
        }
        else
        {
            roundTimer.Start();
        }
    }

    private void ButtonBonusClick(object sender, EventArgs e)
    {
        if (hasUsedBonusThisRound)
        {
            MessageBox.Show("Вече използвахте бонуса си за тази дума!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        hasUsedBonusThisRound = true;
        int chance = random.Next(1, 5); 
        
        switch (chance)
        {
            case 1:
                score += 50;
                MessageBox.Show("🎁 КЪСМЕТ! Спечелихте +50 бонус точки!", "Колело на Късмета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            case 2:
                timeLeft += 15;
                MessageBox.Show("⏳ ВРЕМЕ! Получавате +15 допълнителни секунди!", "Колело на Късмета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            case 3:
                score = Math.Max(0, score - 30);
                MessageBox.Show("💥 КАРЪК! Губите 30 точки.", "Колело на Късмета", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                break;
            case 4:
                if (hintsShown < currentWord.Length - 1) hintsShown++;
                string hintText = currentWord.Substring(0, hintsShown);
                MessageBox.Show($"🔍 БЕЗПЛАТНА ПОДСКАЗКА: {hintText.ToUpper()}", "Колело на Късмета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
        }
        UpdateUIStats();
    }

    private void ButtonToggleThemeClick(object sender, EventArgs e)
    {
        isDarkMode = !isDarkMode;

        if (isDarkMode)
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            buttonToggleTheme.Text = "☀️ Light Mode";
            SetControlsColor(this.Controls, Color.White, Color.FromArgb(45, 45, 48));
            labelScrambledWord.ForeColor = Color.Cyan;
        }
        else
        {
            this.BackColor = Color.WhiteSmoke;
            buttonToggleTheme.Text = "🌙 Dark Mode";
            SetControlsColor(this.Controls, Color.Black, Color.White);
            labelScrambledWord.ForeColor = Color.DarkBlue;
        }
    }

    private void SetControlsColor(Control.ControlCollection controls, Color textColor, Color backColor)
    {
        foreach (Control c in controls)
        {
            if (c is Label || c is ListBox) c.ForeColor = textColor;
            if (c is TextBox || c is ComboBox || c is ListBox) c.BackColor = backColor;
            if (c is TextBox) c.ForeColor = textColor;
            if (c is Button btn)
            {
                if (!isDarkMode && btn == buttonRestart) btn.BackColor = Color.LightBlue;
                else if (!isDarkMode && btn == buttonBonus) btn.BackColor = Color.Gold;
                else
                {
                    btn.BackColor = backColor;
                    btn.ForeColor = textColor;
                }
            }
        }
    }

    private void UpdateLabels()
    {
        labelAttemptsCount.Text = attempts.ToString();
        labelGuessedWordsCount.Text = guessedWords.ToString();
        textBoxFailedAttempts.Text = string.Join(Environment.NewLine, failedAttempts);
        textBoxInput.Clear();
        textBoxInput.Focus();
    }
}