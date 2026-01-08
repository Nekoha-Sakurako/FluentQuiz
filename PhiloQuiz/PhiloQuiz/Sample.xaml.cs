using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PhiloQuiz;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class BlankPage10 : Page
{
    private readonly Random _rand = new();
    private List<SingleChoiceQuestion> _singleChoiceSource;
    private List<TrueFalseQuestion> _trueFalseSource;
    private List<SingleChoiceQuestion> _singleChoiceActive;
    private List<TrueFalseQuestion> _trueFalseActive;

    // store the last submitted answers so ApplyFilter can switch views without losing the original list
    private List<AnswerItemViewModel> _lastAnswerItems;

    public BlankPage10()
    {
        this.InitializeComponent();
        // load data before toggling any UI state that may raise events
        LoadData();
        BuildSets();

        // Set initial mode after data is ready. This will trigger Checked handler and render safely.
        if (Mode1Button != null)
        {
            Mode1Button.IsChecked = true;
        }
        else
        {
            // fallback to render explicitly if buttons aren't available for some reason
            RenderActiveSet();
        }
    }

    private void LoadData()
    {
        var data = Comp0.GetDefault();
        _singleChoiceSource = data.SingleChoice?.ToList();
        _trueFalseSource = data.TrueFalse?.ToList();
    }

    private List<T> Shuffle<T>(IEnumerable<T> items)
    {
        // make null-safe
        if (items == null) return new List<T>();
        var list = items.ToList();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rand.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return list;
    }

    private void BuildSets()
    {
        // Ensure active lists are always initialized to avoid null refs later
        _singleChoiceActive = _singleChoiceSource?.ToList() ?? new List<SingleChoiceQuestion>();
        _trueFalseActive = _trueFalseSource?.ToList() ?? new List<TrueFalseQuestion>();
    }

    private void RenderActiveSet()
    {
        // If data sources are not ready yet, try to ensure they are loaded.
        if (_singleChoiceSource == null || _trueFalseSource == null)
        {
            LoadData();
            BuildSets();
        }

        // Determine active mode
        bool randomMode = Mode2Button?.IsChecked == true;
        _singleChoiceActive = randomMode ? Shuffle(_singleChoiceSource) : _singleChoiceSource.ToList();
        _trueFalseActive = randomMode ? Shuffle(_trueFalseSource) : _trueFalseSource.ToList();

        QuestionsContainer.Children.Clear();

        // Single choice section
        var scTitle = new TextBlock { Text = $"单项选择题（共{_singleChoiceActive.Count}题）", FontSize = 16, FontWeight = FontWeights.SemiBold };
        QuestionsContainer.Children.Add(scTitle);

        foreach (var q in _singleChoiceActive)
        {
            var border = new Border { Background = new SolidColorBrush(Colors.LightGray) { Opacity = 0.08 }, Padding = new Thickness(10), CornerRadius = new CornerRadius(6) };
            var sp = new StackPanel { Spacing = 6 };
            sp.Children.Add(new TextBlock { Text = $"{q.Id}. {q.Question}", TextWrapping = TextWrapping.Wrap, FontWeight = FontWeights.Medium });

            foreach (var option in q.Options)
            {
                // option text like "A....." -> value = 'A'
                var rb = new RadioButton
                {
                    Content = option,
                    GroupName = $"Single_{q.Id}",
                    Tag = option.Length > 0 ? option[0].ToString() : option
                };
                rb.Checked += AnswerChanged_UpdateProgress;
                sp.Children.Add(rb);
            }

            border.Child = sp;
            QuestionsContainer.Children.Add(border);
        }

        // True/False section
        var tfTitle = new TextBlock { Text = $"判断题（共{_trueFalseActive.Count}题）", FontSize = 16, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 8, 0, 0) };
        QuestionsContainer.Children.Add(tfTitle);

        foreach (var q in _trueFalseActive)
        {
            var border = new Border { Background = new SolidColorBrush(Colors.LightGray) { Opacity = 0.08 }, Padding = new Thickness(10), CornerRadius = new CornerRadius(6) };
            var sp = new StackPanel { Spacing = 6 };
            sp.Children.Add(new TextBlock { Text = $"{q.Id}. {q.Question}", TextWrapping = TextWrapping.Wrap, FontWeight = FontWeights.Medium });

            var wrap = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
            var rbTrue = new RadioButton { Content = "正确", GroupName = $"TF_{q.Id}", Tag = true };
            var rbFalse = new RadioButton { Content = "错误", GroupName = $"TF_{q.Id}", Tag = false };
            rbTrue.Checked += AnswerChanged_UpdateProgress;
            rbFalse.Checked += AnswerChanged_UpdateProgress;
            wrap.Children.Add(rbTrue);
            wrap.Children.Add(rbFalse);
            sp.Children.Add(wrap);

            border.Child = sp;
            QuestionsContainer.Children.Add(border);
        }

        QuizProgress.Value = 0;
        ResultBorder.Visibility = Visibility.Collapsed;
        AnswerListView.ItemsSource = null;
        // clear stored answers when re-rendering
        _lastAnswerItems = null;
    }

    private void ModeButton_Checked(object sender, RoutedEventArgs e)
    {
        // ensure only one mode toggled
        if (sender == Mode1Button)
        {
            if (Mode2Button != null) Mode2Button.IsChecked = false;
        }
        else if (sender == Mode2Button)
        {
            if (Mode1Button != null) Mode1Button.IsChecked = false;
        }
        RenderActiveSet();
    }

    private void ModeButton_Unchecked(object sender, RoutedEventArgs e)
    {
        // if both unchecked, keep Mode1 true
        bool m1 = Mode1Button?.IsChecked == true;
        bool m2 = Mode2Button?.IsChecked == true;
        if (!m1 && !m2)
        {
            if (Mode1Button != null)
                Mode1Button.IsChecked = true;
            else if (Mode2Button != null)
                Mode2Button.IsChecked = true;
        }
    }

    private void AnswerChanged_UpdateProgress(object sender, RoutedEventArgs e)
    {
        int totalQuestions = _singleChoiceActive.Count + _trueFalseActive.Count;
        // count checked radio buttons grouped by question id
        int answered = 0;
        // single choice
        foreach (var q in _singleChoiceActive)
        {
            var group = QuestionsContainer.Children.OfType<Border>()
                .SelectMany(b => ((StackPanel)b.Child).Children.OfType<RadioButton>())
                .Where(rb => rb.GroupName == $"Single_{q.Id}");
            if (group.Any(rb => rb.IsChecked == true)) answered++;
        }
        // tf
        foreach (var q in _trueFalseActive)
        {
            var group = QuestionsContainer.Children.OfType<Border>()
                .SelectMany(b => ((StackPanel)b.Child).Children)
                .SelectMany(c => c is StackPanel sp ? sp.Children.OfType<RadioButton>() : Enumerable.Empty<RadioButton>())
                .Where(rb => rb.GroupName == $"TF_{q.Id}");
            if (group.Any(rb => rb.IsChecked == true)) answered++;
        }

        QuizProgress.Value = totalQuestions == 0 ? 0 : (answered * 100.0 / totalQuestions);
    }

    private void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        int score = 0;
        int correctCount = 0;
        int totalQuestions = _singleChoiceActive.Count + _trueFalseActive.Count;
        var answerItems = new List<AnswerItemViewModel>();
        // single choice scoring (3 points)
        foreach (var q in _singleChoiceActive)
        {
            string user = "";
            var rbs = FindRadioButtonsByGroup($"Single_{q.Id}");
            var selected = rbs.FirstOrDefault(rb => rb.IsChecked == true);
            if (selected != null) user = selected.Tag?.ToString() ?? "";
            bool isCorrect = string.Equals(user, q.Answer, StringComparison.OrdinalIgnoreCase);
            if (isCorrect) { score += 3; correctCount++; }
            answerItems.Add(new AnswerItemViewModel
            {
                Title = $"单选题 {q.Id}. {q.Question}",
                YourAnswer = string.IsNullOrEmpty(user) ? "未作答" : user,
                CorrectAnswer = q.Answer,
                IsCorrect = isCorrect
            });
        }

        // true/false scoring (4 points)
        foreach (var q in _trueFalseActive)
        {
            bool? user = null;
            var rbs = FindRadioButtonsByGroup($"TF_{q.Id}");
            var selected = rbs.FirstOrDefault(rb => rb.IsChecked == true);
            if (selected != null && selected.Tag is bool b) user = b;
            bool isCorrect = user.HasValue && user.Value == q.Answer;
            if (isCorrect) { score += 4; correctCount++; }
            answerItems.Add(new AnswerItemViewModel
            {
                Title = $"判断题 {q.Id}. {q.Question}",
                YourAnswer = user.HasValue ? (user.Value ? "正确" : "错误") : "未作答",
                CorrectAnswer = q.Answer ? "正确" : "错误",
                IsCorrect = isCorrect
            });
        }

        // store answers so filtering doesn't lose the original list
        _lastAnswerItems = answerItems;

        // display results
        ScoreValueText.Text = score.ToString();
        int maxScore = _singleChoiceActive.Count * 3 + _trueFalseActive.Count * 4;
        double accuracy = totalQuestions == 0 ? 0 : (correctCount * 100.0 / totalQuestions);
        if (accuracy >= 60)
        {
            ResultMessageText.Text = $"恭喜你！总分{score}分（满分{maxScore}分），答对{correctCount}题，正确率{accuracy:F1}%";
            ResultBorder.Background = new SolidColorBrush(Colors.LightGreen) { Opacity = 0.12 };
        }
        else
        {
            ResultMessageText.Text = $"继续努力！总分{score}分（满分{maxScore}分），答对{correctCount}题，正确率{accuracy:F1}%";
            ResultBorder.Background = new SolidColorBrush(Colors.Pink) { Opacity = 0.08 };
        }
        AppNotification notification = new AppNotificationBuilder()
