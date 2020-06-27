using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WMPLib;

namespace Helltaker1
{
    //To Edit
    //controlWindow에서 캐릭터 선택하기(보류)
    //우클릭하면 -ㅁX 나오게 하기
    //다운로드 매니저 구현
    
    public partial class MainWindow : Window
    {
        public int frame_sheet = 24;
        int height = 100;
        int width = 100;

        Bitmap original;  //bitmap to show
        Bitmap[] frames; //frame for animation

        ContorlWindow control = new ContorlWindow();

        public ImageSource[] imgFrame; //frame for split sheet

        string bitmapPath = "Resources/Lucifer.png"; //file directory

        public int frame; //frame for animated bitmap
        public float fps = 3 / 200f; //fps variable
        public float speed = 3; //frame speed

        private bool isShowGrip = false;


        /*timer for frame update*/
        DispatcherTimer timer;

        //string[] filePaths = Directory.GetFiles("Resources", "*.png"); //get file name


        /*for release bitmap*/
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);



        public MainWindow()
        {
            InitializeComponent();
            frames = new Bitmap[frame_sheet];
            imgFrame = new ImageSource[frame_sheet];

            //bitmapPath = "Resources/" + CharacterName + ".png";

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
                speed = 5f;
                //timer.Interval = TimeSpan.FromSeconds((1 / 66.6f) * speed);
            };
            speed2.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = true;
                speed3.Checked = false;
                speed4.Checked = false;
                speed5.Checked = false;
                speed = 4f;
                //timer.Interval = TimeSpan.FromSeconds((1 / 66.6f) * speed);
            };
            speed3.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = false;
                speed3.Checked = true;
                speed4.Checked = false;
                speed5.Checked = false;
                speed = 3f;
               //timer.Interval = TimeSpan.FromSeconds((1 / 66.6f) * speed);
            };
            speed4.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = false;
                speed3.Checked = false;
                speed4.Checked = true;
                speed5.Checked = false;
                speed = 2f;
                //timer.Interval = TimeSpan.FromSeconds((1 / 66.6f) * speed);
            };
            speed5.Click += (object o, EventArgs e) =>
            {
                speed1.Checked = false;
                speed2.Checked = false;
                speed3.Checked = false;
                speed4.Checked = false;
                speed5.Checked = true;
                speed = 1f;
                //timer.Interval = TimeSpan.FromSeconds((1 / 66.6f) * speed);
            };
            #endregion


            Animation(bitmapPath); //play bitmap animation from directory


            /*frame timer*/
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(fps * speed);
            timer.Tick += NextFrame;
            timer.Start();


            /*mouse event*/
            MouseDown += MainWindow_MouseDown; //drag move
            MouseRightButtonDown += MainWindow_MouseRightClick; //right click event


            /*for notify icon*/
            var menu = new System.Windows.Forms.ContextMenu();
            var noti = new System.Windows.Forms.NotifyIcon
            {
                Icon = System.Drawing.Icon.FromHandle(frames[0].GetHicon()),
                Visible = true,
                Text = "HellTaker",
                ContextMenu = menu,
            };
            var close = new System.Windows.Forms.MenuItem
            {
                Index = 0,
                Text = "닫기",

            };
            var shutdown = new System.Windows.Forms.MenuItem
            {
                Text = "모두 종료",
            };
            var overlay = new System.Windows.Forms.MenuItem
            {
                Index = 1,
                Text = "오버레이",
            };
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

            /*reset*/
            this.Topmost = true;
            overlay.Checked = true;
            Lucifer.Checked = true;
            speed3.Checked = true;


            /*close button*/
            close.Click += (object o, EventArgs e) =>
            {
                //Stop();
                this.Close();
                noti.Dispose();
            };
            /*shutdonw button*/
            shutdown.Click += (object o, EventArgs e) =>
            {
                Shutdown();
            };
            /*overlay button*/
            overlay.Click += (object o, EventArgs e) =>
            {
                this.Topmost = !this.Topmost;
                overlay.Checked = !overlay.Checked;
            };



            /*add to menu*/
            menu.MenuItems.Add(CharSelect);
            menu.MenuItems.Add(SpeedControl);
            menu.MenuItems.Add(overlay);
            //menu.MenuItems.Add(bgm);
            menu.MenuItems.Add(close);
            menu.MenuItems.Add(shutdown);

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

            noti.ContextMenu = menu; //add menu to Contextmenu
        }

        /*Frame process*/
        private void NextFrame(object sender, EventArgs e)
        {
            ShowGrip();
        }

        /*Drag Function*/
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        /*Resize Function*/
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


        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }

        /*split sprites from sheet*/
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

    }
}
