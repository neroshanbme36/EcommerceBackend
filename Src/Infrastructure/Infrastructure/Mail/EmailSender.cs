using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.Infrastructure;
using Application.Dtos.Email;
using Application.Helpers;
using Application.Models;
using Application.Enums;
using Application.Models.Email;
using Microsoft.Extensions.Options;

namespace Infrastructure.Mail
{
  public class EmailSender : IEmailSender
  {
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
      _emailSettings = emailSettings.Value;
    }

    public async Task<Result> SendEmail(IReadOnlyList<EmailDto> emails)
    {
      string emailAccessToken = Encryption.Encrypt(_emailSettings.AccessTokenClearTxt, _emailSettings.AccessTokenEncryKey);

      List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>>();
      requestHeaders.Add(new KeyValuePair<string, string>("AccessToken", emailAccessToken));

      using (var restClient = new RestClient(_emailSettings.BaseAddress, requestHeaders))
      {
        RestClientResult rcResult = await restClient.Post("/api/v1/Emails/send-bulk-email", emails);
        return new Result(rcResult.ApiStatus == ApiStatus.Success, rcResult.Data);
      }
    }
  }
}
