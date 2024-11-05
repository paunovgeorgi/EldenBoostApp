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
    }
}
