using DevBox.WkHtmlToPdf.Interfaces.Services;
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
    private readonly HttpContext _httpContext;

    public ViewRenderService(IRazorViewEngine razorViewEngine, IHttpContextAccessor httpContextAccessor)
    {
        _razorViewEngine = razorViewEngine;
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

        var viewEngineResult = _razorViewEngine.GetView(null, viewName, isMainPage: true);
        if (!viewEngineResult.Success || viewEngineResult.View == null)
            viewEngineResult = _razorViewEngine.FindView(actionContext, viewName, isMainPage: true);

        if (!viewEngineResult.Success || viewEngineResult.View == null)
            throw new ArgumentNullException($"View \"{viewName}\" not found");

        var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        var tempData = new TempDataDictionary(_httpContext, new EmptyTempDataProvider());

        using var stringWriter = new StringWriter();

        var viewContext = new ViewContext(actionContext, viewEngineResult.View, viewDictionary, tempData, stringWriter, new HtmlHelperOptions())
        {
            RouteData = routeData
        };

        await viewEngineResult.View.RenderAsync(viewContext);

        return stringWriter.ToString();
    }

    internal class EmptyTempDataProvider : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
            throw new NotImplementedException();
        }
    }
}
