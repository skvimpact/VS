using System;
using System.Runtime.Serialization;

namespace Reconciliation.DAL
{
    [DataContract(Name = "RICaption", Namespace = "")]
    public class ReconciliationItemCaption
    {
        [DataMember(Name = "A")]
        public string D1 { get; set; }  = "Account";
        [DataMember (Name = "B")]
        public string D2 { get; set; }  = "Legal Entity Code";
        [DataMember (Name = "C")]
        public string D3 { get; set; }  = "Bank Account Code";
        [DataMember (Name = "D")]
        public string D4 { get; set; }  = "Book Code";
        [DataMember (Name = "E")]
        public string D5 { get; set; }  = "Cost Center Code";
        [DataMember (Name = "F")]
        public string D6 { get; set; }  = "Counterparty Code";
        [DataMember (Name = "G")]
        public string D7 { get; set; }  = "Real Counterparty Code";
        [DataMember (Name = "H")]
        public string D8 { get; set; }  = "Currency Code";
        [DataMember(Name = "I")]
        public string D9 { get; set; }  = "Deal ID";
        [DataMember(Name = "J")]
        public string D10 { get; set; } = "IC Code";
        [DataMember(Name = "K")]
        public string D11 { get; set; } = "FA Code";
        [DataMember(Name = "L")]
        public string D12 { get; set; } = "Commission Code";
        [DataMember(Name = "M")]
        public string D13 { get; set; } = "Operating Expense Code";
        [DataMember(Name = "N")]
        public string D14 { get; set; } = "Provision Code";
        [DataMember(Name = "O")]
        public string D15 { get; set; } = "Movement Code";
        [DataMember(Name = "P")]
        public string D16 { get; set; } = "Reconcilation Type Code";
        [DataMember(Name = "Q")]
        public string D17 { get; set; } = "Tax Jurisdiction Code";
        [DataMember(Name = "R")]
        public string D18 { get; set; } = "Tax Code";
        [DataMember(Name = "S")]
        public string D19 { get; set; } = "Income Tax Code";
        [DataMember(Name = "T")]
        public string D20 { get; set; } = "Principal Interest Code";
        [DataMember(Name = "U")]
        public string D21 { get; set; } = "Reval Type Code";
        [DataMember(Name = "V")]
        public string D22 { get; set; } = "Accrual Type Code";
        [DataMember(Name = "W")]
        public string D23 { get; set; } = "Project Code";
        [DataMember(Name = "X")]
        public string D24 { get; set; } = "GW Operation Type Code";
        [DataMember(Name = "Y")]
        public string D25 { get; set; } = "FA Operation Type Code";
        [DataMember(Name = "Z")]
        public string D26 { get; set; } = "Deffered Expense Code";
        [DataMember(Name = "a")]
        public string D27 { get; set; } = "Fund Status Code";
        [DataMember(Name = "b")]
        public string D28 { get; set; } = "FI Code";
        [DataMember(Name = "c")]
        public string D29 { get; set; } = "Capital Move Type Code";
        [DataMember(Name = "d")]
        public string D30 { get; set; } = DateTime.Now.ToString("HH:mm:ss tt");
        [DataMember(Name = "y1")]
        public string L1 { get; set; }  = "OCY Balance Calc";
        [DataMember(Name = "y2")]
        public string L2 { get; set; }  = "LCY Balance Calc";
        [DataMember(Name = "y3")]
        public string L3 { get; set; }  = "OCY Amount";
        [DataMember(Name = "y4")]
        public string L4 { get; set; }  = "LCY Amount";
        [DataMember(Name = "z1")]
        public string R1 { get; set; }  = "OCY Balance Calc";
        [DataMember(Name = "z2")]
        public string R2 { get; set; }  = "LCY Balance Calc";
        [DataMember(Name = "z3")]
        public string R3 { get; set; }  = "OCY Amount";
        [DataMember(Name = "z4")]
        public string R4 { get; set; }  = "LCY Amount";
    }
}
