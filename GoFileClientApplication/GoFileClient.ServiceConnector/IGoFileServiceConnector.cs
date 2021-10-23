namespace GoFileClient.ServiceConnector
{
    public interface IGoFileServiceConnector
    {
        bool UploadFileAsync(string fileFullPath);
    }
}