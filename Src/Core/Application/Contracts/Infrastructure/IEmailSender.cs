using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Email;
using Application.Models;

namespace Application.Contracts.Infrastructure
{
  public interface IEmailSender
  {
    Task<Result> SendEmail(IReadOnlyList<EmailDto> emails);
  }
}