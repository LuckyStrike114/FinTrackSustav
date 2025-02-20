using System;
using System.Media;
using System.Windows;
using FinTrackSustav.Models;

namespace FinTrackSustav
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Pokreni sound effect pri pokretanju
            PlaySound("C:\\Users\\Karlo\\source\\repos\\LuckyStrike114\\FinTrackSustav\\FinTrack\\startup.wav");

            // Inicijalizacija baze
            using (var context = new FinTrackContext())
            {
                context.Database.EnsureCreated();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Pokreni sound effect pri gašenju
            PlaySound("C:\\Users\\Karlo\\source\\repos\\LuckyStrike114\\FinTrackSustav\\FinTrack\\shutdown.wav");
            base.OnExit(e);
        }

        private void PlaySound(string soundFile)
        {
            try
            {
                var player = new SoundPlayer(soundFile);
                player.Play();
                System.Threading.Thread.Sleep(1200);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri reprodukciji zvuka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}