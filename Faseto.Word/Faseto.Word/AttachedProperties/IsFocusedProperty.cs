using System.Windows;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// The IsBusy attached property for a anything that wants to flag if the control is busy
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If we don't have a control, return
            if (!(sender is Control control))
                return;

            control.Loaded += (s, se) => control.Focus();

        }
    }
}
