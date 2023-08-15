using GoFile.Client.Models;

namespace GoFile.Client.Services;
public class GoFileService : IGoFileService
{
    string IGoFileService.CreateAccount()
    {
        //req: https://api.gofile.io/createAccount
        //res: {"status":"ok","data":{"token":"RJQPeZWdbl6TgbKSymPXLa3l202atjvE"}}
        throw new NotImplementedException();
    }

    AccountDetailsModel IGoFileService.GetAccountDetails(string token)
    {
        //req: https://api.gofile.io/getAccountDetails?token=RJQPeZWdbl6TgbKSymPXLa3l202atjvE
        //res: {
        //    "status": "ok",
        //"data": {
        //        "id": "0970e836-44d0-4065-8ac5-7ac603624d19",
        //    "email": "guest7738750260@gofile.io",
        //    "tier": "guest",
        //    "token": "RJQPeZWdbl6TgbKSymPXLa3l202atjvE",
        //    "rootFolder": "ba776775-19a7-47a5-95bb-177b5bc90304",
        //    "filesCount": 0,
        //    "totalSize": 0,
        //    "total30DDLTraffic": 0,
        //    "credit": 0,
        //    "currency": "USD",
        //    "currencySign": "$"
        //}
        //}
        throw new NotImplementedException();
    }
}
