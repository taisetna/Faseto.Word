using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Faseto.Word
{
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private Members
        private static T m_Converter = null;
        #endregion

        #region Markup Extension Methods
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return m_Converter ?? (m_Converter = new T());
        }
        #endregion

        #region Value Conveter Methods
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion
    }
}
