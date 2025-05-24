using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace LocaCraft.Behaviours
{
    class DragAndDropCommandBehaviour : Behavior<UIElement>
    {
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register(nameof(DropCommand), typeof(ICommand), typeof(DragAndDropCommandBehaviour));

        public static readonly DependencyProperty DragOverCommandProperty =
        DependencyProperty.Register(nameof(DragOverCommand), typeof(ICommand), typeof(DragAndDropCommandBehaviour));


        public ICommand? DropCommand
        {
            get => (ICommand)GetValue(DropCommandProperty);
            set => SetValue(DropCommandProperty, value);
        }

        public ICommand? DragOverCommand
        {
            get => (ICommand?)GetValue(DragOverCommandProperty);
            set => SetValue(DragOverCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += OnDrop;
            AssociatedObject.DragOver += DragOver;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DragOver -= DragOver;
            AssociatedObject.Drop -= OnDrop;
        }

        private void DragOver(object sender, DragEventArgs e)
        {
            if (DragOverCommand?.CanExecute(e) == true)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.None;
                DragOverCommand?.Execute(e);
                e.Handled = true;
            }
        }


        private void OnDrop(object sender, DragEventArgs e)
        {
            if (DropCommand?.CanExecute(e.Data) == true)
            {
                DropCommand?.Execute(e.Data);
                e.Handled = true;
            }
        }
    }
}
