using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeWebApp.Models
{
    /// <summary>
    /// Represents a branch in the application.
    /// </summary>
    public class Branch
    {
        public int Id { get; set; }

        [DisplayName("Branch Code")]
        [Required(ErrorMessage = "Branch code is required.")]
        public string Code { get; set; }

        [DisplayName("Branch Name")]
        [Required(ErrorMessage = "Branch name is required.")]
        public string Name { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [DisplayName("Barangay")]
        [Required(ErrorMessage = "Barangay is required.")]
        public string Barangay { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [DisplayName("Permit No.")]
        [Required(ErrorMessage = "Permit number is required.")]
        public string PermitNo { get; set; }

        [ForeignKey("Manager")]
        [Required(ErrorMessage = "Branch manager is required.")]
        public int ManagerId { get; set; }

        [DisplayName("Branch Manager")]
        public virtual Employee Manager { get; set; }

        [DisplayName("Date Opened")]
        [Required(ErrorMessage = "Date opened is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOpened { get; set; }

        /// <summary>
        /// Gets the formatted date opened (MM/dd/yyyy) for display purposes.
        /// </summary>
        [DisplayName("Formatted Date Opened")]
        public string FormattedDateOpened => DateOpened.ToString("MM/dd/yyyy");

        [DisplayName("Is Active")]
        public bool IsActive { get; set; } = false;

        public Branch()
        {

        }
    }
}
