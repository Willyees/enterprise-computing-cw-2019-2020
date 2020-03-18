using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ShareTrader.Models
{
    public class BrokerModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string PhoneNumber { get; set; }

        [MaxLength(256)]
        public string Expertise { get; set; }

        public BrokerModel() : this(0, "","","","", "")
        {

        }

        public BrokerModel(int id, string firstName, string lastName, string email, string phoneNumber, string expertise)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Expertise = expertise;
        }
    }


    public class BrokerContext : DbContext
    {
        public DbSet<BrokerModel> Brokers { get; set; }

        public BrokerContext() : base("name=DefaultConnection")
        {
        }
    }
}