using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;

namespace Mobile.Models
{
    public abstract class ObservableItem
    {
        internal event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateValues(ObservableItem item)
        {
            foreach(PropertyInfo property in item.GetType().GetProperties())
            {
                var newVal = property.GetValue(item);
                property.SetValue(this, newVal);
            }
        }
    }
}
