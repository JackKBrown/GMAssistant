namespace GMAssistant.Converters
{
	public class ExpanderArrowConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			//< Color x: Key = "GreenParchment" >#AAFFAA</Color>
			//< Color x: Key = "RedParchment" >#FF8a8a</Color>
			bool expanded = (bool)value;
			if (expanded) { return "^"; }
			return "⌄";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// You probably don't need this, this is used to convert the other way around
			// so from color to yes no or maybe
			throw new NotImplementedException();
		}
	}
}
