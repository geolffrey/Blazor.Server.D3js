using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Server.Extensions
{
    public static class JSRuntimeExtension
    {
        public static async Task<IJSObjectReference> ComponentModule<T>(this IJSRuntime jsRuntime)
        {
            var typeOf = typeof(T);
            var stringBuilder = new StringBuilder("./");
            stringBuilder.Append(typeOf.FullName?.Remove(0, typeOf.Assembly.GetName().Name.Length + 1).Replace(".", "/"));
            stringBuilder.Append(".razor.js");

            var result = await jsRuntime.InvokeAsync<IJSObjectReference>("import", stringBuilder.ToString());

            return result;
        }
    }
}
