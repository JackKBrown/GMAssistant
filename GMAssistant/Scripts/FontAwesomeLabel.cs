using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAssistant.Scripts
{
	public class FontAwesomeLabel : Label
	{
		public FontAwesomeLabel()
		{
			Populate();
		}

		public bool UseSolidFont
		{
			get { return (bool)GetValue(UseSolidFontProperty); }
			set { SetValue(UseSolidFontProperty, value); }
		}

		public static readonly BindableProperty UseSolidFontProperty =
			BindableProperty.Create(nameof(UseSolidFont), typeof(bool), typeof(FontAwesomeLabel),
				defaultValue: false,
				propertyChanged: (bindable, oldValue, newValue) =>
				{

					((FontAwesomeLabel)bindable).Populate();
				});

		private void Populate()
		{

			this.FontFamily = UseSolidFont ? "FASolid" : "FASolid";

		}
	}
}
