using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LocaCraft.Behaviours
{
    class SelectAllTextOnFocusBehaviour : Behavior<TextBox>
    {
        #region PROPERTIES
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += SelectAllText;
            AssociatedObject.PreviewMouseLeftButtonDown += HandleMouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= SelectAllText;
            AssociatedObject.PreviewMouseLeftButtonDown -= HandleMouseDown;
        }
        #endregion

        /// <summary>
        /// Selects all text in the associated TextBox when it receives keyboard focus.
        /// </summary>
        private void SelectAllText(object sender, KeyboardFocusChangedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }

        /// <summary>
        /// Handles the PreviewMouseLeftButtonDown event for the associated TextBox.
        /// If the TextBox does not currently have keyboard focus, this method sets the focus to it
        /// and marks the event as handled to prevent the default mouse click behavior.
        /// </summary>
        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!AssociatedObject.IsKeyboardFocusWithin)
            {
                AssociatedObject.Focus();
                e.Handled = true;
            }
        }
    }
}
