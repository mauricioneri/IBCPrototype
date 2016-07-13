using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using IBC.Models.Views;
using System.Linq.Expressions;
using System.Text;
namespace IBC.Helpers
{

    public static class IBCHtmlHelper
    {
        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string actionName)
        {
            return htmlHelper.ActionLink(linkText: linkText, actionName: actionName, controllerName: null, routeValues: null, htmlAttributes: new { @class = "btn btn-default" });
        }

        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return htmlHelper.ActionLink(linkText: linkText, actionName: actionName, controllerName: controllerName, routeValues: null, htmlAttributes: new { @class = "btn btn-default" });
        }

        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return htmlHelper.ActionLink(linkText: linkText, actionName: actionName, controllerName: controllerName, routeValues: routeValues, htmlAttributes: new { @class = "btn btn-default" });
        }

        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, string glyphiconSufix)
        {
            return htmlHelper.ActionButtonGlyphicon(linkText, actionName, controllerName, routeValues, glyphiconSufix);
        }


        public static MvcHtmlString ActionButtonEdit(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return ActionButtonEdit(htmlHelper, linkText, actionName, null, routeValues);
        }

        public static MvcHtmlString ActionButtonEdit(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return ActionButtonGlyphicon(htmlHelper, linkText, actionName, controllerName, routeValues, "E");
        }


        public static MvcHtmlString ActionButtonEditBig(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return ActionButtonEditBig(htmlHelper, linkText, actionName, null, routeValues);
        }

        public static MvcHtmlString ActionButtonEditBig(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return ActionButtonGlyphicon(htmlHelper, linkText, actionName, controllerName, routeValues, "E", "");
        }


        public static MvcHtmlString ActionButtonDetails(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return ActionButtonDetails(htmlHelper, linkText, actionName, null, routeValues);
        }

        public static MvcHtmlString ActionButtonDetails(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return ActionButtonGlyphicon(htmlHelper, linkText, actionName, controllerName, routeValues, "D");
        }


        public static MvcHtmlString ActionButtonDelete(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return ActionButtonDelete(htmlHelper, linkText, actionName, null, routeValues);
        }

        public static MvcHtmlString ActionButtonDelete(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return ActionButtonGlyphicon(htmlHelper, linkText, actionName, controllerName, routeValues, "K");
        }


        public static MvcHtmlString ActionButtonBack(this HtmlHelper htmlHelper, string linkText, string actionName)
        {
            return ActionButtonBack(htmlHelper, linkText, actionName, null, null);
        }

        public static MvcHtmlString ActionButtonBack(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return ActionButtonBack(htmlHelper, linkText, actionName, null, routeValues);
        }

        public static MvcHtmlString ActionButtonBack(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return ActionButtonBack(htmlHelper, linkText, actionName, controllerName, null);
        }

        public static MvcHtmlString ActionButtonBack(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {
            return ActionButtonGlyphicon(htmlHelper, linkText, actionName, controllerName, routeValues, "B", "");
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper)
        {
            return new MvcHtmlString(@"<input type='submit' value='Create' class='btn btn-primary' />");
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper, string value)
        {
            var str = string.Format(@"<input type='submit' value='{0}' class='btn btn-primary' />", value);
            return new MvcHtmlString(str.ToString());
        }

        public static MvcHtmlString PageSizeDropDownList(this HtmlHelper htmlHelper, int? CurrentPageSize)
        {
            int PageSize = (CurrentPageSize ?? 10);
            PageSize = PageSize == 0 ? 10 : PageSize;
            ///TODO: Terminar método
            String str = String.Format(@"<select onchange=""$('form').submit();"" id=""PageSize"" name=""PageSize"">""");
            str += @"<option value=""5""" + (PageSize == 5 ? " selected " : "") + ">5</option>";
            str += @"<option value=""10""" + (PageSize == 10 ? " selected " : "") + ">10</option>";
            str += @"<option value=""15""" + (PageSize == 15 ? " selected " : "") + ">15</option>";
            str += @"<option value=""30""" + (PageSize == 30 ? " selected " : "") + ">30</option>";
            str += @"<option value=""50""" + (PageSize == 50 ? " selected " : "") + ">50</option>";
            str += @"<option value=""100""" + (PageSize == 100 ? " selected " : "") + ">100</option>";
            str += @"</select>";
            return new MvcHtmlString(str.ToString());

        }

        public enum CheckboxListStyle
        {
            blue,
            azure,
            green,
            orange,
            red
        }

        public static MvcHtmlString CheckboxListFor<tModel, TProperty>(
            this HtmlHelper<tModel> helper,
            System.Linq.Expressions.Expression<Func<tModel, TProperty>> expression,
            bool isReadOnly = false,
            CheckboxListStyle style = CheckboxListStyle.azure)
        {
            var valueGetter = expression.Compile();
            var model = valueGetter(helper.ViewData.Model) as List<CheckBoxListView>;
            MemberExpression body = expression.Body as MemberExpression;
            string sHtml = string.Empty;
            if (model != null)
            {
                for (int i = 0; i < model.Count(); i++)
                {

                    var compositeId = string.Format(@"{0}{1}_{2}__State", body.Member.Name, Guid.NewGuid().ToString().Replace(@"-", ""), i);
                    var hiddenState = string.Format(@"{0}_{1}__State", body.Member.Name, i);
                    sHtml += (string.Format(@"<label class=""checkbox {4} ct-{0}"" for=""{1}"">
                                            <input type=""checkbox"" value=""""   id=""{1}"" data-toggle=""checkbox"" {2}/>{3}</label><br />",
                        style,
                        compositeId,
                        model[i].State ? "checked" : "",
                        model[i].Description,
                        isReadOnly ? "disabled" : string.Empty
                        ));

                    sHtml += (helper.Hidden(string.Format(@"{0}[{1}].Id", body.Member.Name, i), model[i].Id));
                    sHtml += (helper.Hidden(string.Format(@"{0}[{1}].Description", body.Member.Name, i), model[i].Description));
                    sHtml += (helper.Hidden(string.Format(@"{0}[{1}].State", body.Member.Name, i), model[i].State));
                    sHtml += (@"<script type=""text/javascript"">");
                    sHtml += (@"if(!window.jQuery)");
                    sHtml += (@"{");
                    sHtml += (@" var script = document.createElement('script');");
                    sHtml += (@"   script.type = 'text/javascript';");
                    sHtml += (@"   script.src = '/scripts/jquery-1.10.2.min.js';");
                    sHtml += (@" document.getElementsByTagName('head')[0].appendChild(script);");
                    sHtml += (@"}");
                    sHtml += (@" $(document).ready(function() {");
                    sHtml += (@"});");
                    sHtml += (@"</script>");



                }
            }
            return new MvcHtmlString(sHtml.ToString());
        }

        #region Private Helpers
        private static MvcHtmlString ActionButtonGlyphicon(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, string SiglaIcon)
        {
            return ActionButtonGlyphicon(htmlHelper, linkText, actionName, controllerName, routeValues, SiglaIcon, "btn-sm");
        }
        private static MvcHtmlString ActionButtonGlyphicon(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, string SiglaIcon, string size)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var strGlyphicon = string.Empty;
            switch (SiglaIcon)
            {
                case "E":
                    strGlyphicon = @"pencil";
                    break;
                case "D":
                    strGlyphicon = @"file";
                    break;
                case "K":
                    strGlyphicon = @"trash";
                    break;
                case "B":
                    strGlyphicon = @"menu-left";
                    break;
                default:
                    strGlyphicon = SiglaIcon;
                    break;
            }

            var str = string.Format(@"<a href=""{0}"" class=""btn btn-default {1}"" data-toggle=""tooltip"" data-placement=""bottom""><span class=""glyphicon glyphicon-{2}"" aria-hidden=""true""></span> {3}</a>",
                urlHelper.Action(actionName: actionName, controllerName: controllerName, routeValues: routeValues)
                , size
                , strGlyphicon
                , linkText);
            return new MvcHtmlString(str.ToString());
        }
        #endregion

        public static MvcHtmlString EditorBSFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
                  System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression,
                  string labelSize = "col-md-2",
                  string inputSize = "col-md-6",
                  bool readOnly = false)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData);
            var propertyName = metadata.PropertyName;
            MemberExpression memberExpression = expression.Body as MemberExpression;

            TagBuilder ElementoControle = new TagBuilder("div");
            TagBuilder ElementoInputGroup = new TagBuilder("div");

            ElementoControle.AddCssClass("form-group");
            ElementoInputGroup.AddCssClass(inputSize);


            if (!helper.ViewData.ModelState.IsValidField(metadata.PropertyName))
            {
                ElementoInputGroup.AddCssClass("has-error has-feedback");
            }
            else if (helper.ViewData.ModelState.Keys.Contains(metadata.PropertyName))
            {
                if (helper.ViewData.ModelState[metadata.PropertyName].Value.RawValue.ToString().Length > 0 && !readOnly)
                {
                    ElementoInputGroup.AddCssClass("has-success");
                }
            }

            if (readOnly)
            {
                ElementoInputGroup.InnerHtml += helper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control", @readonly = "readonly" } });
            }
            else
            {

                ElementoInputGroup.InnerHtml += helper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });
                ElementoInputGroup.InnerHtml += helper.ValidationMessageFor(expression, "", new { @class = "text-danger" });
            }
            ElementoControle.InnerHtml += helper.LabelFor(expression, htmlAttributes: new { @class = string.Format(@"control-label {0}", labelSize) });
            ElementoControle.InnerHtml += ElementoInputGroup.ToString();

            return new MvcHtmlString(ElementoControle.ToString());
        }


        public static MvcHtmlString CheckBoxBsFor<TModel>(this HtmlHelper<TModel> helper,
         System.Linq.Expressions.Expression<Func<TModel, bool>> expression,
         string LabelSize = "col-md-2",
         string InputSize = "col-md-6")
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, bool>(expression, helper.ViewData);
            var propertyName = metadata.PropertyName;
            MemberExpression memberExpression = expression.Body as MemberExpression;

            TagBuilder ElementoControle = new TagBuilder("div");
            TagBuilder ElementoInputGroup = new TagBuilder("div");
            TagBuilder ElementoInputGroupq = new TagBuilder("");

            ElementoInputGroupq.InnerHtml += helper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });

            ElementoControle.AddCssClass("form-group");
            ElementoInputGroup.AddCssClass(InputSize);

            if (helper.ViewData.ModelState.IsValidField(metadata.PropertyName))
            {
                ElementoInputGroup.AddCssClass("has-error has-feedback");
            }

            ElementoInputGroup.InnerHtml += helper.CheckBoxFor(expression, new { @class = @"form-control" });
            ElementoInputGroup.InnerHtml += helper.ValidationMessageFor(expression, "", new { @class = "text-danger" });

            ElementoControle.InnerHtml += helper.LabelFor(expression, htmlAttributes: new { @class = string.Format(@"control-label {0}", LabelSize) });
            ElementoControle.InnerHtml += ElementoInputGroup.ToString();

            return new MvcHtmlString(ElementoControle.ToString());
        }


        public static MvcHtmlString DropDownListBs<TModel, TProperty>(this HtmlHelper<TModel> helper,
         System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string labelSize = "col-md-2",
            string inputSize = "col-md-6",
            bool readOnly = false)
        {




            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData);
            var propertyName = metadata.PropertyName;
            MemberExpression memberExpression = expression.Body as MemberExpression;

            TagBuilder ElementoControle = new TagBuilder("div");
            TagBuilder ElementoInputGroup = new TagBuilder("div");

            ElementoControle.AddCssClass("form-group");
            ElementoInputGroup.AddCssClass(inputSize);


            if (!helper.ViewData.ModelState.IsValidField(metadata.PropertyName))
            {
                ElementoInputGroup.AddCssClass("has-error has-feedback");
            }
            else if (helper.ViewData.ModelState.Keys.Contains(metadata.PropertyName))
            {
                if (helper.ViewData.ModelState[metadata.PropertyName].Value.RawValue.ToString().Length > 0 && !readOnly)
                {
                    ElementoInputGroup.AddCssClass("has-success");
                }
            }



            if (readOnly)
            {
                ElementoInputGroup.InnerHtml += helper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control", @readonly = "readonly" } });
            }
            else
            {
                ElementoInputGroup.InnerHtml += helper.DropDownList(metadata.PropertyName, null, htmlAttributes: new { @class = "form-control" });
                ElementoInputGroup.InnerHtml += helper.ValidationMessageFor(expression, "", new { @class = "text-danger" });
            }
            ElementoControle.InnerHtml += helper.LabelFor(expression, htmlAttributes: new { @class = string.Format(@"control-label {0}", labelSize) });
            ElementoControle.InnerHtml += ElementoInputGroup.ToString();

            return new MvcHtmlString(ElementoControle.ToString());

        }

        public static MvcHtmlString SubmitBsButton(this HtmlHelper htmlHelper, string value = "Salvar", string LabelSize = "col-md-offset-2", string InputSize = "col-md-10", string buttonClass = "btn-default")
        {

            TagBuilder ElementoControle = new TagBuilder("div");
            TagBuilder ElementoInputGroup = new TagBuilder("div");
            TagBuilder ElementoInput = new TagBuilder("input");

            ElementoControle.AddCssClass("form-group");
            ElementoInputGroup.AddCssClass(string.Format(@"{0} {1}", LabelSize, InputSize));
            ElementoInput.AddCssClass(string.Format(@"btn {0}", buttonClass));
            ElementoInput.MergeAttribute("type", "submit");
            ElementoInput.MergeAttribute("value", value);

            ElementoInputGroup.InnerHtml += ElementoInput.ToString();
            ElementoControle.InnerHtml += ElementoInputGroup.ToString();

            return new MvcHtmlString(ElementoControle.ToString());
        }

    }
}