public class DagcodeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string[] _whitelist = new[]
    {
        "/dagplanning/dagkeuze",
        "/dagplanning/checkdagcode",
        "/dagplanning/dagcodelogin",
        "/css/", "/js/", "/lib/"
    };

    public DagcodeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext ctx)
    {
        var path = ctx.Request.Path.Value!.ToLower();

        if (_whitelist.Any(w => path.StartsWith(w)))
        {
            await _next(ctx);
            return;
        }

        var dagcode = ctx.Session.GetString("Dagcode");
        if (!string.IsNullOrEmpty(dagcode))
        {
            ctx.Items["MagNietTerug"] = true;
        }

        await _next(ctx);
    }
}