using Avalonia.Input;
using Avalonia.Threading;
using System;
using System.ComponentModel;
namespace AvaloniaPong2.ViewModels
{
    public class PongViewModel : INotifyPropertyChanged, IDisposable
    {
        private const double PaddleSpeed = 25;
        private const double BallSpeed = 4;

        private double _paddle1Y;
        private double _paddle1X;
        private double _paddle2X;
        private double _paddle2Y;
        private double _ballX;
        private double _ballY;
        private double _ballXDirection = BallSpeed;
        private double _ballYDirection = BallSpeed;
        private DispatcherTimer _timer;
        private int _scorePLR1;
        private int _scorePLR2;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public int ScorePLR1
        {
            get => _scorePLR1;
            set
            {
                _scorePLR1 = value;
                OnPropertyChanged(nameof(ScorePLR1));
            }
        }

        public int ScorePLR2
        {
            get => _scorePLR2;
            set
            {
                _scorePLR2 = value;
                OnPropertyChanged(nameof(ScorePLR2));
            }
        }
        public double Paddle1X
        {
            get => _paddle1X;
            set
            {
                _paddle1X = value;
                OnPropertyChanged(nameof(Paddle1X));
            }
        }
        public double Paddle1Y
        {
            get => _paddle1Y;
            set
            {
                _paddle1Y = value;
                OnPropertyChanged(nameof(Paddle1Y));
            }
        }

        public double Paddle2Y
        {
            get => _paddle2Y;
            set
            {
                _paddle2Y = value;
                OnPropertyChanged(nameof(Paddle2Y));
            }
        }

        public double Paddle2X
        {
            get => _paddle2X;
            set
            {
                _paddle2X = value;
                OnPropertyChanged(nameof(Paddle2X));
            }
        }

        public double BallX
        {
            get => _ballX;
            set
            {
                _ballX = value;
                OnPropertyChanged(nameof(BallX));
            }
        }

        public double BallY
        {
            get => _ballY;
            set
            {
                _ballY = value;
                OnPropertyChanged(nameof(BallY));
            }
        }

        public PongViewModel()
        {
            Paddle1Y = 200; 
            Paddle1X = 10;
            Paddle2Y = 200;
            Paddle2X = 780;
            BallX = 400; 
            BallY = 225;
            ScorePLR1 = 0;
            ScorePLR2 = 0;
            _timer = new DispatcherTimer();
            _timer.Tick += OnTimerTick;
            _timer.Interval = TimeSpan.FromMilliseconds(16.666); // 60 FPS
            _timer.Start();
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if (Paddle1Y >2)
                    {Paddle1Y -= PaddleSpeed;}
                    break;
                case Key.S:
                    if (Paddle1Y < 500)
                    {Paddle1Y += PaddleSpeed;}
                    break;
                case Key.Up:
                    if (Paddle2Y > 2)
                    {Paddle2Y -= PaddleSpeed;}
                    break;
                case Key.Down:
                    if (Paddle2Y < 500)
                    {Paddle2Y += PaddleSpeed;}
                    break;
            }
        }

        public void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.S:
                    Paddle1Y += 0;
                    break;
                case Key.Up:
                case Key.Down:
                    Paddle2Y += 0;
                    break;
            }
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            BallX += _ballXDirection;
            BallY += _ballYDirection;
            
            if (BallX <= 20 && BallY >= Paddle1Y && BallY <= Paddle1Y + 100)
            {
                _ballXDirection = -(_ballXDirection); 
                if (_ballXDirection < 0)
                    _ballXDirection -= 0.2;
                else _ballXDirection += 0.2;
            }
            else if (BallX >= 770 && BallY >= Paddle2Y && BallY <= Paddle2Y + 100)
            {
                _ballXDirection = -(_ballXDirection); 
                if (_ballXDirection < 0)
                    _ballXDirection -= 0.2;
                else _ballXDirection += 0.2;
            }
            
            if (BallX <= 0 || BallX >= 800)
            {
                if (BallX <= 0)
                {
                    ScorePLR1++;
                }
                else
                {
                    ScorePLR2++;
                }
                _ballYDirection = -BallSpeed;
                _ballXDirection = BallSpeed; 
                BallX = 400;
                BallY = 225;
            }

            if (BallY <= 2 || BallY >= 598)
            {
                _ballYDirection = -_ballYDirection; 
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Tick -= OnTimerTick;
        }
    }
}