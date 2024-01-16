//namespace DecoratorDesignPatternTests.ARCHIVED;

//public abstract class DriverDecorator : IDriver
//{
//    protected readonly IDriver Driver;

//    protected DriverDecorator(IDriver driver)
//    {
//        Driver = driver;
//    }

//    public override void Start(Browser browser)
//    {
//        Driver?.Start(browser);
//    }

//    public override void Quit()
//    {
//        Driver?.Quit();
//    }

//    public override void GoToUrl(string url)
//    {
//        Driver?.GoToUrl(url);
//    }

//    public override IComponent FindComponent(By locator)
//    {
//        return Driver?.FindComponent(locator);
//    }

//    public override List<IComponent> FindComponents(By locator)
//    {
//        return Driver?.FindComponents(locator);
//    }

//    public override void Refresh()
//    {
//        Driver?.Refresh();
//    }

//    public override bool ComponentExists(IComponent component)
//    {
//        return (bool)Driver?.ComponentExists(component);
//    }

//    public override void DeleteAllCookies()
//    {
//        Driver?.DeleteAllCookies();
//    }

//    public override void ExecuteScript(string script, params object[] args)
//    {
//        Driver?.ExecuteScript(script, args);
//    }
//}
