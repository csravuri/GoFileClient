using GoFile.Client.Models;

namespace GoFile.Client.Services;

public interface IGoFileService
{
    string CreateAccount();
    AccountDetailsModel GetAccountDetails(string token);

    string GetServer();

    void CreateFolder(FolderModel folder);
}
