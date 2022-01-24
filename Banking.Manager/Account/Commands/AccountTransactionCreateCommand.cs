using Banking.Infrastructure.Enums;
using MediatR;

namespace Banking.Manager.Account.Commands
{
    public class AccountTransactionCreateCommand : IRequest<string>
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
