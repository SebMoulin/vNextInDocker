using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;

namespace WebSite3.Utils
{
    public static class SelectListItemUtils
    {
        public static IEnumerable<SelectListItem> CreateList<T>(IEnumerable<T> itemSource,
            Func<T, string> text,
            Func<T, string> value,
            bool addSelectAsDefault)
        {
            var listItems = itemSource
                .Select(bs => new SelectListItem()
                {
                    Text = text(bs),
                    Value = value(bs),
                    Selected = false
                }).ToList();

            if (addSelectAsDefault)
            {
                listItems.Insert(0, new SelectListItem()
                {
                    Selected = true,
                    Text = "Please select...",
                    Value = string.Empty
                });
            }
            return listItems;
        }
    }
}