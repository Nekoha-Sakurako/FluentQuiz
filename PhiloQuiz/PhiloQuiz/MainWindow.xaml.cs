using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PhiloQuiz
{
    public sealed partial class MainWindow : Window
    {
        private readonly Random _rand = new();
        private List<SingleChoiceQuestion> _singleChoiceSource;
        private List<TrueFalseQuestion> _trueFalseSource;
        private List<SingleChoiceQuestion> _singleChoiceActive;
        private List<TrueFalseQuestion> _trueFalseActive;
        // 新增：存储设置的变量（可持久化到本地，这里先存内存）
        private bool _isRandomAnswer = false; // 是否随机作答
        private bool _isTimerEnabled = true;  // 是否开启计时
        private int _timerSeconds = 60;       // 单题限时秒数
        private string _theme = "浅色主题";    // 界面主题

        // store the last submitted answers so ApplyFilter can switch views without losing the original list
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true; // Extend the content into the title bar and hide the default titlebar
            this.SetTitleBar(titleBar); // Set the custom title bar
            navView.SelectedItem = navView.MenuItems[0];
            // 初始导航到BlankPage1
            navFrame.Navigate(typeof(BlankPage));
        }
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var navItem = args.InvokedItemContainer as NavigationViewItem;
            if (navItem == null || navItem.Tag == null) return;
            if (args.IsSettingsInvoked)
            {
                // 打开设置弹窗（复用之前写的OpenSettingsDialog方法）
                OpenSettingsDialog(null, null);
                // 阻止选中状态变化（保持原导航项选中）
                return;
            }
            string tag = navItem.Tag.ToString().Trim();
            System.Diagnostics.Debug.WriteLine("当前Tag：" + tag); // 输出Tag值，确认和预期一致
            Type targetPage = tag switch
            {
                "PhiloPage0" => typeof(BlankPage),
                "CompPage0" => typeof(BlankPage10),
                _ => null
            };

            if (targetPage != null)
            {
                // 强制导航（忽略重复）
                navFrame.Navigate(targetPage);
                // 手动设置选中状态（避免UI不同步）
                sender.SelectedItem = navItem;
            }
        }
        private void NavView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (navFrame.CanGoBack)
            {
                navFrame.GoBack();
                e.Handled = true; // 标记事件已处理
            }
        }
        // 打开设置弹窗（绑定到你的设置按钮Click事件）
        private void OpenSettingsDialog(object sender, RoutedEventArgs e)
        {
            // 弹窗打开前，回显当前设置
            rbSequential.IsChecked = !_isRandomAnswer;
            rbRandom.IsChecked = _isRandomAnswer;
            tsTimer.IsOn = _isTimerEnabled;
            tbTimerSeconds.Text = _timerSeconds.ToString();

            // 回显主题选择
            switch (_theme)
            {
                case "浅色主题": cbTheme.SelectedIndex = 0; break;
                case "深色主题": cbTheme.SelectedIndex = 1; break;
                case "跟随系统": cbTheme.SelectedIndex = 2; break;
            }

            // 显示弹窗（WinUI3的ContentDialog需要指定XamlRoot）
            SettingsDialog.XamlRoot = this.Content.XamlRoot;
            _ = SettingsDialog.ShowAsync();
        }
        // 保存设置按钮点击
        private void SettingsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // 保存答题模式设置
            _isRandomAnswer = rbRandom.IsChecked == true;

            // 保存计时设置（做数值校验）
            _isTimerEnabled = tsTimer.IsOn;
            if (int.TryParse(tbTimerSeconds.Text, out int seconds) && seconds > 0 && seconds <= 300)
            {
                _timerSeconds = seconds;
            }
            else
            {
                // 输入无效时提示并使用默认值
                args.Cancel = true; // 阻止弹窗关闭
                _ = new ContentDialog
                {
                    Title = "输入错误",
                    Content = "单题限时请输入1-300之间的数字！",
                    CloseButtonText = "确定",
                    XamlRoot = this.Content.XamlRoot
                }.ShowAsync();
                return;
            }

            // 保存主题设置
            _theme = (cbTheme.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "浅色主题";

            // 应用主题（简单示例，可扩展完整主题切换）
            //ApplyTheme();

            // 提示设置保存成功
            _ = new ContentDialog
            {
                Title = "设置成功",
                Content = "您的设置已保存并生效！",
                CloseButtonText = "确定",
                XamlRoot = this.Content.XamlRoot
            }.ShowAsync();
        }

        // 取消按钮点击（清空输入错误提示）
        private void SettingsDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // 弹窗关闭时重置输入框（可选）
            if (!int.TryParse(tbTimerSeconds.Text, out _))
            {
                tbTimerSeconds.Text = _timerSeconds.ToString();
            }
        }

        // 重置分数按钮点击
        private void BtnResetScore_Click(object sender, RoutedEventArgs e)
        {
            // 弹出确认框
            _ = new ContentDialog
            {
                Title = "确认重置",
                Content = "确定要重置所有答题分数吗？此操作不可恢复！",
                PrimaryButtonText = "确定",
                CloseButtonText = "取消",
                XamlRoot = this.Content.XamlRoot
            }.ShowAsync();//.ContinueWith(t =>
            {
               // if (Result == ContentDialogResult.Primary)
              //  {
              //      // 这里添加重置分数的逻辑（如清空本地存储的分数数据）
               //     DispatcherQueue.TryEnqueue(() =>
               //     {
              //          _ = new ContentDialog
               //         {
               //             Title = "重置成功",
                //            Content = "所有分数已重置！",
                //            CloseButtonText = "确定",
                //            XamlRoot = this.Content.XamlRoot
                 //       }.ShowAsync();
                 //   });
                }
            }
        }

        // 应用主题的辅助方法（简单示例）
        //private void ApplyTheme()
        //{
         //   switch (_theme)
          //  {
           //     case "浅色主题":
            //        this.RequestedTheme = ElementTheme.Light;
           //         break;
             //   case "深色主题":
              //      this.RequestedTheme = ElementTheme.Dark;
              //      break;
              //  case "跟随系统":
                //    this.RequestedTheme = ElementTheme.Default;
                //    break;
            //}
        }

    
