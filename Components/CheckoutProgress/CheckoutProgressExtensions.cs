using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper and fluent extension methods for checkout progress components.
    /// </summary>
    public static class CheckoutProgressExtensions
    {
        /// <summary>
        /// Creates or renders the checkout progress component through the checkout progress helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static CheckoutProgressBuilder CheckoutProgress(this IHtmlHelper html)
        {
            return new CheckoutProgressBuilder(html.ViewContext.Writer, html);
        }
        /// <summary>
        /// Creates or renders the checkout progress component through the checkout steps helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static CheckoutProgressBuilder CheckoutSteps(this IHtmlHelper html)
        {
            return html.CheckoutProgress();
        }
        /// <summary>
        /// Creates or renders the checkout progress component through the checkout progress steps helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="titles">The titles value.</param>
        /// <returns>The configured builder instance.</returns>
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
