using System;

namespace RoadRunner.Shared.Classes
{
    public class GetCreditCardDetailsNewForPhoneResponseItem
    {
        public String Id { get; set; }
        public String CardHolderName { get; set; }
        public String CardNumber { get; set; }
        public String CardType { get; set; }
        public String ExpirationMonth { get; set; }
        public String ExpirationYear { get; set; }
        public String CVV { get; set; }
        public String ZIP { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Id = {1}{0}Card Holder Name = {2}{0}Card Number = {3}{0}Card Type = {4}{0}Expiration Month = {5}{0}Expiration Year = {6}{0}CVV = {7}{0}ZIP = {8}{0}", Environment.NewLine,
                Id, CardHolderName, CardNumber, CardType, ExpirationMonth, ExpirationYear, CVV, ZIP);
        }
    }
}