.AddText("已提交答案！")
.AddText(ResultMessageText.Text)
.SetAudioEvent(AppNotificationSoundEvent.Default)
.SetTimeStamp(DateTime.Now)
.BuildNotification();

        AppNotificationManager.Default.Show(notification);
        AnswerListView.ItemsSource = answerItems;
        ResultBorder.Visibility = Visibility.Visible;

        // default filter = all
        FilterAllButton.IsChecked = true;
        ApplyFilter();
        //ResultBorder.BringIntoView();
    }

    private IEnumerable<RadioButton> FindRadioButtonsByGroup(string groupName)
    {
        var all = QuestionsContainer.Children.OfType<Border>()
            .SelectMany(b => ((StackPanel)b.Child).Children)
            .SelectMany(c =>
            {
                if (c is RadioButton rb) return new[] { rb };
                if (c is StackPanel sp) return sp.Children.OfType<RadioButton>();
                return Enumerable.Empty<RadioButton>();
            });
        return all.Where(rb => rb.GroupName == groupName);
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        // clear selections
        var allRbs = QuestionsContainer.Children.OfType<Border>()
            .SelectMany(b => ((StackPanel)b.Child).Children)
            .SelectMany(c =>
            {
                if (c is RadioButton rb) return new[] { rb };
                if (c is StackPanel sp) return sp.Children.OfType<RadioButton>();
                return Enumerable.Empty<RadioButton>();
            });

        foreach (var rb in allRbs) rb.IsChecked = false;

        QuizProgress.Value = 0;
        ResultBorder.Visibility = Visibility.Collapsed;
        AnswerListView.ItemsSource = null;
        // clear stored answers when resetting
        _lastAnswerItems = null;
    }

    private void FilterButton_Checked(object sender, RoutedEventArgs e)
    {
        // ensure only one active filter
        if (sender == FilterAllButton)
        {
            if (FilterIncorrectButton != null) FilterIncorrectButton.IsChecked = false;
        }
        else if (sender == FilterIncorrectButton)
        {
            if (FilterAllButton != null) FilterAllButton.IsChecked = false;
        }
        ApplyFilter();
    }

    private void FilterButton_Unchecked(object sender, RoutedEventArgs e)
    {
        // keep at least one filter active
        bool a = FilterAllButton?.IsChecked == true;
        bool b = FilterIncorrectButton?.IsChecked == true;
        if (!a && !b)
        {
            if (FilterAllButton != null)
                FilterAllButton.IsChecked = true;
            else if (FilterIncorrectButton != null)
                FilterIncorrectButton.IsChecked = true;
        }
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        if (_lastAnswerItems == null) return;

        if (FilterIncorrectButton.IsChecked == true)
        {
            AnswerListView.ItemsSource = _lastAnswerItems.Where(i => !i.IsCorrect).ToList();
        }
        else
        {
            AnswerListView.ItemsSource = _lastAnswerItems.ToList();
        }
    }

    // Small VM for list display
    private class AnswerItemViewModel
    {
        public string Title { get; set; }
        public string YourAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public override string ToString()
        {
            return $"{Title}\n你的答案: {YourAnswer} | 正确答案: {CorrectAnswer} {(IsCorrect ? "✓" : "✗")}";
        }
    }
}
