<img src="https://raw.githubusercontent.com/sungaila/PresentationBase/master/Icon.png" align="left" width="64" height="64" alt="PresentationBase Logo">

# PresentationBase
A lightweight MVVM implementation for WPF ([Windows Presentation Foundation](https://en.wikipedia.org/wiki/Windows_Presentation_Foundation)) targeting both **.NET Framework** and **.NET Core**.

It contains base implementations for *view models* (and their *commands*), frequently used *value converters*, useful *XAML markup extensions* and more. I consider these as a bare minimum when I start professional or free time WPF projects.

A sample project can be found here: [SUBSTitute](https://github.com/sungaila/SUBSTitute). Feel free to grab it from [NuGet.org](https://www.nuget.org/packages/PresentationBase) or to fork it for your own needs!

## Examples
Here are some examples for using PresentationBase in your project.

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

### ViewModels ↔ Data Transfer Objects conversion
```csharp
// C# code of DTO class
public class AwesomeTransferDataObject
{
    public string PersonName { get; set; }

    public int PersonAge { get; set; }
}
```

```csharp
// C# code of your ViewModel class
[Dto(typeof(AwesomeTransferDataObject))]
public class AwesomeViewModel : ViewModel
{
    private string _name;

    [DtoProperty(nameof(AwesomeTransferDataObject.PersonName))]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private int _age;

    [DtoProperty(nameof(AwesomeTransferDataObject.PersonAge))]
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }
}
```

```csharp
// C# code of the conversion
var dto = new AwesomeTransferDataObject { PersonName = "John" };
var viewModel = dto.ToViewModel<AwesomeViewModel>();
if (viewModel.Name == "John")
    viewModel.Age = 33;
var dto2 = viewModel.ToDto<AwesomeTransferDataObject>();
```
