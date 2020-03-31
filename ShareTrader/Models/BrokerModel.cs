using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ShareTrader.Models
{
    //add trading record, quality grade
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


        public double QualityGrade { get; set; }

        public BrokerModel() : this(0, "","","","", 0.0)
        {

        }

        public BrokerModel(int id, string firstName, string lastName, string email, string phoneNumber, double qualityGrade)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            QualityGrade = qualityGrade;
        }
    }

    public class ExpertiseBrokers
    {
        //shoudl remove this and use pk brokerid && expertise together
        public int Id { get; set; }
        public int BrokerId { get; set; }
        public string Expertise { get; set; }
    }

    public class BrokerScoreOutModel
    {
        public int Id { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }


        public string PhoneNumber { get; set; }


        public double QualityGrade { get; set; }
        public double Score { get; set; }

        public BrokerScoreOutModel()
        {

        }

        public BrokerScoreOutModel(BrokerModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            QualityGrade = model.QualityGrade;
        }
    }

    public class BrokerContext : DbContext
    {
        public DbSet<BrokerModel> Brokers { get; set; }
        public DbSet<ExpertiseBrokers> Expertises { get; set; }

        public BrokerContext() : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<InterestedContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}