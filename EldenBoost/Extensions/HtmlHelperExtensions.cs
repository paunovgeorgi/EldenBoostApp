using Ganss.Xss;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldenBoost.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static SelectList EnumToSelectList<TEnum>(this IHtmlHelper htmlHelper) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            var enumSelectList = enumValues.Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            });

            return new SelectList(enumSelectList, "Value", "Text");
        }

        public static string SanitizeHtml(string htmlContent)
        {
            var sanitizer = new HtmlSanitizer();

            // Allow specific tags
            sanitizer.AllowedTags.Add("p");
            sanitizer.AllowedTags.Add("a");
            sanitizer.AllowedTags.Add("b");
            sanitizer.AllowedTags.Add("i");
            sanitizer.AllowedTags.Add("u");
            sanitizer.AllowedTags.Add("strong");
            sanitizer.AllowedTags.Add("em");
            sanitizer.AllowedTags.Add("ul");
            sanitizer.AllowedTags.Add("ol");
            sanitizer.AllowedTags.Add("li");
            sanitizer.AllowedTags.Add("br");
            sanitizer.AllowedTags.Add("img");

            // Allow specific attributes
            sanitizer.AllowedAttributes.Add("href");
            sanitizer.AllowedAttributes.Add("title");
            sanitizer.AllowedAttributes.Add("alt");
            sanitizer.AllowedAttributes.Add("src");

            return sanitizer.Sanitize(htmlContent);
        }
    }
}
