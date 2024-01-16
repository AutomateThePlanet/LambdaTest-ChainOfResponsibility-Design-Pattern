//namespace DecoratorDesignPatternTests.ARCHIVED;

//public abstract class ComponentDecorator : IComponent
//{
//    protected readonly IComponent Element;

//    protected ComponentDecorator(IComponent element)
//    {
//        Element = element;
//    }

//    public override By By => Element?.By;
//    public override IWebElement WrappedElement => Element?.WrappedElement;

//    public override string Text => Element?.Text;

//    public override bool? Enabled => Element?.Enabled;

//    public override bool? Displayed => Element?.Displayed;

//    public override void Click()
//    {
//        Element?.Click();
//    }

//    public override string GetAttribute(string attributeName)
//    {
//        return Element?.GetAttribute(attributeName);
//    }

//    public override void TypeText(string text)
//    {
//        Element?.TypeText(text);
//    }
//}
