using RpsTournament.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RpsTournament.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<GameRound> rounds = new();
        private int roundNumber = 1;
        private int wins;
        private int losses;
        private int draws;
        public MainWindow()
        {
            InitializeComponent();
            RoundsDataGrid.ItemsSource = rounds;
            UpdateScore();
        }

        private void UpdateScore()
        {
            ScoreTextBlock.Text = $"Võidud: {wins} | Kaotused: {losses} | Viigid: {draws}";
        }

        private void ClearErrorIfValid()
        {
            if (PlayerNameTextBox.Text.Length >= 2 && PlayerMoveComboBox.SelectedItem != null)
            {
                StatusTextBlock.Text = "";
            }
        }

        private void PlayerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearErrorIfValid();
        }

        private void PlayerMoveComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearErrorIfValid();
        }

        private void PlayRoundButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "";

            if (PlayerNameTextBox.Text.Length < 2 || PlayerNameTextBox.Text.Length > 30)
            {
                StatusTextBlock.Text = "Mängija nimi peab olema 2-30 märki!";
                return;
            }

            if (PlayerMoveComboBox.SelectedItem == null)
            {
                StatusTextBlock.Text = "Palun vali käik!";
                return;
            }

        string moveText = ((ComboBoxItem)PlayerMoveComboBox.SelectedItem).Content.ToString();

            Move playerMove = Enum.Parse<Move>(moveText);

            Move computerMove = GameLogic.GetComputerMove();

            RoundResult result =
                GameLogic.GetResult(playerMove, computerMove);

            GameRound round = new GameRound
            {
                Number = roundNumber,
                PlayerMove = playerMove,
                ComputerMove = computerMove,
                Result = result
            };

            rounds.Add(round);

            if (result == RoundResult.Võit)
                wins++;
            else if (result == RoundResult.Kaotus)
                losses++;
            else
                draws++;

            UpdateScore();

            RoundsDataGrid.ItemsSource = null;
            RoundsDataGrid.ItemsSource = rounds;

            roundNumber++;

            if (roundNumber > 5)
            {
                PlayRoundButton.IsEnabled = false;

                if (wins > losses)
                    MessageBox.Show("Mängija võitis!");
                else if (losses > wins)
                    MessageBox.Show("Arvuti võitis!");
                else
                    MessageBox.Show("Viik!");
            }
        }

        private void NewTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            rounds.Clear();

            wins = 0;
            losses = 0;
            draws = 0;

            roundNumber = 1;

            UpdateScore();

            RoundsDataGrid.ItemsSource = null;
            RoundsDataGrid.ItemsSource = rounds;

            StatusTextBlock.Text = "Uus turniir alustatud!";

            PlayRoundButton.IsEnabled = true;
        }
    }
}