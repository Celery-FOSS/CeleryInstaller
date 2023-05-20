using System.Windows;
using System.Windows.Controls;

namespace CeleryInstaller.Controls
{
    public class RoundButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(RoundButton), new PropertyMetadata(new CornerRadius()));

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

    }
}
