using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public static class CheckoutProgressExtensions
    {
        public static CheckoutProgressBuilder CheckoutProgress(this IHtmlHelper html)
        {
            return new CheckoutProgressBuilder(html.ViewContext.Writer, html);
        }

        public static CheckoutProgressBuilder CheckoutSteps(this IHtmlHelper html)
        {
            return html.CheckoutProgress();
        }

        public static CheckoutProgressBuilder CheckoutProgressSteps(
            this IHtmlHelper html,
            params string[] titles)
        {
            CheckoutProgressBuilder builder = new CheckoutProgressBuilder(html.ViewContext.Writer, html);

            foreach (string title in titles)
            {
                builder.AddStep(title);
            }

            return builder;
        }
    }
}
