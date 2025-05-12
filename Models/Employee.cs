using System.ComponentModel.DataAnnotations;

/*
 * This is the Employee class. It is the data model
 * for an employee in the database.
 */

namespace prog7311.Models
{
    public class Employee
    {
        // Primary key for the employee
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        // Employee name
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        // Employee email address
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        // Employee password
        public string Password { get; set; }
    }
} 