//namespace DecoratorDesignPatternTests.ARCHIVED;

//public class HighlightElementDriver : DriverDecorator
//{
//    public override string Url => throw new NotImplementedException();

//    public HighlightElementDriver(IDriver driver)
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
//        ScrollIntoView(elements.First());
//        return elements;
//    }

//    private void ScrollIntoView(IComponent element)
//    {
//        try
//        {
//            // Get original styles
//            var originalElementBackground = element.WrappedElement.GetCssValue("background");
//            var originalElementColor = element.WrappedElement.GetCssValue("color");
//            var originalElementOutline = element.WrappedElement.GetCssValue("outline");

//            // JavaScript to modify and then revert the element's style
//            var script = @"
//            let defaultBG = arguments[0].style.backgroundColor;
//            let defaultOutline = arguments[0].style.outline;
//            arguments[0].style.backgroundColor = '#FDFF47';
//            arguments[0].style.outline = '#f00 solid 2px';
        
//            setTimeout(function()
//            {
//                arguments[0].style.backgroundColor = defaultBG;
//                arguments[0].style.outline = defaultOutline;
//            }, 500);";

//            // Execute the script
//            Driver.ExecuteScript(script, element.WrappedElement);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//    }
//}
