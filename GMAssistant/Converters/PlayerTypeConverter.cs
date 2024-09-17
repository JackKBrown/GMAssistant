using System.Diagnostics;

namespace GMAssistant.Converters
{
	public class PlayerTypeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			//< Color x: Key = "GreenParchment" >#AAFFAA</Color>
			//< Color x: Key = "RedParchment" >#FF8a8a</Color>
			Debug.Write(value.ToString().ToUpper());
			switch (value.ToString().ToUpper())
			{
				case "PLAYER":
					return "#AAFFAA";
				case "HAZARD":
					return "#FF8a8a";
				case "ENEMY":
					return "#FF8a8a";
			}

			return "#FFFFDD";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// You probably don't need this, this is used to convert the other way around
			// so from color to yes no or maybe
			throw new NotImplementedException();
		}
	}
}
