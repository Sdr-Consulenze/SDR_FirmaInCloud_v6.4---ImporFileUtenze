using Sdr_FirmaInCloud.BL.FirmaInCloud.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud
{
    public class Body
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Residence Residence { get; set; }
        public BirthData BirthData { get; set; }
        public Nationality Nationality { get; set; }
        public IdentificationDocument IdentificationDocument { get;set; }
        public Authorization Authorization { get; set; }
    }
    public class Residence
    {
        public string Address { get; set; }
        public City City { get; set; }
        public Nation Nation { get; set; }
    }
    public class City 
    { 
        public string Name { get; set; }
        public string Zip { get; set; }
        public string District { get; set; }
    }
    public class Nation
    {
        public string IdentificationCode { get; set; }
    }
    public class BirthData
    { 
        public string Date { get; set; }
        public City City { get; set; }
        public Nation Nation { get; set; }
    }

    public class Nationality 
    { 
        public Nation Nation { get; set; }
    }

    public class IdentificationDocument 
    { 
        public Type Type { get; set; }
        public string Number { get; set; }
        public string IssueDate { get; set; }
        public City City { get; set; }
        public Nation Nation { get; set; }
    }

    [DefaultValue(None)]
    public enum Type 
    {
        None = 0,
        /// <remarks/>
        /// carta di identità
        IDENT = 1,
        /// <remarks/>
        /// patente di guida
        PATEN = 2,
        /// <remarks/>
        /// passaporto ordinario
        PASOR = 3,
        /// <remarks/>
        /// carta di identità elettronica
        IDELE = 4,
        /// <remarks/>
        /// tess. app.to. Ag.custodia
        CERID = 5,
        /// <remarks/>
        /// TEss. app.to.ag.custodia
        ACMIL = 6,
        /// <remarks/>
        /// tess. sott. li. ag. custodia
        ACSOT = 7,
        /// <remarks/>
        /// tess. uff. li. ag. custodia
        ACUFF = 8,
        /// <remarks/>
        /// tess. militare truppa
        AMMIL = 9,
        /// <remarks/>
        /// tess. sott. a.m
        AMSOT = 10,
        /// <remarks/>
        /// tess. ufficiali a.m.
        AMUFF = 11,
        /// <remarks/>
        /// tess. app.to carabinieri
        CCMIL = 11,
        /// <remarks/>
        /// tess. sottoufficiali cc
        CCSOT = 12,
        CFUFF = 13
        
    }
    public class Authorization 
    { 
        public string PersonalDataUse { get; set; }
    }
}
