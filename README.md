LocalMessage.Avalonia provides a feature to localize text on UI with `ResourceManager`.

## How to use

### Make Resource.resx

LocalMessage.Avalonia uses localized resource ( `Resource.resx` ).
We have to create resx file for each languages.

By default, LocalMessage.Avalonia looks <code>*AssemblyName*.Properties.Resource</code> embedded resource.

For example, when supporting English(default), Chinese(zh-CN), and Japanese, you need to create `Properties\Resource.resx`, `Properties\Resource.zh-CN.resx` and `Properties\Resource.ja-JP.resx` for the project.

```
.\YourProject\Properties\
    Resource.Designer.cs
    Resource.ja-JP.resx
    Resource.zh-CN.resx
    Resource.resx
```

### Usage in .axaml

By default LocationMessage.Avalonia references the assembly where axaml is defined.

```xml
<UserControl x:Class="***"
        xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:msg="clr-namespace:LocalMessage.Avalonia;assembly=LocalMessage.Avalonia">
    <StackPanel>

        <TextBlock Text="{msg:Loc HelloWorld}"/>

        <TextBlock>
          <TextBlock.Styles>
            <Style Selector="TextBlock">
              <Setter Property="Text" Value="{msg:Loc HelloWorld}"/>
            </Style>
          </TextBlock.Styles>
        </TextBlock>

        <TextBlock Text="{msg:Loc {Binding MsgKey}}"/>

    </StackPanel>
</UserControl>
```

If you change assembly, use attached property `MessageService.AssemblyName`.
If you change resource, use attached property `MessageService.ResourceName`.

```xml
<UserControl x:Class="***"
        xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:msg="clr-namespace:LocalMessage.Avalonia;assembly=LocalMessage.Avalonia"
        
        msg:MessageService.AssemblyName="DemoAvaloniaSubTools"
        msg:MessageService.ResourceName="DemoAvaloniaSubTools.Another.Resource"
        >
  ...
</UserControl>
```

### Usage in code

By default LocationMessage.Avalonia references the assembly where `Loc.Message` is called.
To detect an assembly, LocationMessage.Avalonia use StackTrace.

```cs
// using LocalMessage.Avalonia;

var resourceKey = "HelloWorld";
string message = Loc.Message(resourceKey);
```
