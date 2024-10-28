using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace DiscordRPC
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        bool süre;

        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool flag = Mouse.LeftButton == MouseButtonState.Pressed;
            bool flag2 = flag;
            if (flag2)
            {
                base.DragMove();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Minimized;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (clientid.Text == "" || ImageKey.Text == "")
            {
                MessageBox.Show("Metin Kutularını Boş Bırakmayınız!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (timer.IsChecked == true)
                {
                    this.handlers = default(DiscordRpc.EventHandlers);
                    DiscordRpc.Initialize(clientid.Text, ref this.handlers, true, null);
                    this.presence.state = AltBaslik.Text;
                    this.presence.details = UstBaslik.Text;
                    this.presence.largeImageKey = ImageKey.Text;
                    this.presence.smallImageKey = SmallImage.Text;
                    long startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    this.presence.startTimestamp = startTimestamp;
                    DiscordRpc.UpdatePresence(ref this.presence);
                    File.WriteAllText(appDataPath + @"\DiscordRPC\süre.txt", "1");
                }
                else
                {
                    File.WriteAllText(appDataPath + @"\DiscordRPC\süre.txt", "0");
                    string app = Process.GetCurrentProcess().MainModule.FileName;
                    Process.Start(app);
                    Application.Current.Shutdown();
                }
                File.WriteAllText(appDataPath + @"\DiscordRPC\clientid.txt", clientid.Text);
                File.WriteAllText(appDataPath + @"\DiscordRPC\altbaslik.txt", AltBaslik.Text);
                File.WriteAllText(appDataPath + @"\DiscordRPC\ustbaslik.txt", UstBaslik.Text);
                File.WriteAllText(appDataPath + @"\DiscordRPC\resimadı.txt", ImageKey.Text);
                File.WriteAllText(appDataPath + @"\DiscordRPC\ufakresim.txt", SmallImage.Text);
            }

        }

        private void NeronTechGames_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/nerontechgames");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(appDataPath + @"\DiscordRPC"))
            {
                Directory.CreateDirectory(appDataPath + @"\DiscordRPC");
                File.CreateText(appDataPath + @"\DiscordRPC\clientid.txt");
                File.CreateText(appDataPath + @"\DiscordRPC\altbaslik.txt");
                File.CreateText(appDataPath + @"\DiscordRPC\ustbaslik.txt");
                File.CreateText(appDataPath + @"\DiscordRPC\süre.txt");
                File.CreateText(appDataPath + @"\DiscordRPC\resimadı.txt");
                File.CreateText(appDataPath + @"\DiscordRPC\ufakresim.txt");
            }
            else
            {
                clientid.Text = File.ReadAllText(appDataPath + @"\DiscordRPC\clientid.txt");
                AltBaslik.Text = File.ReadAllText(appDataPath + @"\DiscordRPC\altbaslik.txt");
                UstBaslik.Text = File.ReadAllText(appDataPath + @"\DiscordRPC\ustbaslik.txt");
                string i = File.ReadAllText(appDataPath + @"\DiscordRPC\süre.txt");
                if (i == "1") { timer.IsChecked = true; }
                else { timer.IsChecked = false; }
                ImageKey.Text = File.ReadAllText(appDataPath + @"\DiscordRPC\resimadı.txt");
                SmallImage.Text = File.ReadAllText(appDataPath + @"\DiscordRPC\ufakresim.txt");

                if (timer.IsChecked == true)
                {
                    this.handlers = default(DiscordRpc.EventHandlers);
                    DiscordRpc.Initialize(clientid.Text, ref this.handlers, true, null);
                    this.presence.state = AltBaslik.Text;
                    this.presence.details = UstBaslik.Text;
                    this.presence.largeImageKey = ImageKey.Text;
                    this.presence.smallImageKey = SmallImage.Text;
                    long startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    this.presence.startTimestamp = startTimestamp;
                    DiscordRpc.UpdatePresence(ref this.presence);
                    File.WriteAllText(appDataPath + @"\DiscordRPC\süre.txt", "1");
                }
                else
                {
                    DiscordRpc.Shutdown();
                    this.handlers = default(DiscordRpc.EventHandlers);
                    DiscordRpc.Initialize(clientid.Text, ref this.handlers, true, null);
                    this.presence.state = AltBaslik.Text;
                    this.presence.details = UstBaslik.Text;
                    this.presence.largeImageKey = ImageKey.Text;
                    this.presence.smallImageKey = SmallImage.Text;
                    DiscordRpc.UpdatePresence(ref this.presence);
                    File.WriteAllText(appDataPath + @"\DiscordRPC\süre.txt", "0");
                }
            }
        }
    }
}
