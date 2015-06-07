using System.Web;
using TheKFactorUtils.Abstract;

namespace TheKFactorUtils.Io
{
    public class PathMapper : IPathMapper
    {
        public string MapPath(string relativePath)
        {
            return HttpContext.Current
                              .Server
                              .MapPath(relativePath);
        }
    }
}