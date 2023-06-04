namespace OnlineBankingSystem.Models
{
    public class SendMonyRequestDto
    {

        public string SendIBAN { get; set; }
        public double Balance { get; set; }
        public string RecipientIBAN { get; set; }
        public string RecipientFullName { get; set; }
    }
}
