using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCustomer.Model
{
    public class Access
    {
        private int _accessId;
        private int _customerNr;
        private int _invoiceNr;
        private int _invoiceLine;
        private string _agreementGrantToken;

        public Access()
        {

        }

        public Access(int accessId, int customerNr, int invoiceNr, int invoiceLine, string agreementGrantToken)
        {
            _accessId = accessId;
            _customerNr = customerNr;
            _invoiceNr = invoiceNr;
            _invoiceLine = invoiceLine;
            _agreementGrantToken = agreementGrantToken;
        }

        public int AccessId
        {
            get => _accessId;
            set
            {
                _accessId = value;
            }
        }

        public int CustomerNr
        {
            get => _customerNr;
            set
            {
                _customerNr = value;
            }
        }

        public int InvoiceNr
        {
            get => _invoiceNr;
            set
            {
                _invoiceNr = value;
            }
        }

        public int InvoiceLine
        {
            get => _invoiceLine;
            set
            {
                _invoiceLine = value;
            }
        }

        public string AgreementGrantToken
        {
            get => _agreementGrantToken;
            set
            {
                _agreementGrantToken = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(AccessId)}: {_accessId}, {nameof(CustomerNr)}: {_customerNr}, {nameof(InvoiceNr)}: {_invoiceNr}, " +
                   $"{nameof(InvoiceLine)}: {_invoiceLine} {nameof(AgreementGrantToken)}: {_agreementGrantToken}";
        }
    }
}
