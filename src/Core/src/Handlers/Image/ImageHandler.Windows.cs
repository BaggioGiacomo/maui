﻿using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using WImage = Microsoft.UI.Xaml.Controls.Image;

namespace Microsoft.Maui.Handlers
{
	public partial class ImageHandler : ViewHandler<IImage, Image>
	{
		protected override Image CreateNativeView() => new Image();

		protected override void ConnectHandler(Image nativeView)
		{
			base.ConnectHandler(nativeView);

			nativeView.Loaded += OnNativeViewLoaded;
			nativeView.Unloaded += OnNativeViewUnloaded;
		}

		protected override void DisconnectHandler(Image nativeView)
		{
			base.DisconnectHandler(nativeView);

			if (nativeView.XamlRoot != null)
				nativeView.XamlRoot.Changed -= OnXamlRootChanged;

			nativeView.Loaded -= OnNativeViewLoaded;
			nativeView.Unloaded -= OnNativeViewUnloaded;

			SourceLoader.Reset();
		}

		public override bool NeedsContainer =>
			VirtualView?.Background != null ||
			base.NeedsContainer;

		public static void MapBackground(IImageHandler handler, IImage image)
		{
			handler.UpdateValue(nameof(IViewHandler.ContainerView));

			handler.GetWrappedNativeView()?.UpdateBackground(image);
		}

		public static void MapAspect(IImageHandler handler, IImage image) =>
			handler.TypedNativeView?.UpdateAspect(image);

		public static void MapIsAnimationPlaying(IImageHandler handler, IImage image) =>
			handler.TypedNativeView?.UpdateIsAnimationPlaying(image);

		public static void MapSource(IImageHandler handler, IImage image) =>
			MapSourceAsync(handler, image).FireAndForget(handler);

		public static Task MapSourceAsync(IImageHandler handler, IImage image)
		{
			if (handler.NativeView == null)
				return Task.CompletedTask;

			handler.TypedNativeView.Clear();
			return handler.SourceLoader.UpdateImageSourceAsync();
		}

		void OnSetImageSource(ImageSource? obj)
		{
			NativeView.Source = obj;
		}

		void OnNativeViewLoaded(object sender = null!, RoutedEventArgs e = null!)
		{
			if (NativeView?.XamlRoot != null)
			{
				NativeView.XamlRoot.Changed += OnXamlRootChanged;
			}
		}

		void OnNativeViewUnloaded(object sender = null!, RoutedEventArgs e = null!)
		{
			if (NativeView?.XamlRoot != null)
				NativeView.XamlRoot.Changed -= OnXamlRootChanged;
		}

		void OnXamlRootChanged(XamlRoot sender, XamlRootChangedEventArgs args)
		{
			UpdateValue(nameof(IImage.Source));
		}
	}
}