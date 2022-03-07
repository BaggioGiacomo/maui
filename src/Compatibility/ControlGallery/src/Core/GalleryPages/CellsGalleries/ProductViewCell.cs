﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery
{
	public class ProductCellView : StackLayout
	{
		Label _timeLabel;
		Label _brandLabel;
		StackLayout _stack;
		public ProductCellView(string text)
		{
			_stack = new StackLayout();
			_brandLabel = new Label { Text = "BrandLabel", HorizontalTextAlignment = TextAlignment.Center };
			_stack.Children.Add(_brandLabel);


			var frame = new Frame
			{
				Content = _stack,
				BackgroundColor = new[] { DevicePlatform.Android, DevicePlatform.WinUI }.Contains(DeviceInfo.Platform) ? new Color(0.2f) : new Color(1)
			};
			_timeLabel = new Label
			{
				Text = text
			};
			Children.Add(_timeLabel);
			Children.Add(frame);
			Padding = new Size(20, 20);
		}
	}
}
