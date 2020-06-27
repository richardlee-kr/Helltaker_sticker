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

            MouseDown += ControlWidnow_MouseDown;
            MouseLeftButtonDown += ControlWindow_LeftClick;
            MouseRightButtonDown += ControlWindow_RightClick;
            
            /*Frame Timer*/
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(fps * 3);
            timer.Tick += NextFrame;
            timer.Start();

            wplayer.settings.setMode("loop", true);
            wplayer.URL = "Resources/Helltaker.mp3";
            isPlay = false;

            this.Topmost = true;

            wplayer.controls.stop();
        }

        private void ControlWidnow_MouseDown(object o, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        private void ControlWindow_LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                ShowWindow();
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
            //Console.WriteLine(isPlay);
            if (this.openedWindow != null)
            {
                frame_override = (frame_override + 1) % this.openedWindow.frame_sheet;
                for (int i = 0; i < windowList.Count; i++)
                {
                    //windowList[i].frame = frame_override;
                    windowList[i].Sticker.Source = windowList[i].imgFrame[frame_override];
                }
                speed_override = this.openedWindow.speed;

                //Console.WriteLine(speed_override);
            }
            timer.Interval = TimeSpan.FromSeconds(fps * speed_override);
        }

        private void Slider_Speed(object sender, RoutedEventArgs e)
        {
            wplayer.settings.rate = musicSpeed.Value;
            SpeedText.Text = musicSpeed.Value.ToString();
            //Console.WriteLine(musicSpeed.Value);
        }
        private void Slider_Volume(object sender, RoutedEventArgs e)
        {
            wplayer.settings.volume = (int)musicVolume.Value;
            VolumeText.Text = musicVolume.Value.ToString();
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
