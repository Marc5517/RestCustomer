using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCustomer.Model
{
    public class Product
    {
        private int _productId;
        private int _productNr;
        private int _customerNr;
        private int _invoiceNr;
        private string _serialNr;

        public Product()
        {

        }

        public Product(int productId, int productNr, int customerNr, int invoiceNr, string serialNr)
        {
            _productId = productId;
            _productNr = productNr;
            _customerNr = customerNr;
            _invoiceNr = invoiceNr;
            _serialNr = serialNr;
        }

        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
            }
        }

        public int ProductNr
        {
            get => _productNr;
            set
            {
                _productNr = value;
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

        public string SerialNr
        {
            get => _serialNr;
            set
            {
                _serialNr = value;
            }
        }

        /// <summary>
        /// Viser indholdet af dataen fra varen/varer, og vises på denne måde når vi køre restapi.
        /// </summary>
        /// <returns>Indholdet af varen/varer</returns>
        public override string ToString()
        {
            return $"{nameof(ProductId)}: {_productId}, {nameof(ProductNr)}: {_productNr}, {nameof(CustomerNr)}: {_customerNr}, " +
                   $"{nameof(InvoiceNr)}: {_invoiceNr} {nameof(SerialNr)}: {_serialNr}";
        }
    }
}
