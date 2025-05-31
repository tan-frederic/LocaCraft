using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LocaCraft.Behaviours
{
    class MouseEventCommandBehaviour : Behavior<UIElement>
    {
        #region PROPERTIES
        // DependencyProperty for binding a command to the behavior.
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(MouseEventCommandBehaviour));

        // DependencyProperty for specifying the event name to attach to.
        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.Register(nameof(EventName), typeof(string), typeof(MouseEventCommandBehaviour));

        // The command to execute when the event is triggered.
        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        // The name of the event to listen for on the associated UIElement.
        public string? EventName
        {
            get => (string?)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        // Stores the delegate handler for the event.
        private Delegate? _handler;
        #endregion

        /// <summary>
        /// Attaches the specified event handler to the associated UIElement when the behavior is attached.
        /// It uses reflection to find the event by name and creates a delegate to handle the event,
        /// executing the associated command when the event is raised.
        /// Throws an exception if the event is not found on the associated object.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null && !string.IsNullOrEmpty(EventName))
            {
                EventInfo? eventInfo = AssociatedObject.GetType().GetEvent(EventName);
                if (eventInfo != null)
                {
                    MethodInfo methodInfo = GetType().GetMethod(nameof(OnMouseEvent), BindingFlags.NonPublic | BindingFlags.Instance)!;
                    _handler = Delegate.CreateDelegate(eventInfo.EventHandlerType!, this, methodInfo);
                    eventInfo.AddEventHandler(AssociatedObject, _handler);
                }
                else
                {
                    throw new ArgumentException($"Événement '{EventName}' non trouvé sur {AssociatedObject.GetType().Name}");
                }
            }
        }

        /// <summary>
        /// Detaches the event handler from the associated UIElement when the behavior is detached.
        /// Uses reflection to find the event by name and removes the previously attached delegate,
        /// ensuring that the event handler is properly cleaned up.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null && !string.IsNullOrEmpty(EventName) && _handler != null)
            {
                EventInfo? eventInfo = AssociatedObject.GetType().GetEvent(EventName);
                eventInfo?.RemoveEventHandler(AssociatedObject, _handler);
            }
        }

        /// <summary>
        /// Handles the specified mouse event by checking if the associated command can be executed with the event arguments,
        /// and executes the command if possible.
        /// </summary>
        private void OnMouseEvent(object sender, EventArgs e)
        {
            if (Command?.CanExecute(e) == true)
            {
                Command.Execute(e);
            }
        }
    }
}
