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
        private int _postNr;
        private int _telefonNr;

        public Customer()
        {

        }

        public Customer(int customerNr, string name, string email, string addresse, int postNr, int telefonNr)
        {
            _customerNr = customerNr;
            _name = name;
            _email = email;
            _addresse = addresse;
            _postNr = postNr;
            _telefonNr = telefonNr;
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

        public override string ToString()
        {
            return $"{nameof(CustomerNr)}: {_customerNr}, {nameof(Name)}: {_name}, {nameof(Email)}: {_email} {nameof(Addresse)}: {_addresse}, {nameof(PostNr)}: {_postNr}, {nameof(TelefonNr)}: {_telefonNr}";
        }
    }
}
