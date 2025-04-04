namespace DevBox.WkHtmlToPdf.Interfaces.Services;

public interface IViewRenderService
{
    /// <summary>
    /// Render view to string
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="model">The view model</param>
    /// <returns>The view rendered as string</returns>
    Task<string> RenderToStringAsync(string viewName, object model);
}
