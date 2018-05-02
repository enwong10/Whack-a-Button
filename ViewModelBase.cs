using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Whack_a_Button
{
    /// <summary>
    /// Implements the INotifyPropertyChanged interface and 
    /// exposes a RaisePropertyChanged method for derived 
    /// classes to raise the PropertyChange event.  The event 
    /// arguments created by this class are cached to prevent 
    /// managed heap fragmentation.
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        #region Data

        private static readonly Dictionary<string, PropertyChangedEventArgs> eventArgCache;
        private const string ERROR_MSG = "{0} is not a public property of {1}";

        #endregion // Data

        #region Constructors

        static BindableObject()
        {
            eventArgCache = new Dictionary<string, PropertyChangedEventArgs>();
        }

        protected BindableObject()
        {
            RaisePropertyNotifications = true;
        }

        #endregion // Constructors

        #region Public Members


        public bool RaisePropertyNotifications { get; set; }

        /// <summary>
        /// Raised when a public property of this object is set.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns an instance of PropertyChangedEventArgs for 
        /// the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to create event args for.
        /// </param>		
        public static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException(
                    "propertyName cannot be null or empty.");

            PropertyChangedEventArgs args;

            // Get the event args from the cache, creating them
            // and adding to the cache if necessary.
            lock (typeof(BindableObject))
            {
                bool isCached = eventArgCache.ContainsKey(propertyName);
                if (!isCached)
                {
                    eventArgCache.Add(
                        propertyName,
                        new PropertyChangedEventArgs(propertyName));
                }

                args = eventArgCache[propertyName];
            }

            return args;
        }

        #endregion // Public Members

        #region Protected Members

        /// <summary>
        /// Derived classes can override this method to
        /// execute logic after a property is set. The 
        /// base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected virtual void AfterPropertyChanged(string propertyName)
        {
        }

        /// <summary>
        /// Attempts to raise the PropertyChanged event, and 
        /// invokes the virtual AfterPropertyChanged method, 
        /// regardless of whether the event was raised or not.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (RaisePropertyNotifications)
            {
                this.VerifyProperty(propertyName);

                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    PropertyChangedEventArgs args = GetPropertyChangedEventArgs(propertyName);
                    handler(this, args);
                }

                this.AfterPropertyChanged(propertyName);
            }
        }

        protected void SetProperty<T>(ref T field, T value, Expression<Func<T>> propertyExpression)
        {
            if (field == null ? value != null : !field.Equals(value))
            {
                field = value;
                this.RaisePropertyChanged(propertyExpression.ToPropertyName());
            }
        }

        protected void SetProperty<T>(ref T property, T value, Action onPropertyChanged = null, [CallerMemberName]string propertyname = "")
        {
            if (!Equals(property, value))
            {
                property = value;
                RaisePropertyChanged(propertyname);
                onPropertyChanged?.Invoke();
            }
        }

        #endregion // Protected Members

        #region Private Helpers

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            Type type = this.GetType();

            // Look for a public property with the specified name.
            PropertyInfo propInfo = type.GetProperty(propertyName);

            if (propInfo == null)
            {
                // The property could not be found,
                // so alert the developer of the problem.

                string msg = string.Format(
                    ERROR_MSG,
                    propertyName,
                    type.FullName);

                Debug.Assert(false, msg);
            }
        }

        #endregion // Private Helpers
    }

    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications 
    /// and has a DisplayName property.  This class is abstract.
    /// </summary>
    public abstract class ViewModelBase : BindableObject, IDisposable
    {
        // Todo: move IDisposable implementation to base class

        #region Constructor

        protected ViewModelBase()
        {
        }

        // #DEBUG  Use finalizer to test that ViewModel objects are properly garbage collected.

        ~ViewModelBase()
        {
            if (String.IsNullOrEmpty(DisplayName))
            {
                DisplayName = this.GetType().FullName;
            }

            Debug.WriteLine(String.Format("Destroying {0}", DisplayName));
        }

        #endregion // Constructor

        #region DisplayName

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion // DisplayName

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }


        #endregion // IDisposable Members
    }

    public static class INotifyPropertyChangedExtensions
    {
        public static string ToPropertyName<T>(this Expression<Func<T>> @this)
        {
            var @return = string.Empty;
            if (@this != null)
            {
                var memberExpression = @this.Body as MemberExpression;
                if (memberExpression != null)
                {
                    @return = memberExpression.Member.Name;
                }
            }
            return @return;
        }

        public static string GetPropertyName(this MethodBase methodBase)
        {
            return methodBase.Name.Substring(4);
        }
    }
}