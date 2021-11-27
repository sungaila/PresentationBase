# ![PresentationBase Logo](https://raw.githubusercontent.com/sungaila/PresentationBase/master/Icon_64.png) PresentationBase

[![Azure DevOps builds (branch)](https://img.shields.io/azure-devops/build/sungaila/69f6dcb7-b8ec-4fe8-b5b7-1923483c91f6/2/master?style=flat-square)](https://dev.azure.com/sungaila/PresentationBase/_build/latest?definitionId=2&branchName=master)
[![Azure DevOps tests (branch)](https://img.shields.io/azure-devops/tests/sungaila/PresentationBase/2/master?style=flat-square)](https://dev.azure.com/sungaila/PresentationBase/_build/latest?definitionId=2&branchName=master)
[![SonarCloud Quality Gate](https://img.shields.io/sonar/quality_gate/sungaila_PresentationBase?server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=sungaila_PresentationBase)
[![NuGet version](https://img.shields.io/nuget/v/PresentationBase.svg?style=flat-square)](https://www.nuget.org/packages/PresentationBase/)
[![NuGet downloads](https://img.shields.io/nuget/dt/PresentationBase.svg?style=flat-square)](https://www.nuget.org/packages/PresentationBase/)
[![GitHub license](https://img.shields.io/github/license/sungaila/PresentationBase?style=flat-square)](https://github.com/sungaila/PresentationBase/blob/master/LICENSE)

A lightweight MVVM implementation for WPF ([Windows Presentation Foundation](https://en.wikipedia.org/wiki/Windows_Presentation_Foundation)) targeting both **.NET Framework** and **.NET**.

It contains base implementations for *view models* (and their *commands*), frequently used *value converters*, useful *XAML markup extensions* and more. I consider these as a bare minimum when I start professional or free time WPF projects.

## Examples
Take a look at the [Quick start in the wiki](https://github.com/sungaila/PresentationBase.Core/wiki). Here are some basic examples for using PresentationBase:

### ViewModels with bindable properties
```csharp
// C# code
public class AwesomeViewModel : ViewModel
{
    private string _name;
  
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}
```

```xaml
<!-- XAML -->
<TextBox Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" />
```

### ... and with property validation
```csharp
// C# code
public class AwesomeViewModel : ViewModel
{
    private string _name;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, NameValidation);
    }

    private IEnumerable<string> NameValidation(string value)
    {
        if (string.IsNullOrEmpty(value))
            yield return "Name cannot be null or empty!";
        else if (value == "sungaila")
            yield return "Name cannot be stupid!";
    }
}
```

```xaml
<!-- XAML -->
<TextBox Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" />
```

### ViewModel collections
```csharp
// C# code
public class AwesomeViewModel : ViewModel
{
    public ObservableViewModelCollection<ChildViewModel> Children { get; }
    
    public AwesomeViewModel()
    {
        Children = new ObservableViewModelCollection<ChildViewModel>(this);
        Children.Add(new ChildViewModel { Nickname = "Blinky" });
        Children.Add(new ChildViewModel { Nickname = "Pinky" });
        Children.Add(new ChildViewModel { Nickname = "Inky" });
        Children.Add(new ChildViewModel { Nickname = "Clyde" });
    }
}
```

```xaml
<!-- XAML -->
<ListView ItemsSource="{Binding Children}">
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Nickname}" />
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

### ... and collection compositions
```csharp
// C# code
public class AwesomeViewModel : ViewModel
{
    public ObservableViewModelCollection<ChildViewModel> Children { get; }

    public ObservableViewModelCollection<PersonViewModel> People { get; }

    public CompositeViewModelCollection<ViewModel> Composition { get; }
    
    public AwesomeViewModel()
    {
        Children = new ObservableViewModelCollection<ChildViewModel>(this);
        Children.Add(new ChildViewModel { Nickname = "Blinky" });
        Children.Add(new ChildViewModel { Nickname = "Pinky" });
        Children.Add(new ChildViewModel { Nickname = "Inky" });
        Children.Add(new ChildViewModel { Nickname = "Clyde" });

        People = new ObservableViewModelCollection<PersonViewModel>(this);
        People.Add(new PersonViewModel { Nickname = "Kevin" });
        People.Add(new PersonViewModel { Nickname = "Tommy" });

        Composition = new CompositeViewModelCollection<ViewModel>();
        Composition.Add(Children);
        Composition.Add(People);
    }
}
```

```xaml
<!-- XAML -->
<ListView ItemsSource="{Binding Composition}">
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Nickname}" />
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

### Commands
Your command can be defined anywhere you want (as long as its assembly is referenced by the WPF application). Please note that a parameterless constructor (or none at all) is needed.
```csharp
// C# code
public class AlertCommand : ViewModelCommand<AwesomeViewModel>
{
    public override void Execute(AwesomeViewModel parameter)
    {
        System.Windows.MessageBox.Show("You just clicked that button.");
    }

    public override bool CanExecute(AwesomeViewModel parameter)
    {
        return parameter.Name != "John Doe";
    }
}
```
The only reference needed is the `x:Type` in XAML. **Important:** Make sure to write `CommandParameter` before `Command` to avoid problems with `CanExecute`. Consider to create an issue for the .NET Core team ([like this one](https://github.com/dotnet/wpf/issues/316)) if you want this WPF bug fixed.
```xaml
<!-- XAML -->
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:Core="clr-namespace:PresentationBase;assembly=PresentationBase"
        xmlns:local="clr-namespace:WpfApp">
        
        <Button CommandParameter="{Binding}"
                Command="{Core:CommandBinding {x:Type local:AlertCommand}}" />
</Window>
```

### ... and async commands
```csharp
// C# code
public class AlertCommandAsync : ViewModelCommandAsync<AwesomeViewModel>
{
    protected override async Task ExecutionAsync(AwesomeViewModel parameter)
    {
        await Task.Run(() =>
        {
            System.Threading.Thread.Sleep(2000);
            System.Windows.MessageBox.Show("You clicked that button two seconds ago.");
        });
    }
}
```

```xaml
<!-- XAML -->
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:Core="clr-namespace:PresentationBase;assembly=PresentationBase"
        xmlns:local="clr-namespace:WpfApp">
        
        <Button CommandParameter="{Binding}"
                Command="{Core:CommandBinding {x:Type local:AlertCommandAsync}}" />
</Window>
```

### ValueConverters
```xaml
<!-- XAML -->
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:Converters="clr-namespace:PresentationBase.Converters;assembly=PresentationBase"
        xmlns:local="clr-namespace:WpfApp">
        
        <TextBox Visibility="{Binding Name, Converter={Converters:NullToVisibilityConverter}}" />
</Window>
```
