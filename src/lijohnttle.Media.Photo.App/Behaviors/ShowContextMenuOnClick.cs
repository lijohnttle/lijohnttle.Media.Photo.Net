using System.Windows;
using System.Windows.Controls.Primitives;

namespace lijohnttle.Media.Photo.App.Behaviors
{
    public class ShowContextMenuOnClick : DependencyObject
    {
        public static DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled", typeof(bool), typeof(ShowContextMenuOnClick),
            new PropertyMetadata(defaultValue: false, propertyChangedCallback: OnIsEnabledChanged));

        public static bool GetIsEnabled(ButtonBase target) =>
            (bool)target.GetValue(IsEnabledProperty);

        public static void SetIsEnabled(ButtonBase target, bool value) =>
            target.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as ButtonBase;
            if (button != null)
            {
                if ((bool)e.NewValue == true)
                {
                    button.Click -= OnButtonClick;
                    button.Click += OnButtonClick;
                }
                else
                {
                    button.Click -= OnButtonClick;
                }
            }
        }

        private static void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as ButtonBase;
            if (button != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }
    }
}
