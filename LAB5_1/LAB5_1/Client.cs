using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAB5_1
{
    public class Client
    {
        private string fullName;
        private string adress;


        public Client(string fullName, string adress)
        {
            this.fullName = fullName;
            this.adress = adress;
        }

        public string GetFullName()
        {
            return fullName;
        }

        public string GetAdress()
        {
            return adress;
        }

        public override string ToString()
        {
            return $"ФИО: {fullName}, адрес: {adress}";
        }
    }
}