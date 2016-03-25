
namespace Framework.Library.Mvc
{
    public interface ISerializer
    {
        string SerializeJson(object value);
        string SerializeXml(object value);
    }
}
