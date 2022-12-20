﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Lesson
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"LESSON - {page.Title}";

            if (page is AuthPage)
            {
                ButtonBack.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonBack.Visibility = Visibility.Visible;
            }
        }
        private void ButtonBack_OnClick(object sender, NavigationEventArgs e)
        {
            if(MainFrame.CanGoBack) MainFrame.GoBack();
        }
    }
}
