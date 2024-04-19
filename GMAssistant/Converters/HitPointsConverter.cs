using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAssistant.Model;

namespace GMAssistant.Converters
{
	internal class HitPointsConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			int? currentHP = (int)value;
			//int? MaxHP = (int)parameter;
			return currentHP/5;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
