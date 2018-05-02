using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KimO.Helper
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes, string imageSrc)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imageSrc));
            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());
        }

        public static IHtmlString MenuActionLink(this HtmlHelper htmlHelper, string name, string linkText, object htmlAttributes, object htmlAttributesIcon)
        {
            string[] controllerAction = linkText.ToString().Split('/');


            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var ss = new TagBuilder("span");
            ss.SetInnerText(UlakNot.Web.Resources.Lang.ResourceManager.GetString(name));

            var cc = new TagBuilder("i");
            cc.MergeAttributes(new RouteValueDictionary(htmlAttributesIcon));

            string join = cc.ToString(TagRenderMode.Normal) + ss.ToString(TagRenderMode.Normal);

            var a = new TagBuilder("a") { InnerHtml = join };
            a.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            //RouteValue olmadan bak Action gelmiyor 
            a.Attributes["href"] = urlHelper.Action(controllerAction[1], controllerAction[0]);

            var anchor = new TagBuilder("li") { InnerHtml = a.ToString(TagRenderMode.Normal) };

            return MvcHtmlString.Create(anchor.ToString());
        }

        public static IHtmlString MenuActionLink(this HtmlHelper htmlHelper, string linkText, object htmlAttributesArrow, object htmlAttributesSpan, object htmlAttributesIcon, Dictionary<string, string> childs, object htmlAttributesAction, object htmlAttributesUl, object htmlAttributesLi)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var ss = new TagBuilder("i");
            ss.MergeAttributes(new RouteValueDictionary(htmlAttributesArrow));

            var cc = new TagBuilder("span") { InnerHtml = ss.ToString(TagRenderMode.Normal) };
            cc.MergeAttributes(new RouteValueDictionary(htmlAttributesSpan));

            var cv = UlakNot.Web.Resources.Lang.ResourceManager.GetString(linkText) + cc.ToString(TagRenderMode.Normal);
            var span = new TagBuilder("span") { InnerHtml = cv };

            var icon = new TagBuilder("i");
            icon.MergeAttributes(new RouteValueDictionary(htmlAttributesIcon));

            string join = icon.ToString(TagRenderMode.Normal) + span.ToString(TagRenderMode.Normal);

            var first = new TagBuilder("a") { InnerHtml = join };
            first.Attributes["href"] = "javascript: void(0);";
            first.MergeAttributes(new RouteValueDictionary(htmlAttributesAction));

            string[] controllerAction;
            string childSum = "";

            foreach (string key in childs.Keys)
            {
                controllerAction = childs[key].ToString().Split('/');
                var childA = new TagBuilder("a");
                //RouteValue olmadan bak Action gelmiyor 
                childA.Attributes["href"] = urlHelper.Action(controllerAction[1], controllerAction[0]);
                childA.SetInnerText(key);

                var childLi = new TagBuilder("li") { InnerHtml = childA.ToString(TagRenderMode.Normal) };
                childSum += childLi;

                Array.Clear(controllerAction, 0, controllerAction.Length);
            }

            var childLu = new TagBuilder("ul") { InnerHtml = childSum };
            childLu.MergeAttributes(new RouteValueDictionary(htmlAttributesUl));

            string second = childLu.ToString(TagRenderMode.Normal);

            var anchor = new TagBuilder("li") { InnerHtml = first + second };
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributesLi));

            return MvcHtmlString.Create(anchor.ToString());
        }
    }
}