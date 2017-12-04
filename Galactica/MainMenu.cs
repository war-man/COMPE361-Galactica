﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Telerik.WinControls.UI;
using System.Linq;
using System.Windows.Forms;

namespace Galactica
{
    public partial class MainMenu : RadForm
    {
        public List<HighScore> HighScores;
        public BindingSource CurrBindingSource = new BindingSource();

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
                var currHighScore = new HighScore(columns[0],int.Parse(columns[1]),int.Parse(columns[2]));
                HighScores.Add(currHighScore);
            }

            //HighScores = HighScores.OrderByDescending((HighScore score1,HighScore score2) => score1.CompareTo(score2)).ToList();
            HighScores.Sort((score1, score2) => -1 * score1.CompareTo(score2)); //https://stackoverflow.com/questions/3062513/how-can-i-sort-generic-list-desc-and-asc

            foreach (var highScore in HighScores)
            {

                //ListViewItem item = new ListViewItem(new string[]{highScore.Name, highScore.Score.ToString(), highScore.LevelReached.ToString()});
                CurrBindingSource.Add(highScore);
                //Console.WriteLine(CurrBindingSource.);
            }

            
            HighScoreTable.DataSource = CurrBindingSource;

            //foreach (DataGridViewRow row in HighScoreTable.Rows)   //https://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview
            
            //{
            //    row.HeaderCell.Value = string.Format($"{row.Index + 1}");
            //}
            //foreach (DataGridViewRow row in HighScoreTable.Rows     //https://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview
            //) 
            //{
            //    row.HeaderCell.Value = string.Format($"{row.Index + 1}");
            //}
            //HighScoreTable.
            //HighScoreTable.Sort(Score)
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            using (var game = new Game1())
            {

                ((System.Windows.Forms.Form)FromHandle(game.Window.Handle)).Icon = new System.Drawing.Icon("Content\\Graphics\\PlayerShipIcon_002.ico");


                game.Run();
                


            }

            
            this.Show();
            //this.Focus();
            //ResetCursor();

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void HighScoreTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
