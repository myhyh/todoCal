using System.Collections.Generic;
using System.ComponentModel;

namespace TodoCal.utils;

public class TwoWayBindable<T> : INotifyPropertyChanged
{
    private T _value;
    
    public T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value))
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public static implicit operator T(TwoWayBindable<T> twoWayBindable) => twoWayBindable.Value;
}