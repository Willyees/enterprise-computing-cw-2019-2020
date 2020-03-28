using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareTrader.Models
{
    public class BrokerOutViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Expertise { get; set; }

        public double QualityGrade { get; set; }

        public BrokerOutViewModel(int id, string firstName, string lastName, string email, string phoneNumber, string expertise, double qualityGrade)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Expertise = expertise;
            QualityGrade = qualityGrade;
        }

        public BrokerOutViewModel(BrokerModel broker) : this(broker.Id, broker.FirstName, broker.LastName, broker.Email, broker.PhoneNumber, broker.Expertise, broker.QualityGrade)
        {

        }

        public BrokerOutViewModel()
        {

        }
    }

    public class BrokerQueryModel
    {


        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Email { get; set; }
        public string Expertise { get; set; }


        public BrokerQueryModel(string firstName, string lastName, string email, string expertise)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Expertise = expertise;
        }


    }
}