/*
 * Kristian Kerrigan
 * MainWindow.xaml.cs
 * This file is the code behind for the MainWindow.xaml
 */

using System;
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

namespace KKerrigan_Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<CategoryWithClues> Categories { get; set; }
        public CategoryWithClues SelectedCategory { get; set; }
        public int? SelectedValue { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Service.LoadSavedCategories();
            Categories = Service.GetRandomFilledCategories();
            SelectedCategory = Categories.First();

            this.DataContext = this;
            
        }

        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Used a SortedSet to deal with the issue of duplicate point values
            SortedSet<int?> points = new SortedSet<int?>();

            foreach(Clue clue in SelectedCategory.Clues)
            {
                points.Add(clue.Value);
            }

            lbQuestions.ItemsSource = null;
            lbPoints.ItemsSource = points;
        }

        private void lbPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortedSet<string> clues = new SortedSet<string>();

            foreach(Clue clue in SelectedCategory.Clues.Where(clue => clue.Value == SelectedValue))
            {
                clues.Add(clue.Question);
            }
            lbQuestions.ItemsSource = clues;
        }

        private void lbQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            string selectedQuestion = (string)lb.SelectedItem;

            if(selectedQuestion != null)
            {
                MessageBox.Show(selectedQuestion + "\n\nClick OK to see answer.", 
                                SelectedCategory.Title + " for $" + SelectedValue, 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Question);
                string answer = SelectedCategory.Clues.Where(clue => clue.Question.Equals(selectedQuestion)).First().Answer;
                MessageBox.Show("Answer: " + answer,
                                "Question Answer", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Information);
            }
        }
    }
}
