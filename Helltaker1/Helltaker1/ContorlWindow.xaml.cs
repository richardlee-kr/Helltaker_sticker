using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WMPLib;

namespace Helltaker1
{
    public partial class ContorlWindow : Window
    {
        public int frame_override = -1;
        float fps = 3 / 200f;
        float speed_override;

        private bool isPlay;

        List<MainWindow> windowList = new List<MainWindow>();

        WindowsMediaPlayer wplayer = new WindowsMediaPlayer(); //WMP


        MainWindow openedWindow;
        DispatcherTimer timer;


        public ContorlWindow()
        {
            InitializeComponent();

            MouseRightButtonDown += ControlWindow_RightClick;
            
            /*Frame Timer*/
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(fps);
            timer.Tick += NextFrame;
            timer.Start();

            wplayer.settings.setMode("loop", true);
            wplayer.URL = "Resources/Helltaker.mp3";
            isPlay = false;


            wplayer.controls.stop();
        }

        private void ControlWindow_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                isPlay = !isPlay;
                MusicPlayer();
            }
        }

       private void ShowWindow()
        {
            openedWindow = new MainWindow();
            openedWindow.Show();
            windowList.Add(openedWindow);
        }

        private void NextFrame(object sender, EventArgs e)
        {
            if (this.openedWindow != null)
            {
                frame_override = (frame_override + 1) % this.openedWindow.frame_sheet;
                for (int i = 0; i < windowList.Count; i++)
                {
                    //windowList[i].frame = frame_override;
                    windowList[i].Sticker.Source = windowList[i].imgFrame[frame_override];
                }

                //Console.WriteLine(speed_override);
            }
            timer.Interval = TimeSpan.FromSeconds(fps * speed_override);
        }

        #region Character Click Event
        private void Azazel_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Azazel();
        }
        private void Cerberus_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Cerberus();
        }
        private void Judgement_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Judgement();
        }
        private void Justice_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Justice();
        }
        private void Lucifer_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Lucifer();
        }
        private void Lucifer_Apron_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Lucifer_Apron();
        }
        private void Malina_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Malina();
        }
        private void Modeus_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Modeus();
        }
        private void Pandemonica_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Pandemonica();
        }
        private void Zdrada_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Zdrada();
        }
        private void Glorious_left_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Glorious_left();
        }
        private void Glorious_right_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Glorious_right();
        }
        #endregion
        
        /*Silder*/
        private void Slider_Volume(object sender, RoutedEventArgs e)
        {
            wplayer.settings.volume = (int)musicVolume.Value;
            VolumeText.Text = musicVolume.Value.ToString();
        }
        private void Slider_Speed(object sender, RoutedEventArgs e)
        {
            wplayer.settings.rate = musicSpeed.Value;
            SpeedText.Text = musicSpeed.Value.ToString();
        }
        private void Slider_frameSpeed(object sender, RoutedEventArgs e)
        {
            speed_override = 6 - (int)frameSpeed.Value;
            frameSpeedText.Text = frameSpeed.Value.ToString();
        }

        /*mp3 player*/
        private void MusicPlayer()
        {
            if (isPlay)
                Play();
            else
                Pause();
        }
        private void Play()
        {
            wplayer.controls.play();
        }
        private void Pause()
        {
            wplayer.controls.pause();
        }
    }
}
