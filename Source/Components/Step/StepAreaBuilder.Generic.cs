#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Linq;
using System.Linq.Expressions;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Builds and renders the step visual component for Razor views.
    /// </summary>
    /// <typeparam name="TModel">The model type used by the component builder.</typeparam>
    public sealed class StepAreaBuilder<TModel> : IDisposable
    {
        #region Instance fields and properties

        private readonly IHtmlHelper<TModel> _htmlHelper;
        private readonly StepAreaBuilder _innerBuilder;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StepAreaBuilder" /> class.
        /// </summary>
        /// <param name="htmlHelper">The typed Razor HTML helper used to resolve model expressions.</param>
        public StepAreaBuilder(IHtmlHelper<TModel> htmlHelper)
        {
            _htmlHelper = htmlHelper;
            _innerBuilder = new StepAreaBuilder(htmlHelper.ViewContext.Writer, htmlHelper);
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds class to the step component.
        /// </summary>
        /// <param name="cssClass">The CSS class appended to the rendered component.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> AddClass(string cssClass)
        {
            _innerBuilder.AddClass(cssClass);
            return this;
        }

        /// <summary>
        ///     Starts the step rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder Begin()
        {
            return _innerBuilder.Begin();
        }

        /// <summary>
        ///     Adds an enablement rule for step when all values in the step component.
        /// </summary>
        /// <param name="targetStepId">The step id affected by the rule.</param>
        /// <param name="fieldIds">The source field ids that must all contain values.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> EnableStepWhenAllValues(string targetStepId, params string[] fieldIds)
        {
            _innerBuilder.EnableStepWhenAllValues(targetStepId, fieldIds);
            return this;
        }

        /// <summary>
        ///     Adds an enablement rule for step when all values in the step component.
        /// </summary>
        /// <param name="targetStepId">The step id affected by the rule.</param>
        /// <param name="expressions">The model expressions used to resolve source field ids.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> EnableStepWhenAllValues(string targetStepId, params Expression<Func<TModel, object?>>[] expressions)
        {
            _innerBuilder.EnableStepWhenAllValues(targetStepId, expressions.Select(ResolveFieldId).ToArray());
            return this;
        }

        /// <summary>
        ///     Adds an enablement rule for step when checked in the step component.
        /// </summary>
        /// <param name="targetStepId">The step id affected by the rule.</param>
        /// <param name="fieldId">The source field id observed by the rule.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> EnableStepWhenChecked(string targetStepId, string fieldId)
        {
            _innerBuilder.EnableStepWhenChecked(targetStepId, fieldId);
            return this;
        }

        /// <summary>
        ///     Adds an enablement rule that activates a step when the model field is checked.
        /// </summary>
        /// <typeparam name="TProperty">The model property type resolved by the expression.</typeparam>
        /// <param name="targetStepId">The step id affected by the rule.</param>
        /// <param name="expression">The model expression used to resolve the source field id.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> EnableStepWhenChecked<TProperty>(string targetStepId, Expression<Func<TModel, TProperty>> expression)
        {
            _innerBuilder.EnableStepWhenChecked(targetStepId, ResolveFieldId(expression));
            return this;
        }

        /// <summary>
        ///     Adds an enablement rule for step when value in the step component.
        /// </summary>
        /// <param name="targetStepId">The step id affected by the rule.</param>
        /// <param name="fieldId">The source field id observed by the rule.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> EnableStepWhenValue(string targetStepId, string fieldId)
        {
            _innerBuilder.EnableStepWhenValue(targetStepId, fieldId);
            return this;
        }

        /// <summary>
        ///     Adds an enablement rule that activates a step when the model field has a value.
        /// </summary>
        /// <typeparam name="TProperty">The model property type resolved by the expression.</typeparam>
        /// <param name="targetStepId">The step id affected by the rule.</param>
        /// <param name="expression">The model expression used to resolve the source field id.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> EnableStepWhenValue<TProperty>(string targetStepId, Expression<Func<TModel, TProperty>> expression)
        {
            _innerBuilder.EnableStepWhenValue(targetStepId, ResolveFieldId(expression));
            return this;
        }

        /// <summary>
        ///     Configures numbered behavior for the step component.
        /// </summary>
        /// <param name="value">The attribute value to render.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> Numbered(bool value = true)
        {
            _innerBuilder.Numbered(value);
            return this;
        }

        /// <summary>
        ///     Renders the configured step component as HTML content.
        /// </summary>
        /// <returns>The rendered step area content.</returns>
        public IHtmlContent Render()
        {
            return _innerBuilder.Render();
        }

        private string ResolveFieldId<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return _htmlHelper.IdFor(expression);
        }

        /// <summary>
        ///     Configures an HTML attribute on the step area component.
        /// </summary>
        /// <param name="name">The attribute name without the generated prefix.</param>
        /// <param name="value">True to render numbered steps; false to hide step numbers.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> SetAttribut(string name, string? value)
        {
            _innerBuilder.SetAttribute(name, value);
            return this;
        }

        /// <summary>
        ///     Configures a data attribute on the step area component.
        /// </summary>
        /// <param name="name">The attribute name without the generated prefix.</param>
        /// <param name="value">True to render steps as fieldsets; false to render standard containers.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> SetData(string name, string? value)
        {
            _innerBuilder.SetData(name, value);
            return this;
        }

        /// <summary>
        ///     Configures the id for the step component.
        /// </summary>
        /// <param name="id">The HTML id or stable component id.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> SetId(string id)
        {
            _innerBuilder.SetId(id);
            return this;
        }

        /// <summary>
        ///     Configures whether the fieldsets option is used by the step component.
        /// </summary>
        /// <param name="value">The attribute value to render.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder<TModel> UseFieldsets(bool value = true)
        {
            _innerBuilder.UseFieldsets(value);
            return this;
        }

        #region From interface IDisposable

        /// <summary>
        ///     Completes the active step rendering or capture scope.
        /// </summary>
        public void Dispose()
        {
            _innerBuilder.Dispose();
        }

        #endregion

        #endregion
    }
}