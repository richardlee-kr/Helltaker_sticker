using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WMPLib;

namespace Helltaker1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap original; //표시될 bitmap
        Bitmap[] frames = new Bitmap[24]; //애니메이션 프레임
        Bitmap[] frames_8 = new Bitmap[8];

        ImageSource[] imgFrame = new ImageSource[24]; //이미지소스 프레임
        ImageSource[] imgFrame_8 = new ImageSource[8];

        string bitmapPath = "Resources/Lucifer.png"; //파일 디렉토리

        int frame = -1; //프레임 
        float speed = 3;
        DispatcherTimer timer;

        string[] filePaths = Directory.GetFiles("Resources", "*.png"); //파일 이름 받아오기

        WindowsMediaPlayer wplayer = new WindowsMediaPlayer(); //WMP

        /*for release bitmap*/
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        private bool isShowGrip = false;


        public MainWindow()
        {
            InitializeComponent();


            #region character
            
            var Azazel = new System.Windows.Forms.MenuItem
            {
                Index = 10,
                Text = "Azazel",
            };
            var Cerberus = new System.Windows.Forms.MenuItem
            {
                Index = 9,
                Text = "Cerberus",
            };
            var Judgement = new System.Windows.Forms.MenuItem
            {
                Index = 8,
                Text = "Judgement",
            };
            var Justice = new System.Windows.Forms.MenuItem
            {
                Index = 7,
                Text = "Justice",
            };
            var Lucifer = new System.Windows.Forms.MenuItem
            {
                Index = 6,
                Text = "Lucifer",
            };
            var Malina = new System.Windows.Forms.MenuItem
            {
                Index = 5,
                Text = "Malina",
            };
            var Modeus = new System.Windows.Forms.MenuItem
            {
                Index = 4,
                Text = "Modeus",
            };
            var Pandemonica = new System.Windows.Forms.MenuItem
            {
                Index = 3,
                Text = "Pandemonica",
            };
            var Zdrada = new System.Windows.Forms.MenuItem
            {
                Index = 2,
                Text = "Zdrada",
            };
            var Glorious_right = new System.Windows.Forms.MenuItem
            {
                Index = 1,
                Text = "Glorious_right",
            };
            var Glorious_left = new System.Windows.Forms.MenuItem
            {
                Index = 0,
                Text = "Glorious_left",
            };
            #endregion

            #region Character Click Block         
            Azazel.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = true;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;
                bitmapPath = "Resources/Azazel.png";
                Animation(bitmapPath);
            };
            Cerberus.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = true;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Cerberus.png";
                Animation(bitmapPath);
            };
            Judgement.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = true;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Judgement.png";
                Animation(bitmapPath);
            };
            Justice.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = true;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Justice.png";
                Animation(bitmapPath);
            };
            Lucifer.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = true;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Lucifer.png";
                Animation(bitmapPath);
            };
            Malina.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = true;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Malina.png";
                Animation(bitmapPath);
            };
            Modeus.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = true;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Modeus.png";
                Animation(bitmapPath);
            };
            Pandemonica.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = true;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Pandemonica.png";
                Animation(bitmapPath);
            };
            Zdrada.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = true;
                Glorious_left.Checked = false;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Zdrada.png";
                Animation(bitmapPath);
            };
            Glorious_left.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = true;
                Glorious_right.Checked = false;

                bitmapPath = "Resources/Glorious_success_left.png";
                Animation(bitmapPath);
            };
            Glorious_right.Click += (object o, EventArgs e) =>
            {
                Azazel.Checked = false;
                Cerberus.Checked = false;
                Judgement.Checked = false;
                Justice.Checked = false;
                Lucifer.Checked = false;
                Malina.Checked = false;
                Modeus.Checked = false;
                Pandemonica.Checked = false;
                Zdrada.Checked = false;
                Glorious_left.Checked = false;
                Glorious_right.Checked = true;

                bitmapPath = "Resources/Glorious_success_right.png";
                Animation(bitmapPath);
            };
            #endregion

            #region Speed Value
            var speed1 = new System.Windows.Forms.MenuItem
            {
                Index = 0,
                Text = "1",
            };
            var speed2 = new System.Windows.Forms.MenuItem
            {
                Index = 1,
                Text = "2",
            };
            var speed3 = new System.Windows.Forms.MenuItem
            {
                Index = 2,
                Text = "3",
            };
            var speed4 = new System.Windows.Forms.MenuItem
            {
                Index = 3,
                Text = "4",
            };
            var speed5 = new System.Windows.Forms.MenuItem
            {
                Index = 4,
                Text = "5",
            };
            #endregion

            #region Speed Click Block
            speed1.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = true;
                speed2.Checked = false;
                speed3.Checked = false;
                speed4.Checked = false;
                speed5.Checked = false;
                speed = 5;
                timer.Interval = TimeSpan.FromSeconds((1/66.6f)*speed);
            };
            speed2.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = true;
                speed3.Checked = false;
                speed4.Checked = false;
                speed5.Checked = false;
                speed = 4;
                timer.Interval = TimeSpan.FromSeconds((1/66.6f)*speed);
            };
            speed3.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = false;
                speed3.Checked = true;
                speed4.Checked = false;
                speed5.Checked = false;
                speed = 3;
                timer.Interval = TimeSpan.FromSeconds((1/66.6f)*speed);
            };
            speed4.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = false;
                speed3.Checked = false;
                speed4.Checked = true;
                speed5.Checked = false;
                speed = 2;
                timer.Interval = TimeSpan.FromSeconds((1/66.6f)*speed);
            };
            speed5.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = false;
                speed3.Checked = false;
                speed4.Checked = false;
                speed5.Checked = true;
                speed = 1f;
                timer.Interval = TimeSpan.FromSeconds((1/66.6f)*speed);
            };
            #endregion


            Animation(bitmapPath); //지정된 디렉토리에 있는 png로 실행

            /*프레임 타이머*/
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds((1 / 66.6f) * speed);
            timer.Tick += NextFrame;
            timer.Start();
            Play();

            MouseDown += MainWindow_MouseDown; //드래그로 움직이기
            MouseRightButtonDown += MainWindow_MouseRightClick; //우클릭 이벤트

            /*mp3 player*/
            wplayer.settings.setMode("loop", true);
            wplayer.URL = "Resources/Helltaker.mp3";



            /*for notify icon*/
            var menu = new System.Windows.Forms.ContextMenu();

            var noti = new System.Windows.Forms.NotifyIcon
            {
                Icon = System.Drawing.Icon.FromHandle(frames[0].GetHicon()),
                Visible = true,
                Text = "HellTaker",
                ContextMenu = menu,
            };
            var shutdown = new System.Windows.Forms.MenuItem
            {
                Index = 0,
                Text = "끄기",

            };
            var overlay = new System.Windows.Forms.MenuItem
            {
                Index = 1,
                Text = "오버레이",
            };
            var bgm = new System.Windows.Forms.MenuItem
            {
                Index = 2,
                Text = "브금",
            }; bgm.Checked = true;
            var CharSelect = new System.Windows.Forms.MenuItem
            {
                Index = 3,
                Text = "캐릭터 선택",
            };
            var SpeedControl = new System.Windows.Forms.MenuItem
            {
                Index = 4,
                Text = "프레임 속도 선택",
            };


            /*끄기 버튼*/
            shutdown.Click += (object o, EventArgs e) =>
            {
                System.Windows  .Application.Current.Shutdown();
            };
            /*오버레이 버튼*/
            overlay.Click += (object o, EventArgs e) =>
            {
                this.Topmost = !this.Topmost;
                overlay.Checked = !overlay.Checked;
            };
            /*브금 버튼*/
            bgm.Click += (object o, EventArgs e) =>
            {           
                bgm.Checked = !bgm.Checked;
                if (bgm.Checked)
                    Play();
                else
                    Stop();
            };
            
           /*메뉴에 추가*/
            menu.MenuItems.Add(CharSelect);
            menu.MenuItems.Add(overlay);
            menu.MenuItems.Add(bgm);
            menu.MenuItems.Add(SpeedControl);
            menu.MenuItems.Add(shutdown);

            /*기본 설정*/
            overlay.Checked = true;
            Lucifer.Checked = true;
            speed3.Checked = true;
            this.Topmost = true;



            #region Add Character in List
            CharSelect.MenuItems.Add(Azazel);
            CharSelect.MenuItems.Add(Cerberus);
            CharSelect.MenuItems.Add(Judgement);
            CharSelect.MenuItems.Add(Justice);
            CharSelect.MenuItems.Add(Lucifer);
            CharSelect.MenuItems.Add(Malina);
            CharSelect.MenuItems.Add(Modeus);
            CharSelect.MenuItems.Add(Pandemonica);
            CharSelect.MenuItems.Add(Zdrada);
            CharSelect.MenuItems.Add(Glorious_left);
            CharSelect.MenuItems.Add(Glorious_right);
            #endregion

            #region Add Speed Controller
            SpeedControl.MenuItems.Add(speed1);
            SpeedControl.MenuItems.Add(speed2);
            SpeedControl.MenuItems.Add(speed3);
            SpeedControl.MenuItems.Add(speed4);
            SpeedControl.MenuItems.Add(speed5);
            #endregion

            noti.ContextMenu = menu; //컨텍스트 메뉴에 메뉴 추가
        }

        /*프레임 진행*/
        private void NextFrame(object sender, EventArgs e)
        {
            frame = (frame + 1) % 24;
            Sticker.Source = imgFrame[frame];
            ShowGrip();
            //Console.WriteLine(bitmapPath);
        }

        /*드래그 함수*/
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        private void MainWindow_MouseRightClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right) isShowGrip = !isShowGrip;
        }

        private void ShowGrip()
        {
            if (isShowGrip)
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            else
                this.ResizeMode = ResizeMode.NoResize;
        }


        /*음악 플레이어*/
        private void Play()
        {
            wplayer.controls.play();
        }
        private void Stop()
        {
            wplayer.controls.stop();
        }

        /*프레임 애니메이션 실행*/
        private void Animation(string _path)
        {
            original = System.Drawing.Image.FromFile(bitmapPath) as Bitmap;
            for (int i = 0; i < 24; i++)
            {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i]))
                {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
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
    }
}
