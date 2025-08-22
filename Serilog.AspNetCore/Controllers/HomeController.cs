using Microsoft.AspNetCore.Mvc;

namespace Serilog.AspNetCore.Controllers;
public class HomeController : Controller
{
    readonly IDiagnosticContext _diagnosticContext;

    public HomeController(IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = diagnosticContext ??
            throw new ArgumentNullException(nameof(diagnosticContext));
    }

    public IActionResult Index()
    {
        // The request completion event will carry this property
        _diagnosticContext.Set("CatalogLoadTime", 1423);

        return View();
    }
}
