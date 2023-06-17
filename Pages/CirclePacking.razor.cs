using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Blazor.Server.Extensions;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

namespace Blazor.Server.Pages
{
    [Route("/circle-packing")]
    public sealed partial class CirclePacking : ComponentBase, IAsyncDisposable
    {
        [Inject] IJSRuntime _JSRuntime { get; set; } = default!;
        private IJSObjectReference _Module = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    _Module = await _JSRuntime.ComponentModule<CirclePacking>();
                    await _Module.InvokeVoidAsync("init");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void onClickZoomIn(MouseEventArgs args) => _Module.InvokeVoidAsync("onclickZoomIn");
        private void onClickZoomOut(MouseEventArgs args) => _Module.InvokeVoidAsync("onclickZoomOut");

        private void onClickZoomRandom(MouseEventArgs args) => _Module.InvokeVoidAsync("onclickzoomRandom");

        private void onClickZoomReset(MouseEventArgs args) => _Module.InvokeVoidAsync("onclickzoomReset");
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_Module is not null)
            {
                await _Module.InvokeVoidAsync("dispose");
                await _Module.DisposeAsync();
            }
        }
    }
}
