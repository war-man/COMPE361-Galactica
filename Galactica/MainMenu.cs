﻿// Kevin Belew
// 818366010
// 12/8/17
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Telerik.WinControls.UI;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Galactica
{
    public partial class MainMenu : RadForm
    {
        public HighScore currHighScore;
        public List<HighScore> HighScores;
        public BindingSource CurrBindingSource = new BindingSource();
        public bool DeveloperModeEnabled = false;
        public MainMenu()
        {
            InitializeComponent();

            HighScores = new List<HighScore>();
            
            string[] lines;

            try
            {
                lines = File.ReadAllLines("Content\\Assets\\HighScores.csv");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            foreach (var line in lines)
            {
                if (line.Equals("Player,Score,LevelReached"))
                {
                    continue;
                }
                var columns = line.Split(',');
                var currentHighScore = new HighScore(columns[0],int.Parse(columns[1]),int.Parse(columns[2]));
                HighScores.Add(currentHighScore);
            }

            //HighScores = HighScores.OrderByDescending((HighScore score1,HighScore score2) => score1.CompareTo(score2)).ToList();
            HighScores.Sort((score1, score2) => -1 * score1.CompareTo(score2)); //https://stackoverflow.com/questions/3062513/how-can-i-sort-generic-list-desc-and-asc

            foreach (var highScore in HighScores)
            {

                
                CurrBindingSource.Add(highScore);
                
            }

            
            HighScoreTable.DataSource = CurrBindingSource;

            
           
            
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            
            this.Hide();

            if (!DeveloperModeEnabled)
            {

                using (var game = new GalagaGame())
                {
                    ((System.Windows.Forms.Form) FromHandle(game.Window.Handle)).Icon =
                        new System.Drawing.Icon("Content\\Graphics\\PlayerShipIcon_002.ico");
                    game.Run();
                    newHighScoreMenu1.Show();
                    currHighScore = new HighScore("NA", game.playerScore, game.playerShip.PlayerLevel);
                }
            }
            else
            {
                using (var devGame = new GalagaGame(true))
                {
                    ((System.Windows.Forms.Form)FromHandle(devGame.Window.Handle)).Icon =
                        new System.Drawing.Icon("Content\\Graphics\\PlayerShipIcon_002.ico");
                    devGame.Run();
                    
                }
            }


            this.Show();
           

        }

        private void Title_Click(object sender, System.EventArgs e)
        {

        }

        private void OptionsButton_Click(object sender, System.EventArgs e)
        {
            
            optionsMenu1.Show();
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void HighScoreTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            helpMenu1.Show();
        }

        

        public void UpdateHighScore()
        {
            if (newHighScoreMenu1.NewHighScoreTextBox.Text.Equals(""))
            {
                return;
            }
            currHighScore.Name = newHighScoreMenu1.NewHighScoreTextBox.Text.ToUpper();
            currHighScore.WriteToCsv(".\\Content\\Assets\\HighScores.csv");
            newHighScoreMenu1.Hide();
            HighScores.Add(currHighScore);
            CurrBindingSource.Add(currHighScore);
            HighScores.Sort((score1, score2) => -1 * score1.CompareTo(score2));
            CurrBindingSource.Clear();
            foreach (var highScore in HighScores)
            {

                
                CurrBindingSource.Add(highScore);
                
            }

        }

        private void newHighScoreMenu1_Load(object sender, EventArgs e)
        {
            newHighScoreMenu1.ParentMainMenu = this;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            

        }

        private void helpMenu1_Load(object sender, EventArgs e)
        {

        }

        private void optionsMenu1_Load(object sender, EventArgs e)
        {
            optionsMenu1.MainMenuParent = this;
        }
    }
}
