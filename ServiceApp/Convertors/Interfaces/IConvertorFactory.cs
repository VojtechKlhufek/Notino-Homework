namespace ServiceApp.Convertors.Interfaces
{
    public interface IConvertorFactory
    {
        (string, IConvertor) GetConvertor(string type);
    }
}
