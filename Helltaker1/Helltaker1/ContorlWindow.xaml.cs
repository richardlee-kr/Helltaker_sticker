using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WMPLib;

namespace Helltaker1
{
    public partial class ContorlWindow : Window
    {
        public int frame = -1;
        float fps = 3 / 200f;
        float speed;

        System.Windows.Controls.Image preview;

        int frame_sheet = 24;

        string bitmapPath = "";

        bool isActivatePreview;

        Bitmap original;
        int height = 100; //height of each frame image
        int width = 100; //width of each frame image
        Bitmap[] frames; //frame for animation
        ImageSource[] imgFrame;

        /*for release bitmap*/
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);


        private bool isPlay;

        System.Windows.Forms.MenuItem Vitality;
        System.Windows.Forms.NotifyIcon noti;

        List<MainWindow> windowList = new List<MainWindow>();

        WindowsMediaPlayer wplayer = new WindowsMediaPlayer(); //WMP


        MainWindow openedWindow;
        DispatcherTimer timer;
        DispatcherTimer buttonTimer;


        public ContorlWindow()
        {
            InitializeComponent();
            frames = new Bitmap[frame_sheet];
            imgFrame = new ImageSource[frame_sheet];

            MouseRightButtonDown += ControlWindow_RightClick;
            
            /*Frame Timer*/
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(fps);
            timer.Tick += NextFrame;
            timer.Start();

            /*button Image Frame Timer*/
            buttonTimer = new DispatcherTimer();
            buttonTimer.Interval = TimeSpan.FromSeconds(fps);
            buttonTimer.Tick += NextFrame_Button;

            wplayer.settings.setMode("loop", true);
            wplayer.URL = "Resources/Music/Vitality.mp3";
            isPlay = false;

            var menu = new System.Windows.Forms.ContextMenu();
            noti = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon(@"Resources/helltaker_icon.ico"),
                Visible = true,
                Text = "HellTaker_Settings",
                ContextMenu = menu,
            };
            var shutdown = new System.Windows.Forms.MenuItem
            {
                Text = "shutdown",
            };
            shutdown.Click += (object o, EventArgs e) =>
            {
                Shutdown();
            };
            noti.Click += (object sender, EventArgs e) =>
            {
                this.Show();
            };
            var musicList = new System.Windows.Forms.MenuItem
            {
                Text = "MusicList",
            };

            #region MusicList Componenet
            Vitality = new System.Windows.Forms.MenuItem
            {
                Text = "Vitality",
            };
            var Apropos = new System.Windows.Forms.MenuItem
            {
                Text = "Apropos",
            };
            var Epitomize = new System.Windows.Forms.MenuItem
            {
                Text = "Epitomize",
            };
            var Luminescent = new System.Windows.Forms.MenuItem
            {
                Text ="Luminescent",
            };

            Vitality.Click += (object o, EventArgs e) =>
            {
                Vitality.Checked = true;
                Apropos.Checked = false;
                Epitomize.Checked = false;
                Luminescent.Checked = false;
                wplayer.URL = "Resources/Music/Vitality.mp3";
            };
            Apropos.Click += (object o, EventArgs e) =>
            {
                Vitality.Checked = false;
                Apropos.Checked = true;
                Epitomize.Checked = false;
                Luminescent.Checked = false;
                wplayer.URL = "Resources/Music/Apropos.mp3";
            };
            Epitomize.Click += (object o, EventArgs e) =>
            {
                Vitality.Checked = false;
                Apropos.Checked = false;
                Epitomize.Checked = true;
                Luminescent.Checked = false;
                wplayer.URL = "Resources/Music/Epitomize.mp3";
            };
            Luminescent.Click += (object o, EventArgs e) =>
            {
                Vitality.Checked = false;
                Apropos.Checked = false;
                Epitomize.Checked = false;
                Luminescent.Checked = true;
                wplayer.URL = "Resources/Music/Luminescent.mp3";
            };

            #endregion

            wplayer.controls.stop();

            musicList.MenuItems.Add(Vitality);
            musicList.MenuItems.Add(Apropos);
            musicList.MenuItems.Add(Epitomize);
            musicList.MenuItems.Add(Luminescent);


            menu.MenuItems.Add(musicList);
            menu.MenuItems.Add(shutdown);

            this.Topmost = true;
            this.Topmost = false;
            Vitality.Checked = true;

