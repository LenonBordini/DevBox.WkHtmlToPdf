using DevBox.WkHtmlToPdf.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace DevBox.WkHtmlToPdf.Services;

internal class ViewRenderService : IViewRenderService
{
    private readonly IRazorViewEngine _razorViewEngine;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IWebHostEnvironment _env;
    private readonly HttpContext _httpContext;

    public ViewRenderService(
        IRazorViewEngine razorViewEngine,
        ITempDataProvider tempDataProvider,
        IWebHostEnvironment env,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _razorViewEngine = razorViewEngine;
        _tempDataProvider = tempDataProvider;
        _env = env;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<string> RenderToStringAsync(string viewName, object model)
    {
        if (!viewName.StartsWith("Views/", StringComparison.OrdinalIgnoreCase))
            viewName = $"Views/{viewName.TrimStart('/')}";
        if (!viewName.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase))
            viewName += ".cshtml";

        var routeData = _httpContext.GetRouteData();

        var actionContext = new ActionContext(_httpContext, routeData, new ActionDescriptor());

        var viewEngineResult = _razorViewEngine.GetView(_env.WebRootPath, viewName, isMainPage: false);
        if (!viewEngineResult.Success || viewEngineResult.View == null)
            viewEngineResult = _razorViewEngine.FindView(actionContext, viewName, isMainPage: false);

        if (!viewEngineResult.Success || viewEngineResult.View == null)
            throw new ArgumentNullException($"View {viewName} not found");

        var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        var tempData = new TempDataDictionary(_httpContext, _tempDataProvider);

        using var stringWriter = new StringWriter();

        var viewContext = new ViewContext(actionContext, viewEngineResult.View, viewDictionary, tempData, stringWriter, new HtmlHelperOptions())
        {
            RouteData = routeData
        };

        await viewEngineResult.View.RenderAsync(viewContext);

        return stringWriter.ToString();
    }
}
