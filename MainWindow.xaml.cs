using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading; 

namespace Corsa_contromano
{
 
    public partial class MainWindow : Window
    {


        public DispatcherTimer gameTimer = new DispatcherTimer();
        Rect playerHitBox;
        Rect obstacleHitBox;
        Random rand = new Random();
        int  skin, Punti = 0, temp = 0;
        bool gameover = false;
        string nome;
        int[] obstaclePosition = { 320, 310, 300, 305, 315 };
        float playermove = 5, altezza;
        ImageBrush playerSprite = new ImageBrush();
        ImageBrush boomSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();
       
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.R && gameover)
            {
                StartGame();
            }

            if (e.Key == Key.C && gameover)
            {
                gioco.Visibility = Visibility.Collapsed;
                menu1.Visibility = Visibility.Visible;



            }
            if (e.Key == Key.E)
            {
                gioco.Visibility = Visibility.Collapsed;
                inizio.Visibility = Visibility.Visible;


            }
        }
        private void EndGame(int Punti, string nome)
        {



            boomSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma7.png", UriKind.Relative));
            l55.Visibility = Visibility.Visible;
            player.Fill = boomSprite;
            PuntiText.Content = "\t"+ nome + " il tuo punteggio finale è :\t " + Punti;
            String punteggio = nome + " " + Punti;
            InserisciDati(punteggio);
            gameover = true;
            gameTimer.Stop();
        }
    


            void InserisciDati(string dati)
                {
                    using (StreamWriter sw = File.AppendText(@"..\..\..\score.txt"))
                    {
           
                        sw.WriteLine(dati);
                    }

                    string[] righe = File.ReadAllLines(@"..\..\..\score.txt");

       
                    righe = righe.OrderBy(r => int.Parse(r.Split(' ')[1])).ToArray();

       
                    Array.Reverse(righe);


            if (!righe[0].Contains("RECORD"))
            {
                righe[0] = righe[0] + " RECORD";
            }

            for (int i = 1; i < righe.Length; i++)
            {
                righe[i] = righe[i].Replace(" RECORD", "");
            }

        


            File.WriteAllLines(@"..\..\..\score.txt", righe);
                }



    private void Skin()
        {
            skin = rand.Next(1, 8);
            switch (skin)
            {
                case 1:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\macchina2.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;
                case 2:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\macchina3.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;
                case 3:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\macchina.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;
                case 4:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\m1.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;
                case 5:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\m2.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;
                case 6:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\m3.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;
                case 7:
                    obstacleSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\cars\m4.png", UriKind.Relative));
                    obstacle.Fill = obstacleSprite;
                    break;

            }
        }
       

        private void StartGame()
        {
            
            
            InitializeComponent();
            menu1.Visibility = Visibility.Collapsed;
           
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            l55.Visibility = Visibility.Collapsed;
            player.Fill = playerSprite;
            Skin();
            if (temp==0)
            {
                gameTimer.Tick += Movimento;
            }

           
            background2.Fill = backgroundSprite;
            background.Fill = backgroundSprite;

            altezza = rand.Next(141, 315);
            Canvas.SetLeft(background, 0); 
            Canvas.SetLeft(background2, 1262); 
            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, altezza);
            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, altezza);

            gameover = false;
            Punti = 0;
            playermove = 5;
                gameTimer.Start();
        }
             private void Movimento(object sender, EventArgs e)
        {
            //gestione velocità
            if (Punti ==0)
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(20);
                playermove = 5;
            }
            if (Punti == 2)
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(15);
                playermove = 5;
            }
            if (Punti == 4)
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(10);
                playermove = 7;
            }
            if (Punti == 6)
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(5);
                playermove = 9;
            }
            if (Punti > 8)
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(2);
                playermove = 10;
            }
            //gestione tasti
            if (Keyboard.IsKeyDown(Key.Up))
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playermove);
                RotateTransform rotateTransform1 = new RotateTransform(-15);
                player.RenderTransform = rotateTransform1;
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playermove);
                RotateTransform rotateTransform1 = new RotateTransform(15);
                player.RenderTransform = rotateTransform1;

            }
            else
            {
                RotateTransform rotateTransform1 = new RotateTransform(0);
                player.RenderTransform = rotateTransform1;            
            }
            //generazione posizione casuale macchina
            altezza = rand.Next(121, 315);

            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

                backgroundSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\fondi\s3.png", UriKind.Relative));
            PuntiText.Content = "Punti: " + Punti;
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);

           
            if (Canvas.GetLeft(background) < -1262)
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            
            if (Canvas.GetLeft(background2) < -1262)
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);

            if (Canvas.GetLeft(obstacle) < -50)
            {
                Skin();
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[rand.Next(0, obstaclePosition.Length)]);
                Canvas.SetTop(obstacle, altezza);
                Punti += 1;
            }
            //gestione sconfitta
            if (Canvas.GetTop(player) < 140 || Canvas.GetTop(player) > 320 || playerHitBox.IntersectsWith(obstacleHitBox))
            {

                temp = 1;
                EndGame(Punti, nome);
            }
            player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;   
        }

        //bottoni
        private void Avvio2()
        {
           
            gioco.Visibility = Visibility.Visible;
            menu1.Visibility = Visibility.Collapsed;
            gioco.Focus();
            StartGame();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            playerSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma1.png", UriKind.Relative));
            Avvio2();
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            playerSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma2.png", UriKind.Relative));
            Avvio2();
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            playerSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma3.png", UriKind.Relative));
            Avvio2();

        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            playerSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma4.png", UriKind.Relative));
            Avvio2();
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            playerSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma5.png", UriKind.Relative));
          Avvio2();
        }


        private void b6_Click(object sender, RoutedEventArgs e)
        {
            playerSprite.ImageSource = new BitmapImage(new Uri(@"..\..\..\skin\ma6.png", UriKind.Relative));
            Avvio2();
        }

        private void b10_Click_1(object sender, RoutedEventArgs e)
        {
            inizio.Visibility = Visibility.Collapsed;
            guid.Visibility = Visibility.Visible;
            l66.Content = File.ReadAllText(@"..\..\..\guida.txt");
           
        }
        private void b8_Click(object sender, RoutedEventArgs e)
        {
            nome = texBox1.Text;
            inizio.Visibility = Visibility.Collapsed;
            
            menu1.Visibility = Visibility.Visible;
            
        }

        private void b9_Click(object sender, RoutedEventArgs e)
        {
            inizio.Visibility = Visibility.Collapsed;
            classifica.Visibility = Visibility.Visible;

            l1.Content= File.ReadAllText(@"..\..\..\score.txt");
           
        }
       private void fuga_Click(object sender, RoutedEventArgs e)
        {
            guid.Visibility = Visibility.Collapsed;
            inizio.Visibility = Visibility.Visible;
        }

        private void ajeje_Click(object sender, RoutedEventArgs e)
        {
         

            classifica.Visibility = Visibility.Collapsed;
            inizio.Visibility = Visibility.Visible;
            
           
        }
    }
}
