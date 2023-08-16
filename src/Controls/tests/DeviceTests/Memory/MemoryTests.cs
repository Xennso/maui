﻿using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;
using Xunit;

namespace Microsoft.Maui.DeviceTests.Memory;

public class MemoryTests : ControlsHandlerTestBase
{
	protected override MauiAppBuilder ConfigureBuilder(MauiAppBuilder mauiAppBuilder) =>
		base.ConfigureBuilder(mauiAppBuilder)
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<Grid, LayoutHandler>();

				handlers.AddHandler<Border, BorderHandler>();
				handlers.AddHandler<CheckBox, CheckBoxHandler>();
				handlers.AddHandler<DatePicker, DatePickerHandler>();
				handlers.AddHandler<Editor, EditorHandler>();
				handlers.AddHandler<Entry, EntryHandler>();
				handlers.AddHandler<GraphicsView, GraphicsViewHandler>();
				handlers.AddHandler<IContentView, ContentViewHandler>();
				handlers.AddHandler<Image, ImageHandler>();
				handlers.AddHandler<IScrollView, ScrollViewHandler>();
				handlers.AddHandler<Label, LabelHandler>();
				handlers.AddHandler<Picker, PickerHandler>();
				handlers.AddHandler<RefreshView, RefreshViewHandler>();
				handlers.AddHandler<SwipeView, SwipeViewHandler>();
				handlers.AddHandler<TimePicker, TimePickerHandler>();
			});

	[Theory("Handler Does Not Leak")]
	[InlineData(typeof(Border))]
	[InlineData(typeof(CheckBox))]
	[InlineData(typeof(ContentView))]
	[InlineData(typeof(DatePicker))]
	[InlineData(typeof(Editor))]
	[InlineData(typeof(Entry))]
	[InlineData(typeof(GraphicsView))]
	[InlineData(typeof(Image))]
	[InlineData(typeof(Label))]
	[InlineData(typeof(Picker))]
	[InlineData(typeof(RefreshView))]
	[InlineData(typeof(ScrollView))]
	[InlineData(typeof(SwipeView))]
	[InlineData(typeof(TimePicker))]
	public async Task HandlerDoesNotLeak(Type type)
	{
#if ANDROID
		// NOTE: skip certain controls on older Android devices
		if (type == typeof (DatePicker) && !OperatingSystem.IsAndroidVersionAtLeast(30))
				return;
#endif

		WeakReference viewReference = null;
		WeakReference platformViewReference = null;
		WeakReference handlerReference = null;

		await InvokeOnMainThreadAsync(() =>
		{
			var layout = new Grid();
			var view = (View)Activator.CreateInstance(type);
			layout.Add(view);
			var handler = CreateHandler<LayoutHandler>(layout);
			viewReference = new WeakReference(view);
			handlerReference = new WeakReference(view.Handler);
			platformViewReference = new WeakReference(view.Handler.PlatformView);
		});

		await AssertionExtensions.WaitForGC(viewReference, handlerReference, platformViewReference);
		Assert.False(viewReference.IsAlive, $"{type} should not be alive!");
		Assert.False(handlerReference.IsAlive, "Handler should not be alive!");
		Assert.False(platformViewReference.IsAlive, "PlatformView should not be alive!");
	}
}

