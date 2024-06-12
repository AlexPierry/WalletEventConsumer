using System.Text.Json.Serialization;

namespace WalletEventConsumer.Model
{
    public class BalanceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Payload Payload { get; set; } = new();
        public DateTime Date { get; set; }
    }

    public class Payload
    {
        public int Id { get; set; }

        [JsonPropertyName("account_id_from")]
        public string AccountIdFrom { get; set; } = string.Empty;

        [JsonPropertyName("account_id_to")]
        public string AccountIdTo { get; set; } = string.Empty;

        [JsonPropertyName("balance_account_id_from")]
        public double BalanceAccountIdFrom { get; set; }

        [JsonPropertyName("balance_account_id_to")]
        public double BalanceAccountIdTo { get; set; }
    }
}