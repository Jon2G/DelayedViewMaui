# DelayedViewMaui

 [![NuGet version (DelayedViewMaui)](https://img.shields.io/nuget/v/DelayedViewMaui.svg)](https://www.nuget.org/packages/DelayedViewMaui/)

 A view that delays content rendering to optimize page push time


FlipView for MAUI

A ContentView with a customizable flip animation.

[![NuGet version (FlipView-MAUI)](https://img.shields.io/nuget/v/FlipView-MAUI.svg)](https://www.nuget.org/packages/FlipView-MAUI/)


![Sample](https://raw.githubusercontent.com/Jon2G/DelayedViewMaui/main/sample.gif)

Usage:
```
xmlns:dv="clr-namespace:DelayedViewMaui;assembly=DelayedViewMaui"
```

```
<dv:DelayedView>
    <dv:DelayedView.View>
        [....] Any View
    </dv:DelayedView.View>
</dv:DelayedView>
```

