// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MiniTwitApi.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using MiniTwitApi.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/_Imports.razor"
using MiniTwitApi.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/Pages/FetchData.razor"
using MiniTwitApi.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/fetchdata")]
    public partial class FetchData : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 38 "/home/vic/Documents/School/PythonKindergarten/MiniTwitApi/Client/Pages/FetchData.razor"
       
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
