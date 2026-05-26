using System.Linq.Expressions;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public sealed class StepAreaBuilder<TModel> : IDisposable
    {
        private readonly IHtmlHelper<TModel> _htmlHelper;
        private readonly StepAreaBuilder _innerBuilder;

        public StepAreaBuilder(IHtmlHelper<TModel> htmlHelper)
        {
            _htmlHelper = htmlHelper;
            _innerBuilder = new StepAreaBuilder(htmlHelper.ViewContext.Writer, htmlHelper);
        }

        public StepAreaBuilder<TModel> SetId(string id)
        {
            _innerBuilder.SetId(id);
            return this;
        }

        public StepAreaBuilder<TModel> AddClass(string cssClass)
        {
            _innerBuilder.AddClass(cssClass);
            return this;
        }

        public StepAreaBuilder<TModel> SetAttribut(string name, string? value)
        {
            _innerBuilder.SetAttribute(name, value);
            return this;
        }

        public StepAreaBuilder<TModel> SetData(string name, string? value)
        {
            _innerBuilder.SetData(name, value);
            return this;
        }

        public StepAreaBuilder<TModel> Numbered(bool value = true)
        {
            _innerBuilder.Numbered(value);
            return this;
        }

        public StepAreaBuilder<TModel> UseFieldsets(bool value = true)
        {
            _innerBuilder.UseFieldsets(value);
            return this;
        }

        public StepAreaBuilder<TModel> EnableStepWhenValue(string targetStepId, string fieldId)
        {
            _innerBuilder.EnableStepWhenValue(targetStepId, fieldId);
            return this;
        }

        public StepAreaBuilder<TModel> EnableStepWhenValue<TProperty>(string targetStepId, Expression<Func<TModel, TProperty>> expression)
        {
            _innerBuilder.EnableStepWhenValue(targetStepId, ResolveFieldId(expression));
            return this;
        }

        public StepAreaBuilder<TModel> EnableStepWhenChecked(string targetStepId, string fieldId)
        {
            _innerBuilder.EnableStepWhenChecked(targetStepId, fieldId);
            return this;
        }

        public StepAreaBuilder<TModel> EnableStepWhenChecked<TProperty>(string targetStepId, Expression<Func<TModel, TProperty>> expression)
        {
            _innerBuilder.EnableStepWhenChecked(targetStepId, ResolveFieldId(expression));
            return this;
        }

        public StepAreaBuilder<TModel> EnableStepWhenAllValues(string targetStepId, params string[] fieldIds)
        {
            _innerBuilder.EnableStepWhenAllValues(targetStepId, fieldIds);
            return this;
        }

        public StepAreaBuilder<TModel> EnableStepWhenAllValues(string targetStepId, params Expression<Func<TModel, object?>>[] expressions)
        {
            _innerBuilder.EnableStepWhenAllValues(targetStepId, expressions.Select(ResolveFieldId).ToArray());
            return this;
        }

        public StepAreaBuilder Begin()
        {
            return _innerBuilder.Begin();
        }

        public IHtmlContent Render()
        {
            return _innerBuilder.Render();
        }

        public void Dispose()
        {
            _innerBuilder.Dispose();
        }

        private string ResolveFieldId<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return _htmlHelper.IdFor(expression);
        }
    }
}
