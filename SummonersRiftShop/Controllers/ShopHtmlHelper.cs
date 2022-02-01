using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using SummonersRiftShop.Models;

namespace SummonersRiftShop.Views.Home
{
    public static class ShopHtmlHelper
    {
        public static HtmlString GetURL(string itemName)
        {
            //Debug.WriteLine(itemName);
            itemName = itemName.Replace(" ", "_");
            var result = $"/Images/Items/{itemName}.png";
            return new HtmlString(result);
        }

        public static HtmlString GetEffects(string itemEffects)
        {
            string result = "<table class='effects's><tr class='effects'><td class='effects'>";
            itemEffects = itemEffects.Replace("\n", "</td></tr><tr class='effects'><td class='effects'>");
            result = $"{result}{itemEffects}</td></tr></table>";
            return new HtmlString(result);
        }
    }
}
