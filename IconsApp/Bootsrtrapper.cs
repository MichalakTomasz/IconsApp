using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace IconsApp
{
    public abstract class Bootsrtrapper
    {
        public Bootsrtrapper()
        {
            OnInitialize();
        }
        protected static UnityContainer Container { get; } = new UnityContainer();

        protected abstract void OnInitialize();

        public abstract void Run();

        public static string GetDataContext(DependencyObject obj)
        {
            return (string)obj.GetValue(DataContextProperty);
        }

        public static void SetDataContext(DependencyObject obj, string value)
        {
            obj.SetValue(DataContextProperty, value);
        }

        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.RegisterAttached("DataContext", typeof(string), 
                typeof(Bootsrtrapper), new PropertyMetadata(null, OnDataContextChanged));

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModelClass = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(a => a.Name == e.NewValue.ToString());
            var control = d as Control;
            if (control.DataContext == null || !control.DataContext.GetType().Equals(viewModelClass))
            {
                if (!Container.IsRegistered(viewModelClass))
                    Container.RegisterSingleton(viewModelClass);
                    var viewModel = Container.Resolve(viewModelClass);
                if (viewModel != null && control != null)
                    control.DataContext = viewModel;
            }
        }
    }
}
