using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TreeWebApp.Models
{
    /// <summary>
    /// Represents an employee in the application.
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [Required(ErrorMessage = "Middle Name is required.")]
        public string MiddleName { get; set; }

        [DisplayName("Date Hired")]
        [Required(ErrorMessage = "Date hired is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateHired { get; set; }

        /// <summary>
        /// Gets the formatted date hired (MM/dd/yyyy) for display purposes.
        /// </summary>
        [DisplayName("Formatted Date Hired")]
        public string FormattedDateHired => DateHired.ToString("MM/dd/yyyy");

        /// <summary>
        /// Property to store employee photo binary data.
        /// </summary>
        public byte[] PhotoData { get; set; }

        /// <summary>
        /// Property to store employee photo content type (e.g., image/jpeg).
        /// </summary>
        public string PhotoContentType { get; set; }

        /// <summary>
        /// Gets the full name of the employee in the format "Last Name, First Name Middle Name".
        /// </summary>
        [DisplayName("Full Name")]
        public string FullName => $"{LastName}, {FirstName} {MiddleName}";
    }
}
