//namespace DecoratorDesignPatternTests.ARCHIVED;

//public class ScrollIntoViewDriver : DriverDecorator
//{
//    public override string Url => throw new NotImplementedException();

//    public ScrollIntoViewDriver(IDriver driver)
//    : base(driver)
//    {
//    }

//    public override IComponent FindComponent(By locator)
//    {
//        var element = Driver?.FindComponent(locator);
//        ScrollIntoView(element);
//        return element;
//    }

//    public override List<IComponent> FindComponents(By locator)
//    {
//        var elements = Driver?.FindComponents(locator);
//        if (elements.Any())
//        {
//            ScrollIntoView(elements.Last());
//        }
//        return elements;
//    }

//    private void ScrollIntoView(IComponent element)
//    {
//        Driver.ExecuteScript("arguments[0].scrollIntoView(true);", element.WrappedElement);
//    }
//}
