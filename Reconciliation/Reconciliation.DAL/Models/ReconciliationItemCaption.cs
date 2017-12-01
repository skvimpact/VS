using System;
using System.Runtime.Serialization;

namespace Reconciliation.DAL
{
    [DataContract(Name = "RICaption", Namespace = "")]
    public class ReconciliationItemCaption
    {
        [DataMember(Name = "A")]
        public string D1 { get; set; }
        [DataMember (Name = "B")]
        public string D2 { get; set; }
        [DataMember (Name = "C")]
        public string D3 { get; set; }
        [DataMember (Name = "D")]
        public string D4 { get; set; }
        [DataMember (Name = "E")]
        public string D5 { get; set; }
        [DataMember (Name = "F")]
        public string D6 { get; set; }
        [DataMember (Name = "G")]
        public string D7 { get; set; }
        [DataMember (Name = "H")]
        public string D8 { get; set; }
        [DataMember(Name = "I")]
        public string D9 { get; set; }
        [DataMember(Name = "J")]
        public string D10 { get; set; }
        [DataMember(Name = "K")]
        public string D11 { get; set; }
        [DataMember(Name = "L")]
        public string D12 { get; set; }
        [DataMember(Name = "M")]
        public string D13 { get; set; }
        [DataMember(Name = "N")]
        public string D14 { get; set; }
        [DataMember(Name = "O")]
        public string D15 { get; set; }
        [DataMember(Name = "P")]
        public string D16 { get; set; }
        [DataMember(Name = "Q")]
        public string D17 { get; set; }
        [DataMember(Name = "R")]
        public string D18 { get; set; }
        [DataMember(Name = "S")]
        public string D19 { get; set; }
        [DataMember(Name = "T")]
        public string D20 { get; set; }
        [DataMember(Name = "U")]
        public string D21 { get; set; }
        [DataMember(Name = "V")]
        public string D22 { get; set; }
        [DataMember(Name = "W")]
        public string D23 { get; set; }
        [DataMember(Name = "X")]
        public string D24 { get; set; }
        [DataMember(Name = "Y")]
        public string D25 { get; set; }
        [DataMember(Name = "Z")]
        public string D26 { get; set; }
        [DataMember(Name = "a")]
        public string D27 { get; set; }
        [DataMember(Name = "b")]
        public string D28 { get; set; }
        [DataMember(Name = "c")]
        public string D29 { get; set; }
        [DataMember(Name = "d")]
        public string D30 { get; set; } 
        [DataMember(Name = "y1")]
        public string L1 { get; set; }
        [DataMember(Name = "y2")]
        public string L2 { get; set; }
        [DataMember(Name = "y3")]
        public string L3 { get; set; }
        [DataMember(Name = "y4")]
        public string L4 { get; set; }
        [DataMember(Name = "z1")]
        public string R1 { get; set; }
        [DataMember(Name = "z2")]
        public string R2 { get; set; }
        [DataMember(Name = "z3")]
        public string R3 { get; set; }
        [DataMember(Name = "z4")]
        public string R4 { get; set; }

        private int i;
        public ReconciliationItemCaption() {
            this.D1 = "Account";
            this.D2 = "Legal Entity Code";
            this.D3 = "Bank Account Code";
            this.D4 = "Book Code";
            this.D5 = "Cost Center Code";
            this.D6 = "Counterparty Code";
            this.D7 = "Real Counterparty Code";
            this.D8 = "Currency Code";
            this.D9 = "Deal ID";
            this.D10 = "IC Code";
            this.D11 = "FA Code";
            this.D12 = "Commission Code";
            this.D13 = "Operating Expense Code";
            this.D14 = "Provision Code";
            this.D15 = "Movement Code";
            this.D16 = "Reconcilation Type Code";
            this.D17 = "Tax Jurisdiction Code";
            this.D18 = "Tax Code";
            this.D19 = "Income Tax Code";
            this.D20 = "Principal Interest Code";
            this.D21 = "Reval Type Code";
            this.D22 = "Accrual Type Code";
            this.D23 = "Project Code";
            this.D24 = "GW Operation Type Code";
            this.D25 = "FA Operation Type Code";
            this.D26 = "Deffered Expense Code";
            this.D27 = "Fund Status Code";
            this.D28 = "FI Code";
            this.D29 = "Capital Move Type Code";
            this.D30 = DateTime.Now.ToString("HH:mm:ss tt");

            this.L1 = "OCY Balance Calc";
            this.L2 = "LCY Balance Calc";
            this.L3 = "OCY Amount";
            this.L4 = "LCY Amount";
            this.R1 = "OCY Balance Calc";
            this.R2 = "LCY Balance Calc";
            this.R3 = "OCY Amount";
            this.R4 = "LCY Amount";            
        }
    }
}
