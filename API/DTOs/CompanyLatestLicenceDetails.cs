using Domain;

namespace API.DTOs
{
    public class CompanyLatestLicenceDetails
    {
        public Guid Id { get; set; }

        
        public string Code { get; set; } = default!;

        // أسم الشركة
        
        public string CompanyName { get; set; } = default!;

        // اسم الشركة التجاري القديم
        public string? OldComericalName { get; set; }

        // رأس المال
    
        public double CompanyCapital { get; set; } = 0;

        // حالة الرخصة 
        public string LicenceStatus { get; set; }
            

        public string? Info { get; set; }

        public string CompanyType { get; set; } = default!;

        
        // رقم الهاتف
        public string PhoneNumber { get; set; } = default!;

        public string? Address { get; set; }


        public string? TypeOfActivity { get; set; }

        public string CommercialRegistrationNo { get; set; } = default!;

        // المخالفات و العقوبات 
        public string? ViolationsAndPenalties { get; set; }

        
        // مسؤول الامتثال
        public string? ComplianceOfficer { get; set; }
        public string? ComplianceOfficerPhone { get; set; }
       
        public DateTime CreateDate { get; set; }

        public LicenceDetail LatestLicence { get; set; } = new();
    }
}