            noti.ContextMenu = menu;
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
            //openedWindow.MouseDoubleClick += (object o, MouseButtonEventArgs e) =>
            //{
            //    this.Show();
            //};
        }

        private void Shutdown()
        {
            if(openedWindow != null)
            {
                for (int i = 0; i < windowList.Count; i++)
                {
                    windowList[i].noti.Dispose();
                }
            }
            this.noti.Dispose();
            System.Windows.Application.Current.Shutdown();
        }


        private void NextFrame(object sender, EventArgs e)
        {
            frame = (frame + 1) % frame_sheet;
            if (this.openedWindow != null)
            {
                for (int i = 0; i < windowList.Count; i++)
                {
                    //windowList[i].frame = frame_override;
                    windowList[i].Sticker.Source = windowList[i].imgFrame[frame];
                }

                //Console.WriteLine(speed_override);
            }
            timer.Interval = TimeSpan.FromSeconds(1 / speed);
        }

        private void NextFrame_Button(object sender, EventArgs e)
        {
            buttonTimer.Interval = TimeSpan.FromSeconds(1 / speed);

            preview.Source = imgFrame[frame];
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
        private void Bellzebub_Easter_Egg_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow();
            openedWindow.Select_Bellzebub();
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
            speed = (int)frameSpeed.Value;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            return;
        }

        private void FrameSpeedText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                frameSpeed.Value = float.Parse(frameSpeedText.Text);
            }
        }
        private void Animation(string _path)
        {
            original = System.Drawing.Image.FromFile(_path) as Bitmap;
            for (int i = 0; i < frame_sheet; i++)
            {
                frames[i] = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(frames[i]))
                {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, width, height),
                        new System.Drawing.Rectangle(i * width, 0, width, height),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try
                {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
                finally
                {
                    DeleteObject(handle);
                }
            }
        }
        
        
        /*preview frame animation*/
        #region Azazel
        private void Azazel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Azazel.png";
            Animation(bitmapPath);
            preview = Azazel_Image;
            buttonTimer.Start();
        }
        private void Azazel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Azazel_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/azazel.png", UriKind.RelativeOrAbsolute));
        }
        #endregion
        #region Cerberus
        private void Cerberus_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Cerberus.png";
            Animation(bitmapPath);
            preview = Cerberus_Image;
            buttonTimer.Start();
        }
        private void Cerberus_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Cerberus_Image.Source = new BitmapImage(new Uri(@"/Resources/ButtonImages/cerberus.png", UriKind.RelativeOrAbsolute));
        }
        #endregion
        #region Judgement
        private void Judgement_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Judgement.png";
            Animation(bitmapPath);
            preview = Judgement_Image;
            buttonTimer.Start();
        }
        private void Judgement_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Judgement_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/judgement.png", UriKind.RelativeOrAbsolute));
        }
        #endregion
        #region Justice
        private void Justice_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Justice.png";
            Animation(bitmapPath);
            preview = Justice_Image;
            buttonTimer.Start();
        }
        private void Justice_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Justice_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/justice.png", UriKind.RelativeOrAbsolute));
        }
        #endregion
        #region Lucifer
        private void Lucifer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Lucifer.png";
            Animation(bitmapPath);
            preview = Lucifer_Image;
            buttonTimer.Start();
        }
        private void Lucifer_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Lucifer_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/lucifer.png", UriKind.RelativeOrAbsolute));
        }
        #endregion
        #region Lucifer_Apron
        private void Lucifer_Apron_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Apron.png";
            Animation(bitmapPath);
            preview = Lucifer_Apron_Image;
            buttonTimer.Start();
        }
        private void Lucifer_Apron_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Lucifer_Apron_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/lucifer_apron.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
        #region Malina
        private void Malina_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Malina.png";
            Animation(bitmapPath);
            preview = Malina_Image;
            buttonTimer.Start();

        }
        private void Malina_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Malina_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/malina.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
        #region Modeus
        private void Modeus_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Modeus.png";
            Animation(bitmapPath);
            preview = Modeus_Image;
            buttonTimer.Start();

        }
        private void Modeus_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Modeus_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/modeus.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
        #region Pandemonica
        private void Pandemonica_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Pandemonica.png";
            Animation(bitmapPath);
            preview = Pandemonica_Image;
            buttonTimer.Start();

        }
        private void Pandemonica_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Pandemonica_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/pandemonica.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
        #region Zdrada
        private void Zdrada_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Zdrada.png";
            Animation(bitmapPath);
            preview = Zdrada_Image;
            buttonTimer.Start();

        }
        private void Zdrada_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            Zdrada_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/zdrada.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
        #region Glorious_left
        private void Glorious_left_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Glorious_success_left.png";
            Animation(bitmapPath);
            preview = left_Image;
            buttonTimer.Start();

        }
        private void Glorious_left_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            left_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/Glorious_success_left.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
        #region Glorious_right
        private void Glorious_right_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bitmapPath = "Resources/Glorious_success_right.png";
            Animation(bitmapPath);
            preview = right_Image;
            buttonTimer.Start();

        }
        private void Glorious_right_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonTimer.Stop();
            right_Image.Source = new BitmapImage(new Uri(@"Resources/ButtonImages/Glorious_success_right.png", UriKind.RelativeOrAbsolute));

        }
        #endregion
    }
}
