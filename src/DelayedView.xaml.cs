using AsyncAwaitBestPractices;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DelayedView;

[ContentProperty(nameof(View))]
public partial class DelayedView : ContentView
{
    public static bool Debug { get; set; } = false;
    public static readonly BindableProperty ViewProperty = BindableProperty.Create(
        nameof(View),
        typeof(IView),
        typeof(DelayedView),
        propertyChanged: OnViewPropertyChanged
    );
    public View? View
    {
        get => (View?)GetValue(DelayedView.ViewProperty);
        set => SetValue(DelayedView.ViewProperty, value);
    }
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (nameof(IsVisible) == propertyName)
        {
            SetView();
        }
    }
    static void OnViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var pageBase = (DelayedView)bindable;
        if (newValue is View)
        {
            pageBase.SetView();
        }
    }

    public int DelayInMilliseconds { get; set; } = 200;

    public DelayedView()
    {
        InitializeComponent();
    }

    public void SetView()
    {
        try
        {
            if (IsLoaded || !IsVisible)
            {
                return;
            }

            Task.Run(async () =>
            {
                await Task.Delay(DelayInMilliseconds);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    base.Content = this.View;
                    OnDelayCompleted();
                });
            }).SafeFireAndForget();
        }
        catch (Exception ex)
        {
            if (Debug)
            {
                Debugger.Log(0, "DelayedView", $"{ex}\n");
            }
        }
    }

    protected virtual void OnDelayCompleted()
    {
        if (Debug)
        {
            Debugger.Log(0, "DelayedView", $"OnDelayCompleted: {DelayInMilliseconds} {this.GetType()}\n");
        }
    }
}