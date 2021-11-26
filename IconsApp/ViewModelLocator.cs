using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace IconsApp
{
    public class ViewModelLocator
    {


        public static string GetViewModelName(DependencyObject obj)
        {
            return (string)obj.GetValue(ViewModelNameProperty);
        }

        public static void SetViewModelName(DependencyObject obj, string value)
        {
            obj.SetValue(ViewModelNameProperty, value);
        }

        public static readonly DependencyProperty ViewModelNameProperty =
            DependencyProperty.RegisterAttached("ViewModelName", typeof(string), 
                typeof(ViewModelLocator), new PropertyMetadata(null, OnSetViewModelName));

        private static void OnSetViewModelName(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Control;
            if (control != null && e.NewValue != null)
            {
                var types = Assembly.GetExecutingAssembly().GetTypes();
                var type = types.FirstOrDefault(t => t.Name == e.NewValue.ToString());
                if (type != null)
                {
                    
                }
            }
        }
    }
}
