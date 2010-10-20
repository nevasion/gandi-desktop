using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Threading;

namespace GandiDesktop.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged(string propertyName)
        {
            Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
            Action propertyChanged = new Action(() => this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));

            if (dispatcher.CheckAccess())
            {
                propertyChanged();
            }
            else
            {
                dispatcher.BeginInvoke(propertyChanged);
            }
        }

        public void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            this.OnPropertyChanged(memberExpression.Member.Name);
        }
    }
}