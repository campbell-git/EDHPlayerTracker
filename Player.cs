using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace EDHPlayerTracker;

public class Player : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _isActive = false;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }
    }

    private int _health = 40;
    public int Health
    {
        get => _health;
        set
        {
            if (_health != value)
            {
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }
    }

    private DateTime _activeStart = DateTime.Now;
    private TimeSpan ActiveTimer() => DateTime.Now.Subtract(_activeStart);

    private TimeSpan _totalTime = new TimeSpan();
    public TimeSpan TotalTime
    {
        get => IsActive ?
            _totalTime + ActiveTimer() :
            _totalTime;
        private set => _totalTime = value;
    }

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private async void Loop()
    {
        await Task.Delay(100);
        OnPropertyChanged(nameof(TotalTime));
        if (IsActive)
        {
            Loop();
        }
    }

    public void Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            _activeStart = DateTime.Now;
            Loop();
        }
        else
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            TotalTime = TotalTime + ActiveTimer();
        }
    }

}
