using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCustomer.Model
{
    public class Customer
    {
        private int _customerNr;
        private string _name;
        private string _email;
        private string _addresse;
        private string _townCity;
        private string _country;
        private int _postNr;
        private int _telefonNr;
        private string _currency;

        public Customer()
        {

        }

        public Customer(int customerNr, string name, string email, string addresse, string townCity, string country, int postNr, int telefonNr, string currency)
        {
            _customerNr = customerNr;
            _name = name;
            _email = email;
            _addresse = addresse;
            _townCity = townCity;
            _country = country;
            _postNr = postNr;
            _telefonNr = telefonNr;
            _currency = currency;
        }

        public int CustomerNr
        {
            get => _customerNr;
            set
            {
                _customerNr = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
            }
        }

        public string Addresse
        {
            get => _addresse;
            set
            {
                _addresse = value;
            }
        }

        public string TownCity
        {
            get => _townCity;
            set
            {
                _townCity = value;
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
            }
        }

        public int PostNr
        {
            get => _postNr;
            set
            {
                _postNr = value;
            }
        }

        public int TelefonNr
        {
            get => _telefonNr;
            set
            {
                _telefonNr = value;
            }
        }

        public string Currency
        {
            get => _currency;
            set
            {
                _currency = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(CustomerNr)}: {_customerNr}, {nameof(Name)}: {_name}, " +
                   $"{nameof(Email)}: {_email} {nameof(Addresse)}: {_addresse}, {nameof(TownCity)}: {_townCity}, " +
                   $"{nameof(Country)}: {_country}, {nameof(PostNr)}: {_postNr}, {nameof(TelefonNr)}: {_telefonNr}, " +
                   $"{nameof(Currency)}: {_currency}";
        }
    }
}
